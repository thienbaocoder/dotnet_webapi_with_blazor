﻿using System;
using System.Collections.Generic;

namespace webapi_blazor.models.EbayDB;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
