using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ThreadStaticTest
    {
        public static void RunTest()
        {
            Context.ThreadContext.Name = "main";
            Context.ThreadContext.Count = 100;
            Console.WriteLine(Context.ThreadContext);
            Task.Factory.StartNew(() =>
             {
                 Context.ThreadContext.Name = "Thread1";
                 Context.ThreadContext.Count = 1;
                 Console.WriteLine(Context.ThreadContext);
                 Task.Delay(100);
                 Context.ThreadContext.Count = 11;
                 Console.WriteLine(Context.ThreadContext);
             });

            Task.Factory.StartNew(() =>
            {
                Context.ThreadContext.Name = "Thread2";
                Context.ThreadContext.Count = 2;
                Console.WriteLine(Context.ThreadContext);
                Task.Delay(80);
                Context.ThreadContext.Count = 22;
                Console.WriteLine(Context.ThreadContext);
            });
            Task.Delay(200);
            Console.WriteLine(Context.ThreadContext);
        }
    }

    class Context
    {
        [ThreadStatic]
        private static Context context = new Context();

        public int Count { get; set; }

        public string Name { get; set; }

        public static Context ThreadContext
        {
            get { return (context = context ?? new Context()); }
        }

        public override string ToString()
        {
            return string.Format("Name={0},Count={1}", Name, Count);
        }
    }
}
