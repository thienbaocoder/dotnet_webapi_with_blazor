﻿using System;
using System.Collections.Generic;

namespace webapi_blazor.models.EbayDB;

public partial class LoginLog
{
    public int Id { get; set; }

    public string IpAddress { get; set; } = null!;

    public int? UserId { get; set; }

    public string? Username { get; set; }

    public DateTime? LoginTime { get; set; }

    public bool? IsSuccessful { get; set; }

    public bool? Deleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? User { get; set; }
}
