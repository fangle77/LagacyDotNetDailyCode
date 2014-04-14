using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace yltl.Common
{
    /// <summary>
    /// 实体类序列化为xml文档的辅助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConfigHelper<T> where T : class, new()
    {
        static readonly string _xmlFolderName = System.AppDomain.CurrentDomain.BaseDirectory + "CONFIG";
        static string _xmlConfigFileName = "SysConfig.xml";

        public ConfigHelper()
        {
            _xmlConfigFileName = typeof(T).Name + ".xml";
        }

        private void Create()
        {
            Create(new T());
        }

        /// <summary>
        /// 如果不存在则创建
        /// </summary>
        private void Create(T ies)
        {
            if (Directory.Exists(_xmlFolderName) == false)
            {
                Directory.CreateDirectory(_xmlFolderName);
            }

            string file = string.Format("{0}\\{1}", _xmlFolderName, _xmlConfigFileName);
            if (File.Exists(file) == false)
            {
                using (var fs = File.Open(file, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    xs.Serialize(fs, ies);
                }
            }
        }

        static object _lockForReadConfg = new object();

        /// <summary>
        /// 读取配置文件并返回实体，不存在或读取错误时返回null
        /// </summary>
        /// <returns></returns>
        public T Read()
        {
            try
            {
                string file = string.Format("{0}\\{1}", _xmlFolderName, _xmlConfigFileName);
                lock (_lockForReadConfg)
                {
                    if (File.Exists(file) == false)
                    {
                        Create();
                    }
                    XmlSerializer xs = new XmlSerializer(typeof(T));

                    using (var stream = File.OpenRead(file))
                    {
                        var o = xs.Deserialize(stream);
                        return o as T;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        private static FileInfo _LastFileInfo;
        private static T _IESConfig;
        /// <summary>
        /// 重新读取配置文件内容：如果有更新的话则更像内容
        /// </summary>
        /// <returns></returns>
        public static T ReloadFile()
        {
            try
            {
                string fileName = string.Format("{0}\\{1}", _xmlFolderName, _xmlConfigFileName);

                if (_IESConfig == null)
                {
                    _IESConfig = (new ConfigHelper<T>()).Read();
                    _LastFileInfo = new FileInfo(fileName);
                }
                else
                {
                    if (File.Exists(fileName) == false)
                    {
                        (new ConfigHelper<T>()).Create();
                    }
                    if (_LastFileInfo == null) _LastFileInfo = new FileInfo(fileName);

                    FileInfo fi = new FileInfo(fileName);
                    if (fi.Length != _LastFileInfo.Length
                        || fi.LastWriteTime != _LastFileInfo.LastWriteTime)
                    {
                        _IESConfig = (new ConfigHelper<T>()).Read();
                        _LastFileInfo = fi;
                    }
                }
            }
            catch
            {
                return null;
            }
            return _IESConfig;
        }

        /// <summary>
        /// 刷新配置文件内容
        /// </summary>
        /// <param name="config"></param>
        public static void Reflesh(T config)
        {
            if (config == default(T)) return;
            var cfg = new ConfigHelper<T>();
            string fileName = string.Format("{0}\\{1}", _xmlFolderName, _xmlConfigFileName);
            //(new ConfigHelper<T>()).Create(config);
            if (File.Exists(fileName)) File.Delete(fileName);
            cfg.Create(config);
        }
    }
}
