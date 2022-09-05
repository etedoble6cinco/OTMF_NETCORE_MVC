

$(document).ready(function () {


    $("#Cajas").DataTable({
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


    ObtenerImagenCaja();

});


function ObtenerImagenCaja() {

    var EtiquetaImagen = document.getElementById("EtiquetaDeCaja").innerHTML;
    console.log(EtiquetaImagen);
    $("#imagenCaja").append("<img class='rounded mx-auto d-block' src='/Uploads/Etiquetas/Cajas/" + EtiquetaImagen + ".jpeg'/>");

}