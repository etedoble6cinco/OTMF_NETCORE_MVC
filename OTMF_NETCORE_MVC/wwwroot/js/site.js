


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
//--------------------------------------------------------------------------modal para busqueda de ordenes de trabajo
function AsignacionEmpleadosOrdenTrabajo () {
    $("body").append('<div class= "modal fade" id="RelacionEmpleadosOrdenTrabajoModal" >'+
        '<div class="modal-dialog">'+
            '<div class="modal-content">'+
                '<div class="modal-header">'+
                    '<button type="button" class="btn" onclick="CerrarRelacionEmpleados()">&times;</button>'+
                    '<h4 class="modal-title">Buscar Orden Trabajo</h4>'+
        '</div>' +

                '<div class="modal-body">'+
        '<div class="row">'+
        '<div class= "col-4"><div class="form-check">'+
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
               '<div class="container" id="eleccion"></div>'+
               '<div class="container" id="resultados"></div>'+
                '<div class="modal-footer">'+
                   ' <button type="button" class="btn btn-default" onclick="CerrarRelacionEmpleados()">Close</button>'+
                '</div>'+
            '</div>'+
        '</div>'+
        '</div>');


    $("#RelacionEmpleadosOrdenTrabajoModal").modal("show");
    $('input:radio[name="busqueda"]').change(function () {

        if ($(this).is(':checked') && $(this).val() == 'busquedaCodigo') {
            $("#eleccion").html("");
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
            var content = '<div class="d-flex flex-row">' +
                '<div class="form-check"><input type="radio" class="form-check-input" name="estado" id="activo">   <label    class="form-check-label" for="activo"   >Activa</label></div>'+
                '<div class="form-check"><input type="radio" class="form-check-input" name="estado" id="liberar">  <label   class="form-check-label" for="liberar"  >Por Liberar</label></div>'+
                '<div class="form-check"><input type="radio" class="form-check-input" name="estado" id="pausada">  <label   class="form-check-label" for="pausada"  >Pausada</label></div>'+
                '<div class="form-check"><input type="radio" class="form-check-input" name="estado" id="planeada"> <label  class="form-check-label" for="planeada" >Planeada</label></div>'+
                '<div class="form-check"><input type="radio" class="form-check-input" name="estado" id="terminada"><label class="form-check-label" for="terminada">Terminada</label></div>'+
                '</div>';
            $("#eleccion").append(content);
            
        }
        if ($(this).is(':checked') && $(this).val() == 'busquedaFecha') {
            $("#eleccion").html("");
            var content = '<div><input id="busquedaDatePicker" type="text" name="" placeholder></div><button>'
            +'</div > ';
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
                ObtenerOrdenTrabajoByDateRange(start, end);
            });
        }
    });
}
function AgregarTableResultados(data) {
    console.log(data);
    $("#resultados").html("");
    $("#resultados").append("<table class='table hover'>"
        +"<thead>"
        +"<tr>"
        +"<th>Codigo</th>"
        +"<th>Pieza</th>"
        +"<th>Estado</th>"
        +"</tr > "
        +"</thead > "
        +"<tbody id='resultados2'>"
        
        + "</tbody>"
        + "</table > ");

}

function ObtenerOrdenTrabajoByDateRange(start, end) {
    document.getElementById("busquedaDatePicker").value = $(".drp-selected").text();
    
    const dateStart = new Date(start._d);
    const dateEnd = new Date(end._d);
    ;
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
          
        }
    });
}
function FillOrdenTrabajoAutoComplete() {
    $.ajax({
        type: 'POST',
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
function CerrarRelacionEmpleados() {
    $("#RelacionEmpleadosOrdenTrabajoModal").modal("hide");
    $("#RelacionEmpleadosOrdenTrabajoModal").remove();

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
//-------------------------------------------------------modal para busqueda de ordenes de trabajo