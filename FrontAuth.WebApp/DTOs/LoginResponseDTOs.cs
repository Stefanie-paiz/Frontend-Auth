using FrontAuth.WebApp.DTOS.UsuarioDTOs;

namespace FrontAuth.WebApp.DTOs
{
    public class LoginResponseDTO : UsuarioDTO
    {
        public string Token { get; set; }
    }
}
