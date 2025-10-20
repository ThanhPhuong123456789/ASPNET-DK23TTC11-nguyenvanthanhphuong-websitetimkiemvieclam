# Hướng dẫn cài đặt và chạy website tìm kiếm việc làm JobsFinder



<!-- installation -->
### Cài đặt CSDL(download và cài đặt bộ ứng dung của Microsoft Sql Server thành công)

1. Trên nhánh main của source code

2. Mở thư mục "ASPNET-DK23TTC11-nguyenvanthanhphuong-websitetimkiemvieclam/Database", sau đó copy 2 file JobsFinder và JobsFinder_log vào trong đường dẫn của Microsoft SQL Server (ví dụ: C:\Program Files\Microsoft SQL Server\<Tên server>\MSSQL\DATA)
3. Mở SQL Serevr Management và kết nối vào CSDL(đăng nhập username và passwork hoặc đăng nhập bằng Windows Authentication)
4. Chuột phải Database > Attach.. > Add > Chọn JobsFinder.mdf > OK > OK
5. Reload để kiểm tra csdl


## Cài đặt project.
1. Đường dẫn các thư mục cần thay đổi nội dung để có thể kết nối đến database:
   - ASPNET-DK23TTC11-nguyenvanthanhphuong-websitetimkiemvieclam\JobsFinder_Main\Areas\Admin\Views\web.config
   - ASPNET-DK23TTC11-nguyenvanthanhphuong-websitetimkiemvieclam\JobsFinder_Main\Web.config
   - ASPNET-DK23TTC11-nguyenvanthanhphuong-websitetimkiemvieclam\JobsFinder_Main\Views\Web.config
   - ASPNET-DK23TTC11-nguyenvanthanhphuong-websitetimkiemvieclam\Model\App.config
   
1.1 Nếu SQL Server Management studio đăng nhập username và passwork thì thay đổi chuỗi connectionString trong App.config va trong web.config (4 đường dẫn ở trên), cần thay đổi tên server(ví dụ PhuongNVT), tên đăng nhập và mật khẩu đăng nhập. 
   Ví dụ:

```
<connectionStrings>
	<add name="JobsFinderDbContext" connectionString="data source=(Tên server);initial catalog=JobsFinder;user id=(Tên đăng nhập);password=(Mật khẩu);MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>
```
1.2 Nếu SQL Server Management studio đăng nhập bằng Windows Authentication thì thay đổi chuổi connectionString trong App.config va trong web.config (4 đường dẫn ở trên), cần thay đổi tên server:
  Ví dụ:

```
<connectionStrings>
    <add name="JobsFinderDbContext" connectionString="data source=(Tên server);initial catalog=JobsFinder;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>
```
2. Bật TCP/IP trong SQL Server Configuration Manager
3. Vào thư mục ASPNET-DK23TTC11-nguyenvanthanhphuong-websitetimkiemvieclam\JobsFinder_Main, click file tên là "JobsFinder_Main.snl" để mở ứng dụng visual studio. 
 - Click vào Build và đợi build thành công
 - Khởi chạy dự án bằng trình duyệt bằng cách click start.

### Trang quản trị

Sau khi đã khởi thành công thì ta có thể vào trang quản trị với đường dẫn sau: https://localhost:PORT/

Với PORT là cổng truy cập trên máy local.
Có thể thay đổi PORT bằng cách:
- Mở project trong Visual Studio

- Trong Solution Explorer, nhấp chuột phải vào project → chọn Properties

- Chọn tab Web
- Trong phần Servers, sẽ thấy:
    http://localhost:44300/
- Thay đổi 44300 thành port mong muốn rồi lưu và run lại

* Đường dẫn trang Admin
https://localhost:44300/Admin/Login.  với trang này được cấp cho 1 tài khoản là **thanhphuong2** và mật khẩu là **123**


### Trang dành cho client
* Mặc định trang dành cho cient là: https://localhost:44300/
* Người dùng có thể tạo 1 tài khoản và bắt đầu sử dụng web bình thường

### Liên hệ cho tác giả:
* Nguyễn Văn Thanh Phương
* Số điện thoại: 0359995977
* Email: phuongnvt100101@sv-onuni.edu.vn hoặc thanhphuongsw123@gmail.com
