$(document).ready(function () {


    $("#OrdenesDeTrabajo").DataTable({
        "ajax": {
            "url": "OrdenTrabajoes/ObtenerOrdenesTrabajo",
            "type": "GET",
            "datatype": "json"
        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: '<i class="fas fa-file-excel"></i>',
                tittleAttr: 'Exportar a Excel',
                className: 'btn btn-lg btn-success'

            }
            ,
            {
                text: '<i class="fa fa-plus" aria-hidden="true"></i>',
                tittleAttr: 'Agregar Nuevo',
                className: 'btn btn-lg btn-primary',
                action: function () {
                    window.location.href = "OrdenTrabajoes/Create";
                }
            }


        ],
        autoFill: false,
        responsive: true,
        select: false,
        rowId: "idOrdenTrabajo",
        "columns": [
            { "data": "fechaOrdenTrabajo" },
            { "data": "horaInicio" },
            { "data": "horaFinalizacion" },
            { "data": "nombreEstadoOrden" },
            { "data": "idCodigoOrdenTrabajo" },
            { "data": "idCodigoParte" }
        ]
    });
    var table = $('#OrdenesDeTrabajo').DataTable();
    $('#OrdenesDeTrabajo').on('click', 'td', function () {
        var id = table.row(this).id();
        window.location.href = "OrdenTrabajoes/Details/" + id;

    });
    ObtenerPrefixOT();
    ObtenerUltimoIdOT();

    var now = new Date();
    now.setMinutes(now.getMinutes() - now.getTimezoneOffset());
    document.getElementById('HoraInicio').value = now.toISOString().slice(0, 16);
    document.getElementById('HoraFinalizacion').value = now.toISOString().slice(0, 16);
    document.getElementById('FechaOrdenTrabajo').value = now.toISOString().slice(0, 16);

});


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


function ObtenerUltimoIdOT() {
    $.ajax({
        type: 'GET',
        url: '@Url.Action("ObtenerUltimoIdOT")',
        dataType: 'json',
        success: function (data) {
            document.getElementById('IdOrdenTrabajo').value = data.data[0].ultimoid + 1;

        }
    });
}
function ObtenerPrefixOT() {
    $.ajax({
        type: 'GET',
        url: '@Url.Action("ObtenerPrefixOT")',
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
        url: '@Url.Action("UpdatePrefixOT")',
        dataType: 'json',
        data: {
            prefixOt: prefixOt
        },
        success: function (data) {

            ObtenerPrefixOT();
        }
    });
}


