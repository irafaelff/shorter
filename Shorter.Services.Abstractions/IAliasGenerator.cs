using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorter.Services.Abstractions
{
    public interface IAliasGenerator
    {
        string GetRandomAlias();
    }
}
