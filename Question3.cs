using System;
using System.IO;
using System.Threading.Tasks;

namespace Tutorial04_SystemProgramming
{
    public static class Question3
    {
        private static readonly object _fileLock = new object();

        public static void Run()
        {
            Console.WriteLine("--- QUESTION 3: THREAD-SAFE FILE ACCESS ---");
            
            if (File.Exists(SharedResources.LogPath)) File.Delete(SharedResources.LogPath);

            Parallel.For(0, 20, i =>
            {
                LogSafe($"Log entry from Task {Task.CurrentId} at {DateTime.Now:HH:mm:ss.fff}");
            });

            Console.WriteLine($"Đã ghi xong. Kiểm tra file '{SharedResources.LogPath}' để xem kết quả.");
            Console.WriteLine("\n[Discussion]:");
            Console.WriteLine("- Nếu không có 'lock', chương trình sẽ crash với lỗi 'IOException: The process cannot access the file because it is being used by another process'.");
        }

        private static void LogSafe(string message)
        {
            lock (_fileLock)
            {
                File.AppendAllText(SharedResources.LogPath, message + Environment.NewLine);
            }
        }
    }
}