

$(document).ready(function () {
    GetIdentificadorOT();
    ObtenerRelacionMaquinasUsuarios();
   
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
            $("#BitacoraOrdenTrabajo").html("");
            FillDetalles(data);
            GetParteByOTId(idOrdenTrabajo);
            GetEmpleadosByOTId(idOrdenTrabajo);
            ObtenerCajasRecibidas();
            ObtenerCajasRealizadas();
            ObtenerFechaInicio();
            ObtenerFechaFinalizacion();
            ObtenerParteIdByOTId();
            ValidateIfExistsBitacoraOrdenTrabajo(); 
            SetCurrentBitacoraOrdenTrabajo(); /// metodo para guardar en localstorage el id de la bitacora Orden Trabajo
           
        }
    });
}
function FillDetalles(data) {
  
    $("#pills-tabContent").html("");
    $("#part-Image").html("");
    var x = 0;

    $.each(data.data, function (n) {

        $("#pills-tabContent").append(

              "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary displayResult'>Codigo Orden de Trabajo</p>  <p class='displayResult'>" +   data.data[x].idCodigoOrdenTrabajo + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary displayResult'>Numero de Parte</p>           <p class='displayResult'>" +           data.data[x].idCodigoParte + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary displayResult'>Cantidad Piezas por Orden</p> <p class='displayResult'>" + data.data[x].cantidadPiezasPorOrden + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary displayResult'>Numero de Cabidades </p class='displayResult'><button class='btn btn-primary btn-CabidadesPieza' data-bs-toggle='modal' data-bs-target='#EditModalNCP'><strong>"+ data.data[x].numeroCabidadesPieza + "</strong></button>"
            +"</div>"



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



        $("#pills-tabContent").append(
              "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap displayResult'>Aluminio</p>                        <p class='displayResult'>" + truncate(data.data[x].aluminio, 3) + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap displayResult'>Cajas por Tarima</p>                <p class='displayResult'>" + data.data[x].cajasPorTarima + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap displayResult'>Caja</p>                            <p class='displayResult'>" + data.data[x].nombreCaja + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap displayResult'>Cliente</p>                         <p class='displayResult'>" + data.data[x].nombreCliente + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap displayResult'>Color</p>                           <p class='displayResult'>" + data.data[x].nombreColor + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap displayResult'>Ensamble</p>                        <p class='displayResult'>" + data.data[x].nombreEnsamble + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap displayResult'>Hule</p>                            <p class='displayResult'>" + data.data[x].nombreHule + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap displayResult'>Inserto</p>                         <p class='displayResult'>" + data.data[x].nombreInserto + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap displayResult'>Molde</p>                           <p class='displayResult'>" + data.data[x].nombreMolde + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap displayResult'>Pintura</p>                         <p class='displayResult'>" + data.data[x].nombrePintura + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap displayResult'>Tarima</p>                          <p class='displayResult'>" + data.data[x].nombreTarima + "</p></div>" +
              "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap displayResult'>Instructivo</p>                     <p class='displayResult'>" + data.data[x].nombreInstructivoPieza + "</p></div>"
            + "<div class='p-1 m-1 border border-3 bg-white'><p class='badge bg-secondary text-wrap displayResult'>Piezas por Caja</p>                 <p class='displayResult'>" + data.data[x].piezasPorCaja + "</p></div>");
        $("#part-Image").append("<div class='d-flex flex-wrap'>" +
            "<fieldset style='max-width:50%;'><legend><p class='badge bg-primary'>Etiqueta de Parte</p></legend> <img class='img-fluid border border-dark' style='max-width:100%;' src='/Uploads//Etiquetas/Partes/" + data.data[x].nombreEtiqueta + ".jpg' /></fieldset> "
            + "<fieldset  style='max-width:50%;'><legend><p class='badge bg-primary'>Etiqueta de Caja</p></legend><img class='img-fluid border border-dark' style='max-width:100%;' src='/Uploads//Etiquetas/Cajas/" + data.data[x].nombreEtiquetaDeCaja + ".jpg' /></fieldset></div>")

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
                "<div class='btns-group'><button class='btn  btn-primary' data-bs-toggle='modal' data-bs-target='#staticBackdrop' onclick=\"SetEditEmpleado(\'" + data.data[x].idEmpleadoOrdenTrabajo + "\');\"><i class='fas fa-edit'></i></button>" +
                "<button class='btn  btn-primary'  onclick=\"DeleteAsignacionEmpleadoOTById(\'" + data.data[x].idEmpleadoOrdenTrabajo + "\');\"><i class='fa fa-trash' aria-hidden='true'></i></button></div> </section>");

        }
        if (data.data[x].idTipoEmpleadoFK == 6) {
            $("#EmpleadosRelacionados").append("<section id='empleadoMoldeador' class='p-1 m-1 bg-secondary'>"
                + "<div class='badge bg-light text-dark'><h5>Moldeador </h5><p>"
                + data.data[x].nombreEmpleado + "</p></div>" +
                "<div class='btns-group'><button class='btn  btn-primary' data-bs-toggle='modal' data-bs-target='#staticBackdrop' onclick=\"SetEditEmpleado(\'" + data.data[x].idEmpleadoOrdenTrabajo + "\');\"><i class='fas fa-edit' ></i></button>" +
                "<button class='btn  btn-primary'  onclick=\"DeleteAsignacionEmpleadoOTById(\'" + data.data[x].idEmpleadoOrdenTrabajo + "\');\"><i class='fa fa-trash' aria-hidden='true'></i></button></div> </section>");



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
            ///este esta de la orden de trabajo no se husa

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
    setTimeout(ObtenerBitacoraOrdenTrabajoByIdOrdenTrabajo(), 7000);

}
function SetReanudar() {
    RegistrarInicioActiva();
    RegistrarFinalDetenida();
    UpdateOTEstado(9);
    UpdateRegistroBitacoraOrdenTrabajo();
    setTimeout(ObtenerBitacoraOrdenTrabajoByIdOrdenTrabajo(), 7000);
  
}
function SetActiva() {
    InsertRegistroBitacoraOrdenTrabajo();
    //SE REGISTRA EL INICIO DE LA BITACORA 
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
        setTimeout(ObtenerBitacoraOrdenTrabajoByIdOrdenTrabajo(), 7000);
    }
   
}

function SetAceptar() {
  
    UpdateOTEstado(10);
    RegistrarFinalActiva();
    RegistrarInicioLiberar();
    setTimeout(ObtenerBitacoraOrdenTrabajoByIdOrdenTrabajo(), 7000);
}
//El valor de cajas recibidas en la tabla de la orden de trabajo sera la llave foreanea para obtener su detalle de la misma .
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
        }

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
    $("#ShowHoraInicio").append("<p class='badge bg-primary'> <strong>Fecha de Inicio : </strong>" + data.data[0].HoraInicio + "</p>")
}
function FillFechaFinalizacion(data) {
    $("#ShowHoraFinalizacion").html("");
    $("#ShowHoraFinalizacion").append("<p class='badge bg-primary'> <strong> Fecha de Finalizacion : </strong>" + data.data[0].HoraFinalizacion + "</p>");
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
// CREAR METODO PARA RELACIONAR LA DURACION DE LOS ESTADOS CON LA BITACORA ORDEN DE TRABAJO
// DEBE CONTENER EL ID DE LA BITACORA ACTUAL Y
// EL ID DEL CAMBIO DE ESTADO REGISTRANDO CADA QUE SE CAMBIA DE ESTADO
// ESTO PARA PODER CREAR LA CONSULTA
// 

function RegistrarDuracionEstado(
    TipoMovimientoEstado,
    IdOrdenTrabajo,
    IdMotivoCambioEstadoFK,
    IdEstadoOrdenTrabajoFK,
    IdBitacoraOrdenTrabajoFK) {
    $.ajax({
        type: "POST",
        url: '../../OtEstado/RegistrarDuracionEstado',
        data: {
            TipoMovimientoEstado: TipoMovimientoEstado,
            IdOrdenTrabajoFK: IdOrdenTrabajo,
            IdMotivoCambioEstadoFK: IdMotivoCambioEstadoFK,
            IdEstadoOrdenTrabajoFK: IdEstadoOrdenTrabajoFK,
            IdBitacoraOrdenTrabajoFK: IdBitacoraOrdenTrabajoFK
        },
        dataType: "json",
        success: function (data) {

            if (data.data.length > 0) {
               
            }//Se guarda el id del cambio de estado
        }
    });
}

//- Bitacora Orden Trabajo
//- INSERT REGISTRO BITACORA ORDEN DE TRABAJO 
 function  InsertRegistroBitacoraOrdenTrabajo() {

    $.ajax({
        type: "POST",
        url: '../../BitacoraOrdenTrabajo/InsertRegistroBitacoraOrdenTrabajo',
        data: {
            IdOrdenTrabajoFK: localStorage.getItem('currentOT')
        },
        dataType: "json",
        success: function (data) {
        
          setTimeout(ObtenerBitacoraOrdenTrabajoByIdOrdenTrabajo(), 7000);
           
        }
    }); 
}
// Update Registro Bitacora Orden de trabajo - Calculo de los estandares  
function UpdateRegistroBitacoraOrdenTrabajo() {

    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    
    $.ajax({
        type: "POST",
        url: '../../BitacoraOrdenTrabajo/UpdateRegistroBitacoraOrdenTrabajo',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        dataType: "json",
        success: function (data) {
           
            ObtenerBitacoraOrdenTrabajoByIdOrdenTrabajo();
        }
    });
}
// Update Bitacora Orden de trabajo Produccion terminada - Calculo de la produccion restante && validacion de requerimiento : Numero de Piezas en la orden de trabajo 
function UpdateBitacoraOrdenTrabajoProdTerminada() {
    var IdOrdenTrabajo = localStorage.getItem("currentOT");
    $.ajax({
        type: "POST",
        url: '../../BitacoraOrdenTrabajo/UpdateBitacoraOrdenTrabajoProdTerminada',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        dataType: "json",
        success: function (data) {
           
            ObtenerBitacoraOrdenTrabajoByIdOrdenTrabajo();
            $("#ResumenTiemposMuertos").modal("hide");
            EvaluarProduccionCompletada();
            
        }
    });
}
//RegistrarInicioPausa
function InsertRespuestaCambioEstado() {
    var IdOrdenTrabajoFK = localStorage.getItem('currentOT');
    var IdMotivoCambioEstadoFK = document.getElementById('checkRespuesta').value;
    ObtenerUltimoRegistroBitacoraDuracionEstado();
    var IdBitacoraOrdenTrabajo = localStorage.getItem('currentBOT');
    RegistrarDuracionEstado(0, IdOrdenTrabajoFK, IdMotivoCambioEstadoFK, 12, IdBitacoraOrdenTrabajo);
    SetPausa();

}
function CheckRespuestaCambioEstado() {

    localStorage.setItem('DropDownChange', true);

}
//PRIMARY KEY CAMBIO DE ESTADO
//7 - PLANEADO
//9 - ACTIVA
//10 - PARA LIBERAR
//11 - TERMINADA
//12 - PAUSADA


//registrar inicio de estado activo 
function RegistrarInicioActiva() {
    var IdOrdenTrabajoFK = localStorage.getItem('currentOT');
    setTimeout(ObtenerUltimoRegistroBitacoraDuracionEstado(), 1000);
    var IdBitacoraOrdenTrabajo = localStorage.getItem('currentBOT');
    setTimeout(RegistrarDuracionEstado(0, IdOrdenTrabajoFK, 22, 9, IdBitacoraOrdenTrabajo), 7000);

}
//resgistrar final de estado activo 
function RegistrarFinalActiva() {
    var IdOrdenTrabajoFK = localStorage.getItem('currentOT');
    RegistrarDuracionEstado(1, IdOrdenTrabajoFK, 22, 9, 0);

}
//registrar final de estado detenido
function RegistrarFinalDetenida() {
    var IdOrdenTrabajoFK = localStorage.getItem('currentOT');
    RegistrarDuracionEstado(1, IdOrdenTrabajoFK, 22, 12,0);
}
//registrar inicio de estado por liberar
function RegistrarInicioLiberar() {
    var IdOrdenTrabajoFK = localStorage.getItem('currentOT');
    ObtenerUltimoRegistroBitacoraDuracionEstado();
    var IdBitacoraOrdenTrabajo = localStorage.getItem('currentBOT');
    RegistrarDuracionEstado(0, IdOrdenTrabajoFK, 22, 10, IdBitacoraOrdenTrabajo);
}
//registrar final de estado por liberar 
function RegistrarFinalLiberar() {
    var IdOrdenTrabajoFK = localStorage.getItem('currentOT');
    RegistrarDuracionEstado(1, IdOrdenTrabajoFK, 22, 10,0);
}


function ObtenerUltimoRegistroBitacoraDuracionEstado() {
    var IdOrdenTrabajo = localStorage.getItem("currentOT");
    $.ajax({
        type: "POST",
        url: "../../BitacoraOrdenTrabajo/ObtenerUltimoRegistroBitacoraDuracionEstado",

        data: {
            IdOrdenTrabajoFK: IdOrdenTrabajo
        }, dataType: "json",
        success: function (IdBitacoraOrdenTrabajo) {
            localStorage.setItem('currentBOT', IdBitacoraOrdenTrabajo);
        }

    });
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

   /*
    $.each(data.details, function (n) {
        $("#ResumenContainer").html("");
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
    $("#ResumenSumDetenida").html("");
    $("#ResumenSumPorLiberar").html("");
    $("#ResumenSumActiva").html("");
    */
    $.each(data.total, function (z) {
      

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
function UpdateNumeroCabidades() {
    var numCab = $("#NumeroCabidades").val();
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: "POST",
        url: '../../OtEstado/UpdateNumeroCabidades',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo,
            numCab: numCab
        },
        dataType: "json",
        success: function (data) {
            GetOrdenTrabajo(IdOrdenTrabajo)
            $("#EditModalNCP").modal("hide");
        }

    });

}


function ObtenerRelacionMaquinasUsuarios() {
    $.ajax({
        type: "POST",
        url: '../../OtEstado/ObtenerRelacionMaquinasUsuarios',
        dataType: "json",
        success: function (data) {
            FillRelacionMaquinasUsuarios(data);
        }
    });
}
function FillRelacionMaquinasUsuarios(data) {
    const url = window.location.href;
    var queryString = url.split('/');

    if (data.data.length > 0) {
       
        data.data.forEach((c) => {
            if (queryString[queryString.length - 1] == c.idMaquinaFK) {
                $("#MaquinasAsignadas").append("<li id='MaquinaItem' class='nav-item'><a class='nav-link active' href='../../OtEstado/OrdenesDeTrabajoAsignadas/" + c.idMaquinaFK + "'>" + c.nombreMaquina + "</a></li>");
            } else {
                $("#MaquinasAsignadas").append("<li id='MaquinaItem' class='nav-item'><a class='nav-link' href='../../OtEstado/OrdenesDeTrabajoAsignadas/" + c.idMaquinaFK + "'>" + c.nombreMaquina + "</a></li>");
            }
           

        });


        
    }
}
//Bitacora Orden Trabajo
function EvaluarProduccionCompletada() {
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: "POST",
        url: '../../BitacoraOrdenTrabajo/EvaluarProduccionCompletada',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        dataType: "json",
        success: function (data) {
        
            if (data.data == false) {
                UpdateOTEstado(7);
            
            } 
            if (data.data == true) {
                $("#ModalFinalProduccion").modal("show");
            
            }
        }
    });
}

