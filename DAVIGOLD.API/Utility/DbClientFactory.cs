namespace DAVIGOLD.API.Utility
{
    public class DbClientFactory<T>
    {
        private static Lazy<T> _instance = new Lazy<T>(() =>(T)Activator.CreateInstance(typeof(T)), LazyThreadSafetyMode.ExecutionAndPublication);

        public static T Instance => _instance.Value;
    }
}
