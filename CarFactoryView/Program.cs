using CarFactoryService.WorkerList;
using CarFactoryService.Interfaces;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractShopView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IConsumer, ConsumerList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IIngridient, IngridientList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorker, WorkerList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICommodity, CommodityList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorage, StorageList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMain, MainList>(new HierarchicalLifetimeManager());
            
            return currentContainer;
        }
    }
}
