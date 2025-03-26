﻿using System;
using System.Collections.Generic;

namespace webapi_blazor.Models.EbayDB;

public partial class RoleGroup
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int GroupId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }
}
