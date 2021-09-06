using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCD0904Assignment2TrinhDoDuyHung
{
    class Program
    {
        // The list staff
        static Dictionary<string, Staff> listDictionary = new Dictionary<string, Staff>();

        // A global array variable used to store staff whose name or ID matches user input
        static string staffMatch = "";

        // The Main function
        static void Main(string[] args)
        {
            while (true)
            {
                string userChoice = Menu();
                if (userChoice != "6")
                {
                    if (userChoice == "Your choice doesn't exist")
                    {
                        Console.Write("Your choice doesn't exist, please press Enter to continue");
                        Console.ReadLine();
                    }
                    else
                    {
                        ControlFunction(userChoice);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        // Shows menu
        static string Menu()
        {
            /*
                arg: null
                des: This function displays the menu of this program, takes user input, checks and returns to the Main function.
                return: string
            */
            Console.Clear();
            Console.WriteLine("===========================MENU===========================");
            Console.WriteLine("1. Add new staff");
            Console.WriteLine("2. Remove staff");
            Console.WriteLine("3. Edit staff's information");
            Console.WriteLine("4. Search staff");
            Console.WriteLine("5. Show all staff");
            Console.WriteLine("6. Exit");
            Console.WriteLine("==========================================================");
            Console.Write("Enter your choice: ");

            // Takes user input
            string userChoice = Console.ReadLine();

            // Gets numbers in the user input
            userChoice = GetNumber(userChoice);

            // Checks the user input and return if it is valid
            if (Convert.ToInt32(userChoice) <= 6 && Convert.ToInt32(userChoice) > 0)
            {
                return userChoice;
            }
            else
            {
                return "Your choice doesn't exist";
            }
        }

        // Control switch to another function
        static void ControlFunction(string userChoice)
        {
            /*
                args: string userChoice
                des: This function takes user input and passes it to a function whose feature the user wants to use
                return: null
            */
            switch (userChoice)
            {
                case "1":
                    StaffAdd(true);
                    break;
                case "2":
                    StaffRemove(true);
                    break;
                case "3":
                    StaffEdit(true);
                    break;
                case "4":
                    StaffSearch(true);
                    break;
                case "5":
                    StaffDisplay();
                    break;

            }
        }

        // Gets number in a string
        static string GetNumber(string userInput)
        {
            /*
                args: string userInput
                des: This function takes a string and returns a string containing the numbers that exist in the input.
                return: string (return == "-1": the input doesn't contain any numbers)
            */
            userInput = new String(userInput.Where(Char.IsDigit).ToArray());
            if (userInput.Length > 0)
            {
                return userInput;
            }
            else
            {
                return "-1";
            }
        }

        // Remove special characters in a string
        static string RemoveSpecialCharacters(string str)
        {
            /*
                args: string str
                des: This function takes a string and remove special characters.
                     When done, this function returns a new string that doesn't conatin any special characters.
                return: string
            */
            char[] buffer = new char[str.Length];
            int idx = 0;

            foreach (char c in str)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    buffer[idx] = c;
                    idx++;
                }
            }

            return new string(buffer, 0, idx);
        }

        // String matching
        static double StringMatch(string userInput, string stringSample)
        {
            /*
                args: string userInput, string stringSample
                des: This function takes user input as staff name and calculate matching with sample in "StaffName" array.
                     When done, this function returns the percentage of user input with sample.
                return: double matching
            */
            userInput = RemoveSpecialCharacters(userInput.ToLower());
            stringSample = RemoveSpecialCharacters(stringSample.ToLower());
            char[] input = userInput.ToCharArray();
            char[] sample = stringSample.ToCharArray();
            double matching = 0;
            if (input.Length < sample.Length)
            {
                char[] except = sample.Except(input).ToArray();
                matching = 1 - 1.0 * except.Length / sample.Length;
            }
            else
            {
                char[] except = input.Except(sample).ToArray();
                matching = 1 - 1.0 * except.Length / input.Length;
            }
            return matching;
        }

        // Search staff
        static void StaffSearch(bool titleShow)
        {
            /*
                args: bool titleShow
                des: This function takes each user input as staff name or ID. Pass it to the "StringMatch" function for calculating the string matching if the user input is a name.
                     When done, this function displays a list staff that matched user input.
                return: null
            */
            if (listDictionary.Count > 0)
            { 
                if (titleShow)
                {
                    Console.WriteLine("\n=========================SEARCHING==========================");
                }
                Console.Write("\n-----------------------------------------\n");
                Console.Write("|Enter staff name or ID for searching: ");
                string staffInfo = Console.ReadLine();
                string staffInfoID = GetNumber(staffInfo);
                Console.Write("\n-----------------------------------------\n");
                foreach (Staff i in listDictionary.Values)
                {
                    double nameMatching = StringMatch(staffInfo, i.Name);
                    if (nameMatching > 0.5 || staffInfoID == i.ID)
                    {
                        i.Display();
                        staffMatch += i.ID+",";
                    }
                }
                if (staffMatch.Length == 0)
                {
                    Console.Write("Sorry, couldn't found. Press enter to continue");
                    Console.ReadLine();
                }
                else
                {
                    Console.Write("\n-----------------------------------------\n");
                    Console.Write("We found {0} staff that matched your input. Press enter to continue", staffMatch.Split(',').Length - 1);
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("You need to add students first");
                StaffAdd(true);
            }
        } 

        // Displays staff
        static void StaffDisplay()
        {
            if(listDictionary.Count > 0)
            {
                Console.WriteLine("\n========================DISPLAYING========================");
                foreach(Staff i in listDictionary.Values)
                {
                    i.Display();
                }
                Console.WriteLine("==========================================================\n");
                Console.Write("Press Enter to continue");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("You need to add students first");
                StaffAdd(true);
            }
        }

        // Adds staff
        static void StaffAdd(bool titleShow)
        {
            if (titleShow) // To disable the display of a function's title when it has been called again.
            {
                Console.WriteLine("\n==========================ADDING==========================");
            }
            Console.Write("Enter number of staff you want to add: ");
            int n = Convert.ToInt32(GetNumber(Console.ReadLine()));
            int count = 0;
            if (n > 0)
            {
                while (count < n)
                {
                    Staff staffNew = new Staff();
                    Console.Write("\n|Enter staff name: ");
                    string name = Console.ReadLine();
                    if (Convert.ToInt32(GetNumber(name)) > 0)
                    {
                        Console.WriteLine("Your input isn't valid, enter again please");
                    }
                    else
                    {
                        Console.Write("|Enter staff age: ");
                        string age = GetNumber(Console.ReadLine());
                        if (Convert.ToInt32(age) > 0)
                        {
                            Console.Write("|Enter staff contact: ");
                            string contact = GetNumber(Console.ReadLine());
                            if (Convert.ToInt32(contact) > 0)
                            {
                                staffNew.ID = Convert.ToString(listDictionary.Count + 1);
                                staffNew.Name = name;
                                staffNew.Age = age;
                                staffNew.Contact = contact;
                                count += 1;
                                listDictionary.Add(staffNew.ID, staffNew);
                            }
                            else
                            {
                                Console.WriteLine("Your input isn't valid");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Your input isn't valid");
                        }
                    }
                }
                Console.WriteLine("\n==========================================================\n");
                StaffDisplay();
            }
            else
            {
                Console.WriteLine("Your input isn't valid");
                StaffAdd(false);
            }
        }

        // Edit staff information
        static void StaffEdit(bool titleShow)
        {
            if (listDictionary.Count > 0)
            {
                if (titleShow)
                {
                    Console.WriteLine("\n=========================EDITING==========================");
                }
                StaffSearch(false);
                if (staffMatch.Length > 0)
                {
                    Console.Write("\n-----------------------------------------\n");
                    Console.Write("|Re-enter staff ID to confirm: ");
                    string userInputID = GetNumber(Console.ReadLine());
                    if (userInputID != "-1" && Array.IndexOf(staffMatch.Split(','), userInputID) != -1)
                    { 
                        Staff staffEdit = listDictionary[userInputID];
                        Console.Write("\n-----------------------------------------\n");
                        Console.Write("Rename for ID: {0} or press Enter to skip: ", userInputID);
                        string userInputName = Console.ReadLine();
                        if (userInputName.Length > 1)
                        {
                            staffEdit.Name = userInputName;
                        }
                        Console.Write("Change age or press Enter to skip: ");
                        string userInputAge = Console.ReadLine();
                        if (userInputAge.Length > 1)
                        {
                            staffEdit.Age = userInputAge;
                        }
                        Console.Write("Change contact or press Enter to skip: ");
                        string userInputContact = Console.ReadLine();
                        if (userInputContact.Length > 1)
                        {
                            staffEdit.Contact = userInputContact;
                        }
                        staffMatch = "";
                        Console.WriteLine("\n==========================================================\n");
                        StaffDisplay();
                    }
                    else
                    {
                        Console.Write("Your input isn't valid, the process has been aborted. Try later, press enter to comback the menu");
                        Console.ReadLine();
                    }
                }
                else
                {
                    StaffEdit(false);
                }
            }
            else
            {
                Console.WriteLine("You need to add students first");
                StaffAdd(true);
            }
        }

        // Remove staff
        static void StaffRemove(bool titleShow)
        {
            if (listDictionary.Count > 0)
            {
                if (titleShow)
                {
                    Console.WriteLine("\n=========================EDITING==========================");
                }
                StaffSearch(false);
                if (staffMatch.Length > 0)
                {
                    Console.Write("\n-----------------------------------------\n");
                    Console.Write("|Re-enter staff ID to confirm: ");
                    string userInputID = GetNumber(Console.ReadLine());
                    if (userInputID != "-1" && Array.IndexOf(staffMatch.Split(','), userInputID) != -1)
                    {
                        listDictionary.Remove(userInputID);
                        staffMatch = "";
                        StaffDisplay();
                    }
                    else
                    {
                        Console.Write("Your input isn't valid, the process has been aborted. Try later, press enter to comback the menu");
                        Console.ReadLine();
                    }
                }
                else
                {
                    StaffRemove(false);
                }
            }
            else
            {
                Console.WriteLine("You need to add students first");
                StaffAdd(true);
            }
        }
    }
}
