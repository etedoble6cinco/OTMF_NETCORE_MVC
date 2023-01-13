namespace OTMF_NETCORE_MVC.Entities
{
    public class HistorialOrdenTrabajo
    {
        public string NombreEmpleadoMoldeador { get; set; } 
        public string NombreEmpleadoEmpacador { get; set; } 
        public string ClaveOrdenTrabajo  { get; set; }
        public string NombreMaquina { get; set; }   
        public string FechaOrdenTrabajo { get; set; }   
        public string CantidadPiezasPorOrden { get; set; }    
        public int NumeroCajasRecibidas { get; set; }   
        public int NumeroPiezasSueltasRecibidas { get; set; } 
        public int NumeroPiezasRealizadas { get; set; } 
        public string NumeroParte { get; set; } 
        public int NumeroCavidades { get; set; }    
        public double NumeroAluminio { get; set; }  
        public int NumeroCajasTarima { get; set; }
        public string NombreCliente { get; set; }   
        public string NombreColorPieza { get; set; }
        public string NombreEnsablePieza { get; set; }  
        public string NombreHulePieza { get; set; } 
        public string NombreInsertoPieza { get; set; }  
        public string NombreMoldePieza { get; set; }
        public string NombrePinturaPieza { get; set; }  
        public string NombreTarimaPieza { get; set; }
        public string NombreInstructivoPieza { get; set; }  
        public int PiezasPorCaja { get; set; }
        public string NombreAccesorios { get; set; }    
        public string NombreEtiquetaParteFK { get; set; }  
        public string  NombreEtiquetaCajaFK { get; set; }   
        public DateTime CreateDate { get; set; }  
        public DateTime UpdateDate { get; set; }
        public int IdUsuarioCreateBy { get; set; }  
        public int IdUsuarioUpdateBy { get; set; }  
        public string NombreUsuarioCreateBy { get; set; }  
        public string NombreUsuarioUpdateBy { get; set; }    

    }
}
