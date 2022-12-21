
var customAccessorSeconds = function (value, data, type, params, column, row) {
    const totalMinutes = Math.floor(value / 60);

    const seconds = value % 60;
    const hours = Math.floor(totalMinutes / 60);
    const minutes = totalMinutes % 60;

    return hours + ":" + minutes + ":" + seconds;
}



document.getElementById("download-xlsx").addEventListener("click", function () {
    table.download("xlsx", "Reporte-Produccion.xlsx", { sheetName: dataTable });
});
var table;
var dataTable;
function ObtenerReporteProduccion() {

    var ReporteDate = $("#FechaReporte").val();
    $.ajax({
        type: 'POST',
        url: '../../Produccion/ObtenerReporteProduccion',
        dataType: 'json',
        data: {
            ReporteDate: ReporteDate
          
        },
        success: function (data) {
            dataTable = data.data;
            console.log(data);
         



             table = new Tabulator("#x-table", {
                data: data.data,
                renderHorizontal: "virtual",
                pagination: "local",
                paginationSize: 6,
                paginationSizeSelector: [3, 6, 8, 10],
             
                paginationCounter: "rows",// enable responsive layouts
                columns: [ //set column definitions for imported table data
                    { title: "Linea", field: "nombreMaquina", responsive: 0 }, // this column wil never be hidden
                    { title: "No. Operador", field: "claveEmpleado", responsive: 0 }, // hidden first 
                    { title: "No. Parte", field: "codigoParte", responsive: 0 }, // hidden first
                    { title: "Cavidades", field: "numeroCavidades" }, // hidden fifth
                    { title: "Horas Turno", field: "horasTrabajadasCalculado" }, // hidden fifth
                    { title: "Hrs Acumuladas", field: "horasTrabajadasAcumulado", mutator: customAccessorSeconds, mutatorParams: {}, formatter: "textarea" }, // hidden fifth
                    { title: "Hrs Pausa", field: "pausa", responsive: 0, mutator: customAccessorSeconds, mutatorParams: {}, formatter: "textarea" }, // hidden third
                    { title: "Hrs Activa", field: "activa", responsive: 0, mutator: customAccessorSeconds, mutatorParams: {}, formatter: "textarea" }, // hidden third
                    { title: "Estandar por Hora", field: "estandarPorHorasCalculado", responsive: 0 }, // hidden second
                    { title: "Estandar Propuesto", field: "estandarCalculado", responsive: 0 }, // hidden second
                    { title: "Produccion", field: "numeroPiezasRealizadas", sorter: "number", bottomCalc: "sum", bottomCalcParams: { precision: 3 } },
                   
                  
                ],
                
              
            });
   
        }
    });
}







