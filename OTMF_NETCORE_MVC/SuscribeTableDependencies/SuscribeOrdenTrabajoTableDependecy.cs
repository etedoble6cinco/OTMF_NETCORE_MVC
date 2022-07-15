
using OTMF_NETCORE_MVC.Hubs;
using OTMF_NETCORE_MVC.Models;
using TableDependency.SqlClient;

namespace OTMF_NETCORE_MVC.SuscribeTableDependencies
{
    public class SuscribeOrdenTrabajoTableDependecy
    {
        SqlTableDependency<OrdenTrabajo> TableDependency;
        DashboardHub dashboardHub;
        public SuscribeOrdenTrabajoTableDependecy(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;   
        }
        public void SuscribeTableDependecy ()
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=OTMF;Integrated Security=True;Persist Security Info=False";
            TableDependency = new SqlTableDependency<OrdenTrabajo>(connectionString);
            TableDependency.OnChanged += TableDependency_OnChanged;
            TableDependency.OnError += TableDependency_OnError;
            TableDependency.Start();
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
