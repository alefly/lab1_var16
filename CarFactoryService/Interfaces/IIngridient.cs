using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    public interface IIngridient
    {
        List<ComponentView> GetList();

        ComponentView GetElement(int id);

        void AddElement(BindingIngridients model);

        void UpdElement(BindingIngridients model);

        void DelElement(int id);
    }
}
