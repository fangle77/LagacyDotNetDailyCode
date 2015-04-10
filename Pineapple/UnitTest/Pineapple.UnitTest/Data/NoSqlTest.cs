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
        }

        [Test]
        public void NosqlTest()
        {
            var nosql = Container.Resolve<INoSqlData>();
            nosql = nosql.CreateNoSqlData("CompanyInfo");

            nosql.Save("Name", "xmlycm");
            var m = nosql.LoadDynamicModel();
            Assert.IsTrue(m.Name == "xmlycm");

            nosql.Save("Name", "xmlycm2");
            m = nosql.LoadDynamicModel();
            Assert.IsTrue(m.Name == "xmlycm2");
        }

        [Test]
        public void LogTest()
        {
            logger.Info("console out put log.");
        }
    }
}
