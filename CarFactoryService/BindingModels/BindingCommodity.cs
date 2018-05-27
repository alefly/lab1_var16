using System.Collections.Generic;

namespace CarFactoryService.BindingModels
{
    public class BindingCommodity
    {
        public int Id { get; set; }

        public string CommodityName { get; set; }

        public decimal Price { get; set; }

        public List<BindingCommodityIngridient> CommodityIngridients { get; set; }
    }
}
