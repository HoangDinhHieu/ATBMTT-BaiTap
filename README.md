# BÀI TẬP MÔN AN TOÀN VÀ BẢO MẬT THÔNG TIN

- **Sinh viên thực hiện:** Hoàng Đình Hiệu (`HoangDinhHieu`)
- **Môi trường:** C# .NET 8.0 (Windows Forms)
- **Kiến trúc Solution (`ATBMTT.slnx`):**
  - `libSecurity`: Thư viện xử lý thuật toán mật mã lõi (`MyAES`, `MyRSA`, `ASN.1 Parser`, `SHA-256`, `RSA-PSS`).
  - `BaiTap1_AES`: Ứng dụng Windows Forms giải quyết toàn bộ yêu cầu Bài tập 1.
  - `BaiTap2_RSA`: Ứng dụng Windows Forms giải quyết toàn bộ yêu cầu Bài tập 2.

---

## 📌 BÀI TẬP 1: MÃ HOÁ ĐỐI XỨNG AES-256-CBC

### 1. Các tính năng đã hoàn thiện:
1. **Sinh khóa AES:** Tự động sinh khóa ngẫu nhiên 256-bit (32 bytes) chuẩn mã hóa an toàn cao nhất, hiển thị dưới dạng Base64.
2. **Lưu khóa ra file:** Xuất khóa bí mật ra tệp `.key`.
3. **Nhập khóa từ file:** Đọc tệp `.key` và kiểm tra tính hợp lệ độ dài 256 bits.
4. **Mã hoá văn bản (Text):** 
   - Mã hoá chuỗi UTF-8 sang chuỗi Base64 (gồm 16 bytes IV ngẫu nhiên + CipherText).
   - Giải mã và khôi phục văn bản gốc nguyên vẹn.
5. **Mã hoá File lớn (hàng GB):**
   - Áp dụng kỹ thuật **Streaming với `CryptoStream`** (kích thước khối đệm 80 KB).
   - Tối ưu bộ nhớ RAM: Bộ nhớ RAM luôn giữ mức ổn định (< 50MB) ngay cả khi xử lý tệp nhiều Gigabytes.
   - Đo thời gian mã hoá và giải mã chính xác bằng `Stopwatch`.
   - Tính toán thông lượng tốc độ thực tế (MB/s).
6. **Thử nghiệm & So sánh từng byte:**
   - Hỗ trợ công cụ tự sinh file dữ liệu giả lập (100MB, 500MB, 1GB).
   - So sánh từng byte (`byte-by-byte comparison`) giữa file gốc và file sau giải mã.
7. **Kết luận dựa theo số liệu thu được:**
   - Dữ liệu sau giải mã khớp chính xác 100% từng byte (sai khác: 0 bytes), bảo đảm tính toàn vẹn (**Integrity**) và bí mật (**Confidentiality**).
   - Thuật toán AES đối xứng có thông lượng xử lý cực nhanh (đạt từ 200 - 500+ MB/s tùy cấu hình phần cứng ổ cứng SSD/RAM).

---

## 📌 BÀI TẬP 2: MÃ HOÁ BẤT ĐỐI XỨNG RSA & CHỮ KÝ SỐ

