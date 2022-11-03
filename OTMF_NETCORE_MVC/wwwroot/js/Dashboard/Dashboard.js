    "use strict";

    var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

    $(function (){
    connection.start().then(function () {
            
            InvokeOrdenTrabajo();
        }).catch(function (err) {
            return console.error(err.toString());
        });
        ObtenerTotalPiezas();
        ObtenerMeta();
    });

    function InvokeOrdenTrabajo() {
        connection.invoke("SendOrdenTrabajo").catch(function (err) {
            return console.error(err.toString());
        });
    }
connection.on("ReceivedOrdenTrabajo", function (data) {
  
    $("#OrdenesDeTrabajo tbody").html("");
   
    $.each(data.data, function (n) {
        if (EvaluarFechaOrdenTrabajo(data.data[n].fechaOrdenTrabajo)) { 
        $("#OrdenesDeTrabajo tbody").append("<tr>" +
            "<td>" + data.data[n].idCodigoOrdenTrabajo + "</td>" +
            "<td>" + data.data[n].idCodigoParte + "</td>" +
            "<td>" + EvaluarEstadoOT(data.data[n].idEstadoOrden) + "</td>" +
            "<td> " + data.data[n].nombreEstadoOrden + "</td>" +
            "<td> " + FormatearFechaOT(data.data[n].horaInicio) + "</td>" +
            "<td> " + FormatearFechaOT(data.data[n].horaFinalizacion) + "</td>" +
            "<td> " + data.data[n].nombreMaquina + "</td>" +
            "</tr>");
    }        
    });

  

   
     
});

function EvaluarFechaOrdenTrabajo(data) {
   
            let dateTimeEST = new Date(data);
            let currentTime = new Date();
            if (dateTimeEST.getDate() == currentTime.getDate()) {
                
                return true;      
            }
         
        }

function EvaluarEstadoOT(idEstadoOrden) {

    switch (idEstadoOrden) {
        case 7:  //PLEANEADO
            return "<svg width='80' height='80' class='planeado'> <rect x='10' y='10' rx='10' ry='10' width='20' height='20' style='fill:#00FFE8;' /> </svg>";
            break;
        case 8:  //INICIADO
            return "<svg width='80' height='80' class='iniciado'> <rect x='10' y='10' rx='10' ry='10' width='20' height='20' style='fill:orange;' /> </svg>";
            break;
        case 9:  //ACTIVA 
            return "<svg width='80' height='80' class='activa'> <rect x='10' y='10' rx='10' ry='10' width='20' height='20' style='fill:#1FAB00;' /> </svg>";
            break;
        case 10:  //ACEPTADA
            return "<svg width='80' height='80' class='aceptada' > <rect x='10' y='10' rx='10' ry='10' width='20' height='20' style='fill:yellow;' /> </svg>";
            break;
        case 11:  //TERMINADA
            return "<svg width='80' height='80' class='terminada' > <rect x='10' y='10' rx='10' ry='10' width='20' height='20' style='fill:black;' /> </svg>";
            break;
        case 12:  //RECHAZADA
            return "<svg width='80' height='80' class='rechazada' > <rect x='10' y='10' rx='10' ry='10' width='20' height='20' style='fill:red;' /> </svg>";
            break;

    }
  

}
function FormatearFechaOT(fecha) {
    let dateTimeEST = new Date(fecha);
    return dateTimeEST.toLocaleTimeString();
}

function ObtenerTotalPiezas() {
    $.ajax({
        type: 'GET',
        url: '../../Home/ObtenerTotalPiezas',
        dataType: 'json',
        success: function (data) {
           
            document.getElementById("TotalPiezas").innerText = data;

        }
    });
}

function ObtenerMeta() {
    $.ajax({
        type: 'GET',
        url: '../../Home/ObtenerCantidadMeta',
        dataType: 'json',
        success: function (data) {

            document.getElementById("MetaCantidad").innerText ="";
            document.getElementById("MetaCantidad").innerText = data;

        }
    });
}  
function UpsertMeta() {
    var CantidadMeta = document.getElementById("MetaCantidadUpsert").value;
    $.ajax({
        type: 'POST',
        url: '../../Home/UpsertCantidadMeta',
        dataType: 'json',
        data: {
            CantidadMeta: CantidadMeta
        },
        success: function (data) {
            
            ObtenerMeta();
            $("#MetaModal").modal("hide");
        }
    });
}




