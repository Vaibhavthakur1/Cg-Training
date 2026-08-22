using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
     public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
            public bool InStock { get; set; }

            public override string ToString() => $"[{Id}] {Name} ({Category}) - Rs.{Price} (InStock: {InStock})";
     }
}

