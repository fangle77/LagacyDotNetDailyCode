using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TNSConfig
{
    class TNSConfig
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Protocol { get; set; }
        public string SID { get; set; }

        public eConnectData ConnectData { get; set; }

        public string ServiceName { get; set; }

        public string Version { get; set; }

        public TNSConfig() { }

        public TNSConfig(string version, string host, string port, string protocol, string sid, string serviceName)
        {
            this.Version = version;
            this.Host = host;
            this.Port = port;
            this.Protocol = protocol;
            this.SID = sid;
            this.ServiceName = serviceName;
        }

        public string ToTNSString()
        {
            return ToTNSString(false);
        }

        //(SERVER = DEDICATED)
        public string ToTNSString(bool withDEDICATEDServer)
        {
            return BuildTNSConfig(Version, ServiceName, Host, Port, SID, withDEDICATEDServer);
        }

        private string BuildTNSConfig(string version, string alisName, string host, string port, string SID, bool withDEDICATEDServer)
        {
            string dedicatedServer = withDEDICATEDServer ? @"  (SERVER = DEDICATED)
    " : "";
            string config = string.Format(@"
{0} =
  (DESCRIPTION =
    (ADDRESS_LIST =
      (ADDRESS = (PROTOCOL = TCP)(HOST = {1})(PORT = {2}))
    )
    (CONNECT_DATA =
    {4}  ({5} = {3})
    )
  )", alisName, host, port, SID, dedicatedServer, ConnectData);
            return config;
        }
    }

    enum eConnectData
    {
        SERVICE_NAME,
        SID
    }
}
