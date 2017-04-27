
function search() {
    $("#pattern").focus();

    var id = $("#id").val();
    var pattern = $("#pattern").val();
    var source = $("#Source").val();
    if (pattern !== "") {
        $.ajax({
            url: "api/search",
            data: {
                "id": id,
                "pattern": pattern,
                "source": source
            },
            type: "POST",
            dataType: "JSON"
        })
            .done(function (json) {
                $("#result").html("");

                if (json.status) {
                    $("#result").append("<div class=\"status\">" + "Hasil pencarian : " + json.data.length + "</div>");
                    for (var res in json.data) {
                        $("#result").append("<img src= \"" + json.data[res].ImageUrl + "\">");
                        $("#result").append("<div class=\"article\"><h3><a href = \"" + json.data[res].Url + "\" target = \"_blank\">" + json.data[res].Title + "</a></h3><div class=\"date\">Publication date : " + json.data[res].PubDate + "</div><p>" + json.data[res].Match + "</p></div>");
                        $("#result").append("<div class=\"clear\"><hr/>");
                    }
                }
                else {
                    $("#result").append("<p>Error : " + json.data + "</p>");
                }
            })
            .fail(function (xhr, status, errorThrown) {
                $("#result").html("");
                $("result").append("<p>Error :" + errorThrown + "</p>");
            });
    } else {
        $("#result").html("");
    }
}

function scrape() {
    $("#scrape_result").html("");
    $("#scrape_result").append("<i class=\"fa fa-circle-o-notch fa-spin\" style=\"font-size:24px;line-height:40px;\"></i>");

    $.ajax({
        url: "/api/scraper",
        method: "GET",
        dataType: "JSON"
    })
        .done(function (json) {
            $("#scrape_result").html("");

            if (json.status) {
                $("#scrape_result").append("<p>Scrape success!</p>");
            }
            else {
                $("#scrape_result").append("<p>Scrape error: " + json.data + "</p>");
            }
        })
        .fail(function (xhr, status, errorThrown) {
            $("#scrape_result").html("");
            $("#scrape_result").append("<p>Scrape error: " + errorThrown + "</p>");
        });
}