namespace Portal.Domain.Interfaces.Common
{
    /**
    * @Project ASP.NET Core
    * @Author: Nguyen Xuan Nhan
    * @Copyright (C) 2023 FoxMinChan. All rights reserved
    * @License MIT
    * @Create date Mon 27 Mar 2023 00:00:00 AM +07
    */

    public interface ICache
    {
        void Remove(string key);
        T Set<T>(string key, T value);
        bool TryGetValue<T>(string key, out T value);
    }
}
