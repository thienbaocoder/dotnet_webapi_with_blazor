using System;
using System.Collections.Generic;

namespace webapi_blazor.Models.EbayDB;

public partial class GetListOrderDetailByOrderId
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? OrderDetail { get; set; }
}
