﻿$(document).ready(function () {


    $("#Insertos").DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                text: '<i class="fa fa-plus" aria-hidden="true"><div class="badge"></div></i>',
                tittleAttr: 'creadas hoy',
                className: 'btn btn-lg btn-secondary',
                action: function () {

                    window.location.href = "../../Insertoes/Create";

                }
            }
        ]
    });


    var menuItem = document.getElementById("InsertoesMenuItem");
    menuItem.classList.add("active");

});
