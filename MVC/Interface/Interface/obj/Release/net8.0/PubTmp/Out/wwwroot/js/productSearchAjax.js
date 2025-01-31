

document.getElementById("txt-search").value;

function getProduct(event) {
    var url_ = "/recproduct/searchproduct";
    $.ajax(
        {
            url: url_,
            data: { "text": event.target.value },
            success: function (result) {
                document.getElementById("table-of-content").innerHTML = result;
            }
        }
    )
}