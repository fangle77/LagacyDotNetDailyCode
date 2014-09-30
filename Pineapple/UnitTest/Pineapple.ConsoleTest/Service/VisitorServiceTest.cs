using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Core;
using Pineapple.Model;
using Pineapple.Service;
using Microsoft.Practices.Unity;

namespace Pineapple.ConsoleTest.Service
{
    class VisitorServiceTest
    {
        private static VisitorService VisitorService
        {
            get
            {
                return Container.Resolve<VisitorService>();
            }
        }

        public static void InjectionTest()
        {
            Visitor visitor = new Visitor() { VisitDate = DateTime.Now.ToString() };
            visitor = VisitorService.AddVisitor(visitor);

            Console.WriteLine(visitor.Id);
        }

        public static void CacheInterceptorTest()
        {
            Visitor visitor = new Visitor() { VisitDate = DateTime.Now.ToString() };
            visitor = VisitorService.AddVisitor(visitor);

            var top = VisitorService.LoadLatestNVisitors(2);

            visitor = new Visitor() { VisitDate = DateTime.Now.ToString() };
            visitor = VisitorService.AddVisitor(visitor);

            var top2 = VisitorService.LoadLatestNVisitors(2);

            top.ForEach(Console.WriteLine);
            Console.WriteLine("==========");
            top2.ForEach(Console.WriteLine);
        }
    }
}
