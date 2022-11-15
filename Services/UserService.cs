using System.Security.Claims;

namespace TaskManager.Services
{
    public class UserService : IUserService
    {
        private readonly HttpContext _httpContext;

        public UserService(IHttpContextAccessor httpContextAccesor)
        {
            _httpContext = httpContextAccesor.HttpContext;
        }

        public string GetUserId()
        {
            if (_httpContext.User.Identity.IsAuthenticated)
            {
                var id = _httpContext.User.Claims
                    .Where(x => x.Type == ClaimTypes.NameIdentifier)
                    .FirstOrDefault();
                return id.Value;
            }
            else
            {
                throw new Exception("El usuario no esta autenticado");
            }
        }
    }

}
