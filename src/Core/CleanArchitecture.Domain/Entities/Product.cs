using CleanArchitecture.Domain.Common;
using System.Collections;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public class Product:BaseEntity
    {
        public Product()
        {
            
        }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}