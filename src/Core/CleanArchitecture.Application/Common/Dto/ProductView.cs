using System;

namespace CleanArchitecture.Application.Common.Dto
{
    public class ProductView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
