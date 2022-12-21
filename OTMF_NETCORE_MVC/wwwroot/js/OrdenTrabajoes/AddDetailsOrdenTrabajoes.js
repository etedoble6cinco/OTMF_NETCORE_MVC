$(document).ready(function () {
    localStorage.clear();
    var IdOrdenTrabajo = document.getElementById('IdOrdenTrabajo').value;
    localStorage.setItem('currentOT', IdOrdenTrabajo);
    GetEmpleadosByOTId(IdOrdenTrabajo);
    ObtenerMaquinasAsignadasByOTId(IdOrdenTrabajo);
});

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
        url: '../../OTEstado/ObtenerEmpleadosByOTId',
        data: { id: idOrdenTrabajo },
        dataType: "json",
        success: function (data) {


            FillEmpleados(data, idOrdenTrabajo);
        }
    });


}
function FillEmpleados(data, idOrdenTrabajo) {
    var x = 0;
    console.log(data);
    $("#EmpleadosRelacionados").html("");

    $.each(data.data, function (n) {

        if (data.data[x].idTipoEmpleadoFK == 5) {
            $("#EmpleadosRelacionados").append("<section id='empleadoEmpacador' class='p-1 m-1 bg-secondary'>"
                + "<div class='badge bg-light text-dark'><h5>Empacador </h5><p>"
                + data.data[x].nombreEmpleado + "</p></div>" +
                "<button class='btn btn-lg btn-primary m-1 p-2' data-bs-toggle='modal' data-bs-target='#staticBackdrop' onclick=\"SetEditEmpleado(\'" + data.data[x].idEmpleadoOrdenTrabajo + "\');\"><i class='fas fa-edit'></i></button>" +
                "<button class='btn btn-lg btn-primary m-1 p-2'  onclick=\"DeleteAsignacionEmpleadoOTById(\'" + data.data[x].idEmpleadoOrdenTrabajo + "\');\"><i class='fa fa-trash' aria-hidden='true'></i></button> </section>");

        }
        if (data.data[x].idTipoEmpleadoFK == 6) {
            $("#EmpleadosRelacionados").append("<section id='empleadoMoldeador' class='p-1 m-1 bg-secondary'>"
                + "<div class='badge bg-light text-dark'><h5>Moldeador </h5><p>"
                + data.data[x].nombreEmpleado + "</p></div>" +
                "<button class='btn btn-lg btn-primary m-1 p-2' data-bs-toggle='modal' data-bs-target='#staticBackdrop' onclick=\"SetEditEmpleado(\'" + data.data[x].idEmpleadoOrdenTrabajo + "\');\"><i class='fas fa-edit' ></i></button>" +
                "<button class='btn btn-lg btn-primary m-1 p-2'  onclick=\"DeleteAsignacionEmpleadoOTById(\'" + data.data[x].idEmpleadoOrdenTrabajo + "\');\"><i class='fa fa-trash' aria-hidden='true'></i></button> </section>");



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
        url: '../../OTEstado/UpsertAsignacionEmpleadoOTById',
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

function ObtenerMaquinasAsignadasByOTId(IdOrdenTrabajo) {

    $.ajax({
        type: "POST",
        url: '../../OrdenTrabajoes/ObtenerMaquinasAsignadasByOTId',
        data: {
            IdOrdenTrabajo: IdOrdenTrabajo
        },
        dataType: "json",
        success: function (data) {
            $("#MaquinasRelacionadas").html("");
            FillMaquinas(data);
        }
    });

}

function FillMaquinas(data) {

    $.each(data.data, function (n) {

        ObtenerMaquinasById(data.data[n]);

    });
    $("#MaquinasRelacionadas").append("<section class='m-1'>" +
        "<button type='button' class='btn btn-primary m-1'  data-bs-toggle='modal' data-bs-target='#MaquinaModal'>" +
        "Asignar Linea</button>"
        + "</section>");

}

function ObtenerMaquinasById(dataRel) {

    $.ajax({
        type: "POST",
        url: "../../OrdenTrabajoes/ObtenerMaquinaById",
        data: {
            IdMaquina: dataRel.IdMaquinaFK
        }
        , dataType: "json",
        success: function (data) {
            console.log(dataRel);
            $("#MaquinasRelacionadas").append("<section id='Maquina' class='p-1 m-1 bg-secondary'>"
                + "<div class='badge bg-light text-dark'><h5>Linea </h5><p>"
                + data.data.nombreMaquina + "</p></div>" +
                "<button class='btn btn-lg btn-primary m-1 p-2' data-bs-toggle='modal' data-bs-target='#MaquinaModal' onclick=\"SetMaquinasAsignadasByOTId(\'" + dataRel.IdMaquinaOrdeTrabajo + "\');\"><i class='fas fa-edit' ></i></button>" +
                "<button class='btn btn-lg btn-primary m-1 p-2'  onclick=\"DeleteMaquinasAsignadasByOTId(\'" + dataRel.IdMaquinaOrdeTrabajo + "\');\"><i class='fa fa-trash' aria-hidden='true'></i></button> </section>");

        }
    });
}

function SetMaquinasAsignadasByOTId(IdMaquinaOrdenTrabajo) {
    localStorage.setItem('IdMaquinaOrdenTrabajo', IdMaquinaOrdenTrabajo);
}
function UpsertMaquinasAsignadasByOTId() {

    var IdMaquinaOrdenTrabajo = localStorage.getItem('IdMaquinaOrdenTrabajo');
    var IdMaquina = $("#EleccionMaquina").val();
    var IdOrdenTrabajo = localStorage.getItem('currentOT');

    $.ajax({
        type: "POST",
        url: "../../OrdenTrabajoes/UpsertMaquinasAsignadasByOTId",
        data: {
            IdMaquinaOrdenTrabajo: IdMaquinaOrdenTrabajo,
            IdOrdenTrabajo: IdOrdenTrabajo,
            IdMaquina: IdMaquina
        }, dataType: "json",
        success: function (data) {
            ObtenerMaquinasAsignadasByOTId(IdOrdenTrabajo);
        }
    });
}
function DeleteMaquinasAsignadasByOTId(IdMaquinaOrdenTrabajo) {
    var IdOrdenTrabajo = localStorage.getItem('currentOT');
    $.ajax({
        type: "POST",
        url: "../../OrdenTrabajoes/DeleteMaquinasAsignadasByOTId",
        data: {
            IdMaquinaOrdenTrabajo: IdMaquinaOrdenTrabajo
        }, dataType: "json",
        success: function (data) {
            $("#MaquinasRelacionadas").html("");
            ObtenerMaquinasAsignadasByOTId(IdOrdenTrabajo);
        }
    });
}