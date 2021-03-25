using System.Threading.Tasks;

namespace crackdotnet
{
    class Program
    {
        public static Task Main(string[] args)
            => Startup.RunAsync(args);
    }
}
