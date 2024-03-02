using MerryDllFramework;
using MerryDllFramework_Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MerryDllFramework_Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            MerryDll dll = new MerryDll();
            Console.OutputEncoding = Encoding.Unicode;
          //  dll.Start(new List<string> { "4058990007F3", null, "4058990007F3" }, IntPtr.Zero);
            dll.StartRun();
            while (true)
            {
                string item=Console.ReadLine();
                Console.WriteLine(dll.Run(item));
            }
            /*    if (args.Length != 0)
                {
                    foreach (var item in args)
                    {

                        var result = dll.Run(item);
                        Console.WriteLine(result);
                    }
                }           
                else
                {
                    for (var i = 0; i < 1; i++)
                    {
                        Thread.Sleep(1000);
                        new List<string>()
                        {
                            "GetTxMCUFW",
                        }.ForEach(item =>
                        {
                            Console.WriteLine(dll.Run(item));

                        });
                    }
                    Console.ReadKey();
                }*/
        }
    }
}
