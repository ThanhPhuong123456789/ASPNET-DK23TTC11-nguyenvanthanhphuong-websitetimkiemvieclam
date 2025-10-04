var job = {
    init: function () {
        job.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();

            var btn = $(this);

            var id = btn.data('id');

            $.ajax({
                url: "/Admin/Job/ChangeStatus",
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
job.init();

function salaryChange(select) {
    var minMax = document.getElementById("salary-min-max");
    var negotiate = document.getElementById("salary-negotiate");
    if (select.value == "True") {
        minMax.style.display = "block";
        negotiate.style.display = "none";
    } else {
        minMax.style.display = "none";
        negotiate.style.display = "block";
    }
}

$(function () {
    $('#Deadline').datetimepicker();
});
