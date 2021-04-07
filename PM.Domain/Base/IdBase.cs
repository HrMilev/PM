using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Domain.Base
{
    public class IdBase<T>
    {
        public T Id { get; set; }
    }
}
