﻿


$(document).ready(function () {
    $("#mainNav").hide();
    GetIdentificadorOT();

   
});
//METODO PARA OBTENER LA INFORMACION DE LAS PESTANAS 
function GetIdentificadorOT() {
    var arr = $("#OtList button #IdentificadorOT").map(function () {
        return this.textContent;
    }).get();

    for (let i = 0; i < arr.length; i++) {
        $.ajax({
            type: "POST",
            url: '../../OTEstado/GetIdentificadorOT',
            data: { id: arr[i] },
            dataType: "json",
            success: function (ot) {
                
                document.getElementById(arr[i]).innerText = ot.ot.idCodigoOrdenTrabajo;
            }, error: function (err) {
                console.log(err)
            }
        });
    }

}
// METODO PARA OBTENER TODA LA INFORMACION DE LAS ORDENES DE TRABAJO , CUANDO SE DA CLICK EN ELLA 
function GetOrdenTrabajo(idOrdenTrabajo) {

    localStorage.clear();

    localStorage.setItem('currentOT', idOrdenTrabajo);
    GetEstadoOT();
    $.ajax({
        type: "POST",
        url: '../../OTEstado/DetalleDeOT',
        data: { id: idOrdenTrabajo },
        dataType: "json",
        success: function (data) {

            FillDetalles(data);
            GetParteByOTId(idOrdenTrabajo);
            GetEmpleadosByOTId(idOrdenTrabajo);
            ObtenerCajasRecibidas();
            ObtenerCajasRealizadas();
            ObtenerFechaInicio();
            ObtenerFechaFinalizacion();
            ObtenerParteIdByOTId();
        }
    });
}
function FillDetalles(data) {

    $("#pills-tabContent").html("");
    $("#part-Image").html("");
    var x = 0;

    $.each(data.data, function (n) {

        $("#pills-tabContent").append(

            "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary'>Codigo Orden de Trabajo</p><p>" + data.data[x].idCodigoOrdenTrabajo + "</p></div>"

            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary'>Numero de Parte</p><p>" + data.data[x].idCodigoParte + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary'>Cantidad Piezas por Orden</p><p>" + data.data[x].cantidadPiezasPorOrden + "</p></div>"



        );


        x++;
    });


}
function GetParteByOTId(idOrdenTrabajo) {



    $.ajax({
        type: "POST",
        url: '../../OtEstado/ObtenerParteByOTId',
        data: { id: idOrdenTrabajo },
        dataType: "json",
        success: function (data) {
            
            FillParte(data);
        }
    });
}

