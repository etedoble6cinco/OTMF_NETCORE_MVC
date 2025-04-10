﻿using Microsoft.AspNetCore.SignalR;
using OTMF_NETCORE_MVC.Services;
using System.Text.Json;

namespace OTMF_NETCORE_MVC.Hubs
{
    public class DashboardHub : Hub
    {
        RepositorioOrdenTrabajo repositorioOrdenesTrabajo;

        public DashboardHub(IConfiguration configuration)
        {
            var _configuration = configuration.GetConnectionString("DefaultConnection");
            repositorioOrdenesTrabajo = new RepositorioOrdenTrabajo(_configuration);
        }
        public async Task SendOrdenTrabajo()
        {
            var ot = await repositorioOrdenesTrabajo.ObtenerOrdenesTrabajo();
            await Clients.All.SendAsync("ReceivedOrdenTrabajo", new { data = ot });

        }
        public async Task SendOrdenTrabajoNoFilter()
        {
            var ot = await repositorioOrdenesTrabajo.ObtenerOrdenTrabajoNoFilter();
            await Clients.All.SendAsync("ReceivedOrdenTrabajoNoFilter", new { data = ot });
        }
       
    }
}
