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
        connection.invoke("SendOrdenTrabajoNoFilter").catch(function (err) {
            return console.error(err.toString());
        });
    }
connection.on("ReceivedOrdenTrabajoNoFilter", function (data) {
   
  
    var planeada = 0, activa = 0, paraliberar = 0, terminada = 0, pausada=0;
    $.each(data.data, function (n) {
      
        switch (data.data[n].idEstadoOrden) {
            case 7:  //PLEANEADO
                planeada++;
                break;
            case 9:  //ACTIVA 
                activa++;
                break;
            case 10:  //PARA LIBERAR
                paraliberar++;
                break;
            case 11:  //TERMINADA
                terminada++;
                break;
            case 12:  //PAUSADA
                pausada++;
                break;

        }
       
            
          
    });
    
    ObteneroOtProgressBar(planeada, activa, terminada, pausada, paraliberar);
 
    ObtenerTotalPiezas();
    SetNumeroSemana();
   
     
});

function EvaluarFechaOrdenTrabajo(data) {
   
            let dateTimeEST = new Date(data);
            let currentTime = new Date();
            if (dateTimeEST.getDate() == currentTime.getDate()) {
                
                return true;      
            }
         
        }

function EvaluarEstadoOT(idEstadoOrden) {
   
   

}
function FormatearFechaOT(fecha) {
    let dateTimeEST = new Date(fecha);
    return dateTimeEST.toLocaleTimeString();
}

function ObtenerTotalPiezas() {
    const d = new Date();
    var mes  = d.getMonth()+1;
    $.ajax({
        type: 'POST',
        url: '../../Home/ObtenerSumTotalProduccionByMonth',
        dataType: 'json',
        data: {
            mes:mes
        }
        ,
        success: function (data) {
           
            document.getElementById("TotalPiezas").innerText = data;
            PorcenajeProdMeta();
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
//PORCENTAJE CUMPLIDO DE LA META EN PRODUCCION 
function PorcenajeProdMeta() {
   
    setTimeout(function () {
        var ppm = 0;
        var TotalPiezas = document.getElementById("TotalPiezas").innerText;
        var Meta = document.getElementById("MetaCantidad").innerText;
        let x = TotalPiezas * 100;
        ppm = x / Meta;
        document.getElementById("PorcentajeProdMeta").innerText = ppm + "%";
        $("#progressbarcontent").html("");
        $("#progressbarcontent").append("<div class='progress progress-sm mr-2'>" +
            "<div class= 'progress-bar bg-info' role='progressbar'"+
             "style='width: "+ppm+"%' aria-valuenow='"+ppm+"' aria-valuemin='0' aria-valuemax='100'></div></div>");


   
    },2000);

}
//CALCULAR MAS MAQUINAS POR DIA
//GRAFICA PARA FILTRAR POR DIA PARA SABER LAS ORDENDES DE TRABAJO POR DIA  
function ObteneroOtProgressBar(planeada, activa, terminada, pausada, paraliberar) {
    $("#OtProgressBar").html("");
    $("#OtProgressBar").append('<h4 class="small font-weight-bold"> Pausadas <span class="float-right">' + pausada + '</span></h4>' +
        '<div class="progress mb-4">' +
        '<div class="progress-bar bg-danger" role="progressbar" style="width:' + pausada + '%"' +
        'aria-valuenow="20" aria-valuemin="0" aria-valuemax="100"></div>' +
        '</div>' +
        '<h4 class="small font-weight-bold">Por liberar<span class="float-right">' + paraliberar + '</span></h4>' +
        '<div class="progress mb-4">' +
        '<div class="progress-bar bg-warning" role="progressbar" style="width:' + paraliberar + '%"' +
        'aria-valuenow="40" aria-valuemin="0" aria-valuemax="100"></div>' +
        '</div>' +
        '<h4 class="small font-weight-bold">Planeadas <span class="float-right">' + planeada + '</span></h4>' +
        '<div class="progress mb-4">' +
        '<div class="progress-bar bg-info" role="progressbar" style="width:' + planeada + '%"' +
        'aria-valuenow="60" aria-valuemin="0" aria-valuemax="100"></div>' +
        '</div>' +
        '<h4 class="small font-weight-bold">Terminadas <span class="float-right">' + terminada + '</span></h4>' +
        '<div class="progress mb-4">' +
        '<div class="progress-bar bg-dark" role="progressbar" style="width:'+terminada+'%"' +
                'aria-valuenow="80" aria-valuemin="0" aria-valuemax="100"></div>'+
        '</div>' +
        '<h4 class="small font-weight-bold">Activas<span class="float-right">'+activa+'</span></h4>' +
        '<div class="progress">'+
        '<div class="progress-bar bg-success" role="progressbar" style="width:'+activa+'%"'+
        'aria-valuenow="100" aria-valuemin="0" aria-valuemax="100"></div>'+
        '</div>');
}


//metodo para obtenerNumeroSemana
async function ObtenerNumeroSemana() {
    const response = await fetch('../../Home/ObtenerNumeroSemanasDashBoard', {
        method: 'GET',
        headers: {
            'Accept': 'application/json; charset=utf-8',
            'Content-Type': 'application/json;charset=UTF-8'
        }
    });
    const NumeroSemanaData = await response.json();
  
    return NumeroSemanaData;

}

//metodo para llamar obtner numero de la semana
async function SetNumeroSemana() {
    await ObtenerNumeroSemana().then(async function (data) {
       
        await FillNumeroSemana(data);
    });
}
//metodo para llenar contenido del numero de la semana 
async function FillNumeroSemana(data) {
   
    document.getElementById("NumeroSemana").innerText = data.data;
}