using System;
using System.IO;

namespace Tutorial04_SystemProgramming
{
    public static class SharedResources
    {
        public static string WatchPath = Path.Combine(Directory.GetCurrentDirectory(), "WatchFolder");
        
        public static string LogPath = "app_log.txt";

        public static void PrepareDirectories()
        {
            if (!Directory.Exists(WatchPath)) Directory.CreateDirectory(WatchPath);
        }
    }
}