$(document).ready(function () {
    ObtenerFraccionEstandarRelevo();
    ObtenerPorcentajeScrapPermitido();
    ObtenerTurnoOt();
    $("#Usuarios").DataTable({});

});

function ObtenerPorcentajeScrapPermitido() {

    $.ajax({
        type: 'GET',
        url: '@Url.Action("ObtenerPorcentajeScrapPermitido")',
        dataType: 'json',
        success: function (data) {

            $("#ScrapPermitido").html("");
            $("#ScrapPermitido").append("<p>" + data.data[0].porcentajeScrapPermitido + "</p>");
        }
    })

}
function UpdatePrimerHorario() {
    var HorasTrabajadasPrimerTurno = document.getElementById("HorasTrabajadasPrimerHorario").value;
    var NombrePrimerTurno = document.getElementById("NombreTurnoPrimerHorario").value;
    console.log(NombreTurnoPrimerHorario);
    $.ajax({
        type: 'POST',
        url: '@Url.Action("UpdatePrimerTurnoOt")',
        dataType: 'json',
        data: {
            NombreTurno: NombrePrimerTurno,
            HorasTrabajadas: HorasTrabajadasPrimerTurno
        },
        success: function (data) {

            ObtenerTurnoOt();
            $("#PrimerHorario").modal('hide');

        }
    });
}
function UpdateSegundoHorario() {
    var HorasTrabajadasSegundoTurno = document.getElementById("HorasTrabajadasSegundoTurno").value;
    var NombreSegundoTurno = document.getElementById("NombreSegundoTurno").value;
    $.ajax({
        type: 'POST',
        url: '@Url.Action("UpdateSegundoTurnoOt")',
        dataType: 'json',
        data: {
            NombreTurno: NombreSegundoTurno,
            HorasTrabajadas: HorasTrabajadasSegundoTurno
        },
        success: function (data) {

            ObtenerTurnoOt();
            $("#SegundoHorario").modal('hide');
        }
    });

}
function ObtenerTurnoOt() {
    $.ajax({
        type: 'GET',
        url: '@Url.Action("ObtenerTurnoOt")',
        dataType: 'json',
        success: function (data) {

            $("#HorariosContent").html("");
            $("#HorariosContent").append("<tr><td>" + data.data[0].nombreTurno + "</td>" +
                "<td>" + data.data[0].horasTrabajadas + "</td>" +
                "<td><button class='btn btn-primary' data-bs-toggle='modal' data-bs-target='#PrimerHorario'>Editar</button></td></tr>");
            $("#HorariosContent").append("<tr><td>" + data.data[1].nombreTurno + "</td>" +
                "<td>" + data.data[1].horasTrabajadas + "</td>" +
                "<td><button class='btn btn-primary' data-bs-toggle='modal' data-bs-target='#SegundoHorario'>Editar</button></td></tr>");
        }
    })
}
function ObtenerFraccionEstandarRelevo() {
    $.ajax({
        type: 'GET',
        url: '@Url.Action("ObtenerFraccionEstandarRelevo")',
        dataType: 'json',
        success: function (data) {

            $("#FraccionEstandar").html("");
            $("#FraccionEstandar").append("<p>" + data.data[0].fracEstandarRelevo +
                "</p>");
        }
    });
}
function UpdateFraccionEstandarRelevo() {
    var FraccionEstandarRelevo = document.getElementById("FraccionEstandarRelevo").value;
    $.ajax({
        type: 'POST',
        url: '@Url.Action("UpdateFraccionEstandarRelevo")',
        dataType: 'json',
        data: {
            FracEstandarRelevo: FraccionEstandarRelevo
        },
        success: function (data) {

            ObtenerFraccionEstandarRelevo();
            $('#FraccionEstandarModal').modal('hide');
        }
    });
}
function UpdatePorcentajeScrapPermitido() {
    var PorcentajeScrapPermitido = document.getElementById("PorcentajeScrapPermitido").value;
    $.ajax({
        type: 'POST',
        url: '@Url.Action("UpdatePorcentajeScrapPermitido")',
        dataType: 'json',
        data: {
            PorcentajeScrapPermitido: PorcentajeScrapPermitido
        },
        success: function (data) {
            ObtenerPorcentajeScrapPermitido();
            $('#ScrapPermitidoModal').modal('hide');
        }

    });
}

