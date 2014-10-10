using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Core.Cache
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class CacheKeyAttribute : Attribute
    {
    }
}