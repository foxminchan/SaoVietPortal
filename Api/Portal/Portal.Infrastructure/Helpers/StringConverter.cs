using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Portal.Infrastructure.Helpers
{
    /**
    * @Project ASP.NET Core
    * @Author: Nguyen Xuan Nhan
    * @Copyright (C) 2023 FoxMinChan. All rights reserved
    * @License MIT
    * @Create date Mon 27 Mar 2023 00:00:00 AM +07
    */

    public class StringConverter : ValueConverter<string, DateTime>
    {
        public StringConverter() : base(
            v => DateTime.Parse(v),
            v => v.ToString("dd/MM/yyyy")
        )
        {
        }
    }
}
