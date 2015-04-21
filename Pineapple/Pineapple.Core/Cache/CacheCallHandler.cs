using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Pineapple.Core.Cache
{
    public class CacheCallHandler : ICallHandler
    {
        [Dependency]
        public CacheProxy CacheProxy { private get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var cacheAttr = GetAttribute(input);
            if (cacheAttr == null) return getNext()(input, getNext);
            string cacheKey = GetCacheKey(cacheAttr, input);

            ICache cacheHandler = CacheProxy.GetCacheHandler(cacheAttr.CacheMode);

            switch (cacheAttr.CacheType)
            {
                case CacheType.Fetch:
                    if (cacheHandler.Contain(cacheAttr.Group, cacheKey))
                    {
                        return input.CreateMethodReturn(cacheHandler.Get(cacheAttr.Group, cacheKey));
                    }
                    else
                    {
                        var r = getNext()(input, getNext);
                        cacheHandler.Add(cacheAttr.Group, cacheKey, r.ReturnValue);
                        return r;
                    }
                case CacheType.Clear:
                    cacheHandler.Remove(cacheAttr.Group, cacheKey);
                    return getNext()(input, getNext);
            }
            return getNext()(input, getNext);
        }

        public int Order { get; set; }

        private CacheAttribute GetAttribute(IMethodInvocation input)
        {
            var attr = Attribute.GetCustomAttributes(input.MethodBase, typeof(CacheAttribute));
            if (attr.Length == 0) return null;
            return attr[0] as CacheAttribute;
        }

        private string GetCacheKey(CacheAttribute cacheAttribute, IMethodInvocation input)
        {
            if (input.Inputs == null || input.Inputs.Count == 0) return cacheAttribute.CacheKey;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", cacheAttribute.CacheKey);

            var parameters = input.MethodBase.GetParameters();
            int index = -1;
            foreach (ParameterInfo parameterInfo in parameters)
            {
                index++;

                var keyAttrs = Attribute.GetCustomAttribute(parameterInfo, typeof(CacheKeyAttribute));
                if (keyAttrs == null) continue;

                if (parameterInfo.ParameterType.IsPrimitive || parameterInfo.ParameterType == typeof(Type))
                {
                    sb.AppendFormat(";{0}={1}", parameterInfo.Name, input.Arguments[index]);
                }
                else
                {
                    PropertyInfo[] propertyInfos =
                        parameterInfo.ParameterType.GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                                          BindingFlags.GetProperty);

                    if (propertyInfos.Length == 0)
                    {
                        sb.AppendFormat(";{0}={1}", parameterInfo.Name, input.Arguments[index]);
                    }
                    else if (propertyInfos.Length > 0)
                    {
                        foreach (PropertyInfo property in propertyInfos)
                        {
                            if (property.GetCustomAttributes(typeof(CacheKeyAttribute), false).Length > 0)
                            {
                                sb.AppendFormat(";{0}={1}", property.Name, property.GetValue(input.Arguments[index], null));
                            }
                        }
                    }
                }
            }
            return sb.ToString();
        }

        /*
         * 
        public PropertyInfo[] GetCacheKey(ParameterInfo parameterInfo)
        {
            string key = parameterInfo.ParameterType.Module.Name + parameterInfo.MetadataToken;

            if (!cacheKeys.ContainsKey(key))
                return null;

            return cacheKeys[key];
        }
         * 
         ParameterInfo parameterInfo = inputs.GetParameterInfo(i);
                PropertyInfo[] propertyInfos = cacheMeta.GetCacheKey(parameterInfo);
                if (propertyInfos != null)
                {
                    if (propertyInfos.Length == 0)
                    {
                        cacheKey.Append(";");
                        cacheKey.Append(parameterInfo.Name).Append("=").Append(inputs[i]);
                    }
                    else
                    {
                        foreach (PropertyInfo propertyInfo in propertyInfos)
                        {
                                cacheKey.Append(";");
                            cacheKey.Append(propertyInfo.Name).Append("=").Append(
                                propertyInfo.GetValue(inputs[i], null));
                        }
                    }
                }
         */
    }
}
