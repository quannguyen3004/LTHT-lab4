using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace Tutorial04_SystemProgramming
{
    public static class Question5
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF"); 
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("0123456789ABCDEF"); 

        public static void Run()
        {
            Console.WriteLine("--- QUESTION 5: ENCRYPTION & COMPRESSION ---");
            
            string originalText = "Đây là dữ liệu mật. Systems Programming 2026. " + new string('A', 1000); 
            string originalFile = "secret_data.txt";
            string finalFile = "secret_data.enc.gz";

            File.WriteAllText(originalFile, originalText);
            Console.WriteLine($"1. Tạo file gốc: {originalFile} ({new FileInfo(originalFile).Length} bytes)");

        
            
            EncryptThenCompress(originalFile, finalFile);
            
            Console.WriteLine($"2. Đã Mã hóa & Nén -> {finalFile} ({new FileInfo(finalFile).Length} bytes)");

            string restoredText = DecompressThenDecrypt(finalFile);
            
            Console.WriteLine("3. Khôi phục dữ liệu:");
            Console.WriteLine($"   Nội dung: {restoredText.Substring(0, 50)}..."); 
            
            Console.WriteLine("\n[Discussion]:");
            Console.WriteLine("- Câu hỏi: Tại sao Encrypt thường làm trước Compress? (Thực ra đây là sai lầm).");
            Console.WriteLine("- Thực tế: Dữ liệu đã mã hóa có độ ngẫu nhiên (entropy) rất cao -> Nén hầu như không giảm dung lượng.");
            Console.WriteLine("- Code trên làm theo yêu cầu đề bài, nhưng nếu bạn đổi thứ tự (Nén -> Mã hóa), file sẽ nhỏ hơn nhiều.");
        }

        private static void EncryptThenCompress(string inputFile, string outputFile)
        {
            using (FileStream fsOut = File.Create(outputFile))
            using (GZipStream gzip = new GZipStream(fsOut, CompressionLevel.Optimal)) 
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                using (CryptoStream cs = new CryptoStream(gzip, encryptor, CryptoStreamMode.Write)) 
                using (StreamWriter writer = new StreamWriter(cs)) 
                {
                    string content = File.ReadAllText(inputFile);
                    writer.Write(content);
                }
            }
        }

        private static string DecompressThenDecrypt(string inputFile)
        {
            using (FileStream fsIn = File.OpenRead(inputFile))
            using (GZipStream gzip = new GZipStream(fsIn, CompressionMode.Decompress))
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                using (CryptoStream cs = new CryptoStream(gzip, decryptor, CryptoStreamMode.Read))
                using (StreamReader reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}