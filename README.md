# Hướng dẫn cài đặt và chạy web JobsFinder



<!-- installation -->
### Cài đặt CSDL

1. Clone branch main project

2. Mở thư mục "Database", sao đó copy 2 file vào trong đường dẫn sau: C:\Program Files\Microsoft SQL Server\<Tên server>\MSSQL\DATA
3. Mở SQL Serevr Management và kết nối vào CSDL
4. Chuột phải Database > Attach.. > Add > Chọn JobsFinder.mdf > OK > OK
5. Reload để kiểm tra csdl


## Cài đặt project.
1. Vào thư mục JobsFinder_Main, có 1 file tên là "JobsFinder_Main.snl". mở file này bằng visual studio 2022 (Hoặc phiên bản khác)

2. Thay đổi chuỗi connectionString trong App.config va trong web.config, cần thay đổi tên server, tên đăng nhập và mật khẩu đăng nhập.

```
<connectionStrings>
	<add name="JobsFinderDbContext" connectionString="data source=(Tên server);initial catalog=JobsFinder;user id=(Tên đăng nhập);password=(Mật khẩu);MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>
```
3. Nếu không có tên máy chủ và mật khẩu có thể thử đoạn mã sau:
```
<connectionStrings>
    <add name="JobsFinderDbContext" connectionString="data source=(Tên máy chủ);initial catalog=JobsFinder;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>
```
3. Khởi chạy dự án bằng trình duyệt.

## Thiết lập cơ bản
### Trang quản trị

Sau khi đã khởi thành công thì ta có thể vào trang quản trị với đường dẫn sau: https://localhost:PORT/

Với PORT là cổng truy cập trên máy local ví dụ 44300

* Đường dẫn trang Admin
https://localhost:44300/Admin/Login.  với trang này được cấp cho 1 tài khoản là **admin** và mật khẩu là **123**


### Trang dành cho client
* Mặc định trang dành cho cient là: https://localhost:44300/
* Người dùng có thể tạo 1 tài khoản và bắt đầu sử dụng web bình thường

### Liên hệ cho nhóm tác giả:
* Nguyễn Văn Thanh Phương
* Số điện thoại: 0359995977
* Email: phuongnvt100101@sv-onuni.edu.vn hoặc thanhphuongsw123@gmail.com
