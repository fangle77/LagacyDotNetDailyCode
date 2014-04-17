using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class HtmlTagValidator
    {
        public static bool ValidateHtmlTag1(string html, out string message)
        {
            message = string.Empty;
            if (string.IsNullOrEmpty(html))
                return true;
            if (string.IsNullOrEmpty(html.Trim()))
                return true;
            html = ReplaceHtmlAutoTag(new string[] { "br", "hr" }, html);
            Regex myReg = new Regex(@"(<[/]?[hH]{1}[1-6]{1})|(<[/]?[a-zA-Z]+)", RegexOptions.IgnoreCase);
            MatchCollection matchs = myReg.Matches(html);

            string tagName = "";

            string strFullTag = "";
            List<string> needTags = new List<string>();
            System.Collections.Hashtable htTag = new System.Collections.Hashtable();
            string lastTagName = "";

            //End Tag
            for (int i = 0; i < matchs.Count; i++)
            {
                Match match = matchs[i];
                if (match.Success == false || string.IsNullOrEmpty(match.Value) == true)
                    continue;
                tagName = match.Value.Replace("<", "").Replace("/", "").Trim().ToLower();


                if (match.Value.StartsWith("<") == true &&
                    string.IsNullOrEmpty(tagName) == false)
                {
                    string strPartHtml = "";
                    if (i < matchs.Count - 1)
                    {
                        strPartHtml = html.Substring(match.Index + match.Length, (matchs[i + 1].Index - match.Index - match.Length));
                    }
                    else
                    {
                        strPartHtml = html.Substring(match.Index + match.Length, (html.Length - match.Index - match.Length));
                    }
                    if (strPartHtml.IndexOf("/>") >= 0)
                    {
                        //not need end tag.
                        continue;
                    }
                    else if (strPartHtml.IndexOf(">") >= 0)
                    {
                        //need end Tag.
                        if (match.Value.StartsWith("</"))
                        {
                            if (strFullTag.EndsWith(string.Format("<{0}>", tagName)))
                            {
                                strFullTag = strFullTag.Substring(0, strFullTag.Length - (string.Format("<{0}>", tagName).Length));
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(strFullTag))
                                {
                                    message = string.Format("Element '{0}' is missing its closing tag.", lastTagName);
                                    return false;
                                }
                                else
                                {
                                    message = string.Format("Element '{0}' has no matching start tag.", tagName);
                                    return false;
                                }
                            }
                        }
                        else if (match.Value.StartsWith("<"))
                        {
                            strFullTag = strFullTag + match.Value.Trim().ToLower() + ">";
                        }

                        lastTagName = tagName;
                        //Store Tag Name
                        if (needTags.IndexOf(tagName) < 0)
                            needTags.Add(tagName);
                        //Record Tag Count
                        int tagCount = 0;
                        string key = match.Value.Trim().ToLower() + ">";
                        if (htTag.ContainsKey(key))
                            tagCount = (int)(htTag[key]);
                        htTag[key] = tagCount + 1;
                        continue;
                    }
                    else
                    {
                        message = string.Format("Element '{0}' is missing the '>' character.", tagName);
                        return false;
                    }
                }
            }

            for (int i = 0; i < needTags.Count; i++)
            {
                tagName = needTags[i].Trim().ToLower();
                int startTag = 0;
                int endTag = 0;
                if (htTag.ContainsKey(string.Format("<{0}>", tagName)))
                {
                    startTag = (int)(htTag[string.Format("<{0}>", tagName)]);
                }
                if (htTag.ContainsKey(string.Format("</{0}>", tagName)))
                {
                    endTag = (int)(htTag[string.Format("</{0}>", tagName)]);
                }
                if (startTag > endTag)
                {
                    message = string.Format("Element '{0}' is missing its closing tag.", tagName);
                    return false;
                }
                else if (startTag < endTag)
                {
                    message = string.Format("Element '{0}' has no matching start tag.", tagName);
                    return false;
                }
            }
            return true;
        }
        protected static string ReplaceHtmlAutoTag(string[] tags, string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;
            Regex myReg;
            MatchCollection matchs;
            foreach (string tag in tags)
            {
                if (string.IsNullOrEmpty(tag))
                    continue;
                string tagName = tag.Trim().ToLower();
                if (string.IsNullOrEmpty(tagName))
                    continue;
                myReg = new Regex(@"<[\s]*" + tagName + @"[\s]*/>", RegexOptions.IgnoreCase);
                matchs = myReg.Matches(html);
                for (int i = 0; i < matchs.Count; i++)
                {
                    if (matchs[i].Success && !string.IsNullOrEmpty(matchs[i].Value))
                    {
                        html = html.Replace(matchs[i].Value, "<" + tagName + "/>");
                    }
                }
                myReg = new Regex(@"<[\s]*" + tagName + @"[\s]*>", RegexOptions.IgnoreCase);
                matchs = myReg.Matches(html);
                for (int i = 0; i < matchs.Count; i++)
                {
                    if (matchs[i].Success && !string.IsNullOrEmpty(matchs[i].Value))
                    {
                        html = html.Replace(matchs[i].Value, "<" + tagName + "/>");
                    }
                }
            }
            return html;
        }

        private static readonly Regex HtmlTagReg = new Regex(@"<(\s*)(/?\s*\w+/?)(\s+[^<>]+)?\s*>");

        /* match ex:
V[0]:	<img      src=\"xxx.jpg\" / >
G[0]:	<img      src=\"xxx.jpg\" / >
G[1]:	
G[2]:	img
G[3]:	      src=\"xxx.jpg\" / 

V[1]:	<p />  
G[0]:	<p />
G[1]:	
G[2]:	p
G[3]:	 /
 * 
V[25]:	<div class=\"xxx\">
G[0]:	<div class=\"xxx\">
G[1]:	
G[2]:	div
G[3]:	 class=\"xxx\"
 * 
V[26]:	</div>
G[0]:	</div>
G[1]:	
G[2]:	/div
G[3]:	
*/
        public static bool ValidateHtmlTag(string html, out string message)
        {
            return ValidateHtmlTag(html, true, out message);
        }
        public static bool ValidateHtmlTag(string html, bool fullValidate, out string message)
        {
            message = string.Empty;
            if (string.IsNullOrEmpty(html)) return true;

            MatchCollection matchs = HtmlTagReg.Matches(html);
            if (matchs.Count == 0) return true;

            HtmlNestedRule nestRule = new HtmlNestedRule();

            List<HtmlTag> startTags = new List<HtmlTag>();
            List<HtmlTag> missingEndTags = new List<HtmlTag>();
            List<HtmlTag> missingStartTags = new List<HtmlTag>();
            List<HtmlTag> redundantSpaceTags = new List<HtmlTag>();
            List<HtmlTag[]> invalidNestedTags = new List<HtmlTag[]>();
            List<HtmlTag> notAllowTags = new List<HtmlTag>();

            Func<int> totalErrorTag = () => { return missingEndTags.Count + missingStartTags.Count + redundantSpaceTags.Count + invalidNestedTags.Count + notAllowTags.Count; };

            int elementsCount = 0;
            foreach (Match match in matchs)
            {
                if (!match.Success) continue;

                HtmlTag tag = new HtmlTag(match.Groups[0].Value, match.Groups[1].Value, match.Groups[2].Value.Replace(" ", ""), match.Groups[3].Value.Replace(" ", ""), ++elementsCount);
                if (tag.IsRedundantSpace)
                {
                    redundantSpaceTags.Add(tag);
                }

                if (tag.IsSelfClose) continue;

                if (tag.IsEndTag)
                {
                    int lastMatchEndTagIndex = MatchTagPair(tag, startTags);
                    if (lastMatchEndTagIndex >= 0)
                    {
                        AddMissingEndTag(missingEndTags, startTags, lastMatchEndTagIndex);
                    }
                    else
                    {
                        missingStartTags.Add(tag);
                    }
                }
                else
                {
                    ValidNotAllowTag(tag, notAllowTags);
                    ValidNestedRule(tag, nestRule, invalidNestedTags, startTags);
                    startTags.Add(tag);
                }
                if (!fullValidate && totalErrorTag() > 0)
                {
                    break;
                }
            }
            missingEndTags.AddRange(startTags);

            message = BuildMessage(redundantSpaceTags, missingStartTags, missingEndTags, invalidNestedTags, notAllowTags);
            return totalErrorTag() == 0;
        }

        private static string BuildMessage(List<HtmlTag> redundantSpaceTags, List<HtmlTag> missingStartTags, List<HtmlTag> missingEndTags, List<HtmlTag[]> invalidNestedTags, List<HtmlTag> notAllowTags)
        {
            StringBuilder messageBuilder = new StringBuilder();
            string error_MissingStartTag = "element({1}):{0},missing start tag; ";
            string error_MissingEndTag = "element({1}):{0},missing end tag; ";
            string error_RedundantSpace = "element({1}):{0},redundant space after < ; ";
            string warn_invalidNestedTag = "element({2}):{1},can not nested in {0}; ";
            string error_notAllowTag = "element({1}):{0},not an allow tag name; "; ;

            missingStartTags.ForEach(t => { messageBuilder.AppendFormat(error_MissingStartTag, t.FullTag, t.Position); messageBuilder.AppendLine(); });
            missingEndTags.ForEach(t => { messageBuilder.AppendFormat(error_MissingEndTag, t.FullTag, t.Position); messageBuilder.AppendLine(); });
            redundantSpaceTags.ForEach(t => { messageBuilder.AppendFormat(error_RedundantSpace, t.FullTag, t.Position); messageBuilder.AppendLine(); });
            invalidNestedTags.ForEach(tt => { messageBuilder.AppendFormat(warn_invalidNestedTag, tt[0].TagName, tt[1].FullTag, tt[1].Position); messageBuilder.AppendLine(); });
            notAllowTags.ForEach(t => { messageBuilder.AppendFormat(error_notAllowTag, t.FullTag, t.Position); messageBuilder.AppendLine(); });
            return messageBuilder.ToString();
        }

        private static void ValidNestedRule(HtmlTag tag, HtmlNestedRule nestRule, List<HtmlTag[]> invalidNestedTags, List<HtmlTag> startTags)
        {
            if (startTags.Count > 0)
            {
                string lastTagName = startTags[startTags.Count - 1].TagName.Trim('/');
                string tagName = tag.TagName.Trim('/');
                if (nestRule.IsNestValid(lastTagName, tagName) == false)
                {
                    invalidNestedTags.Add(new[] { startTags[startTags.Count - 1], tag });
                }
            }
        }

        private static void AddMissingEndTag(List<HtmlTag> missingEndTags, List<HtmlTag> startTags, int matchedEndTagIndex)
        {
            for (int i = startTags.Count - 1; i > matchedEndTagIndex; i--)
            {
                missingEndTags.Add(startTags[i]);
                startTags.RemoveAt(i);
            }
            startTags.RemoveAt(matchedEndTagIndex); //remove has pair element
        }

        private static int MatchTagPair(HtmlTag tag, List<HtmlTag> startTags)
        {
            if (startTags.Count == 0) return -1;
            return startTags.FindLastIndex(startTags.Count - 1, tag.IsTagPair);
        }

        private static void ValidNotAllowTag(HtmlTag tag, List<HtmlTag> notAllowTags)
        {
            if (tag.IsNotAllow) notAllowTags.Add(tag);
        }

        class HtmlTag
        {
            public HtmlTag(string fullTag, string redundantSpace, string tagName, string attribute, int position)
            {
                FullTag = fullTag;
                TagName = tagName.ToLower();
                Attribute = attribute;
                Position = position;
                IsRedundantSpace = redundantSpace.Length > 0;
                IsEndTag = tagName.StartsWith("/");
                IsSelfClose = IsSelfCloseTag(TagName, attribute);
                IsNotAllow = NotAllowTags.Contains(TagName.Trim('/'));
            }
            public int Position;
            public string FullTag;
            public string TagName;
            public string Attribute;
            public bool IsRedundantSpace;
            public bool IsEndTag;
            public bool IsSelfClose;
            public bool IsNotAllow;

            private static readonly HashSet<string> SelfCloseTags = new HashSet<string>(new[] { "br", "hr", "input", "img" });
            private static readonly HashSet<string> NotAllowTags = new HashSet<string>(new[] { "xml", "meta" });
            private static bool IsSelfCloseTag(string tagName, string attribute)
            {
                return SelfCloseTags.Contains(tagName.Trim('/')) || tagName.EndsWith("/") || attribute == "/";
            }

            public static bool IsTagPair(HtmlTag tag1, HtmlTag tag2)
            {
                if (tag1 == null || tag2 == null) return false;
                if (tag1.IsSelfClose || tag2.IsSelfClose) return false;
                if (tag1.IsEndTag == tag2.IsEndTag) return false;
                return tag1.TagName.Trim('/') == tag2.TagName.Trim('/');
            }

            public bool IsTagPair(HtmlTag tagPair)
            {
                return IsTagPair(this, tagPair);
            }

            public override string ToString()
            {
                return string.Format("FullTag={0},TagName={1},IsRedundantSpace={2},IsEndTag={3},IsSelfClose={4},Position={5},IsNotAllow={6}", FullTag, TagName, IsRedundantSpace, IsEndTag, IsSelfClose, Position, IsNotAllow);
            }
        }

        class HtmlNestedRule
        {
            private HashSet<string> InlineElement = new HashSet<string>("a,img,br,span,input,select,textarea,label,button,tt,i,b,big,small,em,strong,dfn,code,samp,kbd,var,cite,abbr,acronym,sub,sup,q,bdo,object,scriptm,map".Split(','));
            private HashSet<string> ListAllow = new HashSet<string>(new[] { "li" });
            private HashSet<string> DLAllow = new HashSet<string>(new[] { "dt", "dd" });
            private HashSet<string> SelectAllow = new HashSet<string>(new[] { "optgroup", "option" });
            private HashSet<string> TableAllow = new HashSet<string>(new[] { "caption", "colgroup", "col", "thead", "tbody" });
            private HashSet<string> TableHeadAllow = new HashSet<string>(new[] { "tr" });
            private HashSet<string> TRAllow = new HashSet<string>(new[] { "th", "td" });

            private readonly Dictionary<string, HashSet<string>> AllowNestedTag;

            public HtmlNestedRule()
            {
                AllowNestedTag = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
                AllowNestedTag.Add("ul", ListAllow);
                AllowNestedTag.Add("ol", ListAllow);
                AllowNestedTag.Add("dl", DLAllow);
                AllowNestedTag.Add("dt", InlineElement);
                AllowNestedTag.Add("select", SelectAllow);
                AllowNestedTag.Add("p", new HashSet<string>());
                for (int i = 1; i <= 6; i++) AllowNestedTag.Add("h" + i, InlineElement);

                AllowNestedTag.Add("table", TableAllow);
                AllowNestedTag.Add("colgroup", new HashSet<string>(new[] { "col" }));
                AllowNestedTag.Add("thead", TableHeadAllow);
                AllowNestedTag.Add("tbody", TableHeadAllow);
                AllowNestedTag.Add("tr", TRAllow);
            }

            public bool IsNestValid(string tagName, string nestedTagName)
            {
                if (string.IsNullOrEmpty(tagName) || string.IsNullOrEmpty(nestedTagName)) return true;
                if (AllowNestedTag.ContainsKey(tagName) == false) return true;
                return AllowNestedTag[tagName].Contains(nestedTagName.ToLower());
            }
        }

        static readonly Regex regEx_tag = new Regex("<(/?\\w+/?)(\\s[^<>]+)?>");
        static readonly Regex regEx_tagpair = new Regex("<(\\w+)></\\1>");
        static readonly Regex regEx_ignored = new Regex("</?br/?>|</?img/?>|</?input/?>", RegexOptions.IgnoreCase);

        public static String checkHtmlTags(String str)
        {
            if (str == null) return "";

            MatchCollection matchs = regEx_tag.Matches(str);
            StringBuilder sb = new StringBuilder();
            String tag, attr;
            foreach (Match match in matchs)
            {
                tag = match.Groups[1].Value;
                attr = match.Groups[2].Value;
                if ((attr.EndsWith("/")) || tag.EndsWith("/"))
                {
                    continue;
                }

                sb.Append('<').Append(tag).Append('>');
            }

            String fixedstr = sb.ToString().ToLower();

            fixedstr = regEx_ignored.Replace(fixedstr, "");

            Match mat = regEx_tagpair.Match(fixedstr);
            while (mat.Success)
            {
                fixedstr = regEx_tagpair.Replace(fixedstr, "", 1);
                mat = regEx_tagpair.Match(fixedstr);
            }

            return fixedstr;
        }
    }



    class HtmlTagValidatorTest
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
            // string tag1 = "Feel like a big leaguer with this Derek Jeter replica jersey from Adidas. Made of 100-percent polyester Air Mesh,the jersey will have you looking and feeling just like a real ballplayer,with a button front and a screen printed team name on the chest. Team logo is on the sleeve. Adidas 3 stripe logo on the rear collar. <ul><strong>Features</strong><li>100% polyester air mesh</li><li>Screen printed MLB team color baseball jersey</li><li>Official MLB product</li><li>Turn garment inside out,Machine wash cold separately,Gentel Cycle,Only Non-Cholorine bleach when needed,Tumble dry low,Remove promptly,Do not iron,Do not dry clean</li><li>China</li></ul><br /><br /><B>Mustela 2 in 1 Hair and Body Wash</B><br><P>Baby's first baths are a special time: choose a gentle cleanser that won't harm their delicate skin. Mustela 2 in 1 Hair and Body Wash doesn't contain soap and can be used on the head and body. It protects skin from drying out with its coconut oil nutrients. Gently formulated,it's as easy on baby's eyes as it is on the skin.</P><br><B>Why You'll Love It:</B> One-stop solution to all your baby's skin and hair needs<br><br><B>Age:</B> Newborn and up<br><br><B>Features</B> <UL><li>Gentle for baby's bath</li><li>Tested by dermatologists</li><li>Hypoallergenic</li><li>Doesn't contain soap</li></UL><b> Mustela Facial Cleansing Cloths </b><br>  <p> Mustela Facial Cleansing Cloths are perfect for baby's sensitive skin. It's one-step cleansing that's also hypoallergenic and especially formulated to minimize the risk of allergic reactions.     </p><br>  <b> Why You'll Love It: </b> Trust Mustela Facial Cleansing cloths for keeping your baby's skin soft and fresh.  <br><br>    <b>Features</b><br>  <ul><li> Ideal for quick clean-ups and perfect for travel </li>  <li>Does not sting eyes </li>  <li>Alcohol-free </li>  <li>Tested under the supervision of dermatologists and ophthalmologists </li>  <li> Contains Mustela's no-rinse cleansing fluid </li>  <li>Easy to clean baby's face </li>  <li>Disposable </li>  <li>Keeps the skin soft,moisturized and soothed. </li>    ";
            string tag1 = "Feel like a big leaguer with this Derek Jeter replica jersey from Adidas. Made of 100-percent polyester Air Mesh,the jersey will have you looking and feeling just like a real ballplayer,with a button front and a screen printed team name on the chest. Team logo is on the sleeve. Adidas 3 stripe logo on the rear collar. <ul><strong>Features</strong><li>100% polyester air mesh</li><li>Screen printed MLB team color baseball jersey</li><li>Official MLB product</li><li>Turn garment inside out,Machine wash cold separately,Gentel Cycle,Only Non-Cholorine bleach when needed,Tumble dry low,Remove promptly,Do not iron,Do not dry clean</li><li>China</li></ul><br /><br />";

            while (true)
            {
                Console.Write("input a number to run times: ");
                string v = Console.ReadLine().Trim();
                if (v.Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                int times = 0;
                if (int.TryParse(v, out times) == false) continue;
                DateTime dt1 = DateTime.Now;
                string msg = string.Empty;
                for (int i = 0; i < times; i++)
                {
                    if (!string.IsNullOrEmpty(html))
                    {
                        HtmlTagValidator.ValidateHtmlTag(html, out msg);
                        //msg = HtmlTagValidator.checkHtmlTags(html);
                        Console.WriteLine("result:{0}", msg);
                    }
                    else
                    {
                        foreach (string tag in TestData)
                        {
                            HtmlTagValidator.ValidateHtmlTag(tag, out msg);
                            Console.WriteLine("result:{0}", msg);
                        }
                    }
                }
                DateTime dt2 = DateTime.Now;
                Console.WriteLine("length:{2},run times:{0},millionsecond:{1}ms", times, (dt2 - dt1).TotalMilliseconds, tag1.Length);
                Console.WriteLine();
            }
        }
    }
}
