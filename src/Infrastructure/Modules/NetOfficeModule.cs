using Core;
using DataAccess;
using Infrastructure.Logging;
using Ninject.Modules;

namespace Infrastructure.Modules
{
    public class NetOfficeModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IReadExcelFiles>().To<ReadExcelFiles>();
            Bind<IProcessor>().To<ProcessMunicipalItems>();
            Bind<IProcessMunicipalItems>().To<ProcessMunicipalItems>();
            Bind<IWriteExcelFiles>().To<WriteExcelFiles>();
            Bind<ILog>().To<Logger>();
        }
    }
}