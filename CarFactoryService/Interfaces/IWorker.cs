using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    public interface IWorker
    {
        List<WorkerView> GetList();

        WorkerView GetElement(int id);

        void AddElement(BindingWorkers model);

        void UpdElement(BindingWorkers model);

        void DelElement(int id);
    }
}
