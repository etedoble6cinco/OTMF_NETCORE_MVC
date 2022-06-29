namespace OTMF_NETCORE_MVC.Entities
{
    public class ObtenerPartes
    {

        public int IdParte { get; set; }    
        public int IdCodigoParte { get; set; }  
        public string Aluminio { get; set; }    
        public int CajasPorTarima { get; set; } 
        public double Costo { get; set; }   
        public int EstandarPorHora { get; set; }    
        public int PiezasPorCaja { get; set; }  
        public int Scrap { get; set; }  
        public int Std_Pintura { get; set; } 
        public int IdCaja { get; set; }
        
        public string  NombreCaja   { get; set; }  
        public int IdColor { get; set; }    
        
        public string NombreColor { get; set; } 
        public int IdPintura { get; set; }  
        public string NombrePintura { get; set; }   
        public int IdTarima { get; set; }   
        public string NombreTarima { get; set; }    
        public int IdMolde { get; set; }    
        public string NombreMolde { get; set; } 
        public int IdEnsamble { get; set; } 
        public string NombreEnsamble { get; set; }  

        public int IdEstandarConRelevo { get; set; }    
        public string  NombreEstandarConRelevo { get; set; }
        public int IdEtiqueta { get; set; } 
        public string NombreEtiqueta { get; set; }
        public int IdInserto { get; set; }
        public string NombreInserto { get; set; }
        public int  IdHule { get; set; }
        public string  NombreHule { get; set; }

        public int IdEstandar { get; set; }
        public string NombreEstandar { get; set; }  
        public int IdCliente { get; set; }
        public string  NombreCliente { get; set; }  
    }
}
