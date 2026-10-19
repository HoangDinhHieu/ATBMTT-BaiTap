using System.Security.Cryptography;
using System.Text;

namespace libSecurity
{
    /// <summary>
    /// Lớp xử lý mã hoá / giải mã AES-256-CBC
    /// </summary>
    public class MyAES
    {
        private byte[] aesKey = Array.Empty<byte>();  // Khoá AES 256-bit (32 bytes)
        private byte[] aesIV  = Array.Empty<byte>();  // Init Vector 128-bit (16 bytes)

        // ============================================================
        // 1. SINH KHOÁ
        // ============================================================
        /// <summary>Sinh khoá AES 256-bit ngẫu nhiên, trả về chuỗi Base64</summary>
        public string GenKey()
        {
            using Aes aes = Aes.Create();
            aes.KeySize = 256;
            aes.GenerateKey();
            aes.GenerateIV();
            aesKey = aes.Key;
            aesIV  = aes.IV;
            return Convert.ToBase64String(aesKey);
        }

        // ============================================================
        // 2. LƯU KHOÁ RA FILE
        // ============================================================
        /// <summary>Lưu khoá Base64 vào file .key</summary>
        public void SaveKey(string filePath, string keyBase64)
        {
            File.WriteAllText(filePath, keyBase64, Encoding.UTF8);
        }

        // ============================================================
        // 3. IMPORT KHOÁ TỪ FILE
        // ============================================================
        /// <summary>Đọc khoá từ file, trả về chuỗi Base64</summary>
        public string LoadKey(string filePath)
        {
            string keyBase64 = File.ReadAllText(filePath, Encoding.UTF8).Trim();
            // Xác nhận khoá hợp lệ (32 bytes = 256 bit)
            byte[] keyBytes = Convert.FromBase64String(keyBase64);
            if (keyBytes.Length != 32)
                throw new Exception($"Khoá không hợp lệ: {keyBytes.Length * 8} bit (cần 256 bit)");
            aesKey = keyBytes;
            return keyBase64;
        }

        // ============================================================
        // 4. MÃ HOÁ VĂN BẢN
        // ============================================================
        /// <summary>Mã hoá chuỗi văn bản, trả về Base64 (gồm IV + CipherText)</summary>
        public string MaHoaText(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] resultBytes = MaHoaBytes(plainBytes);
            return Convert.ToBase64String(resultBytes);
        }

        public byte[] MaHoaBytes(byte[] plainBytes)
        {
            using Aes aes = Aes.Create();
            aes.Key = aesKey;
            aes.GenerateIV();            // IV ngẫu nhiên mới mỗi lần mã hoá
            aesIV = aes.IV;

            ICryptoTransform encryptor = aes.CreateEncryptor();
            byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            // Ghép: [16 bytes IV] + [CipherText]
            byte[] result = new byte[aes.IV.Length + cipherBytes.Length];
            Buffer.BlockCopy(aes.IV,     0, result, 0,            aes.IV.Length);
            Buffer.BlockCopy(cipherBytes, 0, result, aes.IV.Length, cipherBytes.Length);
            return result;
        }

        // ============================================================
        // GIẢI MÃ VĂN BẢN
        // ============================================================
        /// <summary>Giải mã chuỗi Base64 (IV + CipherText) → văn bản gốc</summary>
        public string GiaiMaText(string fullBase64, string keyBase64)
        {
            byte[] fullBytes = Convert.FromBase64String(fullBase64);
            byte[] plainBytes = GiaiMaBytes(fullBytes, keyBase64);
            return Encoding.UTF8.GetString(plainBytes);
        }

        public byte[] GiaiMaBytes(byte[] fullBytes, string keyBase64)
        {
            byte[] key = Convert.FromBase64String(keyBase64);
            byte[] iv  = new byte[16];
            byte[] cipherBytes = new byte[fullBytes.Length - 16];
            Buffer.BlockCopy(fullBytes, 0,  iv,          0, 16);
            Buffer.BlockCopy(fullBytes, 16, cipherBytes, 0, cipherBytes.Length);

            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV  = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor();
            return decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
        }

        // ============================================================
        // 5. MÃ HOÁ FILE (STREAMING - hỗ trợ file hàng GB)
        // ============================================================
        /// <summary>
        /// Mã hoá file nguồn → file đích dùng CryptoStream (buffer 80KB).
        /// Định dạng file kết quả: [16 bytes IV] + [CipherText]
        /// Trả về thời gian thực hiện (ms)
        /// </summary>
        public long MaHoaFile(string inputPath, string outputPath, string keyBase64,
                               IProgress<long>? progress = null)
        {
            byte[] key = Convert.FromBase64String(keyBase64);
            var sw = System.Diagnostics.Stopwatch.StartNew();

            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.GenerateIV();

            using var fsIn  = new FileStream(inputPath,  FileMode.Open,   FileAccess.Read,  FileShare.Read,  81920, useAsync: false);
            using var fsOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, 81920, useAsync: false);

            // Ghi IV vào đầu file
            fsOut.Write(aes.IV, 0, aes.IV.Length);

            using var encryptor = aes.CreateEncryptor();
            using var cs = new CryptoStream(fsOut, encryptor, CryptoStreamMode.Write);

            byte[] buffer = new byte[81920]; // 80 KB buffer
            long totalRead = 0;
            long fileSize  = fsIn.Length;
            int read;
            while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
            {
                cs.Write(buffer, 0, read);
                totalRead += read;
                progress?.Report(totalRead * 100 / fileSize); // % tiến độ
            }
            cs.FlushFinalBlock();

            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        // ============================================================
        // GIẢI MÃ FILE (STREAMING)
        // ============================================================
        /// <summary>
        /// Giải mã file → file đích. Trả về thời gian thực hiện (ms)
        /// </summary>
        public long GiaiMaFile(string inputPath, string outputPath, string keyBase64,
                                IProgress<long>? progress = null)
        {
            byte[] key = Convert.FromBase64String(keyBase64);
            var sw = System.Diagnostics.Stopwatch.StartNew();

            using var fsIn = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, 81920, useAsync: false);

            // Đọc 16 bytes đầu làm IV
            byte[] iv = new byte[16];
            fsIn.Read(iv, 0, 16);

            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV  = iv;

            long fileSize  = fsIn.Length - 16;
            long totalRead = 0;

            using var decryptor = aes.CreateDecryptor();
            using var cs    = new CryptoStream(fsIn, decryptor, CryptoStreamMode.Read);
            using var fsOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, 81920, useAsync: false);

            byte[] buffer = new byte[81920];
            int read;
            while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
            {
                fsOut.Write(buffer, 0, read);
                totalRead += read;
                if (fileSize > 0)
                    progress?.Report(totalRead * 100 / fileSize);
            }

            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        // ============================================================
        // SO SÁNH BYTES: file nguồn vs file giải mã
        // ============================================================
        /// <summary>
        /// So sánh nội dung 2 file theo từng byte.
        /// Trả về (isEqual, totalBytes, diffCount)
        /// </summary>
        public (bool isEqual, long totalBytes, long diffCount) CompareFiles(string file1, string file2)
        {
            using var fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read, FileShare.Read, 81920);
            using var fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read, FileShare.Read, 81920);

            if (fs1.Length != fs2.Length)
                return (false, fs1.Length, Math.Abs(fs1.Length - fs2.Length));

            byte[] buf1 = new byte[81920];
            byte[] buf2 = new byte[81920];
            long totalBytes = fs1.Length;
            long diffCount  = 0;
            int r1, r2;

            while ((r1 = fs1.Read(buf1, 0, buf1.Length)) > 0)
            {
                r2 = fs2.Read(buf2, 0, buf2.Length);
                for (int i = 0; i < r1 && i < r2; i++)
                    if (buf1[i] != buf2[i]) diffCount++;
            }
            return (diffCount == 0, totalBytes, diffCount);
        }

        // ============================================================
        // TIỆN ÍCH: Sinh file giả lớn để test benchmark
        // ============================================================
        /// <summary>Tạo file dữ liệu ngẫu nhiên với kích thước cho trước (MB)</summary>
        public static void GenerateTestFile(string path, int sizeMB, IProgress<long>? progress = null)
        {
            long totalBytes = (long)sizeMB * 1024 * 1024;
            byte[] buffer   = new byte[81920];
            var rng = RandomNumberGenerator.Create();
            long written = 0;

            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 81920);
            while (written < totalBytes)
            {
                rng.GetBytes(buffer);
                long remaining = totalBytes - written;
                int toWrite    = (int)Math.Min(buffer.Length, remaining);
                fs.Write(buffer, 0, toWrite);
                written += toWrite;
                progress?.Report(written * 100 / totalBytes);
            }
        }
    }
}
