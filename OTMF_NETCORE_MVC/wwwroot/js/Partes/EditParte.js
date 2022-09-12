$(document).ready(function () {
    localStorage.clear();
    ObtenerAccesoriosByParteId();
});

function ObtenerAccesoriosByParteId() {
    var x = document.getElementById("IdParte").value;
    $.ajax({
        type: 'POST',
        url: '../../OTEstado/ObtenerAccesorioByParteId',
        dataType: 'json',
        data: {
            IdParte: x
        },
        success: function (data) {
            console.log(data);
            FillAccesoriosAsignados(data);

        }
    });
}
function FillAccesoriosAsignados(data) {
    $("#partesAsignadas").html("");
    $.each(data.data, function (n) {
        console.log(data.data);
        $("#partesAsignadas").append("<section id='accesoriosAsignados' class='p-1 m-1 bg-secondary'>"
            + "<div class='badge bg-light text-dark'><h5>Accesorios</h5><p>"
            + data.data[n].NombreAccesorio + "</p></div>" +
            "<button class='btn btn-lg btn-primary m-1 p-2' type='button' data-bs-toggle='modal' data-bs-target='#modalAccessoriosUpdate' onclick=\"SetEditAccesorioAsignado(\'" + data.data[n].IdParteAccesorio + "\');\"><i class='fas fa-edit'></i></button>" +
            "<button class='btn btn-lg btn-primary m-1 p-2' type='button' onclick=\"DeleteAsignacionAccesorioById(\'" + data.data[n].IdParteAccesorio + "\');\"><i class='fa fa-trash' aria-hidden='true'></i></button> </section>");
    });
}
function InsertAccesorio() {
    var x = document.getElementById("IdParte").value;
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
function UpdateAsignacionAccesorioById() {
    var IdParteAccesorio = localStorage.getItem("ParteAccesorio");
    var IdAccesorio = document.getElementById("SelectAccesorios").value;
    $.ajax({
        type: 'POST',
        url: '../../Partes/UpdateAsignacionAccesorioById',
        dataType: 'json',
        data: {
            IdParteAccesorio: IdParteAccesorio,
            IdAccesorio: IdAccesorio
        },
        success: function (data) {
            console.log(data);
        }
    });


}
function SetEditAccesorioAsignado(IdParteAccesorio) {
    localStorage.setItem("ParteAccesorio", IdParteAccesorio);
}
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