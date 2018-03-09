using SharedKernel.Helpers;
using System;

namespace ProjetoContext.DomainModels
{
    public class Produto : IAggregateRoot
    {
        public Produto()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
