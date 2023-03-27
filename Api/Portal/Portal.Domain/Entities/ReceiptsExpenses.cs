﻿namespace Portal.Domain.Entities
{
    /**
    * @Project ASP.NET Core
    * @Author: Nguyen Xuan Nhan
    * @Copyright (C) 2023 FoxMinChan. All rights reserved
    * @License MIT
    * @Create date Mon 27 Mar 2023 00:00:00 AM +07
    */

    public class ReceiptsExpenses
    {
        public Guid receiptExpenseId { get; set; }
        public bool type { get; set; }
        public string? date { get; set; }
        public float amount { get; set; }
        public string? note { get; set; }
        public string? branchId { get; set; }
        public Branch? branch { get; set; }
    }
}
