using System;
using System.Collections.Generic;
using System.Linq;

namespace SplitNumber
{
    class Program
    {
        static int count = 0;
        static void Main(string[] args)
        {
            while (true)
            {
                count = 0;
                if (int.TryParse(Console.ReadLine(), out var i))
                {
                    Console.WriteLine(f(i));
                }
                Console.WriteLine($"Call: {count}");
                Console.WriteLine();
            }
        }

        static Dictionary<int, long> cache = new Dictionary<int, long>();
        static long f(int a)
        {
            count++;
            if (a == 1)
            {
                return 1;
            }
            if (cache.ContainsKey(a))
            {
                return cache[a];
            }
            else
            {
                var l = new List<long>();
                var length = a - 1;
                for (int i = 2; i < length; i++)
                {
                    l.Add(i * f(a - i));
                }
                l.Add(a);
                var ret = l.Max();
                cache[a] = ret;
                return ret;
            }
        }
    }
}
