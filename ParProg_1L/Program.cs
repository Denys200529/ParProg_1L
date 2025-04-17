using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ParProg_1L 
{
    public class Program
    {
        internal static readonly object START_LOCK = new object(); 
        internal static bool started = false; 

        public const int THREAD_COUNT = 20;
        public const double STEP = 0.5;

        public static void Main(string[] args)
        {
            List<WorkerThread> workers = new List<WorkerThread>();
            List<int> delays = new List<int>();
            Random rand = new Random();

            for (int i = 0; i < THREAD_COUNT; i++)
            {
                int delay = rand.Next(3000, 10001); 
                delays.Add(delay);
                workers.Add(new WorkerThread(i + 1, STEP));
            }

            foreach (var worker in workers)
            {
                new Thread(worker.Run).Start();
            }

            lock (START_LOCK)
            {
                started = true;
                Monitor.PulseAll(START_LOCK);
            }

            new ControllerThread(workers, delays).Start();
        }
    }
}
