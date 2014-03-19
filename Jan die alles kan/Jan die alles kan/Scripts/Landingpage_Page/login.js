$(document).ready(function () {
    $('#login').click(function (event) {
        event.preventDefault();

        $.post("http://localhost:52802/Ajax/FormLogin", { username: $('input[name="username"]').val(), password: $('input[name="password"]').val() }, function (data) {
            if (data == "ad") {
                window.location = "http://localhost:52802/Dashboard/Index";
            }
            else if (data == "de") {
                alert("Invalid login");
            }
            else if (data == "us") {
                window.location = "http://localhost:52802/Page/Overview";
            }
            else {
                alert("Invalid login");
            }
        });
    });
});