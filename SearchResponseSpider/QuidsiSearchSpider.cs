using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SearchResponseSpider
{
    public class QuidsiSearchSpider
    {
        private const int Timeout = 30000;
        private const string DebugTaskString = "Task String = ";
        private static readonly Regex SearchInterFaceReg = new Regex(@"http://[\w-:./&?#%]+/SearchInterface", RegexOptions.IgnoreCase);
        private static readonly Regex HtmlIdReg = new Regex(@"<html[^>]*id=[""']?(\w+)[""']?[^>]+>", RegexOptions.IgnoreCase);
        private static readonly Regex AspxEventIdReg = new Regex(@"<input[^>]+value=[""']([^>]+)[""'][^>]+/>", RegexOptions.IgnoreCase);
        private static readonly Dictionary<string, string> PyramidSite = new Dictionary<string, string>(10, StringComparer.OrdinalIgnoreCase)
	       	{
	       		{"Diapers","aspx"}, {"Soap","aspx"}, {"BeautyBar","aspx"}, 
	       		{"Wag","qs"}, {"Yoyo","qs"}, {"Casa","qs"},
	       		{"Vine","qs"}, {"AfterSchool","qs"}, {"BookWorm","qs"},
	       		{"Look","qs"}
	       	};


        private string AddDebugQuery(string originalUrl)
        {
            return UrlUtility.AddQuery(originalUrl, "debug", "Y");
        }

        private List<string> GetTaskStringFromPageSource(string pageSource)
        {
            List<string> taskStrings = new List<string>();
            string result = pageSource;
            if (string.IsNullOrEmpty(result)) return null;
            Regex reg = new Regex(DebugTaskString);
            Match mat = reg.Match(result);
            while (mat.Success)
            {
                int startIndex = mat.Index;
                startIndex = startIndex + DebugTaskString.Length;
                int length = result.Length;
                StringBuilder taskString = new StringBuilder(768);
                for (int i = startIndex; i < length; i++)
                {
                    if (result[i] == '\r' || result[i] == '\n')
                    {
                        break;
                    }
                    taskString.Append(result[i]);
                }
                taskStrings.Add(taskString.ToString());
                mat = reg.Match(result, mat.Index + mat.Length);
            }
            return taskStrings;
        }

        private string GetHtmlIdFromPageSource(string pageSource)
        {
            if (string.IsNullOrEmpty(pageSource)) return string.Empty;

            Match match = HtmlIdReg.Match(pageSource);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        private string GetEventIdFromPageSource(string pageSource)
        {
            if (string.IsNullOrEmpty(pageSource)) return string.Empty;
            Match match = AspxEventIdReg.Match(pageSource);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        private void GetPyramid(string htmlId, out string pyramid, out string webType)
        {
            pyramid = webType = string.Empty;
            if (string.IsNullOrEmpty(htmlId)) return;

            foreach (string s in PyramidSite.Keys)
            {
                if (htmlId.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    pyramid = s;
                    webType = PyramidSite[s];
                }
            }
        }

        private string GetMercadoTestUrl(string url, out string pyramid, out List<string> taskStrings, out string webType)
        {
            taskStrings = new List<string>();
            pyramid = string.Empty;
            webType = string.Empty;
            if (string.IsNullOrEmpty(url)) return string.Empty;
            url = AddDebugQuery(url);

            string pageSource = NetRequest.GetRequest(url);

            string htmlId = GetHtmlIdFromPageSource(pageSource);
            taskStrings = GetTaskStringFromPageSource(pageSource);

            if (taskStrings == null || taskStrings.Count == 0)
            {
                return string.Empty;
            }

            GetPyramid(htmlId, out pyramid, out webType);
            string mercadoTestUrl = string.Format("{0}/mercadotest.{1}", AppConfig.Instance.GetMercadoDomain(pyramid), webType);
            return mercadoTestUrl;
        }

        private string BuildAspxPostData(string eventId, string taskString)
        {
            //return string.Format("__EVENTVALIDATION={0}&__VIEWSTATE=&taskTextBox={1}&testButton=Test"
            //                     ,eventId,taskString);

            return string.Format("__VIEWSTATE=&__EVENTVALIDATION={0}&taskTextBox={1}&testButton=Test"
                                 , System.Web.HttpUtility.UrlEncode(eventId), System.Web.HttpUtility.UrlEncode(taskString));

            //return "__VIEWSTATE=&__EVENTVALIDATION=%2FwEdAAOvVXD1oYELeveMr0vHCmYPvI670HDHqp41aLfHIIWHPCIUWMq6FVtEClB6zqOx5npsd9AUEG%2FpPGMDv28jBfwl4effoSocI0u6CFDxKQhyQQ%3D%3D&taskTextBox=TASK+search+%7B+cfg+answers_per_page+%3D+%2223%22%3B+cfg+refine_option_show_all_values+%3D+%27Brand%27%3B+user_attribute_value+Segment_NV3N+%3D+%22Segment%3DNV3N%22+%22Segment%22+%22NV3N%22%3B+cfg+segment_modifier+%3D+%221.2%22%3B+attribute_value_nested+%22category_root%22+%3D+%22category%3DDiapering%22+%22category_root%22+%223%22%3B+attribute_value_nested+%22cat_3%22+%3D+%22category%3D+Wipes%22+%22cat_3%22+%22144%22%3B+cfg+answer_sort+%3D+%22Relevance%22%3B+page_section_hide%3D%28%22new_products%22%29%3B+mode+page+%3D+%22SEARCH%2BNAV%22%3B+attribute_value_string+%22Geo%22+%3D+%28%22Geo%3DAll%22%7C%22Geo%3DEast%22%29+%22Geo%22+%28%22All%22%7C%22East%22%29%3B+%7D+&testButton=Test";

        }

        private string BuildMVCPostData(string taskString)
        {
            return "taskString=" + HttpUtility.UrlEncode(taskString);
        }

        private string AddMercadoResultMethod(string mercadoTestUrl)
        {
            if (mercadoTestUrl.LastIndexOf("qs", StringComparison.OrdinalIgnoreCase) < 0) return mercadoTestUrl;
            return UrlUtility.AddQuery(mercadoTestUrl, "Method", "MercadoResult");
        }

        private string GetMercadoXmlFromPageSource(string pageSource)
        {
            if (string.IsNullOrEmpty(pageSource)) return string.Empty;

            string startTag = "<?xml version";
            string endTag = "</SOAP-ENV:Envelope>";

            int startIndex = pageSource.IndexOf(startTag, StringComparison.OrdinalIgnoreCase);
            int endIndex = pageSource.IndexOf(endTag, StringComparison.OrdinalIgnoreCase);

            if (startIndex < 0 || endIndex < 0 || endIndex < startIndex) return string.Empty;

            return pageSource.Substring(startIndex, (endIndex - startIndex) + endTag.Length);
        }

        public void GetMercadoResponseWithMercadoTestPage(SpiderTask spiderTask)
        {
            List<string> taskStrings = new List<string>();
            string pyramid, webType;
            string mercadoTestUrl = GetMercadoTestUrl(spiderTask.Url, out pyramid, out taskStrings, out webType);

            string postResponse = string.Empty;
            if (webType == "qs")
            {
                foreach (string taskString in taskStrings)
                {
                    string postData = BuildMVCPostData(taskString);
                    mercadoTestUrl = AddMercadoResultMethod(mercadoTestUrl);
                    postResponse = NetRequest.PostRequest(mercadoTestUrl, postData);
                    string mercadoXml = GetMercadoXmlFromPageSource(postResponse);
                    SpiderTaskItem spiderTaskItem = new SpiderTaskItem();
                    spiderTaskItem.TaskString = taskString;
                    spiderTaskItem.ResponseXml = mercadoXml;
                    spiderTask.SpiderTaskItems.Add(spiderTaskItem);
                }
            }
            else
            {
                foreach (string taskString in taskStrings)
                {
                    string testPageSource = NetRequest.GetRequest(mercadoTestUrl);
                    string eventId = GetEventIdFromPageSource(testPageSource);
                    string postData = BuildAspxPostData(eventId, taskString);
                    postResponse = NetRequest.PostRequest(mercadoTestUrl, postData);
                    string mercadoXml = GetMercadoXmlFromPageSource(postResponse);
                    SpiderTaskItem spiderTaskItem = new SpiderTaskItem();
                    spiderTaskItem.TaskString = taskString;
                    spiderTaskItem.ResponseXml = mercadoXml;
                    spiderTask.SpiderTaskItems.Add(spiderTaskItem);
                }
            }
        }
    }
}
