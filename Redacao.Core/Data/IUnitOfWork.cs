using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Core.Data
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
