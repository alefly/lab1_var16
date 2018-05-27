using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;

namespace CarFactoryService.WorkerList
{
    public class ConsumerList : IConsumer
    {
        private ListDataSingleton source;

        public ConsumerList()
        {
            source = ListDataSingleton.GetInstance();
        }

        public List<ConsumerView> GetList()
        {
            List<ConsumerView> result = new List<ConsumerView>();
            for (int i = 0; i < source.Consumer.Count; ++i)
            {
                result.Add(new ConsumerView
                {
                    Id = source.Consumer[i].Id,
                    ConsumerName = source.Consumer[i].ConsumerName
                });
            }
            return result;
        }

        public ConsumerView GetElement(int id)
        {
            for (int i = 0; i < source.Consumer.Count; ++i)
            {
                if (source.Consumer[i].Id == id)
                {
                    return new ConsumerView
                    {
                        Id = source.Consumer[i].Id,
                        ConsumerName = source.Consumer[i].ConsumerName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(BindingConsumer model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Consumer.Count; ++i)
            {
                if (source.Consumer[i].Id > maxId)
                {
                    maxId = source.Consumer[i].Id;
                }
                if (source.Consumer[i].ConsumerName == model.ConsumerName)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            source.Consumer.Add(new Consumer {
                Id = maxId + 1,
                ConsumerName = model.ConsumerName
            });
        }

        public void UpdElement(BindingConsumer model)
        {
            int index = -1;
            for (int i = 0; i < source.Consumer.Count; ++i)
            {
                if (source.Consumer[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Consumer[i].ConsumerName == model.ConsumerName && 
                    source.Consumer[i].Id != model.Id)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Consumer[index].ConsumerName = model.ConsumerName;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Consumer.Count; ++i)
            {
                if (source.Consumer[i].Id == id)
                {
                    source.Consumer.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