function ObtenerBitacoraOrdenTrabajoByIdOrdenTrabajo() {
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: "POST",
        url: '../../BitacoraOrdenTrabajo/ObtenerBitacoraOrdenTrabajoByIdOrdenTrabajo',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        dataType: "json",
        success: function (data) {

            FillBitacoraOrdenTrabajoByIdOrdenTrabajo(data);

        }, error: function (data) {
           
        }
    });
}
function FillBitacoraOrdenTrabajoByIdOrdenTrabajo(data) {
  
    $("#BitacoraOrdenTrabajo").html("");
    let cantidadPiezasPorOrdenRealizadas = 0;
    let estandar = 0;
    let estandarPorHora = 0;
    let estandarConRelevo = 0;
    let scrap = 0;
    
    if (data.data.cantidadPiezasPorOrdenRealizadas == null) {

    } else {
        cantidadPiezasPorOrdenRealizadas = data.data.cantidadPiezasPorOrdenRealizadas;
    }
    if (data.data.estandarCalculado == null) {

    } else {
        estandar = data.data.estandarCalculado;
      
    } 
    if (data.data.estandarPorHorasCalculado == null) {

    } else {

        estandarPorHora = data.data.estandarPorHorasCalculado;
       
    }
    if (data.data.estandarConRelevoCalculado == null) {
       
    } else {
        estandarConRelevo = data.data.estandarConRelevoCalculado;
    }
    if (data.data.scrapCalculado == null) {

    } else {
        scrap = data.data.scrapCalculado;
    }
    $("#BitacoraOrdenTrabajo").append("<div class='p-1 m-1 border border-3 bg-white displayResult'><p class='badge bg-secondary text-wrap displayResult'>Piezas Faltantes</p><p class='displayResult'>"
        + cantidadPiezasPorOrdenRealizadas + "</p></div>");
    $("#BitacoraOrdenTrabajo").append("<div class='p-1 m-1 border border-3 bg-white displayResult'><p class='badge bg-secondary text-wrap displayResult'>Estandar</p><p class='displayResult'>" +truncate(estandar,0)+"</p></div>");
    $("#BitacoraOrdenTrabajo").append("<div class='p-1 m-1 border border-3 bg-white displayResult'><p class='badge bg-secondary text-wrap displayResult'>Estandar con Relevo</p><p class='displayResult'>"+ truncate(estandarConRelevo,0)+"</p></div>");
    $("#BitacoraOrdenTrabajo").append("<div class='p-1 m-1 border border-3 bg-white displayResult'><p class='badge bg-secondary text-wrap displayResult'>Estandar por Horas</p><p class='displayResult'>" + estandarPorHora + "</p></div>");
    $("#BitacoraOrdenTrabajo").append("<div class='p-1 m-1 border border-3 bg-white displayResult'><p class='badge bg-secondary text-wrap displayResult'>Scrap</p><p class='displayResult'>" + truncate(scrap,0) + "</p></div>");
  

    ObtenerSumBitacoraOrdenTrabajoProduccion();


}
function ObtenerAllBitacorasOtByOtId() {
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: "POST",
        url: '../../BitacoraOrdenTrabajo/ObtenerAllBitacorasOtByOtId',
        data: {
            IdOrdenTrabajo:IdOrdenTrabajo
        },
        success: function (data) {
            FillAllBitacorasOtById(data);

        }
    });
}
function FillAllBitacorasOtById(data) {
    $("#BitacoraOrdenTrabajo").append("<div class='accordion accordion-flush' id='AccordionHistorialBitacora'></div>");
    $("#AccordionHistorialBitacora").append("<div class='accordion-item'>" +
        "<h2 class='accordion-item' id='flush-headingOne'>" +
        "<button class='accordion-button collapsed' type='button' data-bs-toggle='collapse' data-bs-target='#flush-collapseOne' aria-expanded='false' aria-controls='flush-collapseOne'>"
        +"Historial de Bitacora"
        +"</button>" +
        "</h2>" +
        "<div id='flush-collapseOne' class='accordion-collapse collapse' aria-labelledby='flush-headingOne' data-bs-parent='#accordionFlushExample'>" +
        "<div class='accordion-body'><table class='table'>"
        +"<thead><tr>" +
        "<th>Cajas Recibidas</th>"+
        "<th>Pieza por Orden</th>"+
        "<th>Piezas faltantes</th>"+
        "<th>Estandar</th>"+
        "<th>Estandar por horas</th>"+
        "<th>Estandar c/ Relevo</th>"+
        "<th>Fecha de Creacion</th>"+
        "<th>Horas Trabajadas</th>"+
        "<th>Piezas Realizadas por Turno</th>"+
        "<th>Piezas Recibidas</th>"+
        "</tr></thead>"
        + "<tbody id='DetalleBitacoraOrdenTrabajo'></tbody></table></div> " +
        "</div>" +
        "</div>")

    data.data.forEach((x) => {

        $("#DetalleBitacoraOrdenTrabajo").append("<tr>" +
            "<td>" + x.cajasRecibidas + "</td>" +
            "<td>" + x.cantidadPiezasPorOrden + "</td>" +
            "<td>" + x.cantidadPiezasPorOrdenRealizadas + "</td>" +
            "<td>" + x.estandarCalculado + "</td>" +
            "<td>" + x.estandarConRelevoCalculado + "</td>" +
            "<td>" + x.estandarPorHorasCalculado + "</td>" +
            "<td>" + x.fechaOrdenTrabajo + "</td>" +
            "<td>" + x.horasTrabajadasCalculado + "</td>" +
            "<td>" + x.numeroPiezasRealizadas + "</td>" +
            "<td>" + x.piezasRecibidas + "</td>" 
            + "</tr>");
    });

}

