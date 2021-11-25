
$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    var htmlVal = $(this).siblings(".custom-file-label").html();
    //alert(fileName + " " + htmlVal);
});