using System;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Pineapple.Core.Cache
{
    public class CacheCallHandler : ICallHandler
    {
        LocalCache localCache = new LocalCache();

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var cacheAttr = GetAttribute(input);
            if (cacheAttr == null) return getNext()(input, getNext);

            if (localCache.Contain(cacheAttr.Group, cacheAttr.CacheKey))
            {
                return input.CreateMethodReturn(localCache.Get(cacheAttr.Group, cacheAttr.CacheKey));
            }
            else
            {
                var r = getNext()(input, getNext);
                localCache.Add(cacheAttr.Group, cacheAttr.CacheKey, r.ReturnValue);
                return r;
            }
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

            int count = input.Inputs.Count;
            for (int i = 0; i < count; i++)
            {
                var param = input.Inputs.GetParameterInfo(i);
                var keyAttrs = Attribute.GetCustomAttributes(param, typeof(CacheKeyAttribute));
                if (keyAttrs.Length == 0) continue;

                sb.AppendFormat("{0}:{1}",param.Name,param.MetadataToken);
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
