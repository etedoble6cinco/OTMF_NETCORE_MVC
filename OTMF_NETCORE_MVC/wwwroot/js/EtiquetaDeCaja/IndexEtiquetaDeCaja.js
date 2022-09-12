
$(document).ready(function () {


    $("#EtiquetaDeCaja").DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                text: '<i class="fa fa-plus" aria-hidden="true"><div class="badge"></div></i>',
                tittleAttr: 'creadas hoy',
                className: 'btn btn-lg btn-secondary',
                action: function () {

                    window.location.href = "../../EtiquetaDeCaja/Create";

                }
            }
        ]
    });




});
function ObtenerEtiquetaDeCaja(data) {
    $("#EtiquetaDeCajaImage").html("");
    $("#EtiquetaDeCajaImage").append("<div><img width='250' height='180' class='card' src='/Uploads//Etiquetas/Cajas/" + data + ".jpeg'/></div>");
}
