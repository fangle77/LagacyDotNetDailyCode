using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchResponseSpider
{
    class Program
    {
        static void Main()
        {
            Logger loger = new Logger(DateTime.Now.ToString("yy-MM-dd HHmmss"));
            loger.Log("start:=======  " + DateTime.Now);

            Console.WriteLine("=============" + DateTime.Now);
            try
            {
                SpiderTaskManager manager = new SpiderTaskManager(AppConfig.Instance.ThreadCount);
                NetRequest.TimeOutSecond = AppConfig.Instance.TimeOutSecond;

                manager.TaskInfoEvent += manager_TaskInfoEvent;
                manager.TaskInfoEvent += loger.Log;
                manager.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                loger.Log(ex.Message + ";\r\n " + ex.StackTrace);
            }
            Console.WriteLine("=============" + DateTime.Now);
            loger.Log("end:=======  " + DateTime.Now);
            loger.FlushToFile();
            Console.Read();
        }

        static void manager_TaskInfoEvent(string obj)
        {
            Console.WriteLine(obj);
        }
    }
}
