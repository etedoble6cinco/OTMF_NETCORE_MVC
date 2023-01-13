function CheckChkEspecialValue() {
    var chk = document.getElementById("flexCheckChecked").checked;

    if (chk == true) {
        var inputPrefixValue = document.getElementById("InputNormalot").value;
        document.getElementById("InputPrefixValue").value = inputPrefixValue;
        document.getElementById("IdOTespecial").value = true;

        $('#Createot').submit();
    }
    else {
        var inputPrefixValue = document.getElementById("InputNormalot").value;
        document.getElementById("IdOTespecial").value = false;

        $('#Createot').submit();
    }

}
function Getchkvalue() {
    var inputPrefixValue = document.getElementById("InputPrefixValue").value;
    var prefix = document.getElementById("basic-addon1").innerText;

    $("#prefixOT section").toggle();
    var chk = document.getElementById("flexCheckChecked").value;


}

$(document).ready(function () {

    ObtenerPrefixOT();
    ObtenerUltimoIdOT();
    SetDefaultEstado();
    var now = new Date();
    now.setMinutes(now.getMinutes() - now.getTimezoneOffset());
    document.getElementById('HoraInicio').value = now.toISOString().slice(0, 16);
    document.getElementById('HoraFinalizacion').value = now.toISOString().slice(0, 16);
    document.getElementById('FechaOrdenTrabajo').value = now.toISOString().slice(0, 16);

});
function ObtenerUltimoIdOT() {
    $.ajax({
        type: 'GET',
        url: '../../OrdenTrabajoes/ObtenerUltimoIdOT',
        dataType: 'json',
        success: function (data) {
            document.getElementById('IdOrdenTrabajo').value = data.data[0].ultimoid + 1;

        }
    });
}
function ObtenerPrefixOT() {
    $.ajax({
        type: 'GET',
        url: '../../OrdenTrabajoes/ObtenerPrefixOT',
        dataType: 'json',
        success: function (data) {
            console.log(data.data);
            document.getElementById("basic-addon1").innerText = "";
            document.getElementById("basic-addon1").innerText = data.data.nombrePrefix;
        }

    });

}
function UpdatePrefixOT() {
    var prefixOt = document.getElementById("prefix").value;
    $.ajax({
        type: 'POST',
        url: '../../OrdenTrabajoes/UpdatePrefixOT',
        dataType: 'json',
        data: {
            prefixOt: prefixOt
        },
        success: function (data) {

            ObtenerPrefixOT();
        }
    });
}

function GetchkvalueCantidadPiezasPorOrden() {

    $("#CantidadPiezasPorOrden").toggle();
    if (document.getElementById("CantidadPiezasPorOrdenChk").checked  == true) {

        document.getElementById("IdCantidadPiezasOtflag").value = true;
      

    }
    else {

        document.getElementById("IdCantidadPiezasOtflag").value = false;
    }

    
}


function SetDefaultEstado() {
    const $selectEtiquetaParte = document.querySelector('#IdEstadoOrdenFk');
    $selectEtiquetaParte.value =7 ;
}

