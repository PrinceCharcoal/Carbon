using System.Threading.Tasks;
using System;

namespace Carbon
{
    class Program
    {
        [STAThread]
        public static Task Main(string[] args) 
            => Startup.RunAsync(args);
    }
}