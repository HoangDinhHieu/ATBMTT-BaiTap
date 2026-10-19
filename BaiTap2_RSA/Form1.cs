using System.Security.Cryptography;
using System.Text;
using libSecurity;

namespace BaiTap2_RSA;

public partial class Form1 : Form
{
    private readonly MyRSA _rsaService = new();
    private bool _lastSignWasPss = true;

    public Form1()
    {
        InitializeComponent();
        cmbKeyBits.SelectedIndex = 1; // 2048 bits
        InitializeSignExplanation();
        UpdateHashComparison();
    }

    // ==========================================
    // 1. SINH CẶP KHÓA & VIEW PEM / ASN.1
    // ==========================================
    private void btnGenKeyPair_Click(object sender, EventArgs e)
    {
        int bits = 2048;
        if (cmbKeyBits.SelectedItem != null)
        {
            string s = cmbKeyBits.SelectedItem.ToString()!.Replace(" bits", "").Trim();
            int.TryParse(s, out bits);
        }

        try
        {
            var (pubPem, priPem) = _rsaService.GenKeyPair(bits);
            txtPublicPem.Text = pubPem;
            txtPrivatePem.Text = priPem;
            ParseAndDisplayPem(priPem);
            MessageBox.Show($"Đã sinh thành công cặp khóa RSA {bits} bits!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi sinh cặp khóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnExportPublicPem_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPublicPem.Text))
        {
            MessageBox.Show("Chưa có Public Key để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using SaveFileDialog sfd = new()
        {
            Title = "Lưu Public Key PEM",
            Filter = "PEM File (*.pem)|*.pem|Text File (*.txt)|*.txt|All Files (*.*)|*.*",
            FileName = "public_key.pem"
        };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            _rsaService.ExportPemToFile(sfd.FileName, txtPublicPem.Text);
            MessageBox.Show("Đã lưu Public Key thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnExportPrivatePem_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPrivatePem.Text))
        {
            MessageBox.Show("Chưa có Private Key để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using SaveFileDialog sfd = new()
        {
            Title = "Lưu Private Key PEM",
            Filter = "PEM File (*.pem)|*.pem|Text File (*.txt)|*.txt|All Files (*.*)|*.*",
            FileName = "private_key.pem"
        };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            _rsaService.ExportPemToFile(sfd.FileName, txtPrivatePem.Text);
            MessageBox.Show("Đã lưu Private Key thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnImportPem_Click(object sender, EventArgs e)
    {
        using OpenFileDialog ofd = new()
        {
            Title = "Chọn file PEM để nạp",
            Filter = "PEM File (*.pem)|*.pem|Text File (*.txt)|*.txt|All Files (*.*)|*.*"
        };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            try
            {
                string content = _rsaService.ImportPemFromFile(ofd.FileName);
                if (content.Contains("PRIVATE KEY"))
                {
                    txtPrivatePem.Text = content;
                    ParseAndDisplayPem(content);
                }
                else
                {
                    txtPublicPem.Text = content;
                    ParseAndDisplayPem(content);
                }
                MessageBox.Show("Đã nạp file PEM thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp file PEM: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void ParseAndDisplayPem(string pem)
    {
        if (string.IsNullOrWhiteSpace(pem)) return;

        try
        {
            var (isPrivate, parameters, summary) = _rsaService.ParsePemDetails(pem);
            var sb = new StringBuilder();
            sb.AppendLine(summary);
            sb.AppendLine("=== CHI TIẾT CÁC GIÁ TRỊ TRONG KHÓA ===");
            foreach (var kv in parameters)
            {
                string val = kv.Value.Length > 80 ? kv.Value[..80] + "..." : kv.Value;
                sb.AppendLine($"[{kv.Key}]: {val}");
            }

            byte[] derBytes = MyRSA.GetDerBytesFromPem(pem);
            sb.AppendLine();
            sb.AppendLine($"=== MÃ HEX CỦA ASN.1 DER (Tổng cộng: {derBytes.Length} bytes) ===");
            for (int i = 0; i < Math.Min(derBytes.Length, 128); i++)
            {
                sb.Append($"{derBytes[i]:X2} ");
                if ((i + 1) % 16 == 0) sb.AppendLine();
            }
            if (derBytes.Length > 128)
            {
                sb.AppendLine($"\n... (còn lại {derBytes.Length - 128} bytes)");
            }

            txtAsn1Info.Text = sb.ToString();
        }
        catch (Exception ex)
        {
            txtAsn1Info.Text = "Không thể phân tích ASN.1: " + ex.Message;
        }
    }

    // ==========================================
    // 2. MÃ HÓA & GIẢI MÃ
    // ==========================================
    private void btnUseAesKey32_Click(object sender, EventArgs e)
    {
        byte[] sampleAesKey = new byte[32]; // Khóa AES-256 32 bytes
        RandomNumberGenerator.Fill(sampleAesKey);
        txtDataToEncrypt.Text = Convert.ToBase64String(sampleAesKey);
    }

    private void btnEncryptRsa_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPublicPem.Text))
        {
            MessageBox.Show("Vui lòng sinh hoặc nạp Public Key trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            byte[] inputData = Encoding.UTF8.GetBytes(txtDataToEncrypt.Text);
            if (inputData.Length > 190)
            {
                MessageBox.Show($"Dữ liệu dài {inputData.Length} bytes! Đối với RSA 2048-bit đệm OAEP-SHA256, dữ liệu tối đa là 190 bytes (Khóa AES 32 bytes là phù hợp nhất).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string cipherBase64 = _rsaService.MaHoa(inputData, txtPublicPem.Text);
            txtEncryptedBase64.Text = cipherBase64;
            lblEncryptedBase64.Text = $"Kết quả Mã Hóa (Mảng byte dạng Base64 - {Convert.FromBase64String(cipherBase64).Length} bytes):";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi mã hóa RSA: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnDecryptRsa_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPrivatePem.Text))
        {
            MessageBox.Show("Vui lòng sinh hoặc nạp Private Key trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(txtEncryptedBase64.Text))
        {
            MessageBox.Show("Chưa có dữ liệu mã hóa để giải mã!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            byte[] decryptedBytes = _rsaService.GiaiMa(txtEncryptedBase64.Text, txtPrivatePem.Text);
            string decryptedStr = Encoding.UTF8.GetString(decryptedBytes);
            txtDecryptedData.Text = decryptedStr;

            string originalStr = txtDataToEncrypt.Text;
            if (decryptedStr == originalStr)
            {
                lblCompareResult.Text = "So sánh dữ liệu: KHỚP 100% VỚI DỮ LIỆU NGUỒN! ✅";
                lblCompareResult.ForeColor = Color.DarkGreen;
            }
            else
            {
                lblCompareResult.Text = "So sánh dữ liệu: DỮ LIỆU BỊ SAI LỆCH! ❌";
                lblCompareResult.ForeColor = Color.Red;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi giải mã RSA: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnSaveCipherToFile_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtEncryptedBase64.Text))
        {
            MessageBox.Show("Không có dữ liệu mã hóa để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using SaveFileDialog sfd = new()
        {
            Title = "Lưu Ciphertext Base64 ra File",
            Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*",
            FileName = "rsa_encrypted_data.txt"
        };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            File.WriteAllText(sfd.FileName, txtEncryptedBase64.Text);
            MessageBox.Show("Đã lưu dữ liệu mã hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnLoadCipherFromFile_Click(object sender, EventArgs e)
    {
        using OpenFileDialog ofd = new()
        {
            Title = "Chọn file chứa Ciphertext Base64",
            Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*"
        };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            txtEncryptedBase64.Text = File.ReadAllText(ofd.FileName).Trim();
        }
    }

    // ==========================================
    // 3. ĐÁNH GIÁ TỐC ĐỘ (BENCHMARK)
    // ==========================================
    private async void btnRunBenchmark_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPublicPem.Text) || string.IsNullOrWhiteSpace(txtPrivatePem.Text))
        {
            MessageBox.Show("Vui lòng sinh cặp khóa ở Tab 1 trước khi đo tốc độ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int loops = (int)numLoops.Value;
        string pub = txtPublicPem.Text;
        string pri = txtPrivatePem.Text;

        btnRunBenchmark.Enabled = false;
        txtBenchmarkResult.Text = $"Đang thực hiện benchmark {loops} lần lặp... Vui lòng đợi...";

        try
        {
            var res = await Task.Run(() => _rsaService.BenchmarkSpeed(pub, pri, loops));
            var sb = new StringBuilder();
            sb.AppendLine("==========================================================================");
            sb.AppendLine($"           KẾT QUẢ ĐÁNH GIÁ TỐC ĐỘ RSA ({loops} LẦN LẶP)");
            sb.AppendLine("==========================================================================");
            sb.AppendLine($"1. MÃ HÓA BẰNG PUBLIC KEY (e = 65537 - số mũ bé):");
            sb.AppendLine($"   - Tổng thời gian:     {res.encryptMs:N0} ms");
            sb.AppendLine($"   - Thời gian TB/phép:  {(double)res.encryptMs / loops:N4} ms/op");
            sb.AppendLine($"   - Tốc độ xử lý:       {res.encryptSpeedOps:N1} phép tính/giây (ops/sec)");
            sb.AppendLine();
            sb.AppendLine($"2. GIẢI MÃ BẰNG PRIVATE KEY (d - số mũ bí mật to đùng):");
            sb.AppendLine($"   - Tổng thời gian:     {res.decryptMs:N0} ms");
            sb.AppendLine($"   - Thời gian TB/phép:  {(double)res.decryptMs / loops:N4} ms/op");
            sb.AppendLine($"   - Tốc độ xử lý:       {res.decryptSpeedOps:N1} phép tính/giây (ops/sec)");
            sb.AppendLine();
            sb.AppendLine("--------------------------------------------------------------------------");
            double ratio = (double)res.decryptMs / Math.Max(res.encryptMs, 1);
            sb.AppendLine($"TỶ LỆ TỐC ĐỘ: Giải mã bằng Private Key LÂU HƠN gấp {ratio:N1} LẦN so với Mã hóa!");
            sb.AppendLine("--------------------------------------------------------------------------");
            sb.AppendLine("GIẢI THÍCH NGUYÊN LÝ TOÁN HỌC DÀNH CHO BÁO CÁO:");
            sb.AppendLine("• Phép mã hóa tính:    c = m^e mod n");
            sb.AppendLine("  Vì public exponent e chọn cố định là 65537 (0x10001, chỉ có 2 bit 1 ở dạng nhị phân),");
            sb.AppendLine("  thuật toán bình phương và nhân (Square-and-Multiply) chỉ mất 17 lần nhân modulo.");
            sb.AppendLine("  => Mã hóa diễn ra CỰC KỲ NHANH.");
            sb.AppendLine();
            sb.AppendLine("• Phép giải mã tính:   m = c^d mod n");
            sb.AppendLine("  Vì d là khóa bí mật có kích thước xấp xỉ bằng n (2048 bits ngẫu nhiên đầy đủ),");
            sb.AppendLine("  phép tính lũy thừa modulo đòi hỏi hàng nghìn phép nhân số nguyên cực lớn");
            sb.AppendLine("  (dù đã dùng định lý số dư Trung Hoa CRT để tối ưu).");
            sb.AppendLine("  => Giải mã CHẬM HƠN RẤT NHIỀU lần so với mã hóa.");
            sb.AppendLine("==========================================================================");

            txtBenchmarkResult.Text = sb.ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi benchmark: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnRunBenchmark.Enabled = true;
        }
    }

    // ==========================================
    // 4. SHA-256 AVALANCHE EFFECT
    // ==========================================
    private void UpdateHashComparison()
    {
        string in1 = txtHashInput1.Text;
        string in2 = txtHashInput2.Text;

        var (hex1, b1) = _rsaService.ComputeSha256(in1);
        var (hex2, b2) = _rsaService.ComputeSha256(in2);

        txtHashResult1.Text = hex1;
        txtHashResult2.Text = hex2;

        int diffBits = 0;
        for (int i = 0; i < b1.Length && i < b2.Length; i++)
        {
            byte xor = (byte)(b1[i] ^ b2[i]);
            diffBits += System.Numerics.BitOperations.PopCount(xor);
        }

        double percent = (diffBits / 256.0) * 100.0;
        var sb = new StringBuilder();
        sb.AppendLine("=== ĐÁNH GIÁ SỰ THAY ĐỔI CỦA KẾT QUẢ BĂM (HIỆU ỨNG TUYẾT LỞ - AVALANCHE EFFECT) ===");
        sb.AppendLine($"• Chuỗi 1: \"{in1}\"");
        sb.AppendLine($"• Chuỗi 2: \"{in2}\"");
        sb.AppendLine();
        sb.AppendLine($"• Tổng số bit của SHA-256: 256 bits (32 bytes)");
        sb.AppendLine($"• Số bit bị thay đổi:       {diffBits} / 256 bits");
        sb.AppendLine($"• Tỷ lệ thay đổi:           {percent:N2} %");
        sb.AppendLine();
        sb.AppendLine("=> KẾT LUẬN:");
        sb.AppendLine("Mặc dù dữ liệu đầu vào chỉ thay đổi rất nhỏ (chỉ 1 ký tự hoặc 1 bit),");
        sb.AppendLine("giá trị băm SHA-256 đầu ra bị thay đổi gần ~50% số bit (phân bố ngẫu nhiên hoàn toàn).");
        sb.AppendLine("Đây là tính chất cốt lõi của hàm băm mật mã: Không thể đoán trước và chống giả mạo.");
        txtAvalancheInfo.Text = sb.ToString();
    }

    // ==========================================
    // 5. CHỮ KÝ SỐ & ĐỀ XUẤT PHƯƠNG PHÁP RSA-PSS
    // ==========================================
    private void btnSignPss_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPrivatePem.Text))
        {
            MessageBox.Show("Cần có Private Key để ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            byte[] data = Encoding.UTF8.GetBytes(txtSignData.Text);
            string sig = _rsaService.SignDataPSS(data, txtPrivatePem.Text);
            txtSignatureResult.Text = sig;
            _lastSignWasPss = true;
            lblSignatureResult.Text = "Chữ ký RSA-PSS tạo được (Mỗi lần bấm chữ ký này đều HOÀN TOÀN MỚI):";
            lblVerifyStatus.Text = "Trạng thái: Vừa ký RSA-PSS";
            lblVerifyStatus.ForeColor = Color.DarkGreen;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi ký PSS: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnSignPkcs1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPrivatePem.Text))
        {
            MessageBox.Show("Cần có Private Key để ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            byte[] data = Encoding.UTF8.GetBytes(txtSignData.Text);
            string sig = _rsaService.SignDataPkcs1(data, txtPrivatePem.Text);
            txtSignatureResult.Text = sig;
            _lastSignWasPss = false;
            lblSignatureResult.Text = "Chữ ký PKCS#1 v1.5 tạo được (Bấm bao nhiêu lần chữ ký vẫn Y HỆT NHAU):";
            lblVerifyStatus.Text = "Trạng thái: Vừa ký PKCS#1";
            lblVerifyStatus.ForeColor = Color.DarkBlue;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi ký PKCS#1: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnVerifySig_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPublicPem.Text))
        {
            MessageBox.Show("Cần có Public Key để xác thực chữ ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(txtSignatureResult.Text))
        {
            MessageBox.Show("Chưa có chữ ký để kiểm tra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            byte[] data = Encoding.UTF8.GetBytes(txtSignData.Text);
            bool ok = _rsaService.VerifySignature(data, txtSignatureResult.Text, txtPublicPem.Text, _lastSignWasPss);
            if (ok)
            {
                lblVerifyStatus.Text = "Trạng thái: CHỮ KÝ HỢP LỆ! ✅";
                lblVerifyStatus.ForeColor = Color.DarkGreen;
                MessageBox.Show("CHỮ KÝ HỢP LỆ!\nDữ liệu toàn vẹn và đúng do chủ sở hữu Private Key này ký.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblVerifyStatus.Text = "Trạng thái: CHỮ KÝ KHÔNG HỢP LỆ! ❌";
                lblVerifyStatus.ForeColor = Color.Red;
                MessageBox.Show("CHỮ KÝ KHÔNG HỢP LỆ!\nDữ liệu đã bị sửa đổi hoặc chữ ký không khớp với Public Key.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xác thực: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void InitializeSignExplanation()
    {
        var sb = new StringBuilder();
        sb.AppendLine("==========================================================================");
        sb.AppendLine("  ĐỀ XUẤT PHƯƠNG PHÁP: CÙNG DỮ LIỆU + CÙNG PRIVATE KEY => CHỮ KÝ KHÁC NHAU");
        sb.AppendLine("==========================================================================");
        sb.AppendLine("1. VẤN ĐỀ TRUYỀN THỐNG (PKCS#1 v1.5 - Deterministic Signature):");
        sb.AppendLine("   - Khi ký số truyền thống: Signature = (Hash(Data))^d mod n");
        sb.AppendLine("   - Vì hàm băm Hash() và số mũ d đều cố định, nên cùng một thông điệp");
        sb.AppendLine("     sẽ LUÔN CHO RA CHỮ KÝ Y HỆT NHAU trong mọi lần ký.");
        sb.AppendLine();
        sb.AppendLine("2. PHƯƠNG PHÁP ĐỀ XUẤT TỐI ƯU: SỬ DỤNG CHUẨN RSA-PSS (RFC 8017 / PKCS#1 v2.1):");
        sb.AppendLine("   - RSA-PSS (Probabilistic Signature Scheme) bổ sung một giá trị Salt ngẫu nhiên");
        sb.AppendLine("     vào trước khi thực hiện mã hóa RSA.");
        sb.AppendLine("   - QUY TRÌNH:");
        sb.AppendLine("     + Bước 1: Hash = SHA-256(Message)");
        sb.AppendLine("     + Bước 2: Sinh chuỗi Salt ngẫu nhiên (ví dụ 32 bytes ngẫu nhiên mới mỗi lần)");
        sb.AppendLine("     + Bước 3: Đệm MGF1(Hash, Salt) => tạo khối dữ liệu EM");
        sb.AppendLine("     + Bước 4: Signature = EM^d mod n");
        sb.AppendLine("   => KẾT QUẢ: Mỗi lần người dùng bấm nút Ký, Salt mới được sinh ra");
        sb.AppendLine("      khiến cho CHỮ KÝ SỐ HOÀN TOÀN KHÁC NHAU, dù dữ liệu và khóa không đổi!");
        sb.AppendLine("   => Khi xác minh (Verify): Public Key vẫn giải mã và trích xuất đúng Salt + Hash ban đầu.");
        sb.AppendLine();
        sb.AppendLine("3. PHƯƠNG PHÁP BỔ TRỢ: THÊM TIMESTAMP / NONCE VÀO SIÊU DỮ LIỆU (Metadata):");
        sb.AppendLine("   - Đóng gói dữ liệu kèm thời gian ký: SignedData = { Data, Timestamp = Now, Nonce = Random }");
        sb.AppendLine("   - Do thời gian (Timestamp) thay đổi liên tục, chữ ký cũng sẽ tự động khác nhau.");
        sb.AppendLine("==========================================================================");
        txtSignExplanation.Text = sb.ToString();
    }
}
