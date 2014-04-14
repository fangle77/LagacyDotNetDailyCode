using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TNSConfig
{
    class Network
    {
        /// <summary>
        /// 测试socket连接
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public static bool SocketConnectTo(string ip, string port, out string err)
        {
            err = "";
            try
            {
                Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint p = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
                sck.Connect(p);
                sck.Disconnect(false);
                sck.Close();
                return true;
            }
            catch (Exception ex)
            {
                err = string.Format("连接{0}:{1}失败，失败原因：", ip, port) + ex.Message;
                return false;
            }
        }
    }
}
