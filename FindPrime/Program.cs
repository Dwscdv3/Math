using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading;

namespace FindPrime
{
    static class Program
    {
        //static List<BigInteger> primes = new List<BigInteger>() { 2 };
        static BigInteger current = 3;
        static StreamWriter filePrime = null;
        static StreamWriter fileLast = null;

        static void Main(string[] args)
        {
            Console.WriteLine(
@"FindPrime
");

            //Console.WriteLine("Reading known primes...");
            //if (File.Exists("prime.txt"))
            //{
            //    primes = new List<BigInteger>();
            //    var lines = File.ReadLines("prime.txt");
            //    var count = 0;
            //    foreach (var line in lines)
            //    {
            //        count += 1;
            //        primes.Add(BigInteger.Parse(line));
            //        if ((count & 0b0011_1111_1111_1111_1111) == 0)
            //        {
            //            Console.Write($"\rCount: {count}");
            //        }
            //    }
            //    Console.WriteLine($"\rCount: {count}");
            //    current = primes[primes.Count - 1] + 2;
            //}
            //else
            //{
            //    Console.WriteLine("Not found.");
            //}
            //Console.WriteLine();

            Console.Write("Reading last calculated number... ");
            if (File.Exists("last.txt"))
            {
                try
                {
                    current = BigInteger.Parse(File.ReadAllText("last.txt"));
                    Console.WriteLine(current);
                }
                catch
                {
                    Console.WriteLine($"Error. Fallback to {current}");
                }
            }
            else
            {
                Console.WriteLine($"Not found. Fallback to {current}");
            }

            filePrime = new StreamWriter(
                new FileStream(
                    "prime.txt",
                    FileMode.Append,
                    FileAccess.Write,
                    FileShare.Read));
            fileLast = new StreamWriter(
                new FileStream(
                    "last.txt",
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.Read));

            Console.WriteLine();
            var timer = new Timer(obj =>
            {
                //Console.Write($"\r{primes[primes.Count - 1]}");
                Console.Write($"\r{current}");
            }, null, 500, 500);
            while (true)
            {
                fileLast.BaseStream.Position = 0;
                fileLast.Write(current);
                fileLast.Flush();
                if (IsPrime(current))
                {
                    //primes.Add(current);
                    filePrime.WriteLine(current);
                    filePrime.Flush();
                }
                current += 2;
            }
        }

        //static bool IsPrime(BigInteger bi)
        //{
        //    var index = 0;
        //    var p = primes[index];
        //    while (p * p <= bi && index < primes.Count)
        //    {
        //        p = primes[index];
        //        if (bi % p == 0)
        //        {
        //            return false;
        //        }
        //        index += 1;
        //    }
        //    return true;
        //}

        static bool IsPrime(BigInteger bi)
        {
            BigInteger p = 2;
            while (p * p <= bi)
            {
                if (bi % p == 0)
                {
                    return false;
                }
                p += 1;
            }
            return true;
        }
    }
}
