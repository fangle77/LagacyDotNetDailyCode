using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace yltl.DBUtility
{
    //Element
    /// <summary>
    /// 数据库连接配置
    /// </summary>
    internal class DBConnectionElement : ConfigurationElement
    {
        /// <summary>
        /// eDBType的字符串格式，不区分大小写
        /// </summary>
        [ConfigurationProperty("DBType", IsRequired = true)]
        private string DBType_S
        {
            get { return (string)base["DBType"]; }
            set { base["DBType"] = value; }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        //[ConfigurationProperty("DBType", IsRequired = true)]
        public eDBType DBType
        {
            get
            {
                //return (eDBType)base["DBType"];
                string type = DBType_S;
                return (eDBType)Enum.Parse(typeof(eDBType), type, true);
            }
            //set
            //{
            //    base["DBType"] = value;
            //}
        }

        /// <summary>
        /// 连接的名称，用于区分同一种数据库类型下的不同连接
        /// </summary>
        [ConfigurationProperty("Name", IsRequired = false)]
        public string Name
        {
            get
            {
                return (string)base["Name"];
            }
            set
            {
                base["Name"] = value;
            }
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        [ConfigurationProperty("ConnectionString", IsRequired = true)]
        public string ConnectionString
        {
            get
            {
                return (string)base["ConnectionString"];
            }
            set
            {
                base["ConnectionString"] = value;
            }
        }

        /// <summary>
        /// 指示连接串是否是加密串
        /// </summary>
        [ConfigurationProperty("IsEncrypt", IsRequired = true, DefaultValue = true)]
        public bool IsEncrypt
        {
            get
            {
                return (bool)base["IsEncrypt"];
            }
            set
            {
                base["IsEncrypt"] = value;
            }
        }
    }

    //ElementCollection
    /// <summary>
    /// 数据库连接配置集合
    /// </summary>
    internal class DBConnectionElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DBConnectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var ele = ((DBConnectionElement)element);
            if (ele == null) return "";
            return string.Format("{0}{1}", ele.DBType, ele.Name);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get { return "DBConnection"; }
        }

        public DBConnectionElement this[int index]
        {
            get
            {
                return (DBConnectionElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
    }

    //Section
    /// <summary>
    /// 数据库连接配置节
    /// </summary>
    internal class DBConnectionSection : ConfigurationSection
    {
        /// <summary>
        /// 获取所有的连接配置节
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public DBConnectionElementCollection DBConnections
        {
            get
            {
                return (DBConnectionElementCollection)base[""];
            }
        }
    }
}
