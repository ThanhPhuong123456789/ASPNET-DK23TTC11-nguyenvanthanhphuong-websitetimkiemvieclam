var blog = {
    init: function () {
        blog.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();

            var btn = $(this);

            var id = btn.data('id');

            $.ajax({
                url: "/Admin/Blog/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (respone) {
                    if (respone.status == true) {
                        btn.text("Kích hoạt");
                    } else {
                        btn.text("Khóa");
                    }
                }
            });
        })
    }
}
blog.init();