using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;

namespace TNSConfig
{
    class OraTestConnect
    {
        public static bool TestConnect(string user, string pwd, string serviceName, out string msg)
        {
            //Data Source=lan_189;User ID=user_ahdy_shi;Password=user_ahdy_shi
            string conn = string.Format("Data Source={0};User ID={1};Password={2}", serviceName.Trim(), user, pwd);

            try
            {
                using (OracleConnection con = new OracleConnection(conn))
                {
                    OracleConnection.ClearPool(con);
                    System.Threading.Thread.Sleep(100);
                    con.Open();

                    con.Close();
                    con.Dispose();
                }

                msg = "连接" + serviceName.ToUpper() + "成功";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
    }
}
