using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tutorial04_SystemProgramming
{
    public static class Question1
    {
        private static int _unsafeCounter = 0;
        private static int _lockCounter = 0;
        private static int _interlockedCounter = 0;
        private static readonly object _lockObj = new object();

        public static void Run()
        {
            Console.WriteLine("--- QUESTION 1: RACE CONDITIONS ---");
            int totalTasks = 10;
            int incrementsPerTask = 1000;
            int expected = totalTasks * incrementsPerTask;

            Parallel.For(0, totalTasks, i =>
            {
                for (int j = 0; j < incrementsPerTask; j++)
                {
                    _unsafeCounter++; 

                    lock (_lockObj)
                    {
                        _lockCounter++;
                    }

                    Interlocked.Increment(ref _interlockedCounter);
                }
            });

            Console.WriteLine($"Expected:    {expected}");
            Console.WriteLine($"Unsafe:      {_unsafeCounter} \t(Sai số do Race Condition)");
            Console.WriteLine($"Lock:        {_lockCounter} \t(Chính xác)");
            Console.WriteLine($"Interlocked: {_interlockedCounter} \t(Chính xác & Hiệu năng cao)");
            
            Console.WriteLine("\n[Discussion]:");
            Console.WriteLine("- Race Condition xảy ra vì thao tác '++' không phải là atomic (gồm đọc-sửa-ghi).");
            Console.WriteLine("- Interlocked nhanh hơn Lock vì nó thao tác trực tiếp mức CPU, không cần cơ chế khóa luồng phức tạp.");
        }
    }
}