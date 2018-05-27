using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;

namespace CarFactoryService.WorkerList
{
    public class MainList : IMain
    {
        private ListDataSingleton source;

        public MainList()
        {
            source = ListDataSingleton.GetInstance();
        }

        public List<OrderView> GetList()
        {
            List<OrderView> result = new List<OrderView>();
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                string clientFIO = string.Empty;
                for (int j = 0; j < source.Consumer.Count; ++j)
                {
                    if(source.Consumer[j].Id == source.Bookings[i].ConsumerId)
                    {
                        clientFIO = source.Consumer[j].ConsumerName;
                        break;
                    }
                }
                string productName = string.Empty;
                for (int j = 0; j < source.Commodity.Count; ++j)
                {
                    if (source.Commodity[j].Id == source.Bookings[i].CommodityId)
                    {
                        productName = source.Commodity[j].CommodityName;
                        break;
                    }
                }
                string implementerFIO = string.Empty;
                if(source.Bookings[i].WorkerId.HasValue)
                {
                    for (int j = 0; j < source.Workers.Count; ++j)
                    {
                        if (source.Workers[j].Id == source.Bookings[i].WorkerId.Value)
                        {
                            implementerFIO = source.Workers[j].WorkerName;
                            break;
                        }
                    }
                }
                result.Add(new OrderView
                {
                    Id = source.Bookings[i].Id,
                    ClientId = source.Bookings[i].ConsumerId,
                    ClientName = clientFIO,
                    CommodityId = source.Bookings[i].CommodityId,
                    CommodityName = productName,
                    WorkerId = source.Bookings[i].WorkerId,
                    WorkerName = implementerFIO,
                    Count = source.Bookings[i].Count,
                    Sum = source.Bookings[i].SumPrice,
                    DateCreate = source.Bookings[i].DateCreate.ToLongDateString(),
                    DateImplement = source.Bookings[i].DateWork?.ToLongDateString(),
                    Status = source.Bookings[i].Status.ToString()
                });
            }
            return result;
        }

        public void CreateOrder(BindingBooking model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Bookings[i].Id > maxId)
                {
                    maxId = source.Consumer[i].Id;
                }
            }
            source.Bookings.Add(new Booking
            {
                Id = maxId + 1,
                ConsumerId = model.ConsumerId,
                CommodityId = model.CommodityId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                SumPrice = model.Sum,
                Status = BookingStatus.Принят
            });
        }

        public void TakeOrderInWork(BindingBooking model)
        {
            int index = -1;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Bookings[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            for(int i = 0; i < source.CommodityIngridients.Count; ++i)
            {
                if(source.CommodityIngridients[i].CommodityId == source.Bookings[index].CommodityId)
                {
                    int countOnStocks = 0;
                    for(int j = 0; j < source.StorageIngridients.Count; ++j)
                    {
                        if(source.StorageIngridients[j].IngridientId == source.CommodityIngridients[i].IngridientId)
                        {
                            countOnStocks += source.StorageIngridients[j].Count;
                        }
                    }
                    if(countOnStocks < source.CommodityIngridients[i].Count * source.Bookings[index].Count)
                    {
                        for (int j = 0; j < source.Ingridients.Count; ++j)
                        {
                            if (source.Ingridients[j].Id == source.CommodityIngridients[i].IngridientId)
                            {
                                throw new Exception("Не достаточно компонента " + source.Ingridients[j].IngredientName + 
                                    " требуется " + source.CommodityIngridients[i].Count + ", в наличии " + countOnStocks);
                            }
                        }
                    }
                }
            }
            // списываем
            for (int i = 0; i < source.CommodityIngridients.Count; ++i)
            {
                if (source.CommodityIngridients[i].CommodityId == source.Bookings[index].CommodityId)
                {
                    int countOnStocks = source.CommodityIngridients[i].Count * source.Bookings[index].Count;
                    for (int j = 0; j < source.StorageIngridients.Count; ++j)
                    {
                        if (source.StorageIngridients[j].IngridientId == source.CommodityIngridients[i].IngridientId)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (source.StorageIngridients[j].Count >= countOnStocks)
                            {
                                source.StorageIngridients[j].Count -= countOnStocks;
                                break;
                            }
                            else
                            {
                                countOnStocks -= source.StorageIngridients[j].Count;
                                source.StorageIngridients[j].Count = 0;
                            }
                        }
                    }
                }
            }
            source.Bookings[index].WorkerId = model.WorkerId;
            source.Bookings[index].DateWork = DateTime.Now;
            source.Bookings[index].Status = BookingStatus.Выполняется;
        }

        public void FinishOrder(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Consumer[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Bookings[index].Status = BookingStatus.Готов;
        }

        public void PayOrder(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Consumer[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Bookings[index].Status = BookingStatus.Оплачен;
        }

        public void PutComponentOnStock(BindingStorageComponents model)
        {
            int maxId = 0;
            for (int i = 0; i < source.StorageIngridients.Count; ++i)
            {
                if(source.StorageIngridients[i].StorageId == model.StorageId && 
                    source.StorageIngridients[i].IngridientId == model.IngridientId)
                {
                    source.StorageIngridients[i].Count += model.Count;
                    return;
                }
                if (source.StorageIngridients[i].Id > maxId)
                {
                    maxId = source.StorageIngridients[i].Id;
                }
            }
            source.StorageIngridients.Add(new StockComponent
            {
                Id = ++maxId,
                StorageId = model.StorageId,
                IngridientId = model.IngridientId,
                Count = model.Count
            });
        }
    }
}
