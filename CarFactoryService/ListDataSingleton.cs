using CarFactory;
using System.Collections.Generic;

namespace CarFactoryService
{
    class ListDataSingleton
    {
        private static ListDataSingleton LDS;

        public List<Consumer> Consumer { get; set; }

        public List<Ingredient> Ingridients { get; set; }

        public List<Worker> Workers { get; set; }

        public List<Booking> Bookings { get; set; }

        public List<Commodity> Commodity { get; set; }

        public List<CommodityIngridient> CommodityIngridients { get; set; }

        public List<Storage> Storages { get; set; }

        public List<StockComponent> StorageIngridients { get; set; }

        private ListDataSingleton()
        {
            Consumer = new List<Consumer>();
            Ingridients = new List<Ingredient>();
            Workers = new List<Worker>();
            Bookings = new List<Booking>();
            Commodity = new List<Commodity>();
            CommodityIngridients = new List<CommodityIngridient>();
            Storages = new List<Storage>();
            StorageIngridients = new List<StockComponent>();
        }

        public static ListDataSingleton GetInstance()
        {
            if(LDS == null)
            {
                LDS = new ListDataSingleton();
            }

            return LDS;
        }
    }
}
