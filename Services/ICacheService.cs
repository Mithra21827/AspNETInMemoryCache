﻿namespace AspNETInMemoryCache.Services
{
    public interface ICacheService
    {
        T GetData<T>(string key);

        Object RemoveData(string key);

        bool SetData<T>(string key, T value, DateTimeOffset Ecpirationtime);
    }
}
