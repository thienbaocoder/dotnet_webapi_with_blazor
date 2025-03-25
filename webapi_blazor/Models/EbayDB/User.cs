﻿using System;
using System.Collections.Generic;

namespace webapi_blazor.models.EbayDB;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();

    public virtual ICollection<LoginLog> LoginLogs { get; set; } = new List<LoginLog>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Rating> RatingRatedUsers { get; set; } = new List<Rating>();

    public virtual ICollection<Rating> RatingRaters { get; set; } = new List<Rating>();

    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
}
