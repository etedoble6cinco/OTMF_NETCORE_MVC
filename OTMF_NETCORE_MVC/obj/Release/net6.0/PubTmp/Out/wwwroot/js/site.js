



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
$(document).ready(function () {
    $("body").append('<div class="modal fade" id="RelacionEmpleadosOrdenTrabajoModal" aria-hidden="true" aria-labelledby="RelacionEmpleadosOrdenTrabajoModal" tabindex="-1" >' +
        '<div class="modal-dialog  modal-xl">' +
        '<div class="modal-content">' +
        '<div class="modal-header">' +
        '<button type="button" class="btn" data-bs-dismiss="modal">&times;</button>' +
        '<h4 class="modal-title">Buscar Orden Trabajo</h4>' +
        '</div>' +

        '<div class="modal-body">' +
        '<div class="row">' +
        '<div class= "col-4"><div class="form-check">' +
        '<input type="radio" class="form-check-input" value="busquedaCodigo" id="busquedaCodigo" name="busqueda">' +
        '<label class="form-check-label" for="busquedaCodigo">Busqueda por codigo</label></div>' +
        '</div>'
        + '<div class= "col-4">' +
        '<div class="form-check">' +
        '<input type="radio" class="form-check-input" id="busquedaEstado" value="busquedaEstado" name="busqueda"><label class="form-check-label" for="busquedaEstado"> Busqueda por Estado</label></div>' +
        '</div >' +
        '<div class= "col-4">' +
        '<div class="form-check">' +
        '<input type="radio" class="form-check-input" id="busquedaFecha" value="busquedaFecha" name="busqueda">' +
        '<label for="busquedaFecha">Busqueda por Fecha</label>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '<div class="container" id="eleccion"></div>' +
        '<div class="container" id="resultados"></div>' +
        '<div class="modal-footer">' +
        ' <button type="button" class="btn btn-default" data-bs-dismiss="modal">Close</button>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>');
});
//--------------------------------------------------------------------------modal para busqueda de ordenes de trabajo
function AsignacionEmpleadosOrdenTrabajo () {
    


   
    $('input:radio[name="busqueda"]').change(function () {
        $(".daterangepicker").remove();
        if ($(this).is(':checked') && $(this).val() == 'busquedaCodigo') {
            $("#eleccion").html("");
            $("#resultados").html("");
            var content = '<form autocomplete="off" class="d-flex flex-row">' +
                '<div class="autocomplete">'
                + ' <input id="myInput"  type="text" name="Ot" placeholder="Clave">'
                + ' </div>'
                + '<input class="btn btn-sm btn-primary" id="idCode" onclick="ResultadosBusquedaOt()" value="Buscar" type="button">'
                + '</form>';
            $("#eleccion").append(content);
            FillOrdenTrabajoAutoComplete();
        }
        if ($(this).is(':checked') && $(this).val() == 'busquedaEstado') {
            $("#eleccion").html("");
            $("#resultados").html("");
            var content = '<div class="d-flex flex-row justify-content-center" id="eleccionEstado" onclick="evaluarEstadoSeleccionado()">' +
                '<div class="form-check"><input type="radio" class="form-check-input" name="estado" value="activo" id="activo">   <label class="form-check-label" for="activo">Activa</label></div>'+
                '<div class="form-check"><input type="radio" class="form-check-input" name="estado" value="liberar" id="liberar">  <label class="form-check-label" for="liberar">Liberar</label></div>'+
                '<div class="form-check"><input type="radio" class="form-check-input" name="estado" value="pausada" id="pausada">  <label class="form-check-label" for="pausada">Pausada</label></div>'+
                '<div class="form-check"><input type="radio" class="form-check-input" name="estado" value="planeada" id="planeada"> <label class="form-check-label" for="planeada">Planeada</label></div>'+
                '<div class="form-check"><input type="radio" class="form-check-input" name="estado" value="terminada" id="terminada"><label class="form-check-label" for="terminada">Terminada</label></div>'+
                '</div>';
            $("#eleccion").append(content);
            
        }
        if ($(this).is(':checked') && $(this).val() == 'busquedaFecha') {
            $("#eleccion").html("");
            $("#resultados").html("");
          
            var content = '<div><input id="busquedaDatePicker" type="text"></div>';
            $("#eleccion").append(content);
            $('#busquedaDatePicker').daterangepicker({
                "showDropdowns": true,
                "showWeekNumbers": true,
                "linkedCalendars": false,
                "autoUpdateInput": false,
                "showCustomRangeLabel": false,
                "startDate": "09/07/2022",
                "endDate": "09/13/2022"
            }, function (start, end, label) {
                document.getElementsByClassName("applyBtn").value = '';
               ObtenerOrdenTrabajoByDateRange(start, end);
            });
        }
    });
}
function evaluarEstadoSeleccionado() {
    $('input:radio[name="estado"]').change(function () {
        if ($(this).is(':checked') && $(this).val() == 'activo')
        {
         
            ObtenerOrdenesTrabajoDetallesByState(9);
        }
        if ($(this).is(':checked') && $(this).val() == 'liberar')
        {
            
            ObtenerOrdenesTrabajoDetallesByState(10);
        }
        if ($(this).is(':checked') && $(this).val() == 'pausada')
        {
            
            ObtenerOrdenesTrabajoDetallesByState(12);
        }
        if ($(this).is(':checked') && $(this).val() == 'planeada')
        {
        
            ObtenerOrdenesTrabajoDetallesByState(7);
        }
        if ($(this).is(':checked') && $(this).val() == 'terminada')
        {
            
            ObtenerOrdenesTrabajoDetallesByState(11);
        }
    });
}
function ObtenerOrdenesTrabajoDetallesByState(IdEstadoOrdenFK) {
    $.ajax({
        type: 'POST',
        url: '../../OrdenTrabajoes/ObtenerOrdenesTrabajoDetallesByState',
        data: {
            EstadoOrden: IdEstadoOrdenFK
        }, dataType: 'json',
        success: function (data) {
            AgregarTableResultados(data);
        }
    });
}

