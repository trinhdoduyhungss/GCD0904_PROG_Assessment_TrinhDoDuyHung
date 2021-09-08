using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCD0904Assignment2TrinhDoDuyHung
{
    class IO
    {
        public static string GetInput(string mess)
        {
            // Print message
            Console.Write($"{mess} ");

            // Takes user input
            string userChoice = Console.ReadLine();

            // Gets numbers in the user input
            return userChoice;
        }

        public static string Menu()
        {
            Console.Clear();
            Console.WriteLine("===========================MENU===========================");
            Console.WriteLine("1. Add new staff");
            Console.WriteLine("2. Remove staff");
            Console.WriteLine("3. Edit staff's information");
            Console.WriteLine("4. Search staff");
            Console.WriteLine("5. Show all staff");
            Console.WriteLine("6. Exit");
            Console.WriteLine("==========================================================");
            return GetInput("Enter your choice: ");
        }

        public static void Alert(string mess, bool stop)
        {
            Console.Write("\n{0}\n",mess);
            if(stop)
            {
                Console.ReadLine();
            }
        }

        public static void Header(string title)
        {
            Console.WriteLine($"\n========================={title}==========================");
        }

        public static void Footer(string title, bool stop)
        {
            Console.WriteLine("\n==========================================================\n");
            Console.Write(title);
            if (stop)
            {
                Console.ReadLine();
            }
        }
    
        public static void BreakLine()
        {
            Console.Write("\n-----------------------------------------\n");
        }
    }
}
