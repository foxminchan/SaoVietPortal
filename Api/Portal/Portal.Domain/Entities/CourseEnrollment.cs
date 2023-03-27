namespace Portal.Domain.Entities
{
    /**
    * @Project ASP.NET Core
    * @Author: Nguyen Xuan Nhan
    * @Copyright (C) 2023 FoxMinChan. All rights reserved
    * @License MIT
    * @Create date Mon 27 Mar 2023 00:00:00 AM +07
    */

    public class CourseEnrollment
    {
        public string? studentId { get; set; }
        public Student? student { get; set; }
        public string? classId { get; set; }
        public Class? @class { get; set; }
        public List<StudentProgress>? studentProgresses { get; set; }
        public List<CourseRegistration>? courseRegistrations { get; set; }
    }
}
