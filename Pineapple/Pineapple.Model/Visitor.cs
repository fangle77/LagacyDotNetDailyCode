using System;

namespace Pineapple.Model
{
    public class Visitor
    {
        public string VisitorId { get; set; }

        public DateTime FirstVisitTime { get; set; }

        public string EnterUrl { get; set; }

        public string RefererUrl { get; set; }

        public string UserName { get; set; }
    }

    public class VisitLog
    {
        public string VisitorId { get; set; }

        public DateTime VisitTime { get; set; }

        public string ClientIp { get; set; }

        public string UserAgent { get; set; }

        public string SessionId { get; set; }
    }
}