function ResultadosBusquedaOt() {
    var stringBusqueda = document.getElementById("myInput").value;

    $.ajax({
        type: 'POST',
        url: '../../OrdenTrabajoes/ObtenerOrdenTrabajoByOtCode',
        data: {
            IdClaveOrdenTrabajo: stringBusqueda
        },
        dataType: 'json',
        success: function (data) {
            AgregarTableResultados(data);
        }
    });
}
function AgregarTableResultados(data) {

    $("#resultados").html("");
    $("#resultados").append("<table id='OrdenTrabajo' class='table hover'>"
        +"<thead>"
        +"<tr>"
        +"<th>Orden de Trabajo</th>"
        +"<th>Numero de Parte</th>"
        +"<th>Estado</th>"
        +"<th>Piezas Realizadas</th>"
        + "<th>Detalles</th>"
        + "<th>Detalles</th>"
        +"</tr> "
        +"</thead > "
        +"<tbody id='resultados2'>"
        + "</tbody>"
        + "</table> ");
    $("#resultados2").html("");
    $.each(data.data, function (n) {
      
            $("#resultados2").append("<tr>" +
                "<td>" + data.data[n].IdCodigoOrdenTrabajo + "</td>" +
                "<td>" + data.data[n].IdCodigoParte + "</td>" +
                "<td>" + data.data[n].NombreEstadoOrden + "</td>" +
                "<td id='PiezasRealizadasTotal_" + n + "'>" + ObtenerPiezasRealizadas(data.data[n].IdOrdenTrabajo, n) + "</td>" +
                "<td><button type='button' class='btn btn-lg btn-primary m-1 p-2' data-bs-toggle='tooltip'' data-bs-placement='top' title='Detalles Orden de Trabajo' onclick=\"ObtenerDetallesOrdenTrabajo(\'" + data.data[n].IdOrdenTrabajo + "\');\"><i class='fa fa-eye' aria-hidden='true'></i></button></td>"+
            "<td><button type='button' class='btn btn-lg btn-primary m-1 p-2' data-bs-toggle='tooltip' data-bs-placement='top' title='Detalle de Bitacora de Orden Trabajo'  onclick=\"ObtenerAllBitacorasOtByOtId(\'" + data.data[n].IdOrdenTrabajo + "\');\"><i class='fa fa-info-circle' aria-hidden='true'></i></button></td>"+
                   
                 "</tr>")
    
    });
  
}

