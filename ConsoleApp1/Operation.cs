using System;

namespace ConsoleApp1
{
    public class Operation : ITransientOperation, IScopedOperation, ISingletonOperation
    {
        public Guid OperationId { get; } = Guid.NewGuid();
    }
}

