
    $(document).ready(function () {


        $("#Clientes").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    text: '<i class="fa fa-plus" aria-hidden="true"><div class="badge"></div></i>',
                    tittleAttr: 'creadas hoy',
                    className: 'btn btn-lg btn-secondary',
                    action: function () {

                        window.location.href = "../../Clientes/Create";

                    }
                }
            ]
        });

        var menuItem = document.getElementById("ClientesMenuItem");
        menuItem.classList.add("active");


    });