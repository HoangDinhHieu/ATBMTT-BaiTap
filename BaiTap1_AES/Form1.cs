using System.Text;
using libSecurity;

namespace BaiTap1_AES;

public partial class Form1 : Form
{
    private readonly MyAES _aesService = new();

    public Form1()
    {
        InitializeComponent();
        Log("Khởi tạo ứng dụng thành công. Sẵn sàng thực hiện bài tập AES.");
    }

    private void Log(string message)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        txtLog.AppendText($"[{timestamp}] {message}{Environment.NewLine}");
    }

    // ==========================================
    // 1. QUẢN LÝ KHOÁ AES
    // ==========================================
    private void btnGenKey_Click(object sender, EventArgs e)
    {
        try
        {
            string keyBase64 = _aesService.GenKey();
            txtKey.Text = keyBase64;
            byte[] rawBytes = Convert.FromBase64String(keyBase64);
            lblKeyInfo.Text = $"Trạng thái khóa: Đã sinh ngẫu nhiên ({rawBytes.Length} Bytes = {rawBytes.Length * 8} Bits) OK";
            lblKeyInfo.ForeColor = Color.DarkGreen;
            Log($"Sinh khóa mới: {keyBase64} ({rawBytes.Length * 8} bits)");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi sinh khóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnExportKey_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtKey.Text))
        {
            MessageBox.Show("Vui lòng sinh hoặc nhập khóa trước khi lưu ra file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using SaveFileDialog sfd = new()
        {
            Filter = "Key File (*.key)|*.key|All Files (*.*)|*.*",
            Title = "Lưu Khóa AES ra File",
            FileName = "aes_secret.key"
        };

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            try
            {
                _aesService.SaveKey(sfd.FileName, txtKey.Text);
                Log($"Đã lưu khóa AES ra file: {sfd.FileName}");
                MessageBox.Show("Lưu khóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu khóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void btnImportKey_Click(object sender, EventArgs e)
    {
        using OpenFileDialog ofd = new()
        {
            Filter = "Key File (*.key)|*.key|All Files (*.*)|*.*",
            Title = "Nhập Khóa AES từ File"
        };

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            try
            {
                string key = _aesService.LoadKey(ofd.FileName);
                txtKey.Text = key;
                byte[] rawBytes = Convert.FromBase64String(key);
                lblKeyInfo.Text = $"Trạng thái khóa: Đã nạp từ file ({rawBytes.Length} Bytes = {rawBytes.Length * 8} Bits) OK";
                lblKeyInfo.ForeColor = Color.DarkGreen;
                Log($"Đã nạp khóa từ file: {ofd.FileName}");
                MessageBox.Show("Nạp khóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đọc file khóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // ==========================================
    // 2. MÃ HÓA & GIẢI MÃ VĂN BẢN (TEXT)
    // ==========================================
    private void btnEncryptText_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtKey.Text))
        {
            MessageBox.Show("Chưa có khóa AES! Hãy sinh hoặc nhập khóa trước.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            string plainText = txtPlainText.Text;
            string cipherText = _aesService.MaHoaText(plainText);
            txtEncryptedText.Text = cipherText;
            Log($"Mã hóa văn bản thành công. Chiều dài Base64: {cipherText.Length} ký tự");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi mã hóa văn bản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnDecryptText_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtKey.Text))
        {
            MessageBox.Show("Chưa có khóa AES! Hãy sinh hoặc nhập khóa trước.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(txtEncryptedText.Text))
        {
            MessageBox.Show("Không có chuỗi mã hóa để giải mã!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            string decrypted = _aesService.GiaiMaText(txtEncryptedText.Text, txtKey.Text);
            txtDecryptedText.Text = decrypted;
            Log($"Giải mã văn bản thành công: \"{decrypted}\"");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi giải mã: Khóa hoặc chuỗi mã hóa không hợp lệ!\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // ==========================================
    // 3. MÃ HÓA & GIẢI MÃ FILE
    // ==========================================
    private void btnBrowseSource_Click(object sender, EventArgs e)
    {
        using OpenFileDialog ofd = new()
        {
            Title = "Chọn File nguồn cần mã hóa",
            Filter = "All Files (*.*)|*.*"
        };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            txtSourceFilePath.Text = ofd.FileName;
            txtEncryptedFilePath.Text = ofd.FileName + ".aes";
            txtDecryptedFilePath.Text = Path.Combine(
                Path.GetDirectoryName(ofd.FileName) ?? "",
                "Decrypted_" + Path.GetFileName(ofd.FileName));
            FileInfo fi = new(ofd.FileName);
            Log($"Đã chọn file nguồn: {ofd.FileName} ({fi.Length:N0} bytes ~ {fi.Length / (1024.0 * 1024):N2} MB)");
        }
    }

    private void btnBrowseEncrypted_Click(object sender, EventArgs e)
    {
        using SaveFileDialog sfd = new()
        {
            Title = "Chọn nơi lưu file mã hóa",
            Filter = "AES Encrypted File (*.aes)|*.aes|All Files (*.*)|*.*"
        };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            txtEncryptedFilePath.Text = sfd.FileName;
        }
    }

    private void btnBrowseDecrypted_Click(object sender, EventArgs e)
    {
        using SaveFileDialog sfd = new()
        {
            Title = "Chọn nơi lưu file giải mã",
            Filter = "All Files (*.*)|*.*"
        };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            txtDecryptedFilePath.Text = sfd.FileName;
        }
    }

    private async void btnEncryptFile_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtKey.Text))
        {
            MessageBox.Show("Chưa có khóa AES! Hãy sinh hoặc nhập khóa trước.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (!File.Exists(txtSourceFilePath.Text))
        {
            MessageBox.Show("File nguồn không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string src = txtSourceFilePath.Text;
        string dest = txtEncryptedFilePath.Text;
        string key = txtKey.Text;
        FileInfo fi = new(src);
        double sizeMB = fi.Length / (1024.0 * 1024.0);

        SetUIWorking(true, "Đang mã hóa file...");
        progressBar.Value = 0;
        var progress = new Progress<long>(p => progressBar.Value = (int)Math.Clamp(p, 0, 100));

        try
        {
            long elapsedMs = await Task.Run(() => _aesService.MaHoaFile(src, dest, key, progress));
            double speed = elapsedMs > 0 ? (sizeMB / (elapsedMs / 1000.0)) : 0;

            lblStatus.Text = $"Trạng thái: Mã hóa xong trong {elapsedMs:N0} ms!";
            lblSpeed.Text = $"Tốc độ: {speed:N2} MB/s | Dung lượng: {sizeMB:N2} MB";
            Log($"=== KẾT QUẢ MÃ HÓA FILE ===");
            Log($"File nguồn: {src} ({sizeMB:N2} MB)");
            Log($"Thời gian mã hóa: {elapsedMs:N0} ms");
            Log($"Tốc độ trung bình: {speed:N2} MB/s");
            MessageBox.Show($"Mã hóa hoàn tất!\nThời gian: {elapsedMs:N0} ms\nTốc độ: {speed:N2} MB/s", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi mã hóa file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            lblStatus.Text = "Trạng thái: Lỗi mã hóa!";
        }
        finally
        {
            SetUIWorking(false, "Sẵn sàng");
        }
    }

    private async void btnDecryptFile_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtKey.Text))
        {
            MessageBox.Show("Chưa có khóa AES! Hãy sinh hoặc nhập khóa trước.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (!File.Exists(txtEncryptedFilePath.Text))
        {
            MessageBox.Show("File mã hóa (.aes) không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string src = txtEncryptedFilePath.Text;
        string dest = txtDecryptedFilePath.Text;
        string key = txtKey.Text;
        FileInfo fi = new(src);
        double sizeMB = fi.Length / (1024.0 * 1024.0);

        SetUIWorking(true, "Đang giải mã file...");
        progressBar.Value = 0;
        var progress = new Progress<long>(p => progressBar.Value = (int)Math.Clamp(p, 0, 100));

        try
        {
            long elapsedMs = await Task.Run(() => _aesService.GiaiMaFile(src, dest, key, progress));
            double speed = elapsedMs > 0 ? (sizeMB / (elapsedMs / 1000.0)) : 0;

            lblStatus.Text = $"Trạng thái: Giải mã xong trong {elapsedMs:N0} ms!";
            lblSpeed.Text = $"Tốc độ: {speed:N2} MB/s | Dung lượng: {sizeMB:N2} MB";
            Log($"=== KẾT QUẢ GIẢI MÃ FILE ===");
            Log($"File đích: {dest}");
            Log($"Thời gian giải mã: {elapsedMs:N0} ms");
            Log($"Tốc độ trung bình: {speed:N2} MB/s");
            MessageBox.Show($"Giải mã hoàn tất!\nThời gian: {elapsedMs:N0} ms\nTốc độ: {speed:N2} MB/s", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi giải mã file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            lblStatus.Text = "Trạng thái: Lỗi giải mã!";
        }
        finally
        {
            SetUIWorking(false, "Sẵn sàng");
        }
    }

    private async void btnCompareBytes_Click(object sender, EventArgs e)
    {
        string file1 = txtSourceFilePath.Text;
        string file2 = txtDecryptedFilePath.Text;

        if (!File.Exists(file1) || !File.Exists(file2))
        {
            MessageBox.Show("Cần có cả File Nguồn và File Giải Mã để so sánh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        SetUIWorking(true, "Đang so sánh từng byte...");
        progressBar.Value = 50;

        try
        {
            var result = await Task.Run(() => _aesService.CompareFiles(file1, file2));
            progressBar.Value = 100;

            Log("=== KẾT QUẢ SO SÁNH BYTES ===");
            Log($"File 1 (Gốc):     {file1}");
            Log($"File 2 (Giải mã): {file2}");
            Log($"Tổng số bytes so sánh: {result.totalBytes:N0} bytes");

            if (result.isEqual)
            {
                lblStatus.Text = "Trạng thái: KHỚP 100% TỪNG BYTE! ✅";
                Log("-> KẾT LUẬN: File giải mã KHỚP CHÍNH XÁC 100% từng byte với file nguồn ban đầu.");
                Log("-> Thuật toán AES-256-CBC đảm bảo tính toàn vẹn (Integrity) và bảo mật (Confidentiality) tuyệt đối.");
                MessageBox.Show($"SO SÁNH THÀNH CÔNG!\n\nTổng số byte: {result.totalBytes:N0}\nSố byte sai khác: 0 (Khớp 100%)\n\nDữ liệu sau giải mã hoàn toàn nguyên vẹn!", "Khớp 100%", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblStatus.Text = $"Trạng thái: SAI KHÁC {result.diffCount:N0} BYTES! ❌";
                Log($"-> KẾT LUẬN: File giải mã BỊ SAI LỆCH {result.diffCount:N0} bytes so với file gốc!");
                MessageBox.Show($"CẢNH BÁO: Dữ liệu không khớp!\nSai khác: {result.diffCount:N0} bytes", "Sai khác", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi so sánh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetUIWorking(false, "Sẵn sàng");
        }
    }

    // ==========================================
    // 4. TIỆN ÍCH TẠO FILE DUMMY BENCHMARK
    // ==========================================
    private async void CreateDummyFile(int sizeMB)
    {
        using SaveFileDialog sfd = new()
        {
            Title = $"Chọn nơi lưu file mẫu {sizeMB} MB",
            FileName = $"test_data_{sizeMB}MB.bin",
            Filter = "Binary File (*.bin)|*.bin|All Files (*.*)|*.*"
        };

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            string path = sfd.FileName;
            SetUIWorking(true, $"Đang tạo file mẫu {sizeMB} MB...");
            progressBar.Value = 0;
            var progress = new Progress<long>(p => progressBar.Value = (int)Math.Clamp(p, 0, 100));

            try
            {
                await Task.Run(() => MyAES.GenerateTestFile(path, sizeMB, progress));
                txtSourceFilePath.Text = path;
                txtEncryptedFilePath.Text = path + ".aes";
                txtDecryptedFilePath.Text = Path.Combine(
                    Path.GetDirectoryName(path) ?? "",
                    "Decrypted_" + Path.GetFileName(path));

                lblStatus.Text = $"Trạng thái: Đã tạo file mẫu {sizeMB} MB!";
                Log($"Đã sinh file giả lập thành công: {path} ({sizeMB} MB)");
                MessageBox.Show($"Đã tạo file {sizeMB} MB thành công và điền vào ô File nguồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tạo file mẫu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetUIWorking(false, "Sẵn sàng");
            }
        }
    }

    private void SetUIWorking(bool isWorking, string status)
    {
        lblStatus.Text = $"Trạng thái: {status}";
        btnEncryptFile.Enabled = !isWorking;
        btnDecryptFile.Enabled = !isWorking;
        btnCompareBytes.Enabled = !isWorking;
        btnGen100MB.Enabled = !isWorking;
        btnGen500MB.Enabled = !isWorking;
        btnGen1GB.Enabled = !isWorking;
    }
}
