namespace Portal.Application.Cache;

public class RedisCacheOption
{
    public bool AbortOnConnectFail { get; set; } = false;
    public bool Ssl { get; set; } = false;
    public int ConnectRetry { get; set; } = 5;
    public int ConnectTimeout { get; set; } = 5000;
    public int DeltaBackOff { get; set; } = 1000;
    public int RedisDefaultSlidingExpirationInSecond { get; set; } = 3600;
    public int SyncTimeout { get; set; } = 5000;
    public string Password { get; set; } = "";
    public string Prefix { get; set; } = "";
    public string Url { get; set; } = "localhost:6379";

    public string GetConnectionString() => string.IsNullOrEmpty(Password) ? Url : $"{Url},password={Password},ssl={Ssl},abortConnect=False";
}