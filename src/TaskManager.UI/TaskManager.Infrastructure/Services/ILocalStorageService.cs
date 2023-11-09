namespace TaskManager.Infrastructure.Services
{
    public interface ILocalStorageService
    {
        void Clearstorage(List<string> keys);

        bool Exists(string key);

        T GetStorageValue<T>(string key);

        void SetStorageValue<T>(string key, T value);
    }
}
