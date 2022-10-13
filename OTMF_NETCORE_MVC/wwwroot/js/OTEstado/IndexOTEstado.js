$(document).ready(function () {

    $("#mainNav").hide();
  

});

function GetMaquinas() {

    $.ajax({
        type: "GET",
        url: "OTEstado/ObtenerMaquinas",
        contentType: "applicacion/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            FillMaquinas(data);
        }
    });
}
function FillMaquinas(data) {

    $("#ContenedorMaquina").html("");
    var x = 0;
    console.log(data)
    $.each(data.data, function (n) {

        $("#ContenedorMaquina").append(
            " <a  class='btn btn-light ' asp-action='OrdenesDeTrabajoAsignadas' asp-route-id=" + data.data[x].idMaquina + ">" +
            " <div class='border-3 p-5 m-3 bg-success shadow-lg'> " +
            "<small class='text-white'> " + data.data[x].nombreMaquina + "</small></div></a>");

        x++;
    });


}

