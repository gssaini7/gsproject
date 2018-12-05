using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using R.Resolver;
using R.DAL.UnitOfWork;
using System.ComponentModel.Composition;

namespace R.DAL
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<iUnitOfWork, cUnitOfWork>();

        }
    }
}
