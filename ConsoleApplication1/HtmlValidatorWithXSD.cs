using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace ConsoleApplication1
{
    class HtmlValidatorWithXSD
    {
        //http://www.w3.org/2002/08/xhtml/xhtml1-transitional.xsd
        //http://www.w3.org/2002/08/xhtml/xhtml1-strict.xsd‎
        //http://www.w3.org/TR/xhtml1-schema/
        //http://zhidao.baidu.com/link?url=fCAfqUuVMeuiOcVEDpLY5Z-qQ2fVwlsnhcwZvpsIq2b_V6L21eKFKuK5hMi-mpnwFB98vTvLNiGDne1jZwB4OK
        //http://support.microsoft.com/kb/318504

        /*
XHTML是一种相对于HTML来说更为严格的标准，它分为三种类型：
一、Transitional 
这是一种过渡类型，它包含了HTML4.01版本的全部标记，方便网页开发者顺利地从HTML的使用过渡到XHTML；
二、strict
严格类型，它将文档结构与表现形式实现了更高的分离，所以，页面的外观要用CSS来控制
三、frameset
框架类型，使用<frameset>以框架的形式将网页分为多个文档
W3C是推荐使用XHTML的，而且使用XHTML的话可以更顺利地通过W3C对网页页面的验证，但是个人还是比较喜欢HTML，因为用的习惯了，现在自己写页面主要还是用HTML＋CSS的方法！
         */

        private static string xsdFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xhtml1-transitional.xsd");

        private static XmlSchemaSet xmlSchemaSet;
        private static XmlSchemaSet XmlReaderSettings
        {
            get
            {
                if (xmlSchemaSet == null)
                {
                    xmlSchemaSet = new XmlSchemaSet();
                    xmlSchemaSet.Add(null, xsdFile);
                }
                xmlSchemaSet.Compile();
                return xmlSchemaSet;
            }
        }

        public void Validate(string htmlFragment)
        {
            if (string.IsNullOrEmpty(htmlFragment)) return;
            htmlFragment = "<root>" + htmlFragment + "</root>";

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationEventHandler += new ValidationEventHandler(settings_ValidationEventHandler);
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = XmlReaderSettings;
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.DtdProcessing = DtdProcessing.Ignore;
            settings.IgnoreComments = true;
            //settings.ValidationFlags=XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.Reset();
            using (var xmlRead = XmlReader.Create(new StringReader(htmlFragment), settings))
            {
                try
                {
                    while (xmlRead.Read())
                    {
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void settings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.Severity);
        }
    }



    class HtmlValidatorWithXSDTest
    {
        private static List<string> TestData = new List<string>()
        {
            //correct
            "Feel like a big leaguer with this Derek Jeter replica jersey from Adidas. Made of 100-percent polyester Air Mesh,the jersey will have you looking and feeling just like a real ballplayer,with a button front and a screen printed team name on the chest. Team logo is on the sleeve. Adidas 3 stripe logo on the rear collar. <ul><strong>Features</strong><li>100% polyester air mesh</li><li>Screen printed MLB team color baseball jersey</li><li>Official MLB product</li><li>Turn garment inside out,Machine wash cold separately,Gentel Cycle,Only Non-Cholorine bleach when needed,Tumble dry low,Remove promptly,Do not iron,Do not dry clean</li><li>China</li></ul><br /><br />",
            //missing start tag
            "<strong>Features</strong><li>100% polyester air mesh</li>Screen printed MLB team color baseball jersey</li></ul>",
            //missing end tag
            "<ul><strong>Features</strong><li>100% polyester air mesh<li>Screen printed MLB team color baseball jersey</li>",
            //redundant space
            "<ul>< strong>Features</strong><li>100% polyester air mesh</li><li>Screen printed MLB team color baseball jersey</li>",
            //mix
            "<p><strong>Fisher-Price Kid-Tough Apptivity Case: Pink</strong>< /p><p>The Learn & Create <br/>Apptivity Case<br> for the <br />iPhone and iPod Touch <img src=\"xxx.jpg\">comes in an assortment <img src=\"yyy.jpg\"/>of four in boy and <img src=\"zzz.jpg\" />girl themes. <p/> <p /><input type=\"hidden\" name=\"tt\" value=\"a\"/>This protective case includes a stylus. When combined with a free downloadable app specifically designed for this case,it unlocks a variety of learning and creative activities. The play is focused around the use of the stylus for writing and art activities. The app contains three main modes (Explore,Learn and Create). These modes include a variety of interactive activities,writing activities and creative activities.</p><ul><li>Product Measures: 9.5\" x 6.5\" x 2\"</li></ul><ul><li>Recommended Ages: 3-5 years</li></ul>",
            "<p><strong>Pampers Preemie Swaddlers Diapers 20ct.</strong> zxczxzczxc <div class=\"xxx\"> adfasdfasdf </div></p> </div>",
            //wrong nested
            "<table><thead><td>a</td></thead><tbody><tr><div style=\"width:100px;\">2135</div><td>dsfiel</td></tr></tbody></table><ul><strong>Features</strong><li><div>100% polyester air mesh</div></li><div></div><li>Screen printed MLB team color baseball jersey</li></ul><H1><div>abc</div></h1>",
            //img input br hr without "/"
            "<img src=\"yyy.jpg\"/><input type=\"hidden\" name=\"tt\" value=\"a\"/><br><br/><hr><hr/>"
        };

        public static void RunTest(string html)
        {
            HtmlValidatorWithXSD validator = new HtmlValidatorWithXSD();
            while (true)
            {
                Console.Write("input a number to run times: ");
                string v = Console.ReadLine().Trim();
                if (v.Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                int times = 1;
                //if (int.TryParse(v, out times) == false) continue;
                DateTime dt1 = DateTime.Now;
                string msg = string.Empty;
                for (int i = 0; i < times; i++)
                {
                    if (!string.IsNullOrEmpty(v))
                    {
                        validator.Validate(v);
                        //msg = HtmlTagValidator.checkHtmlTags(html);
                        Console.WriteLine("result:{0}", msg);
                    }
                    else
                    {
                        foreach (string tag in TestData)
                        {
                            validator.Validate(tag);
                            Console.WriteLine("result:{0}", msg);
                        }
                    }
                }
                DateTime dt2 = DateTime.Now;
                Console.WriteLine("run times:{0},millionsecond:{1}ms", times, (dt2 - dt1).TotalMilliseconds);
                Console.WriteLine();
            }
        }
    }
}
