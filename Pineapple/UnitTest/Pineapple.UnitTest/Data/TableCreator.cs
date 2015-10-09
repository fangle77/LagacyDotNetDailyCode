using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Pineapple.Core;
using Pineapple.Data;

namespace Pineapple.UnitTest.Data
{
    [TestFixture]
    public class TableCreator : BaseTest
    {
        ISqlExecutor executor;
        protected override void DoPrepare()
        {
            executor = Container.Resolve<ISqlExecutor>();
        }

        [Test]
        public void CreateDataDbTables()
        {
            logger.Info("Datadb");
            string dbfile = "App_Data/pineapple.data.db";
            FileInfo fi = new FileInfo(dbfile);
            logger.Info(fi.FullName);
            DirectoryInfo di = new DirectoryInfo("../../../../Pineapple.Data.Sqlite/SqlScript/pinapple.data/Tables");
            logger.Info(di.Exists);

            var fs = di.GetFiles();
            foreach (var f in fs)
            {
                logger.Info(f.Name);
                executor.Execute(GetConnectionString(fi.FullName), File.ReadAllText(f.FullName));
            }
            logger.Info("finished");
        }

        [Test]
        public void CreateDbTables()
        {
            logger.Info("meeting-room");
            string dbfile = "App_Data/meeting-room.db";
            FileInfo fi = new FileInfo(dbfile);
            logger.Info(fi.FullName);
            DirectoryInfo di = new DirectoryInfo("../../../../Pineapple.Data.Sqlite/SqlScript/meeting-room.sqlite");
            logger.Info(di.Exists);

            var fs = di.GetFiles();
            foreach (var f in fs)
            {
                logger.Info(f.Name);
                executor.Execute(GetConnectionString(fi.FullName), File.ReadAllText(f.FullName));
            }
            logger.Info("finished");
        }

        [Test]
        public void CreateActionDbTables()
        {
            logger.Info("ActionDb");
            string dbfile = "App_Data/pineapple.action.db";
            FileInfo fi = new FileInfo(dbfile);
            logger.Info(fi.FullName);
            DirectoryInfo di = new DirectoryInfo("../../../../Pineapple.Data.Sqlite/SqlScript/pinapple.action/Tables");
            logger.Info(di.Exists);

            var fs = di.GetFiles();
            foreach (var f in fs)
            {
                logger.Info(f.Name);
                executor.Execute(GetConnectionString(fi.FullName), File.ReadAllText(f.FullName));
            }
            logger.Info("finished");
        }

        private string GetConnectionString(string dbFile)
        {
            return string.Format("Data Source=\"{0}\";Pooling=True;", dbFile);
        }

    }
}
