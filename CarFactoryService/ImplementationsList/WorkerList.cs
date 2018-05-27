using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;

namespace CarFactoryService.WorkerList
{
    public class WorkerList : IWorker
    {
        private ListDataSingleton source;

        public WorkerList()
        {
            source = ListDataSingleton.GetInstance();
        }

        public List<WorkerView> GetList()
        {
            List<WorkerView> result = new List<WorkerView>();
            for (int i = 0; i < source.Workers.Count; ++i)
            {
                result.Add(new WorkerView
                {
                    Id = source.Workers[i].Id,
                    WorkerName = source.Workers[i].WorkerName
                });
            }
            return result;
        }

        public WorkerView GetElement(int id)
        {
            for (int i = 0; i < source.Workers.Count; ++i)
            {
                if (source.Workers[i].Id == id)
                {
                    return new WorkerView
                    {
                        Id = source.Workers[i].Id,
                        WorkerName = source.Workers[i].WorkerName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(BindingWorkers model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Workers.Count; ++i)
            {
                if (source.Workers[i].Id > maxId)
                {
                    maxId = source.Workers[i].Id;
                }
                if (source.Workers[i].WorkerName == model.WorkerName)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            source.Workers.Add(new Worker
            {
                Id = maxId + 1,
                WorkerName = model.WorkerName
            });
        }

        public void UpdElement(BindingWorkers model)
        {
            int index = -1;
            for (int i = 0; i < source.Workers.Count; ++i)
            {
                if (source.Workers[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Workers[i].WorkerName == model.WorkerName && 
                    source.Workers[i].Id != model.Id)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Workers[index].WorkerName = model.WorkerName;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Workers.Count; ++i)
            {
                if (source.Workers[i].Id == id)
                {
                    source.Workers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
