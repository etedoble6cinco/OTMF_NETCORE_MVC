using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Entities;
using OTMF_NETCORE_MVC.Hubs;
using OTMF_NETCORE_MVC.Models;


namespace OTMF_NETCORE_MVC.Controllers
{
    public class MaquinaEstadoController : Controller
    {
        private readonly OTMFContext _context;
        private readonly string con;
        private readonly string connectionString;
        DashboardHub dashboardHub;
        public MaquinaEstadoController(DashboardHub dashboardHub, OTMFContext context, IConfiguration configuration)

        {
            this.dashboardHub = dashboardHub;
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
            con = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<IActionResult> Index()
        {



            return View(); 
        }


    }
}
