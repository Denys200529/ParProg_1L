using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ParProg_1L 
{
    public class ControllerThread
    {
        private readonly List<WorkerThread> threads;
        private readonly List<int> stopTimes;

        public ControllerThread(List<WorkerThread> threads, List<int> stopTimes)
        {
            this.threads = threads;
            this.stopTimes = stopTimes;
        }

        public void Start()
        {
            new Thread(() =>
            {
                List<ThreadInfo> infos = new List<ThreadInfo>();

                for (int i = 0; i < threads.Count; i++)
                {
                    infos.Add(new ThreadInfo(i, stopTimes[i]));
                }

                infos = infos.OrderBy(info => info.StopTime).ToList();
                int previousTime = 0;

                foreach (var info in infos)
                {
                    int delay = info.StopTime - previousTime;

                    Thread.Sleep(delay);

                    threads[info.Index].Stop();
                    previousTime = info.StopTime;
                }
            }).Start();
        }

        private class ThreadInfo
        {
            public int Index { get; }
            public int StopTime { get; }

            public ThreadInfo(int index, int stopTime)
            {
                Index = index;
                StopTime = stopTime;
            }
        }
    }
}
