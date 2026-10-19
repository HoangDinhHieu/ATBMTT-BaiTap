namespace BaiTap1_AES;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        tabControl1 = new TabControl();
        tabText = new TabPage();
        grpText = new GroupBox();
        btnDecryptText = new Button();
        btnEncryptText = new Button();
        txtDecryptedText = new TextBox();
        lblDecrypted = new Label();
        txtEncryptedText = new TextBox();
        lblEncrypted = new Label();
        txtPlainText = new TextBox();
        lblPlain = new Label();
        tabFile = new TabPage();
        grpFile = new GroupBox();
        lblStatus = new Label();
        lblSpeed = new Label();
        progressBar = new ProgressBar();
        btnCompareBytes = new Button();
        btnDecryptFile = new Button();
        btnEncryptFile = new Button();
        btnBrowseDecrypted = new Button();
        txtDecryptedFilePath = new TextBox();
        lblDecryptedFile = new Label();
        btnBrowseEncrypted = new Button();
        txtEncryptedFilePath = new TextBox();
        lblEncryptedFile = new Label();
        btnBrowseSource = new Button();
        txtSourceFilePath = new TextBox();
        lblSourceFile = new Label();
        grpDummy = new GroupBox();
        lblDummyNotice = new Label();
        btnGen100MB = new Button();
        btnGen500MB = new Button();
        btnGen1GB = new Button();
        grpKey = new GroupBox();
        lblKeyInfo = new Label();
        btnExportKey = new Button();
        btnImportKey = new Button();
        btnGenKey = new Button();
        txtKey = new TextBox();
        lblKey = new Label();
        txtLog = new TextBox();
        lblLog = new Label();

        tabControl1.SuspendLayout();
        tabText.SuspendLayout();
        grpText.SuspendLayout();
        tabFile.SuspendLayout();
        grpFile.SuspendLayout();
        grpDummy.SuspendLayout();
        grpKey.SuspendLayout();
        SuspendLayout();

        // 
        // grpKey
        // 
        grpKey.Controls.Add(lblKeyInfo);
        grpKey.Controls.Add(btnExportKey);
        grpKey.Controls.Add(btnImportKey);
        grpKey.Controls.Add(btnGenKey);
        grpKey.Controls.Add(txtKey);
        grpKey.Controls.Add(lblKey);
        grpKey.Location = new Point(12, 12);
        grpKey.Name = "grpKey";
        grpKey.Size = new Size(880, 85);
        grpKey.TabIndex = 0;
        grpKey.TabStop = false;
        grpKey.Text = "1. Quản lý Khoá AES-256 (32 Bytes = 256 Bits)";

        // lblKey
        lblKey.AutoSize = true;
        lblKey.Location = new Point(15, 28);
        lblKey.Name = "lblKey";
        lblKey.Size = new Size(92, 15);
        lblKey.Text = "Khóa AES (Base64):";

        // txtKey
        txtKey.Location = new Point(115, 25);
        txtKey.Name = "txtKey";
        txtKey.Size = new Size(450, 23);
        txtKey.TabIndex = 1;

        // btnGenKey
        btnGenKey.Location = new Point(575, 23);
        btnGenKey.Name = "btnGenKey";
        btnGenKey.Size = new Size(90, 26);
        btnGenKey.TabIndex = 2;
        btnGenKey.Text = "Sinh Khóa";
        btnGenKey.UseVisualStyleBackColor = true;
        btnGenKey.Click += btnGenKey_Click;

        // btnImportKey
        btnImportKey.Location = new Point(673, 23);
        btnImportKey.Name = "btnImportKey";
        btnImportKey.Size = new Size(95, 26);
        btnImportKey.TabIndex = 3;
        btnImportKey.Text = "Nhập từ File";
        btnImportKey.UseVisualStyleBackColor = true;
        btnImportKey.Click += btnImportKey_Click;

        // btnExportKey
        btnExportKey.Location = new Point(776, 23);
        btnExportKey.Name = "btnExportKey";
        btnExportKey.Size = new Size(90, 26);
        btnExportKey.TabIndex = 4;
        btnExportKey.Text = "Lưu ra File";
        btnExportKey.UseVisualStyleBackColor = true;
        btnExportKey.Click += btnExportKey_Click;

        // lblKeyInfo
        lblKeyInfo.AutoSize = true;
        lblKeyInfo.ForeColor = Color.DarkGreen;
        lblKeyInfo.Location = new Point(115, 55);
        lblKeyInfo.Name = "lblKeyInfo";
        lblKeyInfo.Size = new Size(130, 15);
        lblKeyInfo.Text = "Trạng thái khóa: Chưa có";

        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(tabText);
        tabControl1.Controls.Add(tabFile);
        tabControl1.Location = new Point(12, 105);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(880, 390);
        tabControl1.TabIndex = 1;

        // 
        // tabText
        // 
        tabText.Controls.Add(grpText);
        tabText.Location = new Point(4, 24);
        tabText.Name = "tabText";
        tabText.Padding = new Padding(3);
        tabText.Size = new Size(872, 362);
        tabText.TabIndex = 0;
        tabText.Text = "Mã hóa Văn bản (Text)";
        tabText.UseVisualStyleBackColor = true;

        // grpText
        grpText.Controls.Add(btnDecryptText);
        grpText.Controls.Add(btnEncryptText);
        grpText.Controls.Add(txtDecryptedText);
        grpText.Controls.Add(lblDecrypted);
        grpText.Controls.Add(txtEncryptedText);
        grpText.Controls.Add(lblEncrypted);
        grpText.Controls.Add(txtPlainText);
        grpText.Controls.Add(lblPlain);
        grpText.Location = new Point(10, 10);
        grpText.Name = "grpText";
        grpText.Size = new Size(850, 340);
        grpText.TabIndex = 0;
        grpText.TabStop = false;
        grpText.Text = "Thao tác Chuỗi Văn bản";

        lblPlain.AutoSize = true;
        lblPlain.Location = new Point(15, 25);
        lblPlain.Name = "lblPlain";
        lblPlain.Size = new Size(111, 15);
        lblPlain.Text = "Văn bản gốc (Plain):";

        txtPlainText.Location = new Point(15, 45);
        txtPlainText.Multiline = true;
        txtPlainText.ScrollBars = ScrollBars.Vertical;
        txtPlainText.Size = new Size(820, 60);
        txtPlainText.TabIndex = 0;
        txtPlainText.Text = "Khoa CNTT - An toàn và bảo mật thông tin 2026";

        btnEncryptText.Location = new Point(15, 112);
        btnEncryptText.Name = "btnEncryptText";
        btnEncryptText.Size = new Size(130, 28);
        btnEncryptText.TabIndex = 1;
        btnEncryptText.Text = "Mã hóa Văn bản ⬇";
        btnEncryptText.UseVisualStyleBackColor = true;
        btnEncryptText.Click += btnEncryptText_Click;

        btnDecryptText.Location = new Point(155, 112);
        btnDecryptText.Name = "btnDecryptText";
        btnDecryptText.Size = new Size(130, 28);
        btnDecryptText.TabIndex = 2;
        btnDecryptText.Text = "Giải mã Văn bản ⬇";
        btnDecryptText.UseVisualStyleBackColor = true;
        btnDecryptText.Click += btnDecryptText_Click;

        lblEncrypted.AutoSize = true;
        lblEncrypted.Location = new Point(15, 148);
        lblEncrypted.Name = "lblEncrypted";
        lblEncrypted.Size = new Size(195, 15);
        lblEncrypted.Text = "Kết quả Mã hóa (IV + Cipher Base64):";

        txtEncryptedText.Location = new Point(15, 168);
        txtEncryptedText.Multiline = true;
        txtEncryptedText.ScrollBars = ScrollBars.Vertical;
        txtEncryptedText.Size = new Size(820, 60);
        txtEncryptedText.TabIndex = 3;

        lblDecrypted.AutoSize = true;
        lblDecrypted.Location = new Point(15, 235);
        lblDecrypted.Name = "lblDecrypted";
        lblDecrypted.Size = new Size(100, 15);
        lblDecrypted.Text = "Kết quả Giải mã:";

        txtDecryptedText.Location = new Point(15, 255);
        txtDecryptedText.Multiline = true;
        txtDecryptedText.ScrollBars = ScrollBars.Vertical;
        txtDecryptedText.Size = new Size(820, 60);
        txtDecryptedText.TabIndex = 4;

        // 
        // tabFile
        // 
        tabFile.Controls.Add(grpDummy);
        tabFile.Controls.Add(grpFile);
        tabFile.Location = new Point(4, 24);
        tabFile.Name = "tabFile";
        tabFile.Padding = new Padding(3);
        tabFile.Size = new Size(872, 362);
        tabFile.TabIndex = 1;
        tabFile.Text = "Mã hóa File (Hỗ trợ file lớn GB)";
        tabFile.UseVisualStyleBackColor = true;

        // grpDummy
        grpDummy.Controls.Add(lblDummyNotice);
        grpDummy.Controls.Add(btnGen100MB);
        grpDummy.Controls.Add(btnGen500MB);
        grpDummy.Controls.Add(btnGen1GB);
        grpDummy.Location = new Point(10, 6);
        grpDummy.Name = "grpDummy";
        grpDummy.Size = new Size(850, 60);
        grpDummy.TabIndex = 0;
        grpDummy.TabStop = false;
        grpDummy.Text = "Sinh file giả lập để thử nghiệm kích thước lớn (Benchmark)";

        lblDummyNotice.AutoSize = true;
        lblDummyNotice.ForeColor = Color.DimGray;
        lblDummyNotice.Location = new Point(15, 26);
        lblDummyNotice.Name = "lblDummyNotice";
        lblDummyNotice.Size = new Size(150, 15);
        lblDummyNotice.Text = "Tạo nhanh file mẫu ngẫu nhiên:";

        btnGen100MB.Location = new Point(200, 22);
        btnGen100MB.Name = "btnGen100MB";
        btnGen100MB.Size = new Size(110, 26);
        btnGen100MB.TabIndex = 0;
        btnGen100MB.Text = "Tạo File 100 MB";
        btnGen100MB.UseVisualStyleBackColor = true;
        btnGen100MB.Click += (s, e) => CreateDummyFile(100);

        btnGen500MB.Location = new Point(325, 22);
        btnGen500MB.Name = "btnGen500MB";
        btnGen500MB.Size = new Size(110, 26);
        btnGen500MB.TabIndex = 1;
        btnGen500MB.Text = "Tạo File 500 MB";
        btnGen500MB.UseVisualStyleBackColor = true;
        btnGen500MB.Click += (s, e) => CreateDummyFile(500);

        btnGen1GB.Location = new Point(450, 22);
        btnGen1GB.Name = "btnGen1GB";
        btnGen1GB.Size = new Size(110, 26);
        btnGen1GB.TabIndex = 2;
        btnGen1GB.Text = "Tạo File 1 GB";
        btnGen1GB.UseVisualStyleBackColor = true;
        btnGen1GB.Click += (s, e) => CreateDummyFile(1024);

        // grpFile
        grpFile.Controls.Add(lblSpeed);
        grpFile.Controls.Add(lblStatus);
        grpFile.Controls.Add(progressBar);
        grpFile.Controls.Add(btnCompareBytes);
        grpFile.Controls.Add(btnDecryptFile);
        grpFile.Controls.Add(btnEncryptFile);
        grpFile.Controls.Add(btnBrowseDecrypted);
        grpFile.Controls.Add(txtDecryptedFilePath);
        grpFile.Controls.Add(lblDecryptedFile);
        grpFile.Controls.Add(btnBrowseEncrypted);
        grpFile.Controls.Add(txtEncryptedFilePath);
        grpFile.Controls.Add(lblEncryptedFile);
        grpFile.Controls.Add(btnBrowseSource);
        grpFile.Controls.Add(txtSourceFilePath);
        grpFile.Controls.Add(lblSourceFile);
        grpFile.Location = new Point(10, 72);
        grpFile.Name = "grpFile";
        grpFile.Size = new Size(850, 280);
        grpFile.TabIndex = 1;
        grpFile.TabStop = false;
        grpFile.Text = "Quy trình Xử lý File";

        lblSourceFile.AutoSize = true;
        lblSourceFile.Location = new Point(15, 25);
        lblSourceFile.Name = "lblSourceFile";
        lblSourceFile.Size = new Size(92, 15);
        lblSourceFile.Text = "1. File Nguồn:";

        txtSourceFilePath.Location = new Point(140, 22);
        txtSourceFilePath.Name = "txtSourceFilePath";
        txtSourceFilePath.Size = new Size(590, 23);
        txtSourceFilePath.TabIndex = 0;

        btnBrowseSource.Location = new Point(740, 21);
        btnBrowseSource.Name = "btnBrowseSource";
        btnBrowseSource.Size = new Size(95, 25);
        btnBrowseSource.TabIndex = 1;
        btnBrowseSource.Text = "Chọn File...";
        btnBrowseSource.UseVisualStyleBackColor = true;
        btnBrowseSource.Click += btnBrowseSource_Click;

        lblEncryptedFile.AutoSize = true;
        lblEncryptedFile.Location = new Point(15, 60);
        lblEncryptedFile.Name = "lblEncryptedFile";
        lblEncryptedFile.Size = new Size(111, 15);
        lblEncryptedFile.Text = "2. File Mã hóa (.aes):";

        txtEncryptedFilePath.Location = new Point(140, 57);
        txtEncryptedFilePath.Name = "txtEncryptedFilePath";
        txtEncryptedFilePath.Size = new Size(590, 23);
        txtEncryptedFilePath.TabIndex = 2;

        btnBrowseEncrypted.Location = new Point(740, 56);
        btnBrowseEncrypted.Name = "btnBrowseEncrypted";
        btnBrowseEncrypted.Size = new Size(95, 25);
        btnBrowseEncrypted.TabIndex = 3;
        btnBrowseEncrypted.Text = "Lưu File...";
        btnBrowseEncrypted.UseVisualStyleBackColor = true;
        btnBrowseEncrypted.Click += btnBrowseEncrypted_Click;

        lblDecryptedFile.AutoSize = true;
        lblDecryptedFile.Location = new Point(15, 95);
        lblDecryptedFile.Name = "lblDecryptedFile";
        lblDecryptedFile.Size = new Size(100, 15);
        lblDecryptedFile.Text = "3. File Giải mã:";

        txtDecryptedFilePath.Location = new Point(140, 92);
        txtDecryptedFilePath.Name = "txtDecryptedFilePath";
        txtDecryptedFilePath.Size = new Size(590, 23);
        txtDecryptedFilePath.TabIndex = 4;

        btnBrowseDecrypted.Location = new Point(740, 91);
        btnBrowseDecrypted.Name = "btnBrowseDecrypted";
        btnBrowseDecrypted.Size = new Size(95, 25);
        btnBrowseDecrypted.TabIndex = 5;
        btnBrowseDecrypted.Text = "Lưu File...";
        btnBrowseDecrypted.UseVisualStyleBackColor = true;
        btnBrowseDecrypted.Click += btnBrowseDecrypted_Click;

        btnEncryptFile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnEncryptFile.Location = new Point(140, 130);
        btnEncryptFile.Name = "btnEncryptFile";
        btnEncryptFile.Size = new Size(160, 32);
        btnEncryptFile.TabIndex = 6;
        btnEncryptFile.Text = "🔒 Bắt đầu Mã Hóa File";
        btnEncryptFile.UseVisualStyleBackColor = true;
        btnEncryptFile.Click += btnEncryptFile_Click;

        btnDecryptFile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnDecryptFile.Location = new Point(310, 130);
        btnDecryptFile.Name = "btnDecryptFile";
        btnDecryptFile.Size = new Size(160, 32);
        btnDecryptFile.TabIndex = 7;
        btnDecryptFile.Text = "🔓 Bắt đầu Giải Mã File";
        btnDecryptFile.UseVisualStyleBackColor = true;
        btnDecryptFile.Click += btnDecryptFile_Click;

        btnCompareBytes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnCompareBytes.ForeColor = Color.DarkBlue;
        btnCompareBytes.Location = new Point(480, 130);
        btnCompareBytes.Name = "btnCompareBytes";
        btnCompareBytes.Size = new Size(200, 32);
        btnCompareBytes.TabIndex = 8;
        btnCompareBytes.Text = "🔍 So Sánh Từng Byte (File 1 & 3)";
        btnCompareBytes.UseVisualStyleBackColor = true;
        btnCompareBytes.Click += btnCompareBytes_Click;

        progressBar.Location = new Point(140, 175);
        progressBar.Name = "progressBar";
        progressBar.Size = new Size(590, 20);
        progressBar.TabIndex = 9;

        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(140, 203);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(88, 15);
        lblStatus.Text = "Trạng thái: Sẵn sàng";

        lblSpeed.AutoSize = true;
        lblSpeed.ForeColor = Color.DarkCyan;
        lblSpeed.Location = new Point(140, 225);
        lblSpeed.Name = "lblSpeed";
        lblSpeed.Size = new Size(116, 15);
        lblSpeed.Text = "Tốc độ xử lý: - MB/s";

        // 
        // txtLog
        // 
        txtLog.BackColor = Color.FromArgb(24, 24, 24);
        txtLog.ForeColor = Color.FromArgb(0, 230, 118);
        txtLog.Font = new Font("Consolas", 9F);
        txtLog.Location = new Point(12, 520);
        txtLog.Multiline = true;
        txtLog.Name = "txtLog";
        txtLog.ReadOnly = true;
        txtLog.ScrollBars = ScrollBars.Vertical;
        txtLog.Size = new Size(880, 150);
        txtLog.TabIndex = 2;

        lblLog.AutoSize = true;
        lblLog.Location = new Point(12, 502);
        lblLog.Name = "lblLog";
        lblLog.Size = new Size(198, 15);
        lblLog.Text = "Nhật ký Xử lý & Kết luận Thống kê:";

        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(904, 681);
        Controls.Add(lblLog);
        Controls.Add(txtLog);
        Controls.Add(tabControl1);
        Controls.Add(grpKey);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Bài Tập 1 - Mã Hoá Đối Xứng AES-256 (K59KMT)";

        tabControl1.ResumeLayout(false);
        tabText.ResumeLayout(false);
        grpText.ResumeLayout(false);
        grpText.PerformLayout();
        tabFile.ResumeLayout(false);
        grpFile.ResumeLayout(false);
        grpFile.PerformLayout();
        grpDummy.ResumeLayout(false);
        grpDummy.PerformLayout();
        grpKey.ResumeLayout(false);
        grpKey.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private GroupBox grpKey;
    private Label lblKey;
    private TextBox txtKey;
    private Button btnGenKey;
    private Button btnImportKey;
    private Button btnExportKey;
    private Label lblKeyInfo;
    private TabControl tabControl1;
    private TabPage tabText;
    private TabPage tabFile;
    private GroupBox grpText;
    private TextBox txtPlainText;
    private Label lblPlain;
    private Button btnEncryptText;
    private Button btnDecryptText;
    private Label lblEncrypted;
    private TextBox txtEncryptedText;
    private Label lblDecrypted;
    private TextBox txtDecryptedText;
    private GroupBox grpFile;
    private Label lblSourceFile;
    private TextBox txtSourceFilePath;
    private Button btnBrowseSource;
    private Label lblEncryptedFile;
    private TextBox txtEncryptedFilePath;
    private Button btnBrowseEncrypted;
    private Label lblDecryptedFile;
    private TextBox txtDecryptedFilePath;
    private Button btnBrowseDecrypted;
    private Button btnEncryptFile;
    private Button btnDecryptFile;
    private Button btnCompareBytes;
    private ProgressBar progressBar;
    private Label lblStatus;
    private Label lblSpeed;
    private GroupBox grpDummy;
    private Label lblDummyNotice;
    private Button btnGen100MB;
    private Button btnGen500MB;
    private Button btnGen1GB;
    private TextBox txtLog;
    private Label lblLog;
}
