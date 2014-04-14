using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Algorithm
{
    class Class1
    {
        /*
         * 实现一个函数，对一个正整数n，算得到1需要的最少操作次数。
         * 操作规则为：如果n为偶数，将其除以2；如果n为奇数，可以加1或减1；一直处理下去。
         */
        public static int Function(int n)
        {
            int count = 0;
            while (n > 1)
            {
                if (n == 3) n--;
                else if (n % 0x2 == 0) n >>= 1;
                else n += n % 4 - 2;
                count++;
            }
            return count;
        }

        /*
         * 给定函数d(n)=n+n的各位之和，n为正整数，如d(78)=78+7+8=93。
         * 这样这个函数可以看成一个生成器，如93可以看成由78生成。
         * 定义数A：数A找不到一个数B可以由d(B)=A，即A不能由其他数生成。
         * 现在要写程序，找出1至10000里的所有符合数A定义的数。
         */
        public static int GenarateNum(int n)
        {
            int x = n;
            while (n > 0)
            {
                x += n % 10;
                n = n / 10;
            }
            return x;
        }
        public static IEnumerable<int> GeneateNums(int maxValue)
        {
            DateTime start = DateTime.Now;
            HashSet<int> list = new HashSet<int>();
            for (int i = 1; i <= maxValue; i++)
            {
                list.Add(i);
            }
            int n = maxValue;
            while (n-- > 0)
            {
                int t = GenarateNum(n);
                if (maxValue >= t) list.Remove(t);
            }
            DateTime end = DateTime.Now;
            int ms =(int) (end - start).TotalMilliseconds;
            list.Add(ms);
            return list;
        }
    }
}
