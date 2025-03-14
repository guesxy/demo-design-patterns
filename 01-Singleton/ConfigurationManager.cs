namespace SingletonDemo
{
    public sealed class ConfigurationManager
    {
        private static ConfigurationManager _instance;

        private static readonly object _lock = new object();

        private Dictionary<string, string> _configurations;

        private ConfigurationManager()
        {
            _configurations = new Dictionary<string, string>();
            Console.WriteLine("ConfigurationManager instance created");
        }

        public static ConfigurationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigurationManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public string GetValue(string key) =>
            _configurations.TryGetValue(key, out var value) ? value : null;

        public void SetValue(string key, string value)
        {
            _configurations[key] = value;
        }
    }
}
