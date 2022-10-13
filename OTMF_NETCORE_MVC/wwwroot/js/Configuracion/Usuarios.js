function ObtenerRolesUsuariosByIdUsuario(IdUsuario, RolesUsuarios) {
   
    localStorage.setItem("User", IdUsuario);


    $.ajax({
        type: 'POST',
        url: '../../Configuracion/ObtenerRolesUsuariosByIdUsuario',
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
        url: '../../Configuracion/UpsertRolesUsuariosByIdUsuario',
        dataType: 'json',
        data: {
            IdUsuario: IdUsuario,
            Administrador: Administrador,
            Supervisor: Supervisor,
            Operador: Operador
        },
        success: function (data) {
            $("#exampleModal").modal("hide");
        }
    });

}
function FillRoles(data) {
    const roles = [];
    $.each(data.data, function (n) {
        if (data.data[n].NombreRolUsuario == "Administrador") {
            roles.push(data.data[n].NombreRolUsuario)
        }
        if (data.data[n].NombreRolUsuario == "Supervisor") {
            roles.push(data.data[n].NombreRolUsuario)
        }
        if (data.data[n].NombreRolUsuario == "Operador") {
            roles.push(data.data[n].NombreRolUsuario)
         }
    });

    if (data.data[0]) {

        $.each(roles, function (n) {
           
            if (roles[n] == "Administrador") {

                $("#RolesCheckGroup").append("<div class='form-check'>" +
                    "<input class='form-check-input' type='checkbox'  id='Administrador' checked>" +
                    "<label class='form-check-label' for='flexCheckDefault'>Administrador</label></div>");
            } else {
               
                if (roles[n] == "Supervisor") {
                    $("#RolesCheckGroup").append("<div class='form-check'>" +
                        "<input class='form-check-input' type='checkbox'  id='Supervisor' checked>" +
                        "<label class='form-check-label' for='flexCheckDefault'>Supervisor</label></div>");
                }
                else {
                 
                    if (roles[n] == "Operador") {
                        $("#RolesCheckGroup").append("<div class='form-check'>" +
                            "<input class='form-check-input' type='checkbox'  id='Operador' checked>" +
                            "<label class='form-check-label' for='flexCheckDefault'>Operador</label></div>");
                    }
                }
               
       


            }
        });
       

        if (roles.indexOf("Administrador") === -1) {
            $("#RolesCheckGroup").append("<div class='form-check'>" +
                "<input class='form-check-input' type='checkbox'  id='Administrador'>" +
                "<label class='form-check-label' for='flexCheckDefault'>Administrador</label></div>");
            }
        if (roles.indexOf("Supervisor") === -1) {
            $("#RolesCheckGroup").append("<div class='form-check'>" +
                "<input class='form-check-input' type='checkbox'  id='Supervisor'>" +
                "<label class='form-check-label' for='flexCheckDefault'>Supervisor</label></div>");
        }
        if (roles.indexOf("Operador") === -1) {
            $("#RolesCheckGroup").append("<div class='form-check'>" +
                "<input class='form-check-input' type='checkbox'  id='Operador'>" +
                "<label class='form-check-label' for='flexCheckDefault'>Operador</label></div>");
        }


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

function FillMaquinas(data) {
   
    $("#MaquinaSelectControl").html("");
    ObtenerRelacionMaquinasUsuarios();
    if (data.data.length > 0) {
        $("#MaquinaSelectControl").append("<select class='form-select'  name='MaquinasSelect' id='Maquinas'></select>");
        data.data.forEach((c) => {
            if (c.estadoMaquina == true) {
                $("#AsignacionMaquinas .modal-body #Maquinas").append("<option value='" + c.idMaquina + "'>" + c.nombreMaquina + "</option>");
            }
           
        });
      
      

    }
  
}


function ObtenerMaquinas() {

    $.ajax({
        type: 'POST',
        url: '../../Configuracion/ObtenerMaquinas',
        dataType: 'json',
        success: function (data) {
            FillMaquinas(data);
            $("#MaquinasAsignadas").html("");
        }
    });

}


function ObtenerRolesUsuariosByIdUsuarioAsignacionMaquinas(IdUsuario){
    localStorage.setItem("User", IdUsuario);
    $.ajax({
        type: 'POST',
        url: '../../Configuracion/ObtenerRolesUsuariosByIdUsuario',
        dataType: 'json',
        data: {
            IdUsuario: IdUsuario
        },
        success: function (data) {
            
            if (data.data.length > 0) {
                data.data.forEach(c => CheckRol(c));
            } else {
                $("#MaquinaCheckGroup").show();
                $("#MaquinaSelectControl").html("");
                $("#AsignacionMaquinas .modal-footer").hide();
                $("#MaquinasRelacionadas").hide();
            }
           
        



        }
    });


}
function CheckRol(user) {
   
 
        if (user.NombreRolUsuario === "Operador") {
            $("#MaquinaCheckGroup").hide();
            ObtenerMaquinas();
            $("#AsignacionMaquinas .modal-footer").show();
            $("#MaquinasRelacionadas").show();
        }
        if (user.NombreRolUsuario != "Operador") {
            $("#MaquinaCheckGroup").show();
            $("#MaquinaSelectControl").html("");
            $("#AsignacionMaquinas .modal-footer").hide();
            $("#MaquinasRelacionadas").hide();
        }
}
function ObtenerRelacionMaquinasUsuarios() {
    var IdUsuarioFK = localStorage.getItem("User");
  
    $.ajax({
        type: 'POST',
        url: '../../Configuracion/ObtenerRelacionMaquinasUsuarios',
        dataType: 'json',
        data: {
            IdUsuarioFK: IdUsuarioFK
        },
        success: function (data) {
            console.log(data);
            if (data.data.length > 0) {
                data.data.forEach(c => FillRelacionMaquinasUsuarios(c));
            } 
        }
    });
}

function FillRelacionMaquinasUsuarios(c) {
    
    $("#MaquinasAsignadas").append("<tr>" +
        "<td>" + c.NombreMaquina + "</td><td> <button class='btn  btn-primary'  onclick=\"DeleteRelacionMaquinasUsuarios(\'" + c.IdUsuarioMaquina + "," + c.IdMaquinaFK + "," + c.NombreMaquina + "\')\"><i class='fa fa-trash' aria-hidden='true'></i></button> </td>" +
      
        +"</tr>");

}

function InsertRelacionMaquinasUsuarios() {
    var user = localStorage.getItem("User");
    var M = $("#Maquinas").val();
    var NombreMaquina = $("#Maquinas option:selected").text();
   
    $.ajax({
        type: 'POST',
        url: '../../Configuracion/InsertRelacionMaquinasUsuarios',
        dataType: 'json',
        data: {
            IdUsuarioFK: user,
            IdMaquinaFK: M,
            NombreMaquina: NombreMaquina
        },
        success: function (data) {
            ObtenerRelacionMaquinasUsuarios();
            ObtenerMaquinas();
        }
    });
}
function DeleteRelacionMaquinasUsuarios(data) {
    const a = data.split(",");
    let IdUsuarioMaquina = a[0];
    let IdMaquinaFK = a[1];
    let NombreMaquina = a[2];
    $.ajax({
        type: 'POST',
        url: '../../Configuracion/DeleteRelacionMaquinasUsuarios',
        dataType: 'json',
        data: {
            IdUsuariosMaquinas: IdUsuarioMaquina,
            IdMaquinaFK: IdMaquinaFK,
            NombreMaquina: NombreMaquina
        },
        success: function (data) {
            ObtenerRelacionMaquinasUsuarios();
            ObtenerMaquinas();

            alertify.message("Elemento eliminado");
               

        }
    });
}
