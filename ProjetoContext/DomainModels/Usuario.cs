using SharedKernel.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoContext.DomainModels
{
    public class Usuario : IAggregateRoot
    {
        public Usuario()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
