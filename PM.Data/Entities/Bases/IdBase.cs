using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Data.Entities.Bases
{
    public class IdBase<T>
    {
        public T Id { get; set; }
    }
}
