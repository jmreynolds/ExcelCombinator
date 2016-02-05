using Core;
using DataAccess;
using Ninject.Modules;

namespace Infrastructure.Modules
{
    public class NetOfficeModule : NinjectModule
    {
        public NetOfficeModule()
        {
            
        }
        public override void Load()
        {
            Bind<IReadExcelFiles>().To<ReadExcelFiles>();
            Bind<IProcessor>().To<Processor>();
            Bind<IWriteExcelFiles>().To<WriteExcelFiles>();
        }
    }
}