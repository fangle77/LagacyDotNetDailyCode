using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class DynamicTest : System.Dynamic.DynamicObject
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
            if (binder.Name == "set" && binder.CallInfo.ArgumentCount == 2)
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

        public ConcurrentDictionary<string, object> AllMembers
        {
            get
            {
                return objMapping;
            }
        }
    }

    class DynamicTestTest
    {
        public static void Test()
        {
            dynamic t = new DynamicTest();
            t.set("a", "galrj");
            t.set("B", 123);
            Console.WriteLine(t.a);
            Console.WriteLine(t.b);
            Console.WriteLine(t.c);

            foreach (var s in t.AllMembers)
            {
                Console.WriteLine(s.Key + "," + s.Value);
            }
        }
    }
}