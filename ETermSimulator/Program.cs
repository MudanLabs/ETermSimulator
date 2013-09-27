using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETermSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            ETermSimulator.Eterm e = new Eterm();
            e.Connection("127.0.0.1", 350);
            Console.WriteLine("exit");
        }
    }
}
