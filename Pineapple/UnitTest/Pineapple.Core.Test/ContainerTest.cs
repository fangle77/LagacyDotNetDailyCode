using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pineapple.Core;
using Pineapple.Data;
using Container = Pineapple.Core.Container;

namespace Pineapple.Core.Test
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        public void RegisterAssemblyTest()
        {
            Container.ResisterAssemblyType("Pineapple.Data");
            var data = Container.Resolve<VisitorData>();
            Assert.IsNotNull(data);
            Assert.IsInstanceOfType(data, typeof(VisitorData));
        }
    }
}
