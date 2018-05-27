namespace CarFactoryService.BindingModels
{
    public class BindingBooking
    {
        public int Id { get; set; }

        public int ConsumerId { get; set; }

        public int CommodityId { get; set; }

        public int? WorkerId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
