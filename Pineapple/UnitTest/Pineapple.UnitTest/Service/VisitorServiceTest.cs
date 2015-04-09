using System;
using NUnit.Framework;
using Pineapple.Core;
using Pineapple.Model;
using Pineapple.Service;

namespace Pineapple.UnitTest.Service
{
    public class VisitorServiceTest : BaseTest
    {
        private static VisitorService VisitorService
        {
            get
            {
                return Container.Resolve<VisitorService>();
            }
        }

        [Test]
        public void InjectionTest()
        {
            Visitor visitor = new Visitor() { VisitDate = DateTime.Now.ToString() };
            visitor = VisitorService.AddVisitor(visitor);

            Console.WriteLine(visitor.Id);
        }

        [Test]
        public void CacheInterceptorTest()
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
            Console.WriteLine("==========");
            VisitorService.LoadLatestNVisitors(3).ForEach(Console.WriteLine);
            Console.WriteLine("==========");
            VisitorService.LoadLatestNVisitors(3).ForEach(Console.WriteLine);
        }

        protected override void DoPrepare()
        {

        }
    }
}