function ObtenerRolesUsuariosByIdUsuario(IdUsuario, RolesUsuarios) {
    localStorage.setItem("User", IdUsuario);


    $.ajax({
        type: 'POST',
        url: '@Url.Action("ObtenerRolesUsuariosByIdUsuario")',
        dataType: 'json',
        data: {
            IdUsuario: IdUsuario
        },
        success: function (data) {
            $("#RolesCheckGroup").html("");
            FillRoles(data);

        }
    });

}
function UpsertRolesUsuariosByIdUsuario() {
    var Administrador = 0;
    var Supervisor = 0;
    var Operador = 0;
    var IdUsuario = localStorage.getItem("User");
    if (document.getElementById("Administrador").checked) {
        Administrador = 1;
    } else {

    }
    if (document.getElementById("Supervisor").checked) {
        Supervisor = 2;
    } else {

    }
    if (document.getElementById("Operador").checked) {
        Operador = 3;
    } else {

    }

    $.ajax({
        type: 'POST',
        url: '@Url.Action("UpsertRolesUsuariosByIdUsuario")',
        dataType: 'json',
        data: {
            IdUsuario: IdUsuario,
            Administrador: Administrador,
            Supervisor: Supervisor,
            Operador: Operador
        },
        success: function (data) {

        }
    });

}
function FillRoles(data) {

    if (data.data[0]) {

        $.each(data.data, function (n) {

            if (data.data[n].NombreRolUsuario == "Administrador") {

                $("#RolesCheckGroup").append("<div class='form-check'>" +
                    "<input class='form-check-input' type='checkbox'  id='Administrador' checked>" +
                    "<label class='form-check-label' for='flexCheckDefault'>Administrador</label></div>");
            } else {

                $("#RolesCheckGroup").append("<div class='form-check'>" +
                    "<input class='form-check-input' type='checkbox'  id='Administrador'>" +
                    "<label class='form-check-label' for='flexCheckDefault'>Administrador</label></div>");


            }
        });
        $.each(data.data, function (n) {
            if (data.data[n].NombreRolUsuario == "Supervisor") {
                $("#RolesCheckGroup").append("<div class='form-check'>" +
                    "<input class='form-check-input' type='checkbox'  id='Supervisor' checked>" +
                    "<label class='form-check-label' for='flexCheckDefault'>Supervisor</label></div>");
            } else {

                $("#RolesCheckGroup").append("<div class='form-check'>" +
                    "<input class='form-check-input' type='checkbox'  id='Supervisor'>" +
                    "<label class='form-check-label' for='flexCheckDefault'>Supervisor</label></div>");

            }
        });
        $.each(data.data, function (n) {
            if (data.data[n].NombreRolUsuario == "Operador") {
                $("#RolesCheckGroup").append("<div class='form-check'>" +
                    "<input class='form-check-input' type='checkbox'  id='Operador' checked>" +
                    "<label class='form-check-label' for='flexCheckDefault'>Operador</label></div>");
            } else {

                $("#RolesCheckGroup").append("<div class='form-check'>" +
                    "<input class='form-check-input' type='checkbox'  id='Operador'>" +
                    "<label class='form-check-label' for='flexCheckDefault'>Operador</label></div>");


            }
        });


    } else {
        $("#RolesCheckGroup").html("");
        $("#RolesCheckGroup").append("<div class='form-check'>" +
            "<input class='form-check-input' type='checkbox'  id='Administrador'>" +
            "<label class='form-check-label' for='flexCheckDefault'>Administrador</label></div>");
        $("#RolesCheckGroup").append("<div class='form-check'>" +
            "<input class='form-check-input' type='checkbox'  id='Supervisor'>" +
            "<label class='form-check-label' for='flexCheckDefault'>Supervisor</label></div>");
        $("#RolesCheckGroup").append("<div class='form-check'>" +
            "<input class='form-check-input' type='checkbox'  id='Operador'>" +
            "<label class='form-check-label' for='flexCheckDefault'>Operador</label></div>");
    }


}
