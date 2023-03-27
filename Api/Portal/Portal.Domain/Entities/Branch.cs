namespace Portal.Domain.Entities
{
    /**
    * @Project ASP.NET Core
    * @Author: Nguyen Xuan Nhan
    * @Copyright (C) 2023 FoxMinChan. All rights reserved
    * @License MIT
    * @Create date Mon 27 Mar 2023 00:00:00 AM +07
    */

    public class Branch
    {
        public string? branchId { get; set; }
        public string? branchName { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        public List<Class>? classes { get; set; }
        public List<Staff>? staffs { get; set; }
        public List<ReceiptsExpenses>? receiptsExpenses { get; set; }
    }
}
