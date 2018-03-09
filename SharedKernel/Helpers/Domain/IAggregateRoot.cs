using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Helpers
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}
