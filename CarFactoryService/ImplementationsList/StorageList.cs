using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;

namespace CarFactoryService.WorkerList
{
    public class StorageList : IStorage
    {
        private ListDataSingleton source;

        public StorageList()
        {
            source = ListDataSingleton.GetInstance();
        }

        public List<StorageView> GetList()
        {
            List<StorageView> result = new List<StorageView>();
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<StorageIngridientsView> StorageComponents = new List<StorageIngridientsView>();
                for (int j = 0; j < source.StorageIngridients.Count; ++j)
                {
                    if (source.StorageIngridients[j].StorageId == source.Storages[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Ingridients.Count; ++k)
                        {
                            if (source.CommodityIngridients[j].IngridientId == source.Ingridients[k].Id)
                            {
                                componentName = source.Ingridients[k].IngredientName;
                                break;
                            }
                        }
                        StorageComponents.Add(new StorageIngridientsView
                        {
                            Id = source.StorageIngridients[j].Id,
                            StorageId = source.StorageIngridients[j].StorageId,
                            IngridientId = source.StorageIngridients[j].IngridientId,
                            IngridientName = componentName,
                            Count = source.StorageIngridients[j].Count
                        });
                    }
                }
                result.Add(new StorageView
                {
                    Id = source.Storages[i].Id,
                    StorageName = source.Storages[i].StorageName,
                    StorageComponents = StorageComponents
                });
            }
            return result;
        }

        public StorageView GetElement(int id)
        {
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<StorageIngridientsView> StockComponents = new List<StorageIngridientsView>();
                for (int j = 0; j < source.StorageIngridients.Count; ++j)
                {
                    if (source.StorageIngridients[j].StorageId == source.Storages[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Ingridients.Count; ++k)
                        {
                            if (source.CommodityIngridients[j].IngridientId == source.Ingridients[k].Id)
                            {
                                componentName = source.Ingridients[k].IngredientName;
                                break;
                            }
                        }
                        StockComponents.Add(new StorageIngridientsView
                        {
                            Id = source.StorageIngridients[j].Id,
                            StorageId = source.StorageIngridients[j].StorageId,
                            IngridientId = source.StorageIngridients[j].IngridientId,
                            IngridientName = componentName,
                            Count = source.StorageIngridients[j].Count
                        });
                    }
                }
                if (source.Storages[i].Id == id)
                {
                    return new StorageView
                    {
                        Id = source.Storages[i].Id,
                        StorageName = source.Storages[i].StorageName,
                        StorageComponents = StockComponents
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(BindingStorage model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id > maxId)
                {
                    maxId = source.Storages[i].Id;
                }
                if (source.Storages[i].StorageName == model.StorageName)
                {
                    throw new Exception("Уже есть склад с таким названием" + source.Storages[i].StorageName);
                }
            }
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }

        public void UpdElement(BindingStorage model)
        {
            int index = -1;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Storages[i].StorageName == model.StorageName && 
                    source.Storages[i].Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Storages[index].StorageName = model.StorageName;
        }

        public void DelElement(int id)
        {
            // при удалении удаляем все записи о компонентах на удаляемом складе
            for (int i = 0; i < source.StorageIngridients.Count; ++i)
            {
                if (source.StorageIngridients[i].StorageId == id)
                {
                    source.StorageIngridients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == id)
                {
                    source.Storages.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
