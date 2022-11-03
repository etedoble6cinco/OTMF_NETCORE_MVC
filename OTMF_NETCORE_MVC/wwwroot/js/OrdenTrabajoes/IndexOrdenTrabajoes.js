

$(document).ready(function () {
   

    $("#OrdenesDeTrabajo").DataTable({
        "processing": true,
        "serverside": true,
        "searching": true,
        "info": true,
        "autoWidth": false,
        "ajax": {
            "url": "../../OrdenTrabajoes/ObtenerOrdenesTrabajo",
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
            ,
            {
                text: '<i class="fa fa-file" aria-hidden="true"></i>',
                tittleAttr: 'Obtener Reporte',
                className: 'btn btn-lg btn-warning',
                action: function () {
                    window.location.href = "Produccion/Reporte";
                }
            }
            ,
            {
                text: '<i class="fa fa-search" aria-hidden="true"></i>',
                tittleAttr: 'Obtener Reporte',
                className: 'btn btn-lg btn-warning',
                action: function () {
                    AsignacionEmpleadosOrdenTrabajo();
                }
            }


        ]
       ,
        autoFill: true,
        responsive: true,
        select: true,
        rowId: "idOrdenTrabajo",
        "columns": [
            { "data": "nombreMaquina" },
            { "data": "idCodigoOrdenTrabajo" },
            { "data": "idCodigoParte" },
            { "data": "fechaOrdenTrabajo"},
            { "data": "nombreEstadoOrden" }
            
            
           
        ]
    });
    var table = $('#OrdenesDeTrabajo').DataTable();
    $('#OrdenesDeTrabajo').on('click', 'td', function () {
        var id = table.row(this).id();
        window.location.href = "../../OrdenTrabajoes/Details/" + id;

    });

});


