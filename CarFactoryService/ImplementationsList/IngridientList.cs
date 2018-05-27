using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;

namespace CarFactoryService.WorkerList
{
    public class IngridientList : IIngridient
    {
        private ListDataSingleton source;

        public IngridientList()
        {
            source = ListDataSingleton.GetInstance();
        }

        public List<ComponentView> GetList()
        {
            List<ComponentView> result = new List<ComponentView>();
            for (int i = 0; i < source.Ingridients.Count; ++i)
            {
                result.Add(new ComponentView
                {
                    Id = source.Ingridients[i].Id,
                    IngridientName = source.Ingridients[i].IngredientName
                });
            }
            return result;
        }

        public ComponentView GetElement(int id)
        {
            for (int i = 0; i < source.Ingridients.Count; ++i)
            {
                if (source.Ingridients[i].Id == id)
                {
                    return new ComponentView
                    {
                        Id = source.Ingridients[i].Id,
                        IngridientName = source.Ingridients[i].IngredientName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(BindingIngridients model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Ingridients.Count; ++i)
            {
                if (source.Ingridients[i].Id > maxId)
                {
                    maxId = source.Ingridients[i].Id;
                }
                if (source.Ingridients[i].IngredientName == model.IngridientName)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            source.Ingridients.Add(new Ingredient
            {
                Id = maxId + 1,
                IngredientName = model.IngridientName
            });
        }

        public void UpdElement(BindingIngridients model)
        {
            int index = -1;
            for (int i = 0; i < source.Ingridients.Count; ++i)
            {
                if (source.Ingridients[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Ingridients[i].IngredientName == model.IngridientName && 
                    source.Ingridients[i].Id != model.Id)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Ingridients[index].IngredientName = model.IngridientName;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Ingridients.Count; ++i)
            {
                if (source.Ingridients[i].Id == id)
                {
                    source.Ingridients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
