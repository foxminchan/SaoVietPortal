namespace Portal.Domain.Entities
{
    /**
    * @Project ASP.NET Core
    * @Author: Nguyen Xuan Nhan
    * @Copyright (C) 2023 FoxMinChan. All rights reserved
    * @License MIT
    * @Create date Mon 27 Mar 2023 00:00:00 AM +07
    */

    public class StudentProgress
    {
        public Guid progressId { get; set; }
        public string? lessonName { get; set; }
        public string? lessonContent { get; set; }
        public string? lessonDate { get; set; }
        public string? progressStatus { get; set; }
        public int lessonRating { get; set; }
        public string? staffId { get; set; }
        public Staff? staff { get; set; }
        public string? studentId { get; set; }
        public string? classId { get; set; }
        public CourseEnrollment? courseEnrollment { get; set; }
    }
}
