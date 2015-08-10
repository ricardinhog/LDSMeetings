$(document).ready(function () {
    $("input[type=email]").each(function () {
        if (!$(this).hasClass("form-control")) {
            $(this).addClass("form-control");
            $(this).prop("placeholder", "Endereço de email"); // TODO: check how to use resources inside js file
            $(this).prop("maxlength", "100");
        }
    });

    $("input[type=text]").each(function () {
        if (!$(this).hasClass("form-control")) {
            $(this).addClass("form-control");
        }
    });

    $("input[type=password]").each(function () {
        if (!$(this).hasClass("form-control")) {
            $(this).addClass("form-control");
        }
    });

    $("select").each(function () {
        if (!$(this).hasClass("form-control")) {
            $(this).addClass("form-control");
        }
    });

    $("#member").keypress(function () {
        var text = $("#member").val();
        search("member", text);
    });

    $(".hymn").keypress(function () {
        var text = $(this).val();
        search("hymn", text);
    });
});
function search(obj, keyword) {
    $("#result").html("");
    $("#id").val("0");

    if (obj == "member") {
        $.getJSON("/JSON/SearchMember", "keyword=" + keyword + "&cache=" + new Date().getMilliseconds, function (data) {
            $("#result").html("");
            $.each(data, function (index, value) {
                $("#result").append("<input type='radio' value='" + value.Id + "' name='resultList' onclick=\"select(this.value, '" + value.LastName + ", " + value.FirstName + "');\" />&nbsp;");
                $("#result").append(value.LastName + ", " + value.FirstName + "<br />");
            });
        });
    } else if (obj == "hymn") {
        $.getJSON("/JSON/SearchMusic", "keyword=" + keyword + "&cache=" + new Date().getMilliseconds, function (data) {
            $("#result").html("");
            $.each(data, function (index, value) {
                $("#result").append("<input type='radio' value='" + value.Id + "' name='resultList' onclick=\"select(this.value, '" + value.Name + "');\" />&nbsp;");
                $("#result").append(value.Name + "<br />");
            });
        });
    }
}
function selectMember(id, text) {
    $("#id").val(id);
}