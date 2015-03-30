using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Core.Dyanamic
{
    /// <summary>
    /// dynamic obj = new DynamicModel();
    /// obj.Set("Name1","value1");
    /// obj.Set("Name2", "Value2");
    /// string s = obj.Name1 + obj.Name2;
    /// </summary>
    public class DynamicModel : System.Dynamic.DynamicObject
    {
        private readonly ConcurrentDictionary<string, object> objMapping = new ConcurrentDictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        public override bool TryGetMember(System.Dynamic.GetMemberBinder binder, out object result)
        {
            string name = binder.Name;
            objMapping.TryGetValue(name, out result);
            if (result == null) result = string.Empty;
            return true;
        }

        public override bool TryInvokeMember(System.Dynamic.InvokeMemberBinder binder, object[] args, out object result)
        {
            if ("Set".Equals(binder.Name, StringComparison.OrdinalIgnoreCase) && binder.CallInfo.ArgumentCount == 2)
            {
                string name = string.Concat(args[0]);
                if (string.IsNullOrEmpty(name))
                {
                    result = null;
                    return false;
                }
                object value = args[1];
                objMapping.TryAdd(name, value);
                result = value;
                return true;
            }
            return base.TryInvokeMember(binder, args, out result);
        }
    }
}