function truncate(num, places) {
    return Math.trunc(num * Math.pow(10, places)) / Math.pow(10, places);
}
function FillParte(data) {

    var x = 0;

    $.each(data.data, function (n) {



        $("#pills-tabContent").append("<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap'>Aluminio</p>        <p class='fs-6'>" + truncate(data.data[x].aluminio, 3) + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap'>Cajas por Tarima</p><p class='fs-6'>" + data.data[x].cajasPorTarima + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap'>Caja</p>            <p class='fs-6'>" + data.data[x].nombreCaja + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap'>Cliente</p>         <p class='fs-6'>" + data.data[x].nombreCliente + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap'>Color</p>           <p class='fs-6'>" + data.data[x].nombreColor + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap'>Ensamble</p>        <p class='fs-6'>" + data.data[x].nombreEnsamble + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap'>Hule</p>            <p class='fs-6'>" + data.data[x].nombreHule + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap'>Inserto</p>         <p class='fs-6'>" + data.data[x].nombreInserto + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap'>Molde</p>           <p class='fs-6'>" + data.data[x].nombreMolde + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap'>Pintura</p>         <p class='fs-6'>" + data.data[x].nombrePintura + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap'>Tarima</p>          <p class='fs-6'>" + data.data[x].nombreTarima + "</p></div>" +
              "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap'>Instructivo</p>          <p class='fs-6'>" + data.data[x].nombreInstructivoPieza + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap'>Piezas por Caja</p> <p class='fs-6'>" + data.data[x].piezasPorCaja + "</p></div>");
        $("#part-Image").append("<div class='d-flex flex-wrap'><img  class='img-fluid' style='max-width:50%;'  src='/Uploads//Etiquetas/Partes/" + data.data[x].nombreEtiqueta + ".jpg' />"
            + "<img class='img-fluid' style='max-width:50%;' src='/Uploads//Etiquetas/Cajas/" + data.data[x].nombreEtiquetaDeCaja + ".jpg' /></div>")

        x++;
    });



}
function DeleteAsignacionEmpleadoOTById(id) {
    $.ajax({
        type: "Delete",
        url: '../../OTEstado/DeleteAsignacionEmpleadoOTById',
        data: { idEmpleadoOrdenTrabajo: id },
        dataType: "json",
        success: function (data) {
            var idcurrentOT = localStorage.getItem('currentOT');
            GetEmpleadosByOTId(idcurrentOT);
        }
    });
}
function GetEmpleadosByOTId(idOrdenTrabajo) {
    $.ajax({
        type: "POST",
        url: '../../OtEstado/ObtenerEmpleadosByOTId',
        data: { id: idOrdenTrabajo },
        dataType: "json",
        success: function (data) {
            FillEmpleados(data, idOrdenTrabajo);
        }
    });


}
function FillEmpleados(data, idOrdenTrabajo) {
    var x = 0;

    $("#EmpleadosRelacionados").html("");

    $.each(data.data, function (n) {

        if (data.data[x].idTipoEmpleadoFK == 5) {
            $("#EmpleadosRelacionados").append("<section id='empleadoEmpacador' class='p-1 m-1 bg-secondary'>"
                + "<div class='badge bg-light text-dark'><h5>Empacador </h5><p>"
                + data.data[x].nombreEmpleado + "</p></div>" +
                "<button class='btn btn-lg btn-primary m-1 p-2' data-bs-toggle='modal' data-bs-target='#staticBackdrop' onclick=\"SetEditEmpleado(\'" + data.data[x].idEmpleadoOrdenTrabajo + "\');\"><i class='fas fa-edit'></i></button>" +
                "<button class='btn btn-lg btn-primary m-1 p-2'  onclick=\"DeleteAsignacionEmpleadoOTById(\'" + data.data[x].idEmpleadoOrdenTrabajo + "\');\"><i class='fa fa-trash' aria-hidden='true'></i></button> </section>");

        }
        if (data.data[x].idTipoEmpleadoFK == 6) {
            $("#EmpleadosRelacionados").append("<section id='empleadoMoldeador' class='p-1 m-1 bg-secondary'>"
                + "<div class='badge bg-light text-dark'><h5>Moldeador </h5><p>"
                + data.data[x].nombreEmpleado + "</p></div>" +
                "<button class='btn btn-lg btn-primary m-1 p-2' data-bs-toggle='modal' data-bs-target='#staticBackdrop' onclick=\"SetEditEmpleado(\'" + data.data[x].idEmpleadoOrdenTrabajo + "\');\"><i class='fas fa-edit' ></i></button>" +
                "<button class='btn btn-lg btn-primary m-1 p-2'  onclick=\"DeleteAsignacionEmpleadoOTById(\'" + data.data[x].idEmpleadoOrdenTrabajo + "\');\"><i class='fa fa-trash' aria-hidden='true'></i></button> </section>");



        }

        x++;
    });

    $("#EmpleadosRelacionados").append("<section class='m-1'>" +
        "<button type='button' class='btn btn-primary m-1'  data-bs-toggle='modal' data-bs-target='#staticBackdrop'>" +
        "Agregar Empleados Asignados</button>"
        + "</section>");


}
function SetEditEmpleado(data) {
    localStorage.setItem('EditRelacion', data);

}
function GuardarCambiosEmpleados() {
    var idMovimiento = localStorage.getItem('EditRelacion');
    var idEmpleado = $("#EleccionEmpleado").val();
    var currentOT = localStorage.getItem('currentOT');

    $.ajax({
        type: "POST",
        url: '../../OtEstado/UpsertAsignacionEmpleadoOTById',
        data: {
            idOrdenTrabajo: currentOT,
            idEmpleado: idEmpleado,
            idRelacion: idMovimiento
        },
        dataType: "json",
        success: function (data) {

            GetEmpleadosByOTId(currentOT);

        }
    });
}

function UpdateOTEstado(idEstadoOT) {

    var idOrdenTrabajo = localStorage.getItem("currentOT");
    $.ajax({
        type: "POST",
        url: '../../OtEstado/UpdateOTEstado',
        data: {
            idOrdenTrabajo: idOrdenTrabajo,
            idEstadoOT: idEstadoOT
        },
        dataType: "json",
        success: function (data) {
            GetEstadoOT();
            GetOrdenTrabajo(idOrdenTrabajo);
        }
    });

}


function GetEstadoOT() {
    var idOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: "POST",
        url: '../../OtEstado/ObtenerEstadoOT',
        data: {
            id: idOrdenTrabajo
        },
        dataType: "json",
        success: function (data) {

            EvaluateOTEstado(data);

        }
    });
}
function EvaluateOTEstado(data) {

    switch (data.data[0].idEstadoOrden) {
        case 7:
            $("#AccionesOrdenTrabajo").html("");
            $("#indicadorEstado").removeClass();
            $("#indicadorEstado").addClass("border p-2 bg-info");
            $("#AccionesOrdenTrabajo").append("<button class='btn btn-success bg-gradient m-2' onclick='SetActiva()'><strong>Iniciar</strong></button>");
            break;
        case 2:
            //$("#AccionesOrdenTrabajo").html("");
            //$("#indicadorEstado").removeClass();
            //$("#indicadorEstado").addClass("border p-2 bg-primary");
            //$("#AccionesOrdenTrabajo").append("<button class='btn btn-primary'>se</button>")
            break;
        case 9:
            $("#AccionesOrdenTrabajo").html("");
            $("#indicadorEstado").removeClass();
            $("#indicadorEstado").addClass("border p-2 bg-success");
            $("#AccionesOrdenTrabajo").append("<button class='btn btn-light bg-gradient m-2' onclick='SetAceptar();' ><strong>Liberar</strong></button>")
            $("#AccionesOrdenTrabajo").append("<button class='btn bg-dark bg-gradient m-2 text-white' onclick='SetPausa();'><strong>Pausar</strong></button>")
            break;
        case 10:
            $("#AccionesOrdenTrabajo").html("");
            $("#indicadorEstado").removeClass();
            $("#indicadorEstado").addClass("border p-2 bg-warning");
            $("#AccionesOrdenTrabajo").append("<button class='btn bg-dark bg-gradient m-2 text-white' onclick='SetTerminar();'><strong>Aceptar</strong></button>")

            break;
        case 11:
            $("#AccionesOrdenTrabajo").html("");
            $("#indicadorEstado").removeClass();
            $("#indicadorEstado").addClass("border p-2 bg-dark");
            $("#AccionesOrdenTrabajo").append("<button class='btn btn-warning bg-gradient m-2' onclick='SetAceptar();'>Regresar</button>")
            ///NOSE USA

            break;
        case 12:
            $("#AccionesOrdenTrabajo").html("");
            $("#indicadorEstado").removeClass();
            $("#indicadorEstado").addClass("border p-2 bg-danger");
            $("#AccionesOrdenTrabajo").append("<button class='btn btn-success bg-gradient m-2' onclick='SetReanudar();'>Reanudar</button>")

            break;
    }

}
function SetTerminar() {
    UpdateFechaFinalizacion();
    UpdateOTEstado(11);
    RegistrarFinalLiberar();
    MostrarInformeTiemposMuertos();
}
function SetReanudar() {
    RegistrarInicioActiva();
    RegistrarFinalDetenida();
    UpdateOTEstado(9);
}
function SetActiva() {
    UpdateFechaInicio();
    UpdateOTEstado(9);
    RegistrarInicioActiva();
}
function SetPausa() {
    $("#MotivoCambioEstadoModal").modal('show');
    var ddc = localStorage.getItem('DropDownChange');

    if (ddc == 'true') {
        UpdateFechaFinalizacion();

        UpdateOTEstado(12);
        RegistrarFinalActiva();
    }

}
function SetAceptar() {

    UpdateOTEstado(10);
    RegistrarFinalActiva();
    RegistrarInicioLiberar();
}
//El valor de cajas recibidas en la tabla de la orden de trabajo sera la llave foreanea para obtener el detalle de la fila .
function ObtenerCajasRecibidas() {
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: 'POST',
        url: '../../OtEstado/ObtenerCajasRecibidas',
        dataType: 'json',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        success: function (data) {

            $("#ShowCajasRecibidas").html("");


            FillCajasRecibidas(data);
        }
    });
}
function UpdateCajasRecibidas() {
    var NumeroCajasRecibidas = $("#CajasRecibidas").val();
    var NumeroPiezasRecibidas = $("#PiezasSueltasRecibidas").val();

    var IdDetalleCajasRecibidas = localStorage.getItem('idDetalleCajasRecibidas');
    $.ajax({
        type: 'POST',
        url: '../../OtEstado/UpdateCajasRecibidas',
        dataType: 'json',
        data: {

            IdDetalleCajasRecibidas: IdDetalleCajasRecibidas,
            NumeroCajasRecibidas: NumeroCajasRecibidas,
            NumeroPiezasRecibidas: NumeroPiezasRecibidas
        },
        success: function (data) {
            ObtenerCajasRecibidas();
        },

    });
}
function FillCajasRecibidas(data) {
    localStorage.setItem('idDetalleCajasRecibidas', data.data[0].idDetalleCajasRecibidas);
    $("#ShowCajasRecibidas").append("<div class='card p-1 m-2 border border-2'><p>Cajas Recibidas</p>" + data.data[0].NumeroCajasRecibidas + "</div>" +
        "<div class='card p-1 m-2 border border-2'><p>Piezas Sueltas Recibidas</p>" + data.data[0].NumeroPiezasSueltasRecibidas + "</div>");
    $("#ShowCajasRecibidas").append("<button type='button' id='btnCajasRealizadas' class='btn btn-primary bg-gradient m-2' data-bs-toggle='modal' data-bs-target='#CajasRecibidasModal'>Registrar material recibido</button>");
    $("#CajasRecibidasModal").modal('hide');

}
function UpdateCajasRealizadas() {
    var PiezasRealizadas = $("#PiezasRealizadas").val();
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: "POST",
        url: '../../OtEstado/UpdatePiezasRealizadas',
        dataType: "json",
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo, ////
            PiezasRealizadas: PiezasRealizadas
        },
        success: function (data) {

            ObtenerCajasRealizadas();
        }
    });

}
function ObtenerCajasRealizadas() {
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: 'POST',
        url: '../../OtEstado/ObtenerPiezasRealizadas',
        dataType: 'json',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        success: function (data) {
            $("#ShowCajasRealizadas").html("");
            FillCajasRealizadas(data);
        }
    });
}
function FillCajasRealizadas(data) {

    $("#ShowCajasRealizadas").append("<div class='card p-1 m-2 border border-2'><p>Piezas Realizadas</p>" + data.data[0].PiezasRealizadas + "</div>");
    $("#ShowCajasRealizadas").append("<button type='button' id='btnPiezasRealizadas' class='btn btn-primary bg-gradient m-2' data-bs-toggle='modal' data-bs-target='#PiezasRealizadasModal'>Registrar material realizado</button>");
    $("#PiezasRealizadasModal").modal('hide');
}
function ObtenerFechaInicio() {

    var IdOrdenTrabajo = localStorage.getItem('currentOT');

    $.ajax({

        type: 'POST',
        url: '../../OtEstado/ObtenerFechaInicio',
        dataType: 'json',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        success: function (data) {

            FillFechaInicio(data);

        }
    });
}
function FillFechaInicio(data) {
    $("#ShowHoraInicio").html("");
    $("#ShowHoraInicio").append("<div><p class='badge bg-primary'> <strong>Fecha de Inicio : </strong>" + data.data[0].HoraInicio + "</p></div>")
}
function FillFechaFinalizacion(data) {
    $("#ShowHoraFinalizacion").html("");
    $("#ShowHoraFinalizacion").append("<div><p class='badge bg-primary'> <strong> Fecha de Finalizacion : </strong>" + data.data[0].HoraFinalizacion + "</p></div>");
}
function UpdateFechaInicio() {
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: 'POST',
        url: '../../OtEstado/UpdateFechaInicio',
        dataType: 'json',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        success: function (data) {

            ObtenerFechaInicio();

        }
    });
}
function UpdateFechaFinalizacion() {
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: 'POST',
        url: '../../OtEstado/UpdateFechaFinalizacion',
        dataType: 'json',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        success: function (data) {
            ObtenerFechaFinalizacion();
        }
    });
}
function ObtenerFechaFinalizacion() {
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: 'POST',
        url: '../../OtEstado/ObtenerFechaFinalizacion',
        dataType: 'json',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        success: function (data) {
            FillFechaFinalizacion(data);
        }
    });
}
function ObtenerAccesorioByParteId(IdParte) {
    $.ajax({
        type: 'POST',
        url: '../../OtEstado/ObtenerAccesorioByParteId',
        dataType: 'json',
        data: {
            IdParte: IdParte
        },
        success: function (data) {
            FillAccesoriosByParteId(data);
        }
    });
}
function ObtenerParteIdByOTId() {
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: 'POST',
        url: '../../OtEstado/ObtenerParteIdByOTId',
        dataType: 'json',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        success: function (data) {
            ObtenerAccesorioByParteId(data.data[0].IdParteFK);

        }
    });
}
function FillAccesoriosByParteId(data) {
    $("#ShowAccesorios").html("");
    $("#ShowAccesorios").append("<li class='list-group-item bg-secondary'><h6 class='badge bg-primary'>Accesorios :</h6></li>");
    $.each(data.data, function (n) {
        $("#ShowAccesorios").append("<li class='list-group-item'><p>" + data.data[n].NombreAccesorio + "</p></li>");
    });
}
// METODO PARA ACTUALIZAR LA DURACION DE LOS ESTADOS 

