
using OTMF_NETCORE_MVC.Hubs;
using OTMF_NETCORE_MVC.Models;
using TableDependency.SqlClient;

namespace OTMF_NETCORE_MVC.SuscribeTableDependencies
{
    public class SuscribeOrdenTrabajoTableDependecy
    {
        SqlTableDependency<OrdenTrabajo> TableDependency;
        SqlTableDependency<MaquinaOrdenTrabajo> TableDependency2;
        SqlTableDependency<BitacoraOrdenTrabajo> TableDependency3;
        DashboardHub dashboardHub;
       
        private readonly string con;
        public SuscribeOrdenTrabajoTableDependecy(DashboardHub dashboardHub, IConfiguration configuration)
        {
            this.dashboardHub = dashboardHub;
            this.con = configuration.GetConnectionString("DefaultConnection");
        }
        public void SuscribeTableDependecy ()
        {
           
            TableDependency = new SqlTableDependency<OrdenTrabajo>(con);
            TableDependency.OnChanged += TableDependency_OnChanged;
           
            TableDependency.OnError += TableDependency_OnError;
            TableDependency.Start();
            TableDependency2 = new SqlTableDependency<MaquinaOrdenTrabajo>(con);
            TableDependency2.OnChanged += TableDependency2_OnChanged;
            TableDependency2.OnChanged += TableDependency2_OnError;

            TableDependency2.Start();
        }

        private void TableDependency2_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<MaquinaOrdenTrabajo> e)
        {
            Console.WriteLine($"{nameof(OrdenTrabajo)}SqlTableDependency error: {e}");
        }

        private void TableDependency2_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<MaquinaOrdenTrabajo> e)
        {
            dashboardHub.SendOrdenTrabajo();
        }

        private void TableDependency_OnError (object sender ,  TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(OrdenTrabajo)}SqlTableDependency error: {e.Error.Message}");
        }
        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<OrdenTrabajo> e)
        {
           //if(e.ChangeType != TableDependency)
                dashboardHub.SendOrdenTrabajo();
            
        }

    }
}
