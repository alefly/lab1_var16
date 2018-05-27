using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    public interface IConsumer
    {
        List<ConsumerView> GetList();

        ConsumerView GetElement(int id);

        void AddElement(BindingConsumer model);

        void UpdElement(BindingConsumer model);

        void DelElement(int id);
    }
}
