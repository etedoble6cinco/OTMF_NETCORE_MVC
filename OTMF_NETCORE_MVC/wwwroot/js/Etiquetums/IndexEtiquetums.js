
$(document).ready(function () {


    $("#Etiquetas").DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                text: '<i class="fa fa-plus" aria-hidden="true"><div class="badge"></div></i>',
                tittleAttr: 'creadas hoy',
                className: 'btn btn-lg btn-secondary',
                action: function () {

                    window.location.href = "../../Etiquetums/Create";

                }
            }
        ]
    });


    var menuItem = document.getElementById("EtiquetumsMenuItem");
    menuItem.classList.add("active");


});
function ObtenerEtiquetaDeCaja(data) {
    $("#EtiquetaPieza").html("");
    $("#EtiquetaPieza").append("<div><img width='250' height='200' class='card' src='Uploads//Etiquetas/Partes/" + data + ".jpg'/></div>");
}
