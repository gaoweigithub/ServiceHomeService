using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServiceControl
{
    class Program
    {
        static void Main(string[] args)
        {
            var listeningON = args.Length == 0 ? "http://*:34413/" : args[0];
            var appHost = new ServiceHome.ServiceHomeHost().Init().Start(listeningON);

            Console.WriteLine("AppHost Created at {0}, listening on {1}",
                DateTime.Now, listeningON);
            Console.ReadKey();
        }
    }
}
