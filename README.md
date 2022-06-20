# 💸 HỆ THỐNG ĐẤU GIÁ TỰ ĐỘNG - AUCTION MWG
# 🧑‍💻	CÔNG NGHỆ VÀ KỸ THUẬT ÁP DỤNG TRONG DỰ ÁN
- Web server: .NET CORE (5.0), Razor engine
- Hệ quản trị CSDL: MS SQL server
- Kỹ thuật được áp dụng: AJAX, Bundle, minify, caching...
- Framework hỗ trợ: Entity Framework, administrator bootstrap template
# ⏰ TIMELINE CHI TIẾT CHO DỰ ÁN
| THỜI GIAN | CÔNG VIỆC CỤ THỂ| GHI CHÚ |
| :---:| :---| :--- |
| 13-19/6 | - Xây dựng Database. <br> - Nghiên cứu Ajax, Razor (component)... <br> - Nghiên cứu Entity framework. <br> - Xây đựng kế hoạch, viết Timeline cho dự án. | Đã hoàn thành |
| 20-26/6 <br> và <br> 27/6-3/7 | - Xây dựng trang administrator để đăng nhập hệ thống. <br> - Phân quyền theo 2 roles: admin và mod (admin thì có tất cả các quyền, còn mod chỉ được làm các quyền hạn chế). <br> <br> - Xây dựng trang cấp quyền cho mod và admin: <br> + Quản lí tài khoản các users: <br> • Hiển thị danh sách các users (người mua và bán, có phân trang). <br> • Xem chi tiết tài khoản. <br> • Thêm button khóa tài khoản (nếu vi phạm chính sách). <br> • Tìm kiếm tài khoản theo tên, username, email, sdt... <br> + Xét duyệt đăng sản phẩm: <br> • Hiển thị danh sách chờ duyệt <br> • hiển thị danh sách bị từ chối (kèm lí do). <br> • Xem chi tiết sản phẩm. <br> • Duyệt hoặc từ chối sản phẩm (kèm lí do). <br> - Báo cáo vi phạm: <br> • Hiển thị danh sách báo cáo vi phạm. <br> • Gởi cảnh báo qua Email (optional). <br> • khóa tài khoản (kèm lí do). <br> - Thống kê: <br> • Thống kê danh sách doanh thu theo: sản phẩm, danh mục, hiển thị thoe năm, quý, tháng... <br> • Hình thức thống kê: Theo bảng, cột, biểu đồ... <br> • Xuất thống kê ra file Exell, pdf. <br> • Thêm chức năng in ấn trực tiếp (optional). <br> <br> - Xây dựng chức năng dành riêng cho ADMIN: <br> • Tạo tài khoản nhân viên ( có hỗ trợ tạo nhiều tài khoản). <br> • Xem chi tiết tài khoản. <br> • Hiển thị danh sách các tài khoản (hỗ trợ phần trang). <br> • tìm kiếm tài khoản theo: tên, username, email, sdt. <br> • Chỉnh sửa thông tin cá nhân. <br> • khóa tài khoản. | |
| 4-10/7 <br> và <br> 11-17/7 | - Xây dựng trang chủ có header: Tìm tài khoản, xem sản phẩm, chức năng cho người mua, chức năng cho người bán. đăng nhập ( nếu chưa đăng nhập), đăng xuất (nếu chưa đăng xuất). Đăng kí tài khoản ( Nếu chưa có tài khoản). <br> - Về tài khoản: <br> • Xây dựng trang đăng nhập (nếu đang trong quá trình **ĐẤU GIÁ** thì bắt buộc phải đăng nhập để xác định danh tính). <br> • Xây dựng trang đăng kí tài khoản (nếu chưa có tài khoản). <br> • Chỉnh sửa thông tin tài khoản. <br> • Xem chi tiết thông tin cá nhân. <br><br> - Xây dụng chức năng cho người bán: <br> • Đang sản phẩm (Đăng sản phẩm mình muốn bán và chờ hệ thống phê duyệt). <br> • Hiển thị danh sách sản phẩm: ds được duyệt, đã có người mua, đã thanh toán. <br> • Thay đổi thông tin sản phẩm (**chỉ được thay đổi khi chưa đấu giá, chưa bán**). <br> • Tạo phiên đấu giá (nếu sản phẩm đã dc duyệt, bắt buộc phải tạo phiên đấu giá để sản phẩm mới có thể bán được, bên cạnh đó có thể thêm nút **mua ngay**). <br> • Hiển thị thời gian thực của sản phẩm đấu giá (**sử dụng AJAX**). <br> • Hủy phiên đấu giá (**chỉ được hủy khi time còn lại lớn hơn 15p**). <br><br> - Chức năng cho người mua: <br> • Tạo chức năng đấu giá tự động cho sản phẩm. <br> • Mua ngay: thêm vào danh sách mua ngay, sau khi sản phẩm được mua ngay thì phiên đấu giá sẽ dc hủy, người mua tiến hành thanh toán. <br> • Hủy đấu giá: Chỉ được hủy khi thời gian còn lại lớn hơn **5p** hoặc số tiền sản phẩm đã tăng khi đấu giá. <br> • Hiển thị sản phẩm dã mua: Tiến hành thanh toán và báo cáo vi phạm nếu sản phẩm gian dối. <br> - Chức năng khác: <br> • Hiển thị danh sách tìm kiếm. <br> • Hiển thị danh sách yêu thích. <br> • Hiển thị lịch sử tìm kiếm.  | |
