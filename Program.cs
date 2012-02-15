using System;
using System.Diagnostics;

namespace RandomWalker
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch timer = Stopwatch.StartNew();

            new Crystal(new CrystalSettings
            {
                InitialPoint = new Point(150, 150),
                NumberOfWalkers = 10000,
                SpaceHeight = 300,
                SpaceWidth = 300,
                OutputFileName = "fr.png"
            }).Run();

            timer.Stop();
            Console.WriteLine(timer.Elapsed.TotalSeconds);
        }
    }
}
