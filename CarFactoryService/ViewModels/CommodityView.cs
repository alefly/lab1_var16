using System.Collections.Generic;

namespace CarFactoryService.ViewModels
{
    public class CommodityViewModel
    {
        public int Id { get; set; }

        public string CommodityName { get; set; }

        public decimal Price { get; set; }

        public List<ProductComponentView> CommodityIngridients { get; set; }
    }
}
