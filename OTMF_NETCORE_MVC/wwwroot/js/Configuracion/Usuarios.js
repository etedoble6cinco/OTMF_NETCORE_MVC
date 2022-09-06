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

$(document).ready(function () {


    $("#Usuarios").DataTable({});




});

