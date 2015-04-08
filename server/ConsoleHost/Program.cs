using System;
using System.ServiceModel;
using SuecaServices;

namespace ConsoleHost
{
    class Program
    {
        //TODO: Currently the service is hosted here in a Console application. We have to manage the service lifecycle. Ideally we should host the service in IIS or something like it.
        static void Main(string[] args)
        {
            // start service
            using (ServiceHost hote = new ServiceHost(typeof(ServiceSueca)))
            {
                hote.Open();

                Console.WriteLine("Service started");
                Console.ReadLine();
            }
        }
    }
}
