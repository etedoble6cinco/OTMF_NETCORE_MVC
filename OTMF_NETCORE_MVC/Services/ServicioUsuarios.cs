using System.Security.Claims;

namespace OTMF_NETCORE_MVC.Services
{
    public interface IServicioUsuarios
    {
        int ObtenerUsuarioId();
    }
    public class ServicioUsuarios : IServicioUsuarios
    {
        private readonly HttpContext _httpContext;
        public ServicioUsuarios(IHttpContextAccessor httpContextAccessor)
        {
            
            
            _httpContext = httpContextAccessor.HttpContext;
        }
        public int ObtenerUsuarioId()
        {
            if (_httpContext.User.Identity.IsAuthenticated)
            {
                var idClaims = _httpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                var id = int.Parse(idClaims.Value);
                return id;
            }else
            {
                throw new ApplicationException("El usuario no esta autenticado");
            }

            return 0;   
        }
    }
}
