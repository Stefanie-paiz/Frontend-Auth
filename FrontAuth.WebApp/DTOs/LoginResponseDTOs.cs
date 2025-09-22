using FrontAuth.WebApp.DTOs.UsuarioDTOs;

namespace FrontAuth.WebApp.DTOs.UsuarioDTOs
{
    public class LoginResponseDTO : UsuarioDTO
    {
        public string Token { get; set; }
    }
}