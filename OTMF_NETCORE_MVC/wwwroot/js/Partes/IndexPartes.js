﻿
$(document).ready(function () {

 
    $("#Partes").DataTable({
        "processing": true,
        "serverside" : true,
        "searching": true,
        "info": true,
        "autoWidth": false,
        "ajax": {
            "url": "../../Partes/ObtenerPartes",
            "type": "POST",
            "datatype": "json"
        },
        dom: 'Bfrtip',

        buttons: [
            {
                extend: 'excelHtml5',
                text: '<i class="fas fa-file-excel"></i>',
                tittleAttr: 'Exportar a Excel',
                className: 'btn btn-lg btn-success'

            },

            {
                text: '<i class="fa fa-plus" aria-hidden="true"></i>',
                tittleAttr: 'Agregar Nuevo',
                className: 'btn btn-lg btn-primary',
                action: function () {
                    window.location.href = "Partes/Create";
                }
            }


        ],
       
        autoFill: true,
        responsive: true,
        select: true,
        rowId: "idParte",
        "columns": [

            { "data": "idCodigoParte" },
            { "data": "aluminio" },
            { "data": "cajasPorTarima" },
            { "data": "costo" },
        
            { "data": "piezasPorCaja" },
         
         
            { "data": "nombreCaja" },
            { "data": "nombreColor" },
            { "data": "nombrePintura" },
            { "data": "nombreTarima" },
            { "data": "nombreMolde" },
            { "data": "nombreEnsamble" },
        
            { "data": "nombreEtiqueta" },
            { "data": "nombreInserto" },
            { "data": "nombreHule" },

            { "data": "nombreCliente" }
           
        ]
    });
    var table = $('#Partes').DataTable();
    $('#Partes').on('click', 'td', function () {
        var id = table.row(this).id();
        window.location.href = "Partes/Details/" + id;

    });
    
    var menuItem = document.getElementById("PartesMenuItem");
    menuItem.classList.add("active");
});

