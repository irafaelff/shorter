using Shorter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorter.Repository.Abstractions
{
    public interface IUrlRepository
    {
        Task Add(ShorterUrl shorterUrl);
    }
}
