using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;

namespace CarFactoryService.WorkerList
{
    public class CommodityList : ICommodity
    {
        private ListDataSingleton source;

        public CommodityList()
        {
            source = ListDataSingleton.GetInstance();
        }

        public List<CommodityViewModel> GetList()
        {
            List<CommodityViewModel> result = new List<CommodityViewModel>();
            for (int i = 0; i < source.Commodity.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<ProductComponentView> productComponents = new List<ProductComponentView>();
                for (int j = 0; j < source.CommodityIngridients.Count; ++j)
                {
                    if (source.CommodityIngridients[j].CommodityId == source.Commodity[i].Id)
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
                        productComponents.Add(new ProductComponentView
                        {
                            Id = source.CommodityIngridients[j].Id,
                            CommodityId = source.CommodityIngridients[j].CommodityId,
                            IngridientId = source.CommodityIngridients[j].IngridientId,
                            IngridientName = componentName,
                            Count = source.CommodityIngridients[j].Count
                        });
                    }
                }
                result.Add(new CommodityViewModel
                {
                    Id = source.Commodity[i].Id,
                    CommodityName = source.Commodity[i].CommodityName,
                    Price = source.Commodity[i].Price,
                    CommodityIngridients = productComponents
                });
            }
            return result;
        }

        public CommodityViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Commodity.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<ProductComponentView> productComponents = new List<ProductComponentView>();
                for (int j = 0; j < source.CommodityIngridients.Count; ++j)
                {
                    if (source.CommodityIngridients[j].CommodityId == source.Commodity[i].Id)
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
                        productComponents.Add(new ProductComponentView
                        {
                            Id = source.CommodityIngridients[j].Id,
                            CommodityId = source.CommodityIngridients[j].CommodityId,
                            IngridientId = source.CommodityIngridients[j].IngridientId,
                            IngridientName = componentName,
                            Count = source.CommodityIngridients[j].Count
                        });
                    }
                }
                if (source.Commodity[i].Id == id)
                {
                    return new CommodityViewModel
                    {
                        Id = source.Commodity[i].Id,
                        CommodityName = source.Commodity[i].CommodityName,
                        Price = source.Commodity[i].Price,
                        CommodityIngridients = productComponents
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(BindingCommodity model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Commodity.Count; ++i)
            {
                if (source.Commodity[i].Id > maxId)
                {
                    maxId = source.Commodity[i].Id;
                }
                if (source.Commodity[i].CommodityName == model.CommodityName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Commodity.Add(new Commodity
            {
                Id = maxId + 1,
                CommodityName = model.CommodityName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.CommodityIngridients.Count; ++i)
            {
                if (source.CommodityIngridients[i].Id > maxPCId)
                {
                    maxPCId = source.CommodityIngridients[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.CommodityIngridients.Count; ++i)
            {
                for (int j = 1; j < model.CommodityIngridients.Count; ++j)
                {
                    if(model.CommodityIngridients[i].IngridientId ==
                        model.CommodityIngridients[j].IngridientId)
                    {
                        model.CommodityIngridients[i].Count +=
                            model.CommodityIngridients[j].Count;
                        model.CommodityIngridients.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.CommodityIngridients.Count; ++i)
            {
                source.CommodityIngridients.Add(new CommodityIngridient
                {
                    Id = ++maxPCId,
					CommodityId = maxId + 1,
                    IngridientId = model.CommodityIngridients[i].IngridientId,
                    Count = model.CommodityIngridients[i].Count
                });
            }
        }

        public void UpdElement(BindingCommodity model)
        {
            int index = -1;
            for (int i = 0; i < source.Commodity.Count; ++i)
            {
                if (source.Commodity[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Commodity[i].CommodityName == model.CommodityName && 
                    source.Commodity[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Commodity[index].CommodityName = model.CommodityName;
            source.Commodity[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.CommodityIngridients.Count; ++i)
            {
                if (source.CommodityIngridients[i].Id > maxPCId)
                {
                    maxPCId = source.CommodityIngridients[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.CommodityIngridients.Count; ++i)
            {
                if (source.CommodityIngridients[i].CommodityId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.CommodityIngridients.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.CommodityIngridients[i].Id == model.CommodityIngridients[j].Id)
                        {
                            source.CommodityIngridients[i].Count = model.CommodityIngridients[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if(flag)
                    {
                        source.CommodityIngridients.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for(int i = 0; i < model.CommodityIngridients.Count; ++i)
            {
                if(model.CommodityIngridients[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.CommodityIngridients.Count; ++j)
                    {
                        if (source.CommodityIngridients[j].CommodityId == model.Id &&
                            source.CommodityIngridients[j].IngridientId == model.CommodityIngridients[i].IngridientId)
                        {
                            source.CommodityIngridients[j].Count += model.CommodityIngridients[i].Count;
                            model.CommodityIngridients[i].Id = source.CommodityIngridients[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.CommodityIngridients[i].Id == 0)
                    {
                        source.CommodityIngridients.Add(new CommodityIngridient
                        {
                            Id = ++maxPCId,
							CommodityId = model.Id,
                            IngridientId = model.CommodityIngridients[i].IngridientId,
                            Count = model.CommodityIngridients[i].Count
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.CommodityIngridients.Count; ++i)
            {
                if (source.CommodityIngridients[i].CommodityId == id)
                {
                    source.CommodityIngridients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Commodity.Count; ++i)
            {
                if (source.Commodity[i].Id == id)
                {
                    source.Commodity.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
