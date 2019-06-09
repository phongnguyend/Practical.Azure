using System;
using System.Collections.Generic;
using System.Text;

namespace ClassifiedAds.Domain.Entities
{
    public class Product : Entity<string>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<ProductImage> ProductImages { get; set; }
    }

    public class ProductImage
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }
    }
}
