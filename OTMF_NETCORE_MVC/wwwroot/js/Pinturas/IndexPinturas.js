$(document).ready(function () {


    $("#Pinturas").DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                text: '<i class="fa fa-plus" aria-hidden="true"><div class="badge"></div></i>',
                tittleAttr: 'creadas hoy',
                className: 'btn btn-lg btn-secondary',
                action: function () {

                    window.location.href = "../../Pinturas/Create";

                }
            }
        ]
    });

    var menuItem = document.getElementById("PinturasMenuItem");
    menuItem.classList.add("active");


});