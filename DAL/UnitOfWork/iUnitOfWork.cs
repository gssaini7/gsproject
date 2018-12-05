using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.DAL.UnitOfWork
{
    public interface iUnitOfWork
    {
        void Save();
         Task SaveAsync();
    }
}
