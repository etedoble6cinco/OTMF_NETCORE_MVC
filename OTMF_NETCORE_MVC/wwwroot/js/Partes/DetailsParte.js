$(document).ready(function () {
    ObtenerDetalleParteById();
    ObtenerAccesoriosByParteId();
});
function DeleteAsignacionAccesorioById(IdParteAccesorio) {
    $.ajax({
        type: 'POST',
        url: '../../Partes/DeleteAccesorioByParteId',
        dataType: 'json',
        data: {

            IdParteAccesorio: IdParteAccesorio

        },
        success: function (data) {
            ObtenerAccesoriosByParteId();
        }
    });
}
function ObtenerAccesoriosByParteId() {
    var IdParte = document.getElementById("idParte").value;
    $.ajax({
        type: "POST",
        url: "../../OTEstado/ObtenerAccesorioByParteId",
        dataType: "json",
        data: {
            IdParte: IdParte
        },
        success: function (data) {
            FillAccesorios(data);
        }
    });
}
function FillAccesorios(data) {
    if (data.data.length == 0) {
        $("#partesAsignadas").html("");
        $("#partesAsignadas").append("<section><p class='badge bg-warning'>No tiene accesorios asignados</p></section>");
    } else {
        $("#partesAsignadas").html("");
        $.each(data.data, function (n) {
            console.log(data.data);
            $("#partesAsignadas").append("<section id='accesoriosAsignados' class='p-1 m-1 bg-secondary row'>"
                + "<div><p class='badge bg-primary'>"
                + data.data[n].NombreAccesorio + "</p><button class=' badge bg-danger m-1 p-2' type='button' onclick=\"DeleteAsignacionAccesorioById(\'" + data.data[n].IdParteAccesorio + "\');\"><i class='fa fa-trash' aria-hidden='true'></i></button></div>" +

                " </section>");
        });
    }
   
}
function InsertAccesorio() {
    var x = document.getElementById("idParte").value;
    var IdAccesorio = document.getElementById("SelectAccesorios").value;
    console.log(IdAccesorio);
    $.ajax({
        type: 'POST',
        url: '../../Partes/InsertAccesorioByParteId',
        dataType: 'json',
        data: {
            IdParte: x,
            IdAccesorio: IdAccesorio
        },
        success: function (data) {
            ObtenerAccesoriosByParteId();
        }
    });

}
function ObtenerDetalleParteById() {
    var IdParte = document.getElementById("idParte").value;
    $.ajax({
        type: "POST",
        url: "../../Partes/ObtenerDetalleParteById",
        dataType: "json",
        data: {
            IdParte: IdParte
        },
        success: function (data) {
            console.log(data);
            $("#DetalleParte").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Caja           </p><p>" + data.data[0].NombreCaja +    "</p></div>");
            $("#DetalleParte").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Cliente        </p><p>" + data.data[0].NombreCliente + "</p></div>");
            $("#DetalleParte").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Color          </p><p>" + data.data[0].NombreColor +   "</p></div>");
            $("#DetalleParte").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Ensamble       </p><p>" + data.data[0].NombreEnsamble +"</p></div>");
            $("#DetalleParte").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Etiqueta       </p><p>" + data.data[0].NombreEtiqueta +"</p></div>");
            $("#DetalleParte").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Hule           </p><p>" + data.data[0].NombreHule +    "</p></div>");
            $("#DetalleParte").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Inserto        </p><p>" + data.data[0].NombreInserto + "</p></div>");
            $("#DetalleParte").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Molde          </p><p>" + data.data[0].NombreMolde +   "</p></div>");
            $("#DetalleParte").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Pintura        </p><p>" + data.data[0].NombrePintura + "</p></div>");
            $("#DetalleParte").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Tarima         </p><p>" + data.data[0].NombreTarima +  "</p></div>");
            $("#DetalleParte").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Piezas por Caja</p><p>" + data.data[0].PiezasPorCaja + "</p></div>");
           
        }


    })
}