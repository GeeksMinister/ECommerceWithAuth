﻿using Newtonsoft.Json.Linq;

namespace DataAccessLibrary.Models;

public class Product
{
    [Key]
    public Guid Guid { get; set; } = Guid.NewGuid();

    [StringLength(50, MinimumLength = 5, ErrorMessage = "Name is too short. Check input!")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(512, ErrorMessage = "Too long Image-URL")]
    public string ImageURL { get; set; } = "http://dummyimage.com/187x100.png/cc0000/aaaaaa";

    public int Quantity { get; set; }

    [Ignore]
    public bool InStock { get => Quantity > 0; }

    [Ignore]
    public bool OnSale { get => DiscountUntil.Subtract(DateTime.Now).TotalDays > 0 ; }

    [DataType(DataType.Date)]
    public DateTime DiscountUntil { get; set; }

    public decimal DiscountRate { get; set; }


    [Range(1, int.MaxValue, ErrorMessage = "Invalid value!")]
    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public decimal GetCurrentPrice()
    {
        if (OnSale) return Price - (Price * DiscountRate / 100);
        return Price;
    }
    public Product()
    {

    }

}

