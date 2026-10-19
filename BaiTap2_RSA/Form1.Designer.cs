namespace BaiTap2_RSA;

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
        tabControlMain = new TabControl();
        tabKeyGenPem = new TabPage();
        tabEncryptDecrypt = new TabPage();
        tabBenchmark = new TabPage();
        tabHash = new TabPage();
        tabSignature = new TabPage();

        // --- Controls for Tab 1: Key & PEM ---
        grpKeyGen = new GroupBox();
        btnGenKeyPair = new Button();
        btnExportPublicPem = new Button();
        btnExportPrivatePem = new Button();
        btnImportPem = new Button();
        lblKeyBits = new Label();
        cmbKeyBits = new ComboBox();
        grpPemView = new GroupBox();
        lblPublicPem = new Label();
        txtPublicPem = new TextBox();
        lblPrivatePem = new Label();
        txtPrivatePem = new TextBox();
        grpAsn1 = new GroupBox();
        txtAsn1Info = new TextBox();

        // --- Controls for Tab 2: Encrypt & Decrypt ---
        grpEncrypt = new GroupBox();
        lblDataToEncrypt = new Label();
        txtDataToEncrypt = new TextBox();
        btnUseAesKey32 = new Button();
        btnEncryptRsa = new Button();
        lblEncryptedBase64 = new Label();
        txtEncryptedBase64 = new TextBox();
        btnSaveCipherToFile = new Button();
        btnLoadCipherFromFile = new Button();
        grpDecrypt = new GroupBox();
        btnDecryptRsa = new Button();
        lblDecryptedData = new Label();
        txtDecryptedData = new TextBox();
        lblCompareResult = new Label();

        // --- Controls for Tab 3: Benchmark ---
        grpBenchmark = new GroupBox();
        lblBenchNotice = new Label();
        lblLoopCount = new Label();
        numLoops = new NumericUpDown();
        btnRunBenchmark = new Button();
        txtBenchmarkResult = new TextBox();

        // --- Controls for Tab 4: SHA-256 ---
        grpHash = new GroupBox();
        lblHashInput1 = new Label();
        txtHashInput1 = new TextBox();
        lblHashResult1 = new Label();
        txtHashResult1 = new TextBox();
        lblHashInput2 = new Label();
        txtHashInput2 = new TextBox();
        lblHashResult2 = new Label();
        txtHashResult2 = new TextBox();
        txtAvalancheInfo = new TextBox();

        // --- Controls for Tab 5: Digital Signature ---
        grpSign = new GroupBox();
        lblSignData = new Label();
        txtSignData = new TextBox();
        btnSignPss = new Button();
        btnSignPkcs1 = new Button();
        lblSignatureResult = new Label();
        txtSignatureResult = new TextBox();
        btnVerifySig = new Button();
        lblVerifyStatus = new Label();
        txtSignExplanation = new TextBox();

        tabControlMain.SuspendLayout();
        tabKeyGenPem.SuspendLayout();
        grpKeyGen.SuspendLayout();
        grpPemView.SuspendLayout();
        grpAsn1.SuspendLayout();
        tabEncryptDecrypt.SuspendLayout();
        grpEncrypt.SuspendLayout();
        grpDecrypt.SuspendLayout();
        tabBenchmark.SuspendLayout();
        grpBenchmark.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numLoops).BeginInit();
        tabHash.SuspendLayout();
        grpHash.SuspendLayout();
        tabSignature.SuspendLayout();
        grpSign.SuspendLayout();
        SuspendLayout();

        // 
        // tabControlMain
        // 
        tabControlMain.Controls.Add(tabKeyGenPem);
        tabControlMain.Controls.Add(tabEncryptDecrypt);
        tabControlMain.Controls.Add(tabBenchmark);
        tabControlMain.Controls.Add(tabHash);
        tabControlMain.Controls.Add(tabSignature);
        tabControlMain.Dock = DockStyle.Fill;
        tabControlMain.Location = new Point(0, 0);
        tabControlMain.Name = "tabControlMain";
        tabControlMain.SelectedIndex = 0;
        tabControlMain.Size = new Size(984, 691);
        tabControlMain.TabIndex = 0;

        // ==========================================
        // TAB 1: Key Gen & View PEM / ASN.1
        // ==========================================
        tabKeyGenPem.Controls.Add(grpAsn1);
        tabKeyGenPem.Controls.Add(grpPemView);
        tabKeyGenPem.Controls.Add(grpKeyGen);
        tabKeyGenPem.Location = new Point(4, 24);
        tabKeyGenPem.Name = "tabKeyGenPem";
        tabKeyGenPem.Padding = new Padding(3);
        tabKeyGenPem.Size = new Size(976, 663);
        tabKeyGenPem.TabIndex = 0;
        tabKeyGenPem.Text = "1. Sinh Khóa & Phân Tích PEM (ASN.1 DER)";
        tabKeyGenPem.UseVisualStyleBackColor = true;

        // grpKeyGen
        grpKeyGen.Controls.Add(btnImportPem);
        grpKeyGen.Controls.Add(btnExportPrivatePem);
        grpKeyGen.Controls.Add(btnExportPublicPem);
        grpKeyGen.Controls.Add(btnGenKeyPair);
        grpKeyGen.Controls.Add(cmbKeyBits);
        grpKeyGen.Controls.Add(lblKeyBits);
        grpKeyGen.Location = new Point(10, 10);
        grpKeyGen.Name = "grpKeyGen";
        grpKeyGen.Size = new Size(955, 65);
        grpKeyGen.TabIndex = 0;
        grpKeyGen.TabStop = false;
        grpKeyGen.Text = "Thao tác Cặp khóa RSA";

        lblKeyBits.AutoSize = true;
        lblKeyBits.Location = new Point(15, 28);
        lblKeyBits.Name = "lblKeyBits";
        lblKeyBits.Size = new Size(90, 15);
        lblKeyBits.Text = "Kích thước khóa:";

        cmbKeyBits.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbKeyBits.Items.AddRange(new object[] { "1024 bits", "2048 bits", "3072 bits", "4096 bits" });
        cmbKeyBits.Location = new Point(115, 25);
        cmbKeyBits.Name = "cmbKeyBits";
        cmbKeyBits.Size = new Size(100, 23);
        cmbKeyBits.TabIndex = 0;

        btnGenKeyPair.Location = new Point(230, 23);
        btnGenKeyPair.Name = "btnGenKeyPair";
        btnGenKeyPair.Size = new Size(130, 26);
        btnGenKeyPair.TabIndex = 1;
        btnGenKeyPair.Text = "🔑 Sinh Cặp Khóa";
        btnGenKeyPair.UseVisualStyleBackColor = true;
        btnGenKeyPair.Click += btnGenKeyPair_Click;

        btnExportPublicPem.Location = new Point(370, 23);
        btnExportPublicPem.Name = "btnExportPublicPem";
        btnExportPublicPem.Size = new Size(135, 26);
        btnExportPublicPem.TabIndex = 2;
        btnExportPublicPem.Text = "Lưu Public Key (.pem)";
        btnExportPublicPem.UseVisualStyleBackColor = true;
        btnExportPublicPem.Click += btnExportPublicPem_Click;

        btnExportPrivatePem.Location = new Point(515, 23);
        btnExportPrivatePem.Name = "btnExportPrivatePem";
        btnExportPrivatePem.Size = new Size(135, 26);
        btnExportPrivatePem.TabIndex = 3;
        btnExportPrivatePem.Text = "Lưu Private Key (.pem)";
        btnExportPrivatePem.UseVisualStyleBackColor = true;
        btnExportPrivatePem.Click += btnExportPrivatePem_Click;

        btnImportPem.Location = new Point(660, 23);
        btnImportPem.Name = "btnImportPem";
        btnImportPem.Size = new Size(140, 26);
        btnImportPem.TabIndex = 4;
        btnImportPem.Text = "📂 Nhập File PEM...";
        btnImportPem.UseVisualStyleBackColor = true;
        btnImportPem.Click += btnImportPem_Click;

        // grpPemView
        grpPemView.Controls.Add(txtPrivatePem);
        grpPemView.Controls.Add(lblPrivatePem);
        grpPemView.Controls.Add(txtPublicPem);
        grpPemView.Controls.Add(lblPublicPem);
        grpPemView.Location = new Point(10, 80);
        grpPemView.Name = "grpPemView";
        grpPemView.Size = new Size(955, 280);
        grpPemView.TabIndex = 1;
        grpPemView.TabStop = false;
        grpPemView.Text = "Nội dung Khóa định dạng PEM (Base64 PKCS#1)";

        lblPublicPem.AutoSize = true;
        lblPublicPem.Location = new Point(15, 22);
        lblPublicPem.Name = "lblPublicPem";
        lblPublicPem.Size = new Size(191, 15);
        lblPublicPem.Text = "PUBLIC KEY (Công khai: n, e nhỏ):";

        txtPublicPem.Font = new Font("Consolas", 8.5F);
        txtPublicPem.Location = new Point(15, 42);
        txtPublicPem.Multiline = true;
        txtPublicPem.Name = "txtPublicPem";
        txtPublicPem.ScrollBars = ScrollBars.Vertical;
        txtPublicPem.Size = new Size(450, 225);
        txtPublicPem.TabIndex = 0;
        txtPublicPem.TextChanged += (s, e) => ParseAndDisplayPem(txtPublicPem.Text);

        lblPrivatePem.AutoSize = true;
        lblPrivatePem.Location = new Point(485, 22);
        lblPrivatePem.Name = "lblPrivatePem";
        lblPrivatePem.Size = new Size(244, 15);
        lblPrivatePem.Text = "PRIVATE KEY (Bí mật: n, e, d to, p, q, dp, dq...):";

        txtPrivatePem.Font = new Font("Consolas", 8.5F);
        txtPrivatePem.Location = new Point(485, 42);
        txtPrivatePem.Multiline = true;
        txtPrivatePem.Name = "txtPrivatePem";
        txtPrivatePem.ScrollBars = ScrollBars.Vertical;
        txtPrivatePem.Size = new Size(455, 225);
        txtPrivatePem.TabIndex = 1;
        txtPrivatePem.TextChanged += (s, e) => ParseAndDisplayPem(txtPrivatePem.Text);

        // grpAsn1
        grpAsn1.Controls.Add(txtAsn1Info);
        grpAsn1.Location = new Point(10, 365);
        grpAsn1.Name = "grpAsn1";
        grpAsn1.Size = new Size(955, 290);
        grpAsn1.TabIndex = 2;
        grpAsn1.TabStop = false;
        grpAsn1.Text = "Phân Tích Cấu Trúc Byte ASN.1 DER (Ý nghĩa từng trường: n, e, d, p, q...)";

        txtAsn1Info.BackColor = Color.FromArgb(20, 24, 30);
        txtAsn1Info.Font = new Font("Consolas", 9F);
        txtAsn1Info.ForeColor = Color.FromArgb(100, 220, 255);
        txtAsn1Info.Location = new Point(15, 22);
        txtAsn1Info.Multiline = true;
        txtAsn1Info.Name = "txtAsn1Info";
        txtAsn1Info.ReadOnly = true;
        txtAsn1Info.ScrollBars = ScrollBars.Vertical;
        txtAsn1Info.Size = new Size(925, 255);
        txtAsn1Info.TabIndex = 0;

        // ==========================================
        // TAB 2: Encrypt & Decrypt
        // ==========================================
        tabEncryptDecrypt.Controls.Add(grpDecrypt);
        tabEncryptDecrypt.Controls.Add(grpEncrypt);
        tabEncryptDecrypt.Location = new Point(4, 24);
        tabEncryptDecrypt.Name = "tabEncryptDecrypt";
        tabEncryptDecrypt.Padding = new Padding(3);
        tabEncryptDecrypt.Size = new Size(976, 663);
        tabEncryptDecrypt.TabIndex = 1;
        tabEncryptDecrypt.Text = "2. Mã Hóa & Giải Mã (<= 190 Bytes)";
        tabEncryptDecrypt.UseVisualStyleBackColor = true;

        grpEncrypt.Controls.Add(btnLoadCipherFromFile);
        grpEncrypt.Controls.Add(btnSaveCipherToFile);
        grpEncrypt.Controls.Add(txtEncryptedBase64);
        grpEncrypt.Controls.Add(lblEncryptedBase64);
        grpEncrypt.Controls.Add(btnEncryptRsa);
        grpEncrypt.Controls.Add(btnUseAesKey32);
        grpEncrypt.Controls.Add(txtDataToEncrypt);
        grpEncrypt.Controls.Add(lblDataToEncrypt);
        grpEncrypt.Location = new Point(10, 10);
        grpEncrypt.Name = "grpEncrypt";
        grpEncrypt.Size = new Size(955, 300);
        grpEncrypt.TabIndex = 0;
        grpEncrypt.TabStop = false;
        grpEncrypt.Text = "Mã Hóa bằng PUBLIC KEY (n, e bé)";

        lblDataToEncrypt.AutoSize = true;
        lblDataToEncrypt.Location = new Point(15, 25);
        lblDataToEncrypt.Name = "lblDataToEncrypt";
        lblDataToEncrypt.Size = new Size(346, 15);
        lblDataToEncrypt.Text = "Dữ liệu đầu vào cần mã hóa (Chuỗi UTF-8 hoặc mảng byte <= 190B):";

        txtDataToEncrypt.Location = new Point(15, 45);
        txtDataToEncrypt.Multiline = true;
        txtDataToEncrypt.Name = "txtDataToEncrypt";
        txtDataToEncrypt.Size = new Size(925, 55);
        txtDataToEncrypt.TabIndex = 0;
        txtDataToEncrypt.Text = "Khoa-Bao-Mat-Thong-Tin-AES-Key-256bit-SecretValue123456";

        btnUseAesKey32.Location = new Point(15, 108);
        btnUseAesKey32.Name = "btnUseAesKey32";
        btnUseAesKey32.Size = new Size(230, 28);
        btnUseAesKey32.TabIndex = 1;
        btnUseAesKey32.Text = "🔑 Lấy mẫu Khóa AES 256-bit (32 bytes)";
        btnUseAesKey32.UseVisualStyleBackColor = true;
        btnUseAesKey32.Click += btnUseAesKey32_Click;

        btnEncryptRsa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnEncryptRsa.Location = new Point(260, 108);
        btnEncryptRsa.Name = "btnEncryptRsa";
        btnEncryptRsa.Size = new Size(200, 28);
        btnEncryptRsa.TabIndex = 2;
        btnEncryptRsa.Text = "🔒 Mã Hóa bằng Public Key";
        btnEncryptRsa.UseVisualStyleBackColor = true;
        btnEncryptRsa.Click += btnEncryptRsa_Click;

        btnSaveCipherToFile.Location = new Point(475, 108);
        btnSaveCipherToFile.Name = "btnSaveCipherToFile";
        btnSaveCipherToFile.Size = new Size(160, 28);
        btnSaveCipherToFile.TabIndex = 3;
        btnSaveCipherToFile.Text = "💾 Lưu kết quả ra File";
        btnSaveCipherToFile.UseVisualStyleBackColor = true;
        btnSaveCipherToFile.Click += btnSaveCipherToFile_Click;

        btnLoadCipherFromFile.Location = new Point(645, 108);
        btnLoadCipherFromFile.Name = "btnLoadCipherFromFile";
        btnLoadCipherFromFile.Size = new Size(160, 28);
        btnLoadCipherFromFile.TabIndex = 4;
        btnLoadCipherFromFile.Text = "📂 Đọc Cipher từ File";
        btnLoadCipherFromFile.UseVisualStyleBackColor = true;
        btnLoadCipherFromFile.Click += btnLoadCipherFromFile_Click;

        lblEncryptedBase64.AutoSize = true;
        lblEncryptedBase64.Location = new Point(15, 145);
        lblEncryptedBase64.Name = "lblEncryptedBase64";
        lblEncryptedBase64.Size = new Size(245, 15);
        lblEncryptedBase64.Text = "Kết quả Mã Hóa (Mảng byte dạng Base64):";

        txtEncryptedBase64.Font = new Font("Consolas", 9F);
        txtEncryptedBase64.Location = new Point(15, 165);
        txtEncryptedBase64.Multiline = true;
        txtEncryptedBase64.Name = "txtEncryptedBase64";
        txtEncryptedBase64.ScrollBars = ScrollBars.Vertical;
        txtEncryptedBase64.Size = new Size(925, 120);
        txtEncryptedBase64.TabIndex = 5;

        // grpDecrypt
        grpDecrypt.Controls.Add(lblCompareResult);
        grpDecrypt.Controls.Add(txtDecryptedData);
        grpDecrypt.Controls.Add(lblDecryptedData);
        grpDecrypt.Controls.Add(btnDecryptRsa);
        grpDecrypt.Location = new Point(10, 320);
        grpDecrypt.Name = "grpDecrypt";
        grpDecrypt.Size = new Size(955, 330);
        grpDecrypt.TabIndex = 1;
        grpDecrypt.TabStop = false;
        grpDecrypt.Text = "Giải Mã bằng PRIVATE KEY (n, d to đùng)";

        btnDecryptRsa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnDecryptRsa.Location = new Point(15, 25);
        btnDecryptRsa.Name = "btnDecryptRsa";
        btnDecryptRsa.Size = new Size(230, 32);
        btnDecryptRsa.TabIndex = 0;
        btnDecryptRsa.Text = "🔓 Bắt Đầu Giải Mã (Private Key)";
        btnDecryptRsa.UseVisualStyleBackColor = true;
        btnDecryptRsa.Click += btnDecryptRsa_Click;

        lblCompareResult.AutoSize = true;
        lblCompareResult.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblCompareResult.Location = new Point(260, 34);
        lblCompareResult.Name = "lblCompareResult";
        lblCompareResult.Size = new Size(137, 15);
        lblCompareResult.Text = "So sánh dữ liệu: Chưa chạy";

        lblDecryptedData.AutoSize = true;
        lblDecryptedData.Location = new Point(15, 70);
        lblDecryptedData.Name = "lblDecryptedData";
        lblDecryptedData.Size = new Size(157, 15);
        lblDecryptedData.Text = "Dữ liệu sau khi được Giải mã:";

        txtDecryptedData.Font = new Font("Consolas", 9.5F);
        txtDecryptedData.Location = new Point(15, 90);
        txtDecryptedData.Multiline = true;
        txtDecryptedData.Name = "txtDecryptedData";
        txtDecryptedData.ScrollBars = ScrollBars.Vertical;
        txtDecryptedData.Size = new Size(925, 220);
        txtDecryptedData.TabIndex = 1;

        // ==========================================
        // TAB 3: Benchmark Tốc Độ
        // ==========================================
        tabBenchmark.Controls.Add(grpBenchmark);
        tabBenchmark.Location = new Point(4, 24);
        tabBenchmark.Name = "tabBenchmark";
        tabBenchmark.Padding = new Padding(3);
        tabBenchmark.Size = new Size(976, 663);
        tabBenchmark.TabIndex = 2;
        tabBenchmark.Text = "3. Đánh Giá Tốc Độ (e nhỏ vs d to)";
        tabBenchmark.UseVisualStyleBackColor = true;

        grpBenchmark.Controls.Add(txtBenchmarkResult);
        grpBenchmark.Controls.Add(btnRunBenchmark);
        grpBenchmark.Controls.Add(numLoops);
        grpBenchmark.Controls.Add(lblLoopCount);
        grpBenchmark.Controls.Add(lblBenchNotice);
        grpBenchmark.Location = new Point(10, 10);
        grpBenchmark.Name = "grpBenchmark";
        grpBenchmark.Size = new Size(955, 640);
        grpBenchmark.TabIndex = 0;
        grpBenchmark.TabStop = false;
        grpBenchmark.Text = "Đo Tốc Độ Xử Lý: Mã hóa bằng Public Key (e bé) vs Giải mã bằng Private Key (d to)";

        lblBenchNotice.AutoSize = true;
        lblBenchNotice.Location = new Point(15, 30);
        lblBenchNotice.Name = "lblBenchNotice";
        lblBenchNotice.Size = new Size(417, 15);
        lblBenchNotice.Text = "Khảo sát thời gian tính toán lũy thừa modulo: c = m^e mod n  vs  m = c^d mod n";

        lblLoopCount.AutoSize = true;
        lblLoopCount.Location = new Point(460, 30);
        lblLoopCount.Name = "lblLoopCount";
        lblLoopCount.Size = new Size(72, 15);
        lblLoopCount.Text = "Số lần lặp:";

        numLoops.Location = new Point(540, 27);
        numLoops.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
        numLoops.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
        numLoops.Name = "numLoops";
        numLoops.Size = new Size(80, 23);
        numLoops.TabIndex = 1;
        numLoops.Value = new decimal(new int[] { 100, 0, 0, 0 });

        btnRunBenchmark.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnRunBenchmark.Location = new Point(640, 24);
        btnRunBenchmark.Name = "btnRunBenchmark";
        btnRunBenchmark.Size = new Size(180, 28);
        btnRunBenchmark.TabIndex = 2;
        btnRunBenchmark.Text = "⚡ Bắt Đầu Benchmark";
        btnRunBenchmark.UseVisualStyleBackColor = true;
        btnRunBenchmark.Click += btnRunBenchmark_Click;

        txtBenchmarkResult.BackColor = Color.FromArgb(15, 20, 25);
        txtBenchmarkResult.Font = new Font("Consolas", 9.5F);
        txtBenchmarkResult.ForeColor = Color.FromArgb(120, 255, 120);
        txtBenchmarkResult.Location = new Point(15, 65);
        txtBenchmarkResult.Multiline = true;
        txtBenchmarkResult.Name = "txtBenchmarkResult";
        txtBenchmarkResult.ReadOnly = true;
        txtBenchmarkResult.ScrollBars = ScrollBars.Vertical;
        txtBenchmarkResult.Size = new Size(925, 560);
        txtBenchmarkResult.TabIndex = 3;

        // ==========================================
        // TAB 4: SHA-256 Avalanche Effect
        // ==========================================
        tabHash.Controls.Add(grpHash);
        tabHash.Location = new Point(4, 24);
        tabHash.Name = "tabHash";
        tabHash.Padding = new Padding(3);
        tabHash.Size = new Size(976, 663);
        tabHash.TabIndex = 3;
        tabHash.Text = "4. Chuỗi Băm SHA-256 & Hiệu Ứng Tuyết Lở";
        tabHash.UseVisualStyleBackColor = true;

        grpHash.Controls.Add(txtAvalancheInfo);
        grpHash.Controls.Add(txtHashResult2);
        grpHash.Controls.Add(lblHashResult2);
        grpHash.Controls.Add(txtHashInput2);
        grpHash.Controls.Add(lblHashInput2);
        grpHash.Controls.Add(txtHashResult1);
        grpHash.Controls.Add(lblHashResult1);
        grpHash.Controls.Add(txtHashInput1);
        grpHash.Controls.Add(lblHashInput1);
        grpHash.Location = new Point(10, 10);
        grpHash.Name = "grpHash";
        grpHash.Size = new Size(955, 640);
        grpHash.TabIndex = 0;
        grpHash.TabStop = false;
        grpHash.Text = "Quan sát tính chất Hàm băm một chiều (Avalanche Effect)";

        lblHashInput1.AutoSize = true;
        lblHashInput1.Location = new Point(15, 25);
        lblHashInput1.Name = "lblHashInput1";
        lblHashInput1.Size = new Size(139, 15);
        lblHashInput1.Text = "Chuỗi đầu vào 1 (Gốc):";

        txtHashInput1.Location = new Point(15, 45);
        txtHashInput1.Name = "txtHashInput1";
        txtHashInput1.Size = new Size(925, 23);
        txtHashInput1.TabIndex = 0;
        txtHashInput1.Text = "Dai hoc Kien truc Ha Noi - K59KMT";
        txtHashInput1.TextChanged += (s, e) => UpdateHashComparison();

        lblHashResult1.AutoSize = true;
        lblHashResult1.Location = new Point(15, 75);
        lblHashResult1.Name = "lblHashResult1";
        lblHashResult1.Size = new Size(191, 15);
        lblHashResult1.Text = "Mã băm SHA-256 (64 ký tự hex):";

        txtHashResult1.Font = new Font("Consolas", 9F);
        txtHashResult1.Location = new Point(15, 95);
        txtHashResult1.Name = "txtHashResult1";
        txtHashResult1.ReadOnly = true;
        txtHashResult1.Size = new Size(925, 22);
        txtHashResult1.TabIndex = 1;

        lblHashInput2.AutoSize = true;
        lblHashInput2.Location = new Point(15, 130);
        lblHashInput2.Name = "lblHashInput2";
        lblHashInput2.Size = new Size(295, 15);
        lblHashInput2.Text = "Chuỗi đầu vào 2 (Chỉ đổi 1 ký tự, ví dụ: K59KMT -> K59kmt):";

        txtHashInput2.Location = new Point(15, 150);
        txtHashInput2.Name = "txtHashInput2";
        txtHashInput2.Size = new Size(925, 23);
        txtHashInput2.TabIndex = 2;
        txtHashInput2.Text = "Dai hoc Kien truc Ha Noi - K59kmt";
        txtHashInput2.TextChanged += (s, e) => UpdateHashComparison();

        lblHashResult2.AutoSize = true;
        lblHashResult2.Location = new Point(15, 180);
        lblHashResult2.Name = "lblHashResult2";
        lblHashResult2.Size = new Size(191, 15);
        lblHashResult2.Text = "Mã băm SHA-256 (64 ký tự hex):";

        txtHashResult2.Font = new Font("Consolas", 9F);
        txtHashResult2.Location = new Point(15, 200);
        txtHashResult2.Name = "txtHashResult2";
        txtHashResult2.ReadOnly = true;
        txtHashResult2.Size = new Size(925, 22);
        txtHashResult2.TabIndex = 3;

        txtAvalancheInfo.BackColor = Color.FromArgb(20, 25, 20);
        txtAvalancheInfo.Font = new Font("Consolas", 9.5F);
        txtAvalancheInfo.ForeColor = Color.FromArgb(255, 220, 100);
        txtAvalancheInfo.Location = new Point(15, 240);
        txtAvalancheInfo.Multiline = true;
        txtAvalancheInfo.Name = "txtAvalancheInfo";
        txtAvalancheInfo.ReadOnly = true;
        txtAvalancheInfo.ScrollBars = ScrollBars.Vertical;
        txtAvalancheInfo.Size = new Size(925, 380);
        txtAvalancheInfo.TabIndex = 4;

        // ==========================================
        // TAB 5: Digital Signature & RSA-PSS
        // ==========================================
        tabSignature.Controls.Add(grpSign);
        tabSignature.Location = new Point(4, 24);
        tabSignature.Name = "tabSignature";
        tabSignature.Padding = new Padding(3);
        tabSignature.Size = new Size(976, 663);
        tabSignature.TabIndex = 4;
        tabSignature.Text = "5. Chữ Ký Số & Phương Pháp Đổi Mới RSA-PSS";
        tabSignature.UseVisualStyleBackColor = true;

        grpSign.Controls.Add(txtSignExplanation);
        grpSign.Controls.Add(lblVerifyStatus);
        grpSign.Controls.Add(btnVerifySig);
        grpSign.Controls.Add(txtSignatureResult);
        grpSign.Controls.Add(lblSignatureResult);
        grpSign.Controls.Add(btnSignPkcs1);
        grpSign.Controls.Add(btnSignPss);
        grpSign.Controls.Add(txtSignData);
        grpSign.Controls.Add(lblSignData);
        grpSign.Location = new Point(10, 10);
        grpSign.Name = "grpSign";
        grpSign.Size = new Size(955, 640);
        grpSign.TabIndex = 0;
        grpSign.TabStop = false;
        grpSign.Text = "Thử Nghiệm Ký Số RSA: PKCS#1 v1.5 (Cố định) vs RSA-PSS (Ngẫu nhiên mỗi lần)";

        lblSignData.AutoSize = true;
        lblSignData.Location = new Point(15, 25);
        lblSignData.Name = "lblSignData";
        lblSignData.Size = new Size(136, 15);
        lblSignData.Text = "Nội dung văn bản cần ký:";

        txtSignData.Location = new Point(15, 45);
        txtSignData.Multiline = true;
        txtSignData.Name = "txtSignData";
        txtSignData.Size = new Size(925, 45);
        txtSignData.TabIndex = 0;
        txtSignData.Text = "Hợp đồng chuyển giao công nghệ mật mã an toàn thông tin 2026";

        btnSignPss.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSignPss.ForeColor = Color.DarkGreen;
        btnSignPss.Location = new Point(15, 100);
        btnSignPss.Name = "btnSignPss";
        btnSignPss.Size = new Size(330, 32);
        btnSignPss.TabIndex = 1;
        btnSignPss.Text = "✍️ KÝ SỐ RSA-PSS (Chữ ký KHÁC NHAU mỗi lần bấm)";
        btnSignPss.UseVisualStyleBackColor = true;
        btnSignPss.Click += btnSignPss_Click;

        btnSignPkcs1.Location = new Point(360, 100);
        btnSignPkcs1.Name = "btnSignPkcs1";
        btnSignPkcs1.Size = new Size(260, 32);
        btnSignPkcs1.TabIndex = 2;
        btnSignPkcs1.Text = "✍️ Ký số PKCS#1 v1.5 (Cố định mỗi lần)";
        btnSignPkcs1.UseVisualStyleBackColor = true;
        btnSignPkcs1.Click += btnSignPkcs1_Click;

        btnVerifySig.Location = new Point(635, 100);
        btnVerifySig.Name = "btnVerifySig";
        btnVerifySig.Size = new Size(150, 32);
        btnVerifySig.TabIndex = 3;
        btnVerifySig.Text = "🔍 Kiểm tra Chữ Ký";
        btnVerifySig.UseVisualStyleBackColor = true;
        btnVerifySig.Click += btnVerifySig_Click;

        lblVerifyStatus.AutoSize = true;
        lblVerifyStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblVerifyStatus.Location = new Point(795, 108);
        lblVerifyStatus.Name = "lblVerifyStatus";
        lblVerifyStatus.Size = new Size(130, 15);
        lblVerifyStatus.TabIndex = 4;
        lblVerifyStatus.Text = "Trạng thái: Chưa kiểm";

        lblSignatureResult.AutoSize = true;
        lblSignatureResult.Location = new Point(15, 140);
        lblSignatureResult.Name = "lblSignatureResult";
        lblSignatureResult.Size = new Size(204, 15);
        lblSignatureResult.Text = "Chữ ký số tạo được (Base64 256 bytes):";

        txtSignatureResult.Font = new Font("Consolas", 8.5F);
        txtSignatureResult.Location = new Point(15, 160);
        txtSignatureResult.Multiline = true;
        txtSignatureResult.Name = "txtSignatureResult";
        txtSignatureResult.ScrollBars = ScrollBars.Vertical;
        txtSignatureResult.Size = new Size(925, 95);
        txtSignatureResult.TabIndex = 5;

        txtSignExplanation.BackColor = Color.FromArgb(24, 24, 28);
        txtSignExplanation.Font = new Font("Consolas", 9F);
        txtSignExplanation.ForeColor = Color.FromArgb(250, 200, 120);
        txtSignExplanation.Location = new Point(15, 270);
        txtSignExplanation.Multiline = true;
        txtSignExplanation.Name = "txtSignExplanation";
        txtSignExplanation.ReadOnly = true;
        txtSignExplanation.ScrollBars = ScrollBars.Vertical;
        txtSignExplanation.Size = new Size(925, 355);
        txtSignExplanation.TabIndex = 6;

        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(984, 691);
        Controls.Add(tabControlMain);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Bài Tập 2 - Mật Mã Bất Đối Xứng RSA, SHA-256 & Chữ Ký Số (K59KMT)";

        tabControlMain.ResumeLayout(false);
        tabKeyGenPem.ResumeLayout(false);
        grpKeyGen.ResumeLayout(false);
        grpKeyGen.PerformLayout();
        grpPemView.ResumeLayout(false);
        grpPemView.PerformLayout();
        grpAsn1.ResumeLayout(false);
        grpAsn1.PerformLayout();
        tabEncryptDecrypt.ResumeLayout(false);
        grpEncrypt.ResumeLayout(false);
        grpEncrypt.PerformLayout();
        grpDecrypt.ResumeLayout(false);
        grpDecrypt.PerformLayout();
        tabBenchmark.ResumeLayout(false);
        grpBenchmark.ResumeLayout(false);
        grpBenchmark.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numLoops).EndInit();
        tabHash.ResumeLayout(false);
        grpHash.ResumeLayout(false);
        grpHash.PerformLayout();
        tabSignature.ResumeLayout(false);
        grpSign.ResumeLayout(false);
        grpSign.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TabControl tabControlMain;
    private TabPage tabKeyGenPem;
    private TabPage tabEncryptDecrypt;
    private TabPage tabBenchmark;
    private TabPage tabHash;
    private TabPage tabSignature;

    // Tab 1
    private GroupBox grpKeyGen;
    private Label lblKeyBits;
    private ComboBox cmbKeyBits;
    private Button btnGenKeyPair;
    private Button btnExportPublicPem;
    private Button btnExportPrivatePem;
    private Button btnImportPem;
    private GroupBox grpPemView;
    private Label lblPublicPem;
    private TextBox txtPublicPem;
    private Label lblPrivatePem;
    private TextBox txtPrivatePem;
    private GroupBox grpAsn1;
    private TextBox txtAsn1Info;

    // Tab 2
    private GroupBox grpEncrypt;
    private Label lblDataToEncrypt;
    private TextBox txtDataToEncrypt;
    private Button btnUseAesKey32;
    private Button btnEncryptRsa;
    private Label lblEncryptedBase64;
    private TextBox txtEncryptedBase64;
    private Button btnSaveCipherToFile;
    private Button btnLoadCipherFromFile;
    private GroupBox grpDecrypt;
    private Button btnDecryptRsa;
    private Label lblDecryptedData;
    private TextBox txtDecryptedData;
    private Label lblCompareResult;

    // Tab 3
    private GroupBox grpBenchmark;
    private Label lblBenchNotice;
    private Label lblLoopCount;
    private NumericUpDown numLoops;
    private Button btnRunBenchmark;
    private TextBox txtBenchmarkResult;

    // Tab 4
    private GroupBox grpHash;
    private Label lblHashInput1;
    private TextBox txtHashInput1;
    private Label lblHashResult1;
    private TextBox txtHashResult1;
    private Label lblHashInput2;
    private TextBox txtHashInput2;
    private Label lblHashResult2;
    private TextBox txtHashResult2;
    private TextBox txtAvalancheInfo;

    // Tab 5
    private GroupBox grpSign;
    private Label lblSignData;
    private TextBox txtSignData;
    private Button btnSignPss;
    private Button btnSignPkcs1;
    private Label lblSignatureResult;
    private TextBox txtSignatureResult;
    private Button btnVerifySig;
    private Label lblVerifyStatus;
    private TextBox txtSignExplanation;
}
