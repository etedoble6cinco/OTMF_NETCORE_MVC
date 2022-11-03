$(document).ready(function () {


    $("#Empleadoes").DataTable({
        "ajax": {
            "url": "Empleadoes/ObtenerEmpleados",
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
                    window.location.href = "../../Empleadoes/Create";
                }
            }


        ],
        autoFill: false,
        responsive: true,
        select: false,
        rowId: 'idEmpleado',
        "columns": [
            { "data": "idEmpleado" },
            { "data": "nombreEmpleado" },
            { "data": "claveEmpleado" },
            { "data": "idTipoEmpleado" },
            { "data": "nombreTipoEmpleado" },
            { "data": "idTurno" },
            { "data": "nombreTurno" }


        ],
        columnDefs: [
            {
                targets: 0,
                visible: false
            }
        ]
    });

    var table = $('#Empleadoes').DataTable();
    $('#Empleadoes').on('click', 'td', function () {
        var id = table.row(this).id();
        window.location.href = "Empleadoes/Details/" + id;

    });


});




function GetDetailsEmpleadoes(event) {


    let d = document.getElementsByTagName("#Empleadoes tbody tr");
    console.log(d);
    // 
}