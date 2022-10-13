$(document).ready(function () {


    $("#Ensamble").DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                text: '<i class="fa fa-plus" aria-hidden="true"><div class="badge"></div></i>',
                tittleAttr: 'creadas hoy',
                className: 'btn btn-lg btn-secondary',
                action: function () {

                    window.location.href = "../../Ensambles/Create";

                }
            }
        ]
    });

    var menuItem = document.getElementById("EnsamblesMenuItem");
    menuItem.classList.add("active");


});