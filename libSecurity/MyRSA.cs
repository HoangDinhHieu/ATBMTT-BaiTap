using System.Formats.Asn1;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace libSecurity
{
    /// <summary>
    /// Thư viện xử lý mã hóa RSA, xuất/nhập PEM, phân tích ASN.1 DER, SHA-256 và Chữ ký số
    /// </summary>
    public class MyRSA
    {
        // ============================================================
        // 1. SINH CẶP KHÓA RSA
        // ============================================================
        /// <summary>Sinh cặp khóa RSA với kích thước bit (mặc định 2048)</summary>
        public (string publicKeyPem, string privateKeyPem) GenKeyPair(int keySize = 2048)
        {
            using RSA rsa = RSA.Create(keySize);
            string pubKey = rsa.ExportRSAPublicKeyPem();
            string priKey = rsa.ExportRSAPrivateKeyPem();
            return (pubKey, priKey);
        }

        // ============================================================
        // 2. XUẤT CẶP KHÓA RA FILE PEM
        // ============================================================
        public void ExportPemToFile(string filePath, string pemContent)
        {
            File.WriteAllText(filePath, pemContent, Encoding.UTF8);
        }

        public string ImportPemFromFile(string filePath)
        {
            return File.ReadAllText(filePath, Encoding.UTF8).Trim();
        }

        // ============================================================
        // 3 & 4. PHÂN TÍCH BYTE ASN.1 DER CỦA PEM VÀ TRÍCH XUẤT CÁC THAM SỐ (n, e, d, p, q...)
        // ============================================================
        public static byte[] GetDerBytesFromPem(string pem)
        {
            var lines = pem.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var line in lines)
            {
                if (!line.StartsWith("-----"))
                {
                    sb.Append(line.Trim());
                }
            }
            return Convert.FromBase64String(sb.ToString());
        }

        /// <summary>
        /// Phân tích cấu trúc PKCS#1 DER thành thông tin chi tiết từng thành phần toán học
        /// </summary>
        public (bool isPrivate, Dictionary<string, string> parameters, string asn1Summary) ParsePemDetails(string pem)
        {
            byte[] der = GetDerBytesFromPem(pem);
            var result = new Dictionary<string, string>();
            var summary = new StringBuilder();

            bool isPrivate = pem.Contains("RSA PRIVATE KEY");

            try
            {
                var reader = new AsnReader(der, AsnEncodingRules.DER);
                var seq = reader.ReadSequence();

                if (isPrivate)
                {
                    // PKCS#1 RSAPrivateKey ::= SEQUENCE {
                    //   version           Version (0),
                    //   modulus           INTEGER (n),
                    //   publicExponent    INTEGER (e),
                    //   privateExponent   INTEGER (d),
                    //   prime1            INTEGER (p),
                    //   prime2            INTEGER (q),
                    //   exponent1         INTEGER (d mod (p-1)),
                    //   exponent2         INTEGER (d mod (q-1)),
                    //   coefficient       INTEGER (q^-1 mod p)
                    // }
                    BigInteger version = seq.ReadInteger();
                    BigInteger n = seq.ReadInteger();
                    BigInteger e = seq.ReadInteger();
                    BigInteger d = seq.ReadInteger();
                    BigInteger p = seq.ReadInteger();
                    BigInteger q = seq.ReadInteger();
                    BigInteger dp = seq.ReadInteger();
                    BigInteger dq = seq.ReadInteger();
                    BigInteger qinv = seq.ReadInteger();

                    result["Type"] = "PKCS#1 RSA Private Key";
                    result["Version"] = version.ToString();
                    result["Modulus (n)"] = n.ToString("X");
                    result["Public Exponent (e)"] = e.ToString() + $" (0x{e:X})";
                    result["Private Exponent (d)"] = d.ToString("X");
                    result["Prime 1 (p)"] = p.ToString("X");
                    result["Prime 2 (q)"] = q.ToString("X");
                    result["Exponent 1 (dp)"] = dp.ToString("X");
                    result["Exponent 2 (dq)"] = dq.ToString("X");
                    result["Coefficient (qinv)"] = qinv.ToString("X");

                    summary.AppendLine("=== CẤU TRÚC PKCS#1 RSA PRIVATE KEY (ASN.1 DER) ===");
                    summary.AppendLine($"• Kích thước Modulus (n): {n.ToByteArray().Length * 8} bits");
                    summary.AppendLine($"• e (Public Exponent): {e} (thường là 65537 - số Fermat F4)");
                    summary.AppendLine($"• d (Private Exponent): Kích thước lớn tương đương n ({d.ToByteArray().Length * 8} bits)");
                    summary.AppendLine($"• Hai số nguyên tố bí mật p, q tạo nên n: n = p * q");
                    summary.AppendLine($"• dp, dq, qinv: Các tham số hỗ trợ định lý phần dư Trung Hoa (CRT) tăng tốc giải mã.");
                }
                else
                {
                    // PKCS#1 RSAPublicKey ::= SEQUENCE {
                    //   modulus           INTEGER (n),
                    //   publicExponent    INTEGER (e)
                    // }
                    BigInteger n = seq.ReadInteger();
                    BigInteger e = seq.ReadInteger();

                    result["Type"] = "PKCS#1 RSA Public Key";
                    result["Modulus (n)"] = n.ToString("X");
                    result["Public Exponent (e)"] = e.ToString() + $" (0x{e:X})";

                    summary.AppendLine("=== CẤU TRÚC PKCS#1 RSA PUBLIC KEY (ASN.1 DER) ===");
                    summary.AppendLine($"• Modulus (n): {n.ToByteArray().Length * 8} bits (dùng công khai để mã hóa)");
                    summary.AppendLine($"• Public Exponent (e): {e} (thường nhỏ: 65537 để mã hóa cực nhanh)");
                }
            }
            catch (Exception ex)
            {
                summary.AppendLine($"Lưu ý khi phân tích ASN.1: {ex.Message}");
            }

            return (isPrivate, result, summary.ToString());
        }

        // ============================================================
        // 5. MÃ HÓA DỮ LIỆU BẰNG PUBLIC KEY (<= 190 BYTES CHO 2048-bit OAEP)
        // ============================================================
        /// <summary>
        /// Mã hóa mảng bytes (<= 190 bytes) dùng Public Key PEM (OAEP-SHA256).
        /// Trả về chuỗi mã hóa Base64
        /// </summary>
        public string MaHoa(byte[] data, string publicKeyPem)
        {
            if (data.Length > 190)
                throw new ArgumentException($"Dữ liệu dài {data.Length} bytes, vượt quá giới hạn 190 bytes đối với RSA-2048 OAEP-SHA256!");

            using RSA rsa = RSA.Create();
            rsa.ImportFromPem(publicKeyPem);
            byte[] cipherBytes = rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
            return Convert.ToBase64String(cipherBytes);
        }

        // ============================================================
        // 6. GIẢI MÃ DỮ LIỆU BẰNG PRIVATE KEY
        // ============================================================
        /// <summary>
        /// Giải mã chuỗi Base64 bằng Private Key PEM, trả về mảng bytes gốc
        /// </summary>
        public byte[] GiaiMa(string cipherBase64, string privateKeyPem)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherBase64);
            using RSA rsa = RSA.Create();
            rsa.ImportFromPem(privateKeyPem);
            return rsa.Decrypt(cipherBytes, RSAEncryptionPadding.OaepSHA256);
        }

        // ============================================================
        // 7. ĐÁNH GIÁ TỐC ĐỘ: PUBLIC KEY (e nhỏ) vs PRIVATE KEY (d lớn)
        // ============================================================
        public (long encryptMs, long decryptMs, double encryptSpeedOps, double decryptSpeedOps) BenchmarkSpeed(
            string publicKeyPem, string privateKeyPem, int iterations = 100)
        {
            byte[] sampleData = new byte[32]; // Ví dụ mã hóa khóa AES 256-bit (32 bytes)
            RandomNumberGenerator.Fill(sampleData);

            using RSA rsaPub = RSA.Create();
            rsaPub.ImportFromPem(publicKeyPem);

            using RSA rsaPri = RSA.Create();
            rsaPri.ImportFromPem(privateKeyPem);

            // Benchmark Mã hóa (dùng Public Key: e bé)
            var sw = System.Diagnostics.Stopwatch.StartNew();
            byte[] lastCipher = Array.Empty<byte>();
            for (int i = 0; i < iterations; i++)
            {
                lastCipher = rsaPub.Encrypt(sampleData, RSAEncryptionPadding.OaepSHA256);
            }
            sw.Stop();
            long encMs = Math.Max(sw.ElapsedMilliseconds, 1);

            // Benchmark Giải mã (dùng Private Key: d to)
            sw.Restart();
            for (int i = 0; i < iterations; i++)
            {
                rsaPri.Decrypt(lastCipher, RSAEncryptionPadding.OaepSHA256);
            }
            sw.Stop();
            long decMs = Math.Max(sw.ElapsedMilliseconds, 1);

            double encOps = (iterations * 1000.0) / encMs;
            double decOps = (iterations * 1000.0) / decMs;

            return (encMs, decMs, encOps, decOps);
        }

        // ============================================================
        // 8. TÍNH BĂM SHA-256 (Quan sát hiệu ứng tuyết lở - Avalanche Effect)
        // ============================================================
        public (string hashHex, byte[] hashBytes) ComputeSha256(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = SHA256.HashData(inputBytes);
            return (Convert.ToHexString(hash), hash);
        }

        // ============================================================
        // 9. CHỮ KÝ SỐ (RSA-PSS: Chữ ký khác nhau mỗi lần cùng dữ liệu + cùng key)
        // ============================================================
        /// <summary>
        /// Ký số với chuẩn RSA-PSS (tự động kết hợp Salt ngẫu nhiên mỗi lần ký)
        /// Cùng dữ liệu + Cùng private key -> Chữ ký mỗi lần bấm là hoàn toàn KHÁC NHAU
        /// </summary>
        public string SignDataPSS(byte[] data, string privateKeyPem)
        {
            using RSA rsa = RSA.Create();
            rsa.ImportFromPem(privateKeyPem);
            // Chuẩn PSS sử dụng Salt ngẫu nhiên theo tiêu chuẩn PKCS#1 v2.1
            byte[] signature = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
            return Convert.ToBase64String(signature);
        }

        /// <summary>
        /// Ký số truyền thống PKCS#1 v1.5 (xác định - determinism: cùng data -> cùng chữ ký)
        /// Dùng để so sánh sự khác biệt với PSS
        /// </summary>
        public string SignDataPkcs1(byte[] data, string privateKeyPem)
        {
            using RSA rsa = RSA.Create();
            rsa.ImportFromPem(privateKeyPem);
            byte[] signature = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            return Convert.ToBase64String(signature);
        }

        /// <summary>
        /// Xác thực chữ ký số (Verify)
        /// </summary>
        public bool VerifySignature(byte[] data, string signatureBase64, string publicKeyPem, bool isPss)
        {
            byte[] signature = Convert.FromBase64String(signatureBase64);
            using RSA rsa = RSA.Create();
            rsa.ImportFromPem(publicKeyPem);
            var padding = isPss ? RSASignaturePadding.Pss : RSASignaturePadding.Pkcs1;
            return rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, padding);
        }
    }
}
