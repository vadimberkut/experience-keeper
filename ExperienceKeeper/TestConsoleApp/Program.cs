using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUlid;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var a = Enumerable.Range(0, 10).Select(x => Ulid.NewUlid()).ToList();
            var b = a.Take(10).ToList();
            b = b.OrderBy(x => x).ToList();

            var c1 = Ulid.NewUlid();
            Thread.Sleep(1000);
            var c2 = Ulid.NewUlid();
            var default_ = Ulid.Empty;
            //var res0 = c1 < c2;
            var res = String.Compare(c1.ToString(), c2.ToString());

            // Collision / Speed test
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int loops = 1000000;
            Ulid[] ulids = new Ulid[loops];
            for (int i = 0; i < loops; i++)
            {
                ulids[i] = Ulid.NewUlid();
            }
            sw.Stop();
            var ms = sw.ElapsedMilliseconds;
            bool allUnique = ulids.Length == ulids.Distinct().Count();
        }
    }
}