function ObtenerPiezasRealizadas(IdOrdenTrabajo,n) {

    $.ajax({
        type: 'POST',
        url: '../../BitacoraOrdenTrabajo/ObtenerSumBitacoraOrdenTrabajoProduccion',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        dataType: 'json',
        success: function (data) {
            console.log(data);
            if (data === undefined && typeof data == 'undefined' ) {
                setTimeout(() => {
                    document.getElementById("PiezasRealizadasTotal_" + n).innerText = "No hay produccion registrada";
                }, 500);
            }
        
            setTimeout(() => {
                document.getElementById("PiezasRealizadasTotal_" + n).innerText = data;
            }, 500);
           
            console.log("PiezasRealizadasTotal_" + n);
        }
    });
   
}
function ObtenerAllBitacorasOtByOtId(IdOrdenTrabajo) {
    $.ajax({
        type: 'GET',
        url: '../../BitacoraOrdenTrabajo/ObtenerAllBitacorasOtByOtId',
        data: {
            Id: IdOrdenTrabajo
        },
        dataType: 'json',
        success: function (data) {
            
            FillAllBitacorasOtByOtId(data);
        }
    });
}
function ObtenerDuracionEstadoByIdBitacoraOrdenTrabajo(IdBitacoraOrdenTrabajoFK) {
    $.ajax({
        type: 'POST',
        url: '../../BitacoraOrdenTrabajo/ObtenerDuracionEstadoByIdBitacoraOrdenTrabajo',
        data: {
            IdBitacoraOrdenTrabajoFK: IdBitacoraOrdenTrabajoFK
        },
        dataType: 'json',
        success: function (data) {

            FillObtenerDuracionEstadoByIdBitacoraOrdenTrabajo(data);


        }
    });

    
}
function FillObtenerDuracionEstadoByIdBitacoraOrdenTrabajo(data){
    console.log(data);
    $("#DetalleDuracionEstados").html("");
    $("#DetalleDuracionEstados").append('<table id="DetalleDuracionEstados" class="table table-strip">' +
        '<thead><tr>' +
        '<th>Duracion</th>' +
        '<th>Fecha de Inicio</th>' +
        '<th>Fecha de Finalizacion</th>' +
        '<th>Hora Inicio</th>' +
        '<th>Hora Finalizacion</th>' +
        '<th>Estado</th>' +
        '<th>Detalle de Cambio</th>' +
      

        '</tr></thead>' +
        '<tbody id="ContenidoDetalleDuracionEstados">' +
        '</tbody>'
        + '</table>');

    $("#ContenidoDetalleDuracionEstados").html("");

    $.each(data.data, function (n) {

        $("#ContenidoDetalleDuracionEstados").append("<tr>" +
            "<td>" + data.data[n].Duracion + "</td>" +
            "<td>" + FormatDate(data.data[n].InicioEstado) + "</td>" +
            "<td>" + FormatDate(data.data[n].FinalEstado) + "</td>" +
            "<td>" + FormatTime(data.data[n].InicioEstado) + "</td>" +
            "<td>" + FormatTime(data.data[n].FinalEstado) + "</td>" +
            "<td>" + data.data[n].NombreEstadoOrden + "</td>" +
            "<td>" + data.data[n].NombreMotivoCambioEstado+ "</td>" +
     


            "</tr>");
    });
}

function FillAllBitacorasOtByOtId(data) {
 
    $("#DetalleBitcoraOrdenTrabajo").remove();
    $("body").append('<div class="modal fade" id="DetalleBitacoraOrdenTrabajo" aria-hidden="true" aria-labelledby="DetalleBitacoraOrdenTrabajo" tabindex="-1">' +
        '<div class="modal-dialog modal-xl">' +
        '<div class="modal-content">' +
        '<div class="modal-header">' +
        '<button type="button" class="btn"  data-bs-dismiss="modal"  ><i class="fa fa-arrow-left" aria-hidden="true"></i></button>' +
        '<h4 class="modal-title">Detalle Bitacora Orden Trabajo</h4>' + 
        '</div>' +
        '<div class="modal-body">' +
        '<div class="container-fluid">' +
        '<div>' +
        '<table class="table table-strip">' +
        '<thead><tr>' +
        '<th>Cajas Recibidas</th>' +
        '<th>Piezas por Orden</th>' +
        '<th>Piezas realizadas</th>' +
        '<th>Estandar</th>' +
        '<th>Estandar con Relevo</th>' +
        '<th>Estandar por Horas</th>' +
        '<th>Fecha de Creacion</th>' +
        '<th>Horas Trabajadas</th>' +
        '<th>Horas Acumuladas</th>' +

        '<th>Detalles</th'+
        '</tr></thead>' +
        '<tbody id="ContenidoDetalleBitacoraOrdenTrabajo">' +
        '</tbody>'
        + '</table>'
        + '</div>'
        + '</div>' +
        '<div class="container" id="resultados"></div>' +
        '<div class="modal-footer" id="DetalleDuracionEstados">' +
        '<button type="button" class="btn btn-default"  data-bs-dismiss="modal">Close</button>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>');
    $("#ContenidoDetalleBitacoraOrdenTrabajo").html("");

    $.each(data.data, function (n) {

        $("#ContenidoDetalleBitacoraOrdenTrabajo").append("<tr>" +
            "<td>" + data.data[n].cajasRecibidas              + "</td>" +
            "<td>" + data.data[n].cantidadPiezasPorOrden          + "</td>" +
            "<td>" + data.data[n].cantidadPiezasPorOrdenRealizadas      + "</td>" +
            "<td>" + data.data[n].estandarCalculado + "</td>" +
            "<td>" + data.data[n].estandarConRelevoCalculado + "</td>" +
            "<td>" + data.data[n].estandarPorHorasCalculado + "</td>" +
            "<td>" + FormatDate(data.data[n].fechaOrdenTrabajo) + "</td>" +
            "<td>" + data.data[n].horasTrabajadasCalculado + "</td>" +
            "<td>" + data.data[n].horasTrabajadasAcumulado + "</td>" +
            "<td><a <a href='#DetalleDuracionEstados'><button type='button' class='btn btn-primary'onclick=\"ObtenerDuracionEstadoByIdBitacoraOrdenTrabajo(\'" + data.data[n].idBitacoraOrdenTrabajo + "\');\">Duracion de estados</button></a></td>" +

            "</tr>");
    });

    $("#DetalleBitacoraOrdenTrabajo").modal("show");



}
async function ObtenerUsuarioSistema() {

    const response = await fetch('../../')
}

