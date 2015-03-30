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
            var nosql = Container.Resolve<INoSqlData>();
            nosql.CreateNoSqlData("CompanyBasic");
            nosql.Save("Name", "xmlycm");
            var m = nosql.LoadDynamicModel();
            Assert.IsTrue(m.Name == "xmlycm");
        }
    }
}
