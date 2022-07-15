    "use strict";

    var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

    $(function (){
    connection.start().then(function () {
            //alert("conexion exitosa");
            InvokeOrdenTrabajo();
        }).catch(function (err) {
            return console.error(err.toString());
        });
    });

    function InvokeOrdenTrabajo() {
        connection.invoke("SendOrdenTrabajo").catch(function (err) {
            return console.error(err.toString());
        });
    }
connection.on("ReceivedOrdenTrabajo", function (data) {
    $("#OrdenesDeTrabajo tbody").html("");
    console.log(data);
    $.each(data.data, function (n) {
        $("#OrdenesDeTrabajo tbody").append("<tr>" +
            "<td>" + data.data[n].idCodigoOrdenTrabajo + "</td>" +
            "<td>" + data.data[n].idCodigoParte + "</td>" +
            "<td>" + data.data[n].idEstadoOrden + "</td>" +
            "<td> " + data.data[n].nombreEstadoOrden + "</td>" +
            "<td> " + data.data[n].horaInicio + "</td>" +
            "<td> " + data.data[n].horaFinalizacion + "</td>");
                               
    });
   
     
    });


    




