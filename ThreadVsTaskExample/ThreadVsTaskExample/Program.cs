using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadVsTaskExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"This program is running on Thread {Thread.CurrentThread.ManagedThreadId}");
            
            LogID(2);

            Thread t = new Thread(()=>
            {
                // I get run as soon as the thread is created and start is called
                GetThreadID(3);
            });

            t.Start();

            ThreadPool.QueueUserWorkItem(Callback, 4);

            Console.ReadKey();
        }

        private static void Callback(object state)
        {
            // I get run when the thread enters the thread pool que and a thread is availabe for me
            GetThreadID((int)state);
        }
        

        static Task LogID(int id)
        {
            return Task.Factory.StartNew(() => {
                // I que a new thread from the thread pool and start it when it's available
                GetThreadID(id);
            },TaskCreationOptions.LongRunning);
        }

        static void GetThreadID(int id)
        {
            // I log some information about the current thread
            Console.Write($"This block of code is running on Thread {Thread.CurrentThread.ManagedThreadId} Identifier: {id} ");
            Console.Write($"Thread is thread pool thread {Thread.CurrentThread.IsThreadPoolThread}\n");
        }
    }
}
