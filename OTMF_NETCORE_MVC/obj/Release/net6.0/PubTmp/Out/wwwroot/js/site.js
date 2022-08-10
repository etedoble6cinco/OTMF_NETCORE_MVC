


function GetDetailsPartes() {
    $.ajax({
        type: 'GET',
        url: '@Url.Action("ObtenerUltimoIdOT")',
        dataType: 'json',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        success: function (data) {
            console.log(data);

        }
    }); 

}
function GetDetailsOrdenesTrabajo() {

}