function RegistrarDuracionEstado(
    TipoMovimientoEstado,
    IdOrdenTrabajo,
    IdMotivoCambioEstadoFK,
    IdEstadoOrdenTrabajoFK) {
    $.ajax({
        type: "POST",
        url: '../../OtEstado/RegistrarDuracionEstado',
        data: {
            TipoMovimientoEstado: TipoMovimientoEstado,
            IdOrdenTrabajoFK: IdOrdenTrabajo,
            IdMotivoCambioEstadoFK: IdMotivoCambioEstadoFK,
            IdEstadoOrdenTrabajoFK: IdEstadoOrdenTrabajoFK
        },
        dataType: "json",
        success: function (data) {

            alertify.notify("Cambio de Estado", data.data.result.nombreEstadoOrden, 1, function () { });
        }
    });
}
function InsertRespuestaCambioEstado() {
    var IdOrdenTrabajoFK = localStorage.getItem('currentOT');
    var IdMotivoCambioEstadoFK = document.getElementById('checkRespuesta').value;
    RegistrarDuracionEstado(0, IdOrdenTrabajoFK, IdMotivoCambioEstadoFK, 12);
    SetPausa();

}
function CheckRespuestaCambioEstado() {

    localStorage.setItem('DropDownChange', true);

}


function RegistrarInicioActiva() {
    var IdOrdenTrabajoFK = localStorage.getItem('currentOT');

    RegistrarDuracionEstado(0, IdOrdenTrabajoFK, 22, 9);
}
function RegistrarFinalActiva() {
    var IdOrdenTrabajoFK = localStorage.getItem('currentOT');

    RegistrarDuracionEstado(1, IdOrdenTrabajoFK, 22, 9);
}

function RegistrarFinalDetenida() {
    var IdOrdenTrabajoFK = localStorage.getItem('currentOT');
    RegistrarDuracionEstado(1, IdOrdenTrabajoFK, 22, 12);
}
function RegistrarInicioLiberar() {
    var IdOrdenTrabajoFK = localStorage.getItem('currentOT');
    RegistrarDuracionEstado(0, IdOrdenTrabajoFK, 22, 10);
}
function RegistrarFinalLiberar() {
    var IdOrdenTrabajoFK = localStorage.getItem('currentOT');
    RegistrarDuracionEstado(1, IdOrdenTrabajoFK, 22, 10);

}
function MostrarInformeTiemposMuertos() {
    var IdOrdenTrabajo = localStorage.getItem("currentOT");
    $.ajax({
        type: "POST",
        url: '../../OtEstado/ObtenerTiemposMuertos',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        dataType: "json",
        success: function (data) {
            FillInformeTiempoMuertos(data);
        }

    })
}
function FillInformeTiempoMuertos(data) {

    console.log(data);
    $.each(data.details, function (n) {

        $("#ResumenContainer").append("<div id='acc-item' class='accordion-item'>" +
            "<h2 class='accordion-header' id='heading-" + data.details[n].idDuracionEstados + "'>" +
            "<button class='accordion-button' type='button' data-bs-toggle='collapse' data-bs-target='#collapse-" + data.details[n].idDuracionEstados + "'><p class='badge bg-primary'>" + n + "</p><p class='badge bg-secondary'>"
            + data.details[n].nombreEstadoOrden + "</p><p class='badge bg-primary'> De: </p><p class='badge bg-secondary'>"
            + FormatearDateEstado(data.details[n].inicioEstado) +
            "</p><p><i class='fas fa-arrow-alt-circle-right' style='font-size:18px'></i></p><p class='badge bg-primary'>A: </p><p class='badge bg-secondary'>" + FormatearDateEstado(data.details[n].finalEstado) + "</p></button></h2>" +
            "<div id='collapse-" + data.details[n].idDuracionEstados + "' class='accordion-collapse collapse' aria-labelled='heading-" + data.details[n].idDuracionEstados + "' data-bs-parent='#accordionExample'>" +
            "<div class='accordion-body'>" +
            "<div class='card'><div class='card-body'>" +
            "<h5 class='card-title'><small class='help-text'>Detalle de registro de tiempo muerto</small></h5>" +
            "<h6 class='card-title'><small class='help-text'>Motivo de Cambio de Estado: </small>" + data.details[n].nombreMotivoCambioEstado + "</h6>"
            + "<div class='d-flex flex-row'><strong>Duracion en Segundos:  </strong><p>" + data.details[n].duracion + "</p></div>"




            + "<div><strong>Derivado</strong><p>" + data.details[n].nombreMotivoCambioEstadoDerivado + "</p></div>" +
            "</div></div></div></div></div>")

    });
    $.each(data.total, function (z) {
        console.log(data.total);

        if (data.total[z].nombreEstado == 'PAUSADA') {
            $("#ResumenSumDetenida").append("<div><p>Duracion: </p><p>" + data.total[z].duracionEstado + "<p></div>")
            $("#ResumenSumDetenida").append("<div><p>Estado: </p><p>" + data.total[z].nombreEstado + "<p></div>")
        }
        if (data.total[z].nombreEstado == 'POR LIBERAR') {
            $("#ResumenSumPorLiberar").append("<div><p>Duracion: </p><p>" + data.total[z].duracionEstado + "<p></div>")
            $("#ResumenSumPorLiberar").append("<div><p>Estado: </p><p>" + data.total[z].nombreEstado + "<p></div>")
        }
        if (data.total[z].nombreEstado == 'ACTIVA') {
            $("#ResumenSumActiva").append("<div><p>Duracion: </p><p>" + data.total[z].duracionEstado + "<p></div>")
            $("#ResumenSumActiva").append("<div><p>Estado: </p><p>" + data.total[z].nombreEstado + "<p></div>")
        }


    });


    $("#ResumenTiemposMuertos").modal("show");

}
function FormatearDateEstado(fecha) {
    let dateTimeEST = new Date(fecha);
    return dateTimeEST.toLocaleTimeString();
}