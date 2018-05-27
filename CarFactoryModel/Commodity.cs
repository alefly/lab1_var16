namespace CarFactory
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class Commodity
    {
        public int Id { get; set; }

        public string CommodityName { get; set; }

        public decimal Price { get; set; }
    }
}
