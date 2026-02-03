using System;
using System.Threading.Tasks;

namespace Tutorial04_SystemProgramming
{
    public static class Question2
    {
        public static async Task Run()
        {
            Console.WriteLine("--- QUESTION 2: TASK COORDINATION ---");

            Console.WriteLine("Bắt đầu 3 tasks...");

            var t1 = SimulateWork("Task 1", 1000);
            var t2 = SimulateWork("Task 2", 2000);
            var t3 = SimulateWork("Task 3", 500);

            await Task.WhenAll(t1, t2, t3);

            Console.WriteLine("--> TẤT CẢ TASKS ĐÃ HOÀN THÀNH! (Program continues)");
            
            Console.WriteLine("\n[Discussion]:");
            Console.WriteLine("- Task.WhenAll là cách dễ nhất cho async/await.");
            Console.WriteLine("- CountdownEvent hữu ích khi bạn không quản lý trực tiếp đối tượng Task (ví dụ thread thuần).");
        }

        private static async Task SimulateWork(string name, int delay)
        {
            await Task.Delay(delay);
            Console.WriteLine($"   Finished: {name}");
        }
    }
}