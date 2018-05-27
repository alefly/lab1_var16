namespace CarFactory
{
    /// <summary>
    /// Сколько компонентов, требуется при изготовлении изделия
    /// </summary>
    public class CommodityIngridient
    {
        public int Id { get; set; }

        public int CommodityId { get; set; }

        public int IngridientId { get; set; }

        public int Count { get; set; }
    }
}
