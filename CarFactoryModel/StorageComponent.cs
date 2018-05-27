namespace CarFactory
{
    /// <summary>
    /// Сколько компонентов хранится на складе
    /// </summary>
    public class StockComponent
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int IngridientId { get; set; }

        public int Count { get; set; }
    }
}