### 1. Các tính năng đã hoàn thiện:
1. **Sinh cặp khóa RSA:** Tùy chọn 1024, 2048, 3072, 4096 bits.
2. **Xuất cặp khóa ra file:** Xuất định dạng chuẩn `.pem` (PKCS#1 Public Key và Private Key).
3. **View PEM & Phân tích ASN.1 DER:**
   - Trích xuất toàn bộ mảng bytes hex của cấu trúc ASN.1 DER.
   - Giải thích chi tiết ý nghĩa từng trường toán học:
     - Khóa Public: Modulus $n$, Public Exponent $e$ (thường là 65537 - Fermat $F_4$).
     - Khóa Private: Modulus $n$, Public Exponent $e$, Private Exponent $d$, hai số nguyên tố bí mật $p, q$ và các số mũ $dp, dq, qinv$ hỗ trợ định lý phần dư Trung Hoa (CRT).
4. **Nhập file PEM:** Nhập khóa Public/Private từ file ngoài và tự động phân tích tham số.
5. **Mã hoá dữ liệu (dữ liệu $\le$ 190 bytes):**
   - Dùng Public Key để mã hóa gói tin nhỏ (ví dụ Khóa phiên AES 256-bit = 32 bytes).
   - Sử dụng chuẩn đệm an toàn `RSAEncryptionPadding.OaepSHA256`.
   - Xuất dữ liệu mã hóa ra chuỗi Base64 và hỗ trợ lưu/đọc file.
6. **Giải mã dữ liệu:** Dùng Private Key giải mã và so sánh đối chiếu với dữ liệu gốc trước khi mã hóa (xác thực khớp 100%).
7. **Đánh giá & Benchmark tốc độ giữa Public Key và Private Key:**
   - Đo lường và so sánh tốc độ xử lý giữa phép mã hóa $c = m^e \pmod n$ (số mũ $e = 65537$ nhỏ) và giải mã $m = c^d \pmod n$ (số mũ $d$ cực lớn 2048 bits).
   - **Kết luận:** Mã hóa bằng Public Key nhanh hơn giải mã bằng Private Key từ 10 đến 30 lần.
8. **Chuỗi băm SHA-256 & Hiệu ứng tuyết lở (Avalanche Effect):**
   - Giao diện trực quan cho phép so sánh hai chuỗi đầu vào chỉ khác nhau 1 ký tự.
   - Tự động thống kê số bit thay đổi: Mặc dù đầu vào thay đổi rất nhỏ, đầu ra SHA-256 thay đổi $\approx 50\%$ số bit một cách ngẫu nhiên.
9. **CHỮ KÝ SỐ & ĐỀ XUẤT PHƯƠNG PHÁP: CÙNG DỮ LIỆU + CÙNG PRIVATE KEY CHO CHỮ KÝ KHÁC NHAU:**
   - **Vấn đề của chuẩn ký truyền thống (PKCS#1 v1.5):** Có tính xác định (*deterministic*), nên cùng dữ liệu + cùng khóa luôn cho ra một chữ ký giống hệt nhau.
   - **Đề xuất giải pháp tối ưu - Chuẩn RSA-PSS (Probabilistic Signature Scheme - RFC 8017 / PKCS#1 v2.1):**
     - Thuật toán RSA-PSS tự động thêm chuỗi muối ngẫu nhiên (*Random Salt*) vào quá trình tạo khối đệm trước khi lũy thừa với số mũ bí mật $d$.
     - Nhờ đó, mỗi lần người dùng bấm nút Ký, **chữ ký số luôn hoàn toàn khác nhau**.
     - Khi xác minh (Verify), người nhận dùng Public Key vẫn kiểm tra được chữ ký hoàn toàn hợp lệ mà không cần lưu giữ Salt trước.
   - **Đề xuất giải pháp bổ trợ:** Kết hợp Time-stamp hoặc Nonce vào cấu trúc dữ liệu trước khi ký.

---

## 🚀 HƯỚNG DẪN CHẠY BÀI TẬP TRÊN MÁY TÍNH

1. Mở file solution `ATBMTT.slnx` bằng **Visual Studio 2022** (hoặc mở từng project `BaiTap1_AES.csproj`, `BaiTap2_RSA.csproj`).
2. Hoặc khởi chạy nhanh thông qua terminal:
   ```powershell
   # Chạy Bài tập 1 - AES
   dotnet run --project BaiTap1_AES/BaiTap1_AES.csproj

   # Chạy Bài tập 2 - RSA
   dotnet run --project BaiTap2_RSA/BaiTap2_RSA.csproj
   ```