# Hướng dẫn cài đặt và chạy website tìm kiếm việc làm JobsFinder



<!-- installation -->
### Cài đặt CSDL(download và cài đặt bộ ứng dung của Microsoft Sql Server thành công)

1. Clone branch main project

2. Mở thư mục "Database", sau đó copy 2 file JobsFinder và JobsFinder_log vào trong đường dẫn của Microsoft SQL Server (ví dụ: C:\Program Files\Microsoft SQL Server\<Tên server>\MSSQL\DATA)
3. Mở SQL Serevr Management và kết nối vào CSDL
4. Chuột phải Database > Attach.. > Add > Chọn JobsFinder.mdf > OK > OK
5. Reload để kiểm tra csdl


## Cài đặt project.
1. Vào thư mục JobsFinder_Main, có 1 file tên là "JobsFinder_Main.snl". mở file này bằng visual studio 2022 (Hoặc phiên bản khác nếu có)
   đường dẫn các thư mục cần thay đổi:
   - D:ASPNET-DK23TTC11-nguyenvanthanhphuong-WEBfindjob\JobsFinder_Main\Areas\Admin\Views\web.config
2. Nếu SQL Server Management studio đăng nhập username và passwork thì thay đổi chuỗi connectionString trong App.config va trong web.config, cần thay đổi tên server(ví dụ PhuongNVT), tên đăng nhập và mật khẩu đăng nhập. 
   Ví dụ:

```
<connectionStrings>
	<add name="JobsFinderDbContext" connectionString="data source=(Tên server);initial catalog=JobsFinder;user id=(Tên đăng nhập);password=(Mật khẩu);MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>
```
3. Nếu SQL Server Management studio đăng nhập bằng Windows Authentication thì thay đổi chuổi connectionString trong App.config va trong web.config, cần thay đổi tên server:
  Ví dụ:

```
<connectionStrings>
    <add name="JobsFinderDbContext" connectionString="data source=(Tên server);initial catalog=JobsFinder;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>
```
4. Bật TCP/IP trong SQL Server Configuration Manager
4. Khởi chạy dự án bằng trình duyệt.

## Thiết lập cơ bản
### Trang quản trị

Sau khi đã khởi thành công thì ta có thể vào trtang quản trị với đường dẫn sau: https://localhost:PORT/

Với PORT là cổng truy cập trên máy local.

* Đường dẫn trang Admin
https://localhost:44300/Admin/Login.  với trang này được cấp cho 1 tài khoản là **admin** và mật khẩu là **123**


### Trang dành cho client
* Mặc định trang dành cho cient là: https://localhost:PORT/
* Người dùng có thể tạo 1 tài khoản và bắt đầu sử dụng web bình thường

### Liên hệ cho tác giả:
* Nguyễn Văn Thanh Phương
* Số điện thoại: 0359995977
* Email: phuongnvt100101@sv-onuni.edu.vn hoặc thanhphuongsw123@gmail.com
