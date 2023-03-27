using System.Text.Json;

namespace Portal.Domain.Entities
{
    /**
    * @Project ASP.NET Core
    * @Author: Nguyen Xuan Nhan
    * @Copyright (C) 2023 FoxMinChan. All rights reserved
    * @License MIT
    * @Create date Mon 27 Mar 2023 00:00:00 AM +07
    */

    public class Student
    {
        public string? studentId { get; set; }
        public string? fullname { get; set; }
        public bool gender { get; set; }
        public string? address { get; set; }
        public string? dob { get; set; }
        public string? pod { get; set; }
        public string? occupation { get; set; }
        public JsonElement? socialNetwork { get; set; }
        public List<ApplicationUser>? users { get; set; }
        public List<CourseEnrollment>? courseEnrollments { get; set; }
    }
}
