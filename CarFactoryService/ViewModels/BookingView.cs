namespace CarFactoryService.ViewModels
{
    public class OrderView
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; }

        public int CommodityId { get; set; }

        public string CommodityName { get; set; }

        public int? WorkerId { get; set; }

        public string WorkerName { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateImplement { get; set; }
    }
}
