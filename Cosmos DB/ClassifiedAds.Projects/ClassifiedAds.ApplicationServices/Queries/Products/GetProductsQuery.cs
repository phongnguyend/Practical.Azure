﻿using ClassifiedAds.Domain.Entities;
using ClassifiedAds.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassifiedAds.ApplicationServices.Queries.Products
{
    public class GetProductsQuery : IQuery<List<Product>>
    {
    }

    internal class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, List<Product>>
    {
        private readonly IProductService _productService;

        public GetProductsQueryHandler(IProductService productService)
        {
            _productService = productService;
        }
        public List<Product> Handle(GetProductsQuery query)
        {
            return _productService.GetProducts().ToList();
        }
    }
}
