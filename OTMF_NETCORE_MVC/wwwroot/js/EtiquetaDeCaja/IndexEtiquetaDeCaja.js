
$(document).ready(function () {


    $("#EtiquetaDeCaja").DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                text: '<i class="fa fa-plus" aria-hidden="true"><div class="badge"></div></i>',
                tittleAttr: 'creadas hoy',
                className: 'btn btn-lg btn-secondary',
                action: function () {

                    window.location.href = "@Url.Action("Create")";

                }
            }
        ]
    });




});
function ObtenerEtiquetaDeCaja(data) {
    $("#EtiquetaDeCajaImage").html("");
    $("#EtiquetaDeCajaImage").append("<div><img width='250' height='180' class='card' src='/Uploads//Etiquetas/Cajas/" + data + ".jpeg'/></div>");
}



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
        url: "",
        type: "POST",
        data: filedata,
        processData: false,
        contentType: false,
        success: function (result) {
            window.location.href = "@Url.Action("Index")";
        },
        error: function () {
        }
    });
}