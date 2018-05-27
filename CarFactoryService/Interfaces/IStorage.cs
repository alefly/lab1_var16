using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    public interface IStorage
    {
        List<StorageView> GetList();

        StorageView GetElement(int id);

        void AddElement(BindingStorage model);

        void UpdElement(BindingStorage model);

        void DelElement(int id);
    }
}
