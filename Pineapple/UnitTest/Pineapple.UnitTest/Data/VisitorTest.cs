using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Pineapple.Business;
using Pineapple.Core;
using Pineapple.Data;

namespace Pineapple.UnitTest.Data
{
    [TestFixture]
    public class VisitorTest : BaseTest
    {
        VisitorManager manager;
        protected override void DoPrepare()
        {
            manager = Container.Resolve<VisitorManager>();
        }

        [Test]
        public void CacheKeyTest()
        {
            var vi = manager.GetVisitor("abcd");
        }
    }
}
