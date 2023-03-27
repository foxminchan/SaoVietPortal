namespace Portal.Domain.Entities
{
    /**
    * @Project ASP.NET Core
    * @Author: Nguyen Xuan Nhan
    * @Copyright (C) 2023 FoxMinChan. All rights reserved
    * @License MIT
    * @Create date Mon 27 Mar 2023 00:00:00 AM +07
    */

    public class Staff
    {
        public string? staffId { get; set; }
        public string? fullname { get; set; }
        public string? dob { get; set; }
        public string? address { get; set; }
        public string? dsw { get; set; }
        public int positionId { get; set; }
        public Position? position { get; set; }
        public string? branchId { get; set; }
        public Branch? branch { get; set; }
        public string? managerId { get; set; }
        public Staff? manager { get; set; }
        public List<Staff>? staffs { get; set; }
        public List<StudentProgress>? studentProgresses { get; set; }
        public List<ApplicationUser>? users { get; set; }
    }
}
