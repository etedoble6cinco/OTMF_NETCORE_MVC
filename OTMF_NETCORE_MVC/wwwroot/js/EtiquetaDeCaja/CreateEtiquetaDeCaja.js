$(document).ready(function () {

});
function f() {
    var uploadfile = $('#imageupload').get(0);
    var files = uploadfile.files;
    var filedata = new FormData();
    var NombreCaja = $("#NombreEtiquetaDeCaja").val();
    var LogoCaja = "Nombre Logo Caja";
    for (var i = 0; i < files.length; i++) {
        filedata.append(LogoCaja, files[i]);
    }
    filedata.append(NombreCaja, "NombreCaja");

    $.ajax({
        url: "../../EtiquetaDeCaja/UploadImage",
        type: "POST",
        data: filedata,
        processData: false,
        contentType: false,
        success: function (result) {
            window.location.href = "../../EtiquetaDeCaja/Index";
        },
        error: function () {
        }
    });
}
