using System;

namespace e_commerce_api.Entities
{
    public record Item
    {
        public Guid Id{get;init;}

        public string Name{get;init;}

        public string Description{get;init;}

        public decimal Price{get; init;}

        public string ItemCategory{get; set;}

        public DateTimeOffset CreatedDate { get; set; }
    }
}