using Hanssens.Net;

namespace TaskManager.Infrastructure.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private LocalStorage storage;

        public LocalStorageService()
        {
            var config = new LocalStorageConfiguration()
            {
                AutoLoad = true,
                AutoSave = true,
                Filename = "TaskManagement"
            };
            storage = new LocalStorage(config);
        }

        public void Clearstorage(List<string> keys)
        {
            foreach (var key in keys)
            {
                storage.Remove(key);
            }
        }

        public bool Exists(string key)
        {
            return storage.Exists(key);
        }

        public T GetStorageValue<T>(string key)
        {
            return storage.Get<T>(key);
        }

        public void SetStorageValue<T>(string key, T value)
        {
            storage.Store(key, value);
            storage.Persist();
        }
    }
}
