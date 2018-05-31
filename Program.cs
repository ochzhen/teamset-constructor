using System;
using System.Diagnostics;

namespace prakt2
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            
            Console.WriteLine("Start.");
            Console.WriteLine("Enter index number");
            int n = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter group number");
            int m = Int32.Parse(Console.ReadLine());
            
            stopwatch.Start();

            var sln = new Solution(n, m);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine("End.");
        }
    }
}