function ObtenerDetallesOrdenTrabajo(IdOrdenTrabajo) {
  
    $.ajax({
        type: 'POST',
        url: '../../OrdenTrabajoes/ObtenerOrdenesTrabajoDetallesById',
        data: {
            IdOrdenTrabajo:IdOrdenTrabajo
        },
        dataType: 'json',
        success: function (data) {
            FillDetallesOrdenTrabajo(data);
        }
        });
}
function FillDetallesOrdenTrabajo(data) {
 
    $("#DetallesOrdenTrabajo").remove();
    $("body").append('<div class= "modal fade" id="DetallesOrdenTrabajo" aria-hidden="true" aria-labelledby="DetallesOrdenTrabajo" tabindex="-1" >' +
        '<div class="modal-dialog  modal-xl">' +
        '<div class="modal-content">' +
        '<div class="modal-header">' +
        '<button type="button" class="btn"  data-bs-dismiss="modal"><i class="fa fa-arrow-left" aria-hidden="true"></i></button>' +
        '<h4 class="modal-title">Detalle de Orden Trabajo</h4>' +
        '</div>' +

        '<div class="modal-body">' +
        '<div class="row">' +
        '<div class= "col-12" id="ContenidoDetalleOrdenTrabajo"></div>'
        +'</div>' +
        '<div class="container" id="resultados"></div>' +
        '<div class="modal-footer">' +
        '<button type="button" class="btn btn-default" data-bs-dismiss="modal" >Close</button>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>');
    $("#OrdenTrabajoContent").html("");
  
    $.each(data.data, function (n) {

        $("#ContenidoDetalleOrdenTrabajo").append("<div>" +
            "<div><strong>Numero de Parte</strong>:" + data.data[n].IdCodigoParte      + "</div>" +
            "<div><strong>Estado de la Orden</strong>:" + data.data[n].NombreEstadoOrden + "</div>" +
            "<div><strong>Orden Trabajo</strong>:" + data.data[n].IdCodigoOrdenTrabajo + "</div>" +
            "<div><strong>Cantidad Piezas por Orden</strong>:" + data.data[n].CantidadPiezasPororden + "</div>" +
            "<div><strong>Estandar </strong>:" + data.data[n].EstandarCalculado + "</div> " +
            "<div><strong>Estandar Con Relevo </strong>:" + data.data[n].EstandarConRelevoCalculado + "</div> " +
            "<div><strong>Estandar Por Hora</strong>:" + data.data[n].EstandarPorHorasCalculado + "</div> " +
            "<div><strong>Etiqueta de Caja</strong>:" + data.data[n].EtiquetaDeCaja + "</div> " +
            "<div><strong>Fecha de Creacion</strong>:" + FormatDate(data.data[n].FechaOrdenTrabajo) + "</div> " +
            "<div><strong>Fraccion Estandar c/ Relevo</strong>:" + data.data[n].FracEstandarConRelevo + "</div> " +
            "<div><strong>Hora Finalizacion</strong>:" + FormatTime(data.data[n].HoraFinalizacion) + "</div> " +
            "<div><strong>Hora Inicio</strong>:" + FormatTime(data.data[n].HoraInicio) + "</div> " +
            "<div><strong>Horas Trabajadas</strong>:" + data.data[n].HorasTrabajadasCalculado + "</div> " +
            "<div><strong>Porcentaje Scrap</strong>:" + data.data[n].PorcentajeScrapCalculado + "</div> " +
            "<div><strong>Scrap</strong>:" + data.data[n].ScrapCalculado + "</div> " +
             "</div>");
    });

    $("#DetallesOrdenTrabajo").modal("show");
 
}




