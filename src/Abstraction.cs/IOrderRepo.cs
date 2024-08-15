using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Entity;
using Backend.src.Shared;

namespace Backend.src.Abstraction
{
    public interface IOrderRepo : IBaseRepo<Order>
    {
    }
}