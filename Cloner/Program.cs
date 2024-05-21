using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WebDoda
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Project by Wick Studio");
            DodaServer dodaServer = new DodaServer(new int[] { 6990 });
            dodaServer.start();
            Console.ReadLine();
            dodaServer.Stop();
            Console.WriteLine("Is it Doda Time ?, Lets Go...");
            dodaServer.DodaTime();
            Console.ResetColor();
            Console.WriteLine("Bed Time, PS: Don't Cum lmao");
            Console.WriteLine("discord.gg/wicks");
            Console.ReadLine();
        }
    }
}