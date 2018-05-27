using System.Collections.Generic;

namespace CarFactoryService.ViewModels
{
    public class StorageView
    {
        public int Id { get; set; }

        public string StorageName { get; set; }

        public List<StorageIngridientsView> StorageComponents { get; set; }
    }
}
