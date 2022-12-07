"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

$(function () {
    connection.start().then(function () {

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
connection.on("ReceivedOrdenTrabajo", async function (data) {
    
    $("#MaquinaList").html("");
    for await (const n of data.data) {

            $("#MaquinaList").append(

                "<tr>" +
                "<td> <strong> " + n.nombreMaquina + "</strong></td > " +
                "<td><strong>" + n.idCodigoOrdenTrabajo + " </strong></td > " +
                "<td><strong>" + n.idCodigoParte + "</strong></td>" +
                "<td><strong>" + EvaluarEstadoOT(n.idEstadoOrden) + "</strong></td>" +
                "</tr>"
            );

    }

    for await (const n of data.data) {

        await FilterByEstadoOT(n.idEstadoOrden, n.idOrdenTrabajo);
    }
   
 });

  // await ObtenerListaMaquinas();

async function EvaluarFechaOrdenTrabajo(data) {

    let dateTimeEST = new Date(data);
    let currentTime = new Date();
    if (dateTimeEST.getDate() == currentTime.getDate()) {

        return true;
    }

}

 function EvaluarEstadoOT(idEstadoOrden) {

    switch (idEstadoOrden) {
        case 7:  //PLEANEADO
            return "<svg width='50' height='50' class='planeado'> <rect x='1' x='1' rx='20' ry='20' width='50' height='50' style='fill:#00FFE8;'  /> </svg>";
            break;
        case 8:  //INICIADO
            return "<svg width='50' height='50' class='iniciado'> <rect x='1' x='1' rx='20' ry='20' width='50' height='50' style='fill:orange;'  /> </svg>";
            break;
        case 9:  //ACTIVA 
            return "<svg width='50' height='50' class='activa'> <rect x='1' x='1' rx='20' ry='20' width='50' height='50' style='fill:#1FAB00;'  /> </svg>";
            break;
        case 10:  //ACEPTADA
            return "<svg width='50' height='50' class='aceptada' > <rect x='1' x='1' rx='20' ry='20' width='50' height='50' style='fill:yellow;'  /> </svg>";
            break;
        case 11:  //TERMINADA
            return "<svg width='50' height='50' class='terminada' > <rect x='1' x='1' rx='20' ry='20' width='50' height='50' style='fill:black;'  /> </svg>";
            break;
        case 12:  //RECHAZADA
            return "<svg width='50' height='50' class='rechazada' > <rect x='1' x='1' rx='20' ry='20' width='50' height='50' style='fill:red;'  /> </svg>";
            break;

    }


}

async function FilterByEstadoOT(idEstadoOrden,idOrdenTrabajo) {

    switch (idEstadoOrden) {
        case 7:  //PLEANEADO
            return false;
            break;
        case 8:  //INICIADO
            return false;
            break;
        case 9:  //ACTIVA 
            return true;
            break;
        case 10:  //ACEPTADA
            return true;
            break;
        case 11:  //TERMINADA
            return false;
            break;
        case 12:  //PAUSADA
            await  SendNotify(idOrdenTrabajo);
            return true;
            break;

    }


}
async function ValidateNotification() {
    if (Notification.permission === "granted") {
        await ShowNotification();
    } else if (Notification.permission !== "denied") {
        Notification.requestPermission().then(permission => {
            if (permission === "granted") {
                alert("note");
            }
        });

    }
}
async function ShowNotification(head , body) {
    const notification = new Notification(head, {
        body: body,
        icon: "../../lib/brand/24261dba-3824-4484-9149-5ba298ae777d.jpeg"
    });
    await PlayAudio();
}
async function SendNotify(idOrdenTrabajo) {

    await postNotify(`../../Home/SendNotify/` + idOrdenTrabajo)
        .then(response => {
      
           ShowNotification();
        });
}

async function PlayAudio() {
        var audio = new Audio('../../Uploads/Alarmas/alarm.mp3');
      //audio de alarma 
     
    audio.play();

    //la alarma sonara solamente  sonora si el general view
}
async function play() {
   await PlayAudio();
}
async function postNotify(url) {

    // Opciones por defecto estan marcadas con un *
    const response = await fetch(url);
    return response.json(); // parses JSON response into native JavaScript objects
}

function FormatearFechaOT(fecha) {
    let dateTimeEST = new Date(fecha);
    return dateTimeEST.toLocaleTimeString();
}


