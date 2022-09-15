﻿namespace DataAccessLibrary.Models;

public class Product
{
    [Key]
    public Guid Guid { get; set; } = Guid.NewGuid();

    [StringLength(50, MinimumLength = 5, ErrorMessage = "Name is too short. Check input!")]
    public string Name { get; set; } = string.Empty;
    public string ImageURL { get; set; } = string.Empty;
    public int Quantity { get; set; }

    [Ignore]
    public bool InStock { get => Quantity > 0; }

    [Ignore]
    public bool OnSale { get => DiscountUntil.Subtract(DateTime.Now).TotalDays > 0; }
    public DateTime DiscountUntil { get; set; }
    public decimal DiscountRate { get; set; }

    [Ignore]
    private decimal _price;
    public decimal Price
    {
        get
        {
            if (!OnSale) return _price;
            _price -= _price * DiscountRate / 100;
            return _price;
        }
        set => _price = value;
    }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = new();
}

