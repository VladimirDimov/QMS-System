[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(QMS.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(QMS.Web.App_Start.NinjectWebCommon), "Stop")]

namespace QMS.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Data;
    using Services.Contracts;
    using Services;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IQmsData>().To<QmsData>().InRequestScope();

            // Bind all services to interfaces
            kernel.Bind<IAreasServices>().To<AreasServices>().InRequestScope();
            kernel.Bind<IDepartmentsServices>().To<DepartmentsServices>().InRequestScope();
            kernel.Bind<IDivisionsServices>().To<DivisionsServices>().InRequestScope();
            kernel.Bind<IDocumentsServices>().To<DocumentsServices>().InRequestScope();
            kernel.Bind<IMessagesServices>().To<MessagesServices>().InRequestScope();
            kernel.Bind<INotesServices>().To<NotesServices>().InRequestScope();
            kernel.Bind<IProceduresServices>().To<ProceduresServices>().InRequestScope();
            kernel.Bind<IRecordsServices>().To<RecordsServices>().InRequestScope();
            kernel.Bind<IRecordFilesServices>().To<RecordFilesServices>().InRequestScope();
            kernel.Bind<IRolesServices>().To<RolesServices>().InRequestScope();
            kernel.Bind<IUsersServices>().To<UsersServices>().InRequestScope();
        }
    }
}
