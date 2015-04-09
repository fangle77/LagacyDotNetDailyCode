using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Pineapple.Core;
using Pineapple.Data;

namespace Pineapple.UnitTest.Data
{
    [TestFixture]
    public class NoSqlTest : BaseTest
    {
        protected override void DoPrepare()
        {
            Container.RegisterAssemblyInterface("Pineapple.Data", "Pineapple.Data.Sqlite");

        }

        [Test]
        public void NosqlTest()
        {
            logger.Info("start test");

            var nosql = Container.Resolve<INoSqlData>();
            nosql = nosql.CreateNoSqlData("CompanyInfo");

            nosql.Save("Name", "xmlycm");
            logger.Info("start test22");
            var m = nosql.LoadDynamicModel();
            logger.Info("start test111");
            Assert.IsTrue(m.Name == "xmlycm");
        }

        [Test]
        public void LogTest()
        {
            logger.Info("console out put log.");
        }
    }
}