function ObtenerOrdenTrabajoByDateRange(start, end) {
    document.getElementById("busquedaDatePicker").value = "";
    document.getElementById("busquedaDatePicker").value = $(".drp-selected").text();
    const dateStart = new Date(start._d);
    const dateEnd = new Date(end._d);
    
    $.ajax({
        type: 'POST',
        url: '../../OrdenTrabajoes/ObtenerOrdenTrabajoByDateRange',
        data: {
            dateStart: dateStart.toISOString(),
            dateEnd: dateEnd.toISOString()
        },
        dataType: 'json',
        success: function (data) {
            AgregarTableResultados(data);
        }
    });
}
function ObtenerOrdenTrabajoByOTCode() {
    var otCode = document.getElementById("idCode").value;
    $.ajax({
        type: 'POST',
        url: '../../OrdenTrabajoes/ObtenerOrdenTrabajoByOTCode',
        dataType: 'json',
        success: function (data) {
            console.log(data);
        }
    });
}
function FillOrdenTrabajoAutoComplete() {
    $.ajax({
        type: 'GET',
        url: '../../OrdenTrabajoes/ObtenerOrdenesTrabajo',
        dataType: 'json',
       
        success: function (data) {
       
            var ot = [];
          
            for (var x = 0; x < data.data.length; x++) {
                ot.push(data.data[x].idCodigoOrdenTrabajo);
            }
            autocomplete(document.getElementById("myInput"), ot);
        }
    });
}

function autocomplete(inp, arr) {
    /*the autocomplete function takes two arguments,
    the text field element and an array of possible autocompleted values:*/
    var currentFocus;
    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function (e) {
        var a, b, i, val = this.value;
        /*close any already open lists of autocompleted values*/
        closeAllLists();
        if (!val) { return false; }
        currentFocus = -1;
        /*create a DIV element that will contain the items (values):*/
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        /*append the DIV element as a child of the autocomplete container:*/
        this.parentNode.appendChild(a);
        /*for each item in the array...*/
        for (i = 0; i < arr.length; i++) {
            /*check if the item starts with the same letters as the text field value:*/
            if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                /*create a DIV element for each matching element:*/
                b = document.createElement("DIV");
                /*make the matching letters bold:*/
                b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                b.innerHTML += arr[i].substr(val.length);
                /*insert a input field that will hold the current array item's value:*/
                b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                /*execute a function when someone clicks on the item value (DIV element):*/
                b.addEventListener("click", function (e) {
                    /*insert the value for the autocomplete text field:*/
                    inp.value = this.getElementsByTagName("input")[0].value;
                    /*close the list of autocompleted values,
                    (or any other open lists of autocompleted values:*/
                    closeAllLists();
                });
                a.appendChild(b);
            }
        }
    });
    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();
            }
        }
    });
    function addActive(x) {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        /*start by removing the "active" class on all items:*/
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("autocomplete-active");
    }
    function removeActive(x) {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    function closeAllLists(elmnt) {
        /*close all autocomplete lists in the document,
        except the one passed as an argument:*/
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
   
    /*initiate the autocomplete function on the "myInput" element, and pass along the countries array as possible autocomplete values:*/
    
}

function FormatDate(fecha) {
    let dateTimeEST = new Date(fecha);
    return dateTimeEST.toLocaleDateString();
}
function FormatTime(fecha) {
    let dateTimeEST = new Date(fecha);
    return dateTimeEST.toLocaleTimeString();
}
function EvaluateEstado(IdEstadoOrdenFK) {
    switch (IdEstadoOrdenFK) {
        case 7: return "<p>PLANEADO</p>";
            break;
        case 9: return "<p>ACTIVA</p>";
            break;
        case 10: return "<p>PARA LIBERAR</p>";
            break;
        case 11: return "<p>TERMINADA</p>";
            break;
        case 12: return "<p>PAUSADA</p>";
            break;


    }
}
function ObtenerTiempoMinutos(date1,date2) {
    dt1 = new Date(date1);
    dt2 = new Date(date2);
     return diff_minutes(dt1, dt2);
}
function diff_minutes(dt2, dt1) {

    var diff = (dt2.getTime() - dt1.getTime()) / 1000;
    diff /= 60;
    return Math.abs(Math.round(diff));

}
//-------------------------------------------------------modal para busqueda de ordenes de trabajo