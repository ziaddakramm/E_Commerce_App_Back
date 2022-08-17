using System;

namespace e_commerce_api.Entities
{
    public record Category
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public DateTimeOffset CreatedDate { get; set; }
       
    }
}
