using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4.Task3
{
    internal static class TestTime
    {
        public static double Run(IAlgorithms testTime)
        {
            double[] srTime = new double[5];
            for (int j = 0; j < 5; j++)
            {
                Stopwatch time = new Stopwatch();
                time.Start();
                testTime.Test();
                time.Stop();
                Math.Round(srTime[j] = time.Elapsed.TotalMilliseconds);
            }
            return AnamylCorrection(srTime);
        }
        private static double AnamylCorrection(double[] time)
        { 
            Array.Sort(time);
            return time[2];
        }
    }
}
