$(document).ready(function () {
    ObtenerFraccionEstandarRelevo();
    ObtenerPorcentajeScrapPermitido();
    ObtenerTurnoOt();

});

function ObtenerPorcentajeScrapPermitido() {

    $.ajax({
        type: 'GET',
        url: "../../Configuracion/ObtenerPorcentajeScrapPermitido",
        dataType: 'json',
        success: function (data) {

            $("#ScrapPermitido").html("");
            $("#ScrapPermitido").append("<p>" + data.data[0].porcentajeScrapPermitido + "</p>");
        }
    })

}
function UpdatePrimerHorario() {
    var HorasTrabajadasPrimerTurno = document.getElementById("HorasTrabajadasPrimerHorario").value;
    var NombrePrimerTurno = document.getElementById("NombreTurnoPrimerHorario").value;
    console.log(NombreTurnoPrimerHorario);
    $.ajax({
        type: 'POST',
        url: '../../Configuracion/UpdatePrimerTurnoOt',
        dataType: 'json',
        data: {
            NombreTurno: NombrePrimerTurno,
            HorasTrabajadas: HorasTrabajadasPrimerTurno
        },
        success: function (data) {

            ObtenerTurnoOt();
            $("#PrimerHorario").modal('hide');

        }
    });
}
function UpdateSegundoHorario() {
    var HorasTrabajadasSegundoTurno = document.getElementById("HorasTrabajadasSegundoTurno").value;
    var NombreSegundoTurno = document.getElementById("NombreSegundoTurno").value;
    $.ajax({
        type: 'POST',
        url: '../../Configuracion/UpdateSegundoTurnoOt',
        dataType: 'json',
        data: {
            NombreTurno: NombreSegundoTurno,
            HorasTrabajadas: HorasTrabajadasSegundoTurno
        },
        success: function (data) {

            ObtenerTurnoOt();
            $("#SegundoHorario").modal('hide');
        }
    });

}
function ObtenerTurnoOt() {
    $.ajax({
        type: 'GET',
        url: '../../Configuracion/ObtenerTurnoOt',
        dataType: 'json',
        success: function (data) {

            $("#HorariosContent").html("");
            $("#HorariosContent").append("<tr><td>" + data.data[0].nombreTurno + "</td>" +
                "<td>" + data.data[0].horasTrabajadas + "</td>" +
                "<td><button class='btn btn-primary' data-bs-toggle='modal' data-bs-target='#PrimerHorario'>Editar</button></td></tr>");
            $("#HorariosContent").append("<tr><td>" + data.data[1].nombreTurno + "</td>" +
                "<td>" + data.data[1].horasTrabajadas + "</td>" +
                "<td><button class='btn btn-primary' data-bs-toggle='modal' data-bs-target='#SegundoHorario'>Editar</button></td></tr>");
        }
    })
}
function ObtenerFraccionEstandarRelevo() {
    $.ajax({
        type: 'GET',
        url: '../../Configuracion/ObtenerFraccionEstandarRelevo',
        dataType: 'json',
        success: function (data) {

            $("#FraccionEstandar").html("");
            $("#FraccionEstandar").append("<p>" + data.data[0].fracEstandarRelevo +
                "</p>");
        }
    });
}
function UpdateFraccionEstandarRelevo() {
    var FraccionEstandarRelevo = document.getElementById("FraccionEstandarRelevo").value;
    $.ajax({
        type: 'POST',
        url: '../../Configuracion/UpdateFraccionEstandarRelevo',
        dataType: 'json',
        data: {
            FracEstandarRelevo: FraccionEstandarRelevo
        },
        success: function (data) {

            ObtenerFraccionEstandarRelevo();
            $('#FraccionEstandarModal').modal('hide');
        }
    });
}
function UpdatePorcentajeScrapPermitido() {
    var PorcentajeScrapPermitido = document.getElementById("PorcentajeScrapPermitido").value;
    $.ajax({
        type: 'POST',
        url: '../../Configuracion/UpdatePorcentajeScrapPermitido',
        dataType: 'json',
        data: {
            PorcentajeScrapPermitido: PorcentajeScrapPermitido
        },
        success: function (data) {
            ObtenerPorcentajeScrapPermitido();
            $('#ScrapPermitidoModal').modal('hide');
        }

    });
}