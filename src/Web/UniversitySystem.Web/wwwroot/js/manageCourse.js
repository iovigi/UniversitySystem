(function () {
    var deleteFunc = function () {
        var btnSelf = $(this);
        var id = btnSelf.data("courseid");
        $.ajax({ url: "/ManageCourse/Delete", data: { courseId: id }, method: "POST" }).done(function (result) {
            if (result.isSucessfull) {
                btnSelf.off();
                var tr = btnSelf.parent().parent();
                tr.remove();

                $.notify("Course delete successfully", { className: 'success' });
            } else {
                $.notify("Course delete fail", { className: 'error' });
            }
        }).fail(function (jqXHR, textStatus) {
            if (jqXHR.status == 403) {
                window.location.href = "/";
            }

            $.notify("Request failed: " + textStatus);
        });
    };

    $(".btnDelete").on("click", deleteFunc);

    $("#AddForm").on("submit", function (e) {
        var name = $("#CourseAddName").val();
        var score = $("#CourseAddScore").val();
        $.ajax({ url: "/ManageCourse/Add", data: { name: name, score: score }, method: "POST" }).done(function (result) {
            console.log(result);
            if (result.isSucessfull) {
                $("#CourseAddName").val("");
                $("#CourseAddScore").val("");

                var tr = $("<tr />");
                var tdName = $("<td />").html(name);
                var tdScore = $("<td />").html(score);
                var tdStudentCount = $("<td />").html(0);

                tr.append(tdName);
                tr.append(tdScore);
                tr.append(tdStudentCount);

                var urlUpdate = "/ManageCourse/Update?courseId=" + result.id;
                var aUpdate = $("<a/>");
                aUpdate.html("Update");
                aUpdate.attr("href", urlUpdate);
                aUpdate.addClass("btn btn-default");
                var tdUpdate = $("<td/>").append(aUpdate);

                tr.append(tdUpdate);

                var btnDelete = $("<button/>");
                btnDelete.text("Delete");
                btnDelete.addClass("btn btn-default btnDelete");
                btnDelete.data("courseid", result.id);
                btnDelete.on("click", deleteFunc);
                tr.append($("<td/>").append(btnDelete));

                $("#courseList").append(tr);

                $.notify("Course add successfully", { className: 'success' });
            } else {
                $.notify("Course add fail", { className: 'error' });
            }
        }).fail(function (jqXHR, textStatus) {
            if (jqXHR.status == 403) {
                window.location.href = "/";
            }

            $.notify("Request failed: " + textStatus);
            });

        e.preventDefault();
    });
})();