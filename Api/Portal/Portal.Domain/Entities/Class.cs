namespace Portal.Domain.Entities
{
    /**
    * @Project ASP.NET Core
    * @Author: Nguyen Xuan Nhan
    * @Copyright (C) 2023 FoxMinChan. All rights reserved
    * @License MIT
    * @Create date Mon 27 Mar 2023 00:00:00 AM +07
    */

    public class Class
    {
        public string? classId { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
        public float? fee { get; set; }
        public string? courseId { get; set; }
        public Course? course { get; set; }
        public string? branchId { get; set; }
        public Branch? branch { get; set; }
        public List<CourseEnrollment>? courseEnrollments { get; set; }
    }
}
