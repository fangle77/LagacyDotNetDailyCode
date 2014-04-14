using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ExcelExportorForWin
{
    /// <summary>
    /// 列元素，作为映射关系的原子单位
    /// </summary>
    public class ColumnElement
    {
        public ColumnElement() { }
        public ColumnElement(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 列名
        /// </summary>
        [XmlAttribute]
        public string Name;

        /// <summary>
        /// 比较两个元素是否相等，按照属性直接进行比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var e = obj as ColumnElement;
            return e != null && this == e;
        }

        /// <summary>
        /// 比较两个元素是否相等，按照属性直接进行比较
        /// </summary>
        /// <param name="ele1"></param>
        /// <param name="ele2"></param>
        /// <returns></returns>
        public static bool operator ==(ColumnElement ele1, ColumnElement ele2)
        {
            if (Object.ReferenceEquals(ele1, null)
                && Object.ReferenceEquals(ele2, null)) return true;

            if (Object.ReferenceEquals(ele1, null)
                || Object.ReferenceEquals(ele2, null)) return false;

            return ele1.Name == ele2.Name;
        }

        /// <summary>
        /// 比较两个元素是否不相等，按照属性直接进行比较
        /// </summary>
        /// <param name="ele1"></param>
        /// <param name="ele2"></param>
        /// <returns></returns>
        public static bool operator !=(ColumnElement ele1, ColumnElement ele2)
        {
            return !(ele1 == ele2);
        }

        /// <summary>
        /// 重新ToString方法，返回Name属性的值
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// 映射关系元素，每一个实例包含一组映射
    /// </summary>
    public class MappingElement
    {
        public MappingElement() { }

        public MappingElement(ColumnElement source, ColumnElement destination)
        {
            Source = source;
            Destination = destination;
        }

        [XmlElement]
        public ColumnElement Source { get; set; }

        [XmlElement]
        public ColumnElement Destination { get; set; }

        /// <summary>
        /// 比较两个实例是否相等，按照属性直接进行比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var me = obj as MappingElement;
            return me != null && (this == me);
        }
        /// <summary>
        ///  比较两个实例是否相等，按照属性直接进行比较
        /// </summary>
        /// <param name="me1"></param>
        /// <param name="me2"></param>
        /// <returns></returns>
        public static bool operator ==(MappingElement me1, MappingElement me2)
        {
            if (Object.ReferenceEquals(me1, null)
               && Object.ReferenceEquals(me2, null)) return true;

            if (Object.ReferenceEquals(me1, null)
               || Object.ReferenceEquals(me2, null)) return false;

            return me1.Source == me2.Source && me1.Destination == me2.Destination;
        }
        /// <summary>
        ///  比较两个实例是否不相等，按照属性直接进行比较
        /// </summary>
        /// <param name="me1"></param>
        /// <param name="me2"></param>
        /// <returns></returns>
        public static bool operator !=(MappingElement me1, MappingElement me2)
        {
            return !(me1 == me2);
        }

        /// <summary>
        /// 重新ToString方法，返回Source.Name+"-"+Destination.Name属性的值
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}-{1}", Source == null ? "" : Source.ToString(), Destination == null ? "" : Destination.ToString());
        }
    }

    /// <summary>
    /// 映射集合
    /// </summary>
    public class MapElementCollection
    {
        private int _id = -1;
        /// <summary>
        /// ID，用于标识唯一
        /// </summary>
        [XmlAttribute("ID")]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 集合名称，不能重复
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 映射关系集合
        /// </summary>
        [XmlArrayItem("Col")]
        public List<MappingElement> Elements { get; set; }

        /// <summary>
        /// 添加一个映射元素到集合中
        /// </summary>
        /// <param name="element"></param>
        public void Add(MappingElement element)
        {
            if (Elements == null) Elements = new List<MappingElement>();
            if (element == null) return;
            var eles = Elements;
            bool exist = false;
            foreach (var m in eles)
            {
                if (m == element) { exist = true; break; }
            }
            if (exist == false)
            {
                eles.Add(element);
                Elements = eles;
            }
        }

        /// <summary>
        ///  比较两个实例是否相等,比较ID值
        /// </summary>
        /// <param name="mec1"></param>
        /// <param name="mec2"></param>
        /// <returns></returns>
        public static bool operator ==(MapElementCollection mec1, MapElementCollection mec2)
        {
            if (Object.ReferenceEquals(mec1, null)
               && Object.ReferenceEquals(mec2, null)) return true;

            if (Object.ReferenceEquals(mec1, null)
              || Object.ReferenceEquals(mec2, null)) return false;

            return mec1.ID == mec2.ID;
        }

        /// <summary>
        ///  比较两个实例是否不相等,比较ID值
        /// </summary>
        /// <param name="mec1"></param>
        /// <param name="mec2"></param>
        /// <returns></returns>
        public static bool operator !=(MapElementCollection mec1, MapElementCollection mec2)
        {
            return !(mec1 == mec2);
        }

        /// <summary>
        /// 比较两个实例是否相等,比较ID值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var mec = obj as MapElementCollection;
            return mec != null && mec == this;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ ID;
        }

        /// <summary>
        /// 获取 列名映射词典
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetColumnMapping()
        {
            if (Elements == null) return null;

            if (Elements.Count == 0) return new Dictionary<string, string>();

            Dictionary<string, string> dic = new Dictionary<string, string>(Elements.Count);

            foreach (var ele in Elements)
            {
                if (dic.ContainsKey(ele.Source.Name) == false)
                {
                    dic.Add(ele.Source.Name, ele.Destination.Name);
                }
            }

            return dic;
        }
    }

    /// <summary>
    /// 映射组，映射的根，包含多个映射集合，而每个映射集合又包含多个映射关系
    /// </summary>
    [XmlRoot]
    [Serializable]
    public class MappingGroup
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlArrayItem("Map")]
        public List<MapElementCollection> Maps { get; set; }

        /// <summary>
        /// 在错误发生是触发事件
        /// </summary>
        public event Action<string> ErrorHappened;

        private void Error(string error)
        {
            if (ErrorHappened != null) ErrorHappened(error);
        }

        /// <summary>
        ///  增加某个映射集,按照ID进行判断，如果存在，则更像
        /// </summary>
        /// <param name="map"></param>
        public void Add(MapElementCollection map)
        {
            Update(map);
        }

        /// <summary>
        /// 修改某个映射集,按照ID进行修改，如果不存在则增加
        /// </summary>
        /// <param name="map">要修改的映射集</param>
        public void Update(MapElementCollection map)
        {
            if (map == null) return;

            if (Maps == null) Maps = new List<MapElementCollection>();
            var ms = Maps;

            if (IsExistName(map.ID, map.Name))
            {
                Error(string.Format("名称已经存在，名称不能重复:" + map.Name));
                return;
            }

            if (map.ID == -1)
            {
                map.ID = GeneratorNewID();
                ms.Add(map);
            }
            else
            {
                int index = ms.FindIndex(m => { return m.ID == map.ID; });
                if (index >= 0)
                {
                    ms[index] = map;
                    Maps = ms;
                }
            }
        }

        /// <summary>
        /// 按名称移除一个映射集合
        /// </summary>
        /// <param name="mapCollectionName"></param>
        public int Remove(string mapCollectionName)
        {
            if (mapCollectionName == null) return 0;
            if (Maps == null || Maps.Count == 0) return 0;

            int index = Maps.FindIndex(m => { return m.Name == mapCollectionName; });

            if (index >= 0)
            {
                Maps.RemoveAt(index);
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 根据ID移除映射集合
        /// </summary>
        /// <param name="id"></param>
        public int Remove(int id)
        {
            if (Maps == null || Maps.Count == 0) return 0;

            int index = Maps.FindIndex(m => { return m.ID == m.ID; });
            if (index >= 0)
            {
                Maps.RemoveAt(index);
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 按名称移除一个映射集合
        /// </summary>
        /// <param name="map"></param>
        public int Remove(MapElementCollection map)
        {
            if (map == null) return 0;
            return Remove(map.ID);
        }

        /// <summary>
        /// 判断名称是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            if (Maps == null || Maps.Count == 0) return false;

            return Maps.FindIndex(m => { return m.Name == name; }) >= 0;
        }

        /// <summary>
        /// 判断名称是否已存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(int id, string name)
        {
            if (Maps == null || Maps.Count == 0) return false;

            return Maps.FindIndex(m => { return m.Name == name && m.ID != id; }) >= 0;
        }

        /// <summary>
        /// 根据名称查找
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MapElementCollection FindCollection(string name)
        {
            if (Maps == null) return null;
            return Maps.Find(m => { return m.Name == name; });
        }

        /// <summary>
        /// 获取映射的所有名称、集合个数词典
        /// </summary>
        [XmlIgnore]
        public Dictionary<string, int> MapNameAndCount
        {
            get
            {
                var maps = Maps;
                if (maps == null) return null;
                if (maps.Count == 0) return new Dictionary<string, int>();

                Dictionary<string, int> dic = new Dictionary<string, int>(maps.Count);
                maps.ForEach(m =>
                {
                    if (m != null && m.Name != null)
                    {
                        dic.Add(m.Name, m.Elements == null ? 0 : m.Elements.Count);
                    }
                });
                return dic;
            }
        }

        /// <summary>
        /// 获取映射名称集
        /// </summary>
        [XmlIgnore]
        public List<int> MapIDs
        {
            get
            {
                if (Maps == null) return null;
                if (Maps.Count == 0) return new List<int>();
                return Maps.Select<MapElementCollection, int>(m => { return m.ID; }).ToList();
            }
        }

        /// <summary>
        /// 生成一个新的ID
        /// </summary>
        /// <returns></returns>
        private int GeneratorNewID()
        {
            var mapIds = MapIDs;
            if (mapIds == null || mapIds.Count == 0) return 0;

            mapIds.Sort();
            for (int i = 0; i < mapIds.Count; i++)
            {
                if (i < mapIds[i]) return i;
            }
            return mapIds.Max() + 1;
        }
    }
}
