﻿using CashFlow.Entities.Base;

namespace CashFlow.Entities
{
    public class Product : WithSofDelete<uint>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ushort Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
