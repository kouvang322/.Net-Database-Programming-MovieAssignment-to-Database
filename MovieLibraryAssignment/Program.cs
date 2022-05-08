using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace MovieLibraryAssignment
{
    class Program
    {

        static void Main(string[] args)
        {
            {
                try
                {
                    var startup = new Startup();
                    var serviceProvider = startup.ConfigureServices();
                    var service = serviceProvider.GetService<IListItemService>();
                    //var service = serviceProvider.GetService<IFileHandler>();

                    service?.DisplayMenu();
                    
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                }
            }
        }
    }
}
    

