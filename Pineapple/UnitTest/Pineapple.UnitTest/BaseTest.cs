using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Pineapple.Core;
using Rhino.Mocks;
using log4net.Config;

namespace Pineapple.UnitTest
{
    public abstract class BaseTest
    {
        MockRepository mock;
        static BaseTest()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.xml"));
        }

        [SetUp]
        public void Prepare()
        {
            mock = new MockRepository();

            DoPrepare();
        }

        protected abstract void DoPrepare();

        [TearDown]
        public virtual void Cleanup()
        {

        }
    }
}
