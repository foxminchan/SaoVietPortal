namespace Portal.Domain.Entities
{
    /**
    * @Project ASP.NET Core
    * @Author: Nguyen Xuan Nhan
    * @Copyright (C) 2023 FoxMinChan. All rights reserved
    * @License MIT
    * @Create date Mon 27 Mar 2023 00:00:00 AM +07
    */

    public class Course
    {
        public string? courseId { get; set; }
        public string? courseName { get; set; }
        public string? description { get; set; }
        public List<Class>? classes { get; set; }
    }
}
