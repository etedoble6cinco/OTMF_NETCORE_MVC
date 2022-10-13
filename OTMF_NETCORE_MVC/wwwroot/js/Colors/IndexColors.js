$(document).ready(function () {


    $("#Colors").DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                text: '<i class="fa fa-plus" aria-hidden="true"><div class="badge"></div></i>',
                tittleAttr: 'creadas hoy',
                className: 'btn btn-lg btn-secondary',
                action: function () {

                    window.location.href = "../../Colors/Create";

                }
            }
        ]
    });

    var menuItem = document.getElementById("ColorsMenuItem");
    menuItem.classList.add("active");


});