$(document).ready(function () {
    ObtenerDetallesOT();
});


function ObtenerDetallesOT() {
    var IdOrdenTrabajo = document.getElementById("IdOrdenTrabajo").value;
    $.ajax({
        method: "POST",
        url: "../../OrdenTrabajoes/ObtenerOrdenesTrabajoDetallesById",
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        dataType: "json",
        success: function (data) {
            console.log(data);

            $("#OTTittle").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Codigo de Parte</p> <p>" + data.data[0].IdCodigoParte + "</p></div>");
            $("#OTTittle").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Estado de la orden de trabajo</p><p>" + data.data[0].NombreEstadoOrden + "</p></div>");

            $("#OTDetails").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Fecha de Creacion</p><p>" + data.data[0].FechaOrdenTrabajo + "</p></div>");
            $("#OTDetails").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Fecha de Finalizacion</p><p>" + data.data[0].HoraFinalizacion + "</p></div>");
            $("#OTDetails").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Fecha de Inicio</p><p>" + data.data[0].HoraInicio + "</p></div>");


            $("#OTDetailsCal").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Estandar</p><p>" + data.data[0].EstandarCalculado + "</p></div>");
            $("#OTDetailsCal").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Estandar Con Relevo</p><p>" + data.data[0].EstandarConRelevoCalculado + "</p></div>");
            $("#OTDetailsCal").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Fraccion Estandar Con Relevo</p><p>" + data.data[0].FracEstandarConRelevo + "</p></div>");
            $("#OTDetailsCal").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Estandar Por Hora</p><p>" + data.data[0].EstandarPorHorasCalculado + "</p></div>");
            $("#OTDetailsCal").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Horas Trabajadas</p><p>" + data.data[0].HorasTrabajadasCalculado + "</p></div>");
            $("#OTDetailsCal").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Porcentaje de Scrap Permitido</p><p>" + data.data[0].PorcentajeScrapCalculado + "</p></div>");
            $("#OTDetailsCal").append("<div class='p-1 m-1 border border-dark'><p class='badge bg-primary'>Scrap </p><p>" + data.data[0].ScrapCalculado + "</p></div>");
        }

    });
}

function FillDetallesOT() {

}
