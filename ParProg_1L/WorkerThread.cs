using System;
using System.Threading;

namespace ParProg_1L // додайте правильний простір імен
{
    public class WorkerThread
    {
        private readonly int id;
        private readonly double step;
        private bool running = true;

        public WorkerThread(int id, double step)
        {
            this.id = id;
            this.step = step;
        }

        public void Stop()
        {
            running = false;
        }

        public void Run()
        {
            lock (Program.START_LOCK)
            {
                while (!Program.started)
                {
                    Monitor.Wait(Program.START_LOCK);
                }
            }

            double sum = 0.0;
            int count = 0;
            double current = 0.0;

            while (running)
            {
                sum += current;
                current += step;
                count++;
            }

            Console.WriteLine($"[Thread {id}] Finished. Sum: {sum:F2}, Elements: {count}");
        }
    }
}
