using System;
using System.Threading.Tasks;

namespace Tutorial04_SystemProgramming
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== TUTORIAL 04: THREAD SAFETY & FILE I/O - NGỌC ===");

            Question1.Run();
            Wait();

            await Question2.Run();
            Wait();

            Question3.Run();
            Wait();

            Question4.Run(); 

            Question5.Run();
            
            Console.WriteLine("\n=== HOÀN TẤT TUTORIAL 04 ===");
            Console.ReadLine();
        }

        static void Wait()
        {
            Console.WriteLine("\n--> Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}