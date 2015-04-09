using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Pineapple.Core;
using Rhino.Mocks;
using log4net;
using log4net.Config;

namespace Pineapple.UnitTest
{
    public abstract class BaseTest
    {
        protected ILog logger = null;

        MockRepository mock;

        [SetUp]
        public void Prepare()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.xml"));

            logger = LogManager.GetLogger(this.GetType());

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
