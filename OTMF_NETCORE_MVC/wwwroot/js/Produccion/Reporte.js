


$(document).ready(()=>{
    console.log("js file");
});


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
            console.log(data)
            $("#ResultDownload").append("<a href="+data.data+">Descargar</a>");
        }
    });
}