function ObtenerSumBitacoraOrdenTrabajoProduccion() {
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: "POST",
        url: '../../BitacoraOrdenTrabajo/ObtenerSumBitacoraOrdenTrabajoProduccion',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        success: function (data) {
            FillSumProduccionActual(data);

        }
    });
}
function FillSumProduccionActual(data) {
    $("#ProduccionTotal").remove();
    $("#BitacoraOrdenTrabajo").append("<div class='p-1 m-1 border border-3 bg-white displayResult' id='ProduccionTotal'><p class='badge bg-secondary text-wrap displayResult'>Produccion Total</p><p class='displayResult'>" + data + "</p></div>");
}
function ValidateSumBitacoraOrdenTrabajoProduccion() {
    var IdOrdenTrabajo = localStorage.getItem('currentOT');

    $.ajax({
        type: "POST",
        url: '../../BitacoraOrdenTrabajo/ValidateSumBitacoraOrdenTrabajoProduccion',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        success: function (data) {
      
            if (data.data == true) {
           
                setTimeout(ObtenerSumBitacoraOrdenTrabajoProduccion(), 7000);
            } else {
              
            }
        }
    });
}
function ValidateIfExistsBitacoraOrdenTrabajo() {
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: "POST",
        url: '../../BitacoraOrdenTrabajo/ValidateIfExistsBitacoraOrdenTrabajo',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        success: function (data) {
           
            if (data == true) {

                setTimeout(ObtenerBitacoraOrdenTrabajoByIdOrdenTrabajo(), 7000);
                ValidateSumBitacoraOrdenTrabajoProduccion();
            } else {

            }
        }

    });
}