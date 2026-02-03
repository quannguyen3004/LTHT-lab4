using System;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace Tutorial04_SystemProgramming
{
    public static class Question4
    {
        public static void Run()
        {
            Console.WriteLine("--- QUESTION 4: FILE MONITORING (FileSystemWatcher) ---");
            SharedResources.PrepareDirectories();

            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = SharedResources.WatchPath;
                watcher.Filter = "*.txt"; 
                watcher.NotifyFilter = NotifyFilters.FileName; 

                watcher.Created += OnFileCreated;
                watcher.EnableRaisingEvents = true; 

                Console.WriteLine($"Đang theo dõi folder: {SharedResources.WatchPath}");
                Console.WriteLine("Hãy thử tạo một file .txt vào folder đó.");
                Console.WriteLine("Nhấn [Enter] để kết thúc theo dõi và qua câu tiếp theo...");
                
                Task.Run(async () => {
                    await Task.Delay(1000);
                    File.WriteAllText(Path.Combine(SharedResources.WatchPath, "test_auto.txt"), "Hello World Auto");
                });

                Console.ReadLine();
            }
        }

        private static void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"-> Phát hiện file mới: {e.Name}");
            
            ThreadPool.QueueUserWorkItem(state =>
            {
                ProcessFileSafe(e.FullPath);
            });
        }

        private static void ProcessFileSafe(string filePath)
        {
            int retries = 3;
            while (retries > 0)
            {
                try
                {
                    string compressedPath = filePath + ".gz";
                    
                    using (FileStream sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    using (FileStream targetStream = File.Create(compressedPath))
                    using (GZipStream compressStream = new GZipStream(targetStream, CompressionLevel.Optimal))
                    {
                        sourceStream.CopyTo(compressStream);
                    }
                    
                    Console.WriteLine($"   [Success] Đã nén: {Path.GetFileName(compressedPath)}");
                    break; 
                }
                catch (IOException)
                {
                    Thread.Sleep(500); 
                    retries--;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"   [Error] {ex.Message}");
                    break;
                }
            }
        }
    }
}