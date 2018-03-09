using System;

namespace ProjetoContext.Application.Usuario
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
