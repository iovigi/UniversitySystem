(function () {
    var hideAllRegButtons = function () {
        $(".register-btn").hide();
    };

    var showAllRegButtons = function () {
        $(".register-btn").show();
    };

    var registerFunc = function () {
        var btnSelf = $(this);
        var id = btnSelf.data("courseid");
        $.ajax({ url: "Course/RegisterCourse", data: { courseId: id }, method: "POST" }).done(function (result) {
            console.log(result);
            if (result.isSuccessfull) {
                btnSelf.off();
                var tr = btnSelf.parent().parent();
                btnSelf.text("Unregister");
                btnSelf.removeClass("register-btn");
                btnSelf.addClass("un-register-btn");
                btnSelf.on("click", unregisterFunc);
                $("#registeredCourses").append(tr);

                if (result.canRegisterMore) {
                    showAllRegButtons();
                } else {
                    hideAllRegButtons();
                }

                $.notify("Course registred successfully", { className: 'success' });
            } else {
                $.notify("Course registred fail", { className: 'error' });
            }
        }).fail(function (jqXHR, textStatus) {
            if (jqXHR.status == 403) {
                window.location.reload();
            }

            $.notify("Request failed: " + textStatus);
        });
    };

    var unregisterFunc = function () {
        var btnSelf = $(this);
        var id = btnSelf.data("courseid");
        $.ajax({ url: "Course/UnregisterCourse", data: { courseId: id }, method: "POST" }).done(function (result) {
            if (result.isSuccessfull) {
                btnSelf.off();
                var tr = btnSelf.parent().parent();
                btnSelf.text("Register");
                btnSelf.removeClass("un-register-btn");
                btnSelf.addClass("register-btn");
                btnSelf.on("click", registerFunc);
                $("#notRegisteredCourses").append(tr);

                if (result.canRegisterMore) {
                    showAllRegButtons();
                } else {
                    hideAllRegButtons();
                }

                $.notify("Course unregistred successfully", { className: 'success' });
            } else {
                $.notify("Course unregistred fail", { className: 'error' });
            }
        }).fail(function (jqXHR, textStatus) {
            if (jqXHR.status == 403) {
                window.location.reload();
            }

            $.notify("Request failed: " + textStatus);
        });
    };

    $(".register-btn").on("click", registerFunc);
    $(".un-register-btn").on("click", unregisterFunc);
})();
