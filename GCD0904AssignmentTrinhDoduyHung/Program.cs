using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCD0904AssignmentTrinhDoduyHung
{
    class Program
    {
        // A global integer variable that determines the maximum number of staffs that can enter
        const int MAX_STAFF = 100;

        // Global array variables used to store staff informations
		static string[] staffID= new string[MAX_STAFF];
		static string[] staffName = new string[MAX_STAFF];
		static string[] staffAge = new string[MAX_STAFF];
        static string[] staffContact = new string[MAX_STAFF];
		static string[] staffDate = new string[MAX_STAFF];

        // A Global integer variable store the number of staff has been entered
        static int filled = 0;

        // A global integer variable that stores the number of staff whose name or ID matches user input
        static int findout = 0;

        // A global array variable used to store staff whose name or ID matches user input
        static string[] staffMatch = new string[MAX_STAFF];

        // The Main function
        static void Main(string[] args)
        {
            while(true)
            {
                string userChoice = Menu();
                if(userChoice != "6")
                {
                    if(userChoice == "Your choice doesn't exist")
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

        // Control switch to another function
        static void ControlFunction(string userChoice)
        {
            /*
                args: string userChoice
                des: This function takes user input and passes it to a function whose feature the user wants to use
                return: null
            */
            switch(userChoice)
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

        // Adds staff
        static void StaffAdd(bool titleShow)
        {
            /*
                args: boolean titleShow
                des: This function takes each user input as staff information, check it and add information if it is valid.
                     When done, this function call the "StaffShow" function for displaying all data you entered.
                return: null
            */
            if(titleShow) // To disable the display of a function's title when it has been called again.
            {
                Console.WriteLine("\n==========================ADDING==========================");
            }
            Console.Write("Enter number of staff you want to add: ");
            int n = Convert.ToInt32(GetNumber(Console.ReadLine()));
            int count=0;
            if(n > 0 && n <= MAX_STAFF - filled) // MAX_STAFF - filled = Remaining "seats" arrays (staffID, staffName, staffAge, staffDate) can store
            {
                while(count < n)
                {
                    Console.Write("\n-----------------------------------------\n");
                    Console.Write("|Enter staff name: ");
                    string name = Console.ReadLine();
                    if(Convert.ToInt32(GetNumber(name)) > 0)
                    {
                        Console.WriteLine("Your input isn't valid, enter again please");
                    }
                    else
                    {
                        Console.Write("|Enter staff age: ");
                        string age = GetNumber(Console.ReadLine());
                        if(Convert.ToInt32(age) > 0)
                        {
                            Console.Write("|Enter staff contact: ");
                            string contact = GetNumber(Console.ReadLine());
                            if(Convert.ToInt32(contact) > 0)
                            {
                                staffID[filled + count] = Convert.ToString(filled + count);
                                staffName[filled + count] = name;
                                staffAge[filled + count] = age;
                                staffContact[filled + count] = contact;
                                staffDate[filled + count] = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
                                count += 1;
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
                filled += n;
                Console.WriteLine("\n==========================================================\n");
                StaffDisplay();
            }
            else
            {
                Console.WriteLine($"Your input isn't valid. It must be a number and smaller than {MAX_STAFF-filled}");
                StaffAdd(false);
            }
        }

        // Displays staff
        static void StaffDisplay()
        {
             /*
                args: null
                des: Using a loop for displaying staff information.
                return: null
            */
            if(filled > 0)
            {
                Console.WriteLine("\n========================DISPLAYING========================");
                for(int i = 0; i < filled; i++)
                {
                    Console.WriteLine($"ID: {staffID[i]}, Name: {staffName[i]}, Age: {staffAge[i]}, Contact: {staffContact[i]}, Day start: {staffDate[i]}");
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

        // Edit staff information
        static void StaffEdit(bool titleShow)
        {
             /*
                args: boolean titleShow
                des: This function takes user input as staff information, check it and change information if it is valid.
                     When done, this function call the "StaffShow" function for displaying all data you entered.
                return: null
             */
             if(filled > 0)
             {
                 if(titleShow)
                 {
                    Console.WriteLine("\n=========================EDITING==========================");
                 }
                 StaffSearch(false);
                 if(findout > 0)
                 {
                     Console.Write("\n-----------------------------------------\n");
                     Console.Write("|Re-enter staff ID to confirm: ");
                     int ID = Convert.ToInt32(GetNumber(Console.ReadLine()));
                     if(ID >= 0 && Array.IndexOf(staffMatch, ID.ToString()) != -1)
                     {
                         Console.Write("\n-----------------------------------------\n");
                         Console.Write("Rename for ID: {0} or press Enter to skip: ", ID.ToString());
                         string userInput = Console.ReadLine();
                         if(userInput.Length > 1)
                         {
                            staffName[ID] = userInput;
                         }
                         Console.Write("Change age or press Enter to skip: ");
                         userInput = Console.ReadLine();
                         if(userInput.Length > 1)
                         {
                            staffAge[ID] = userInput;
                         }
                         Console.Write("Change contact or press Enter to skip: ");
                         userInput = Console.ReadLine();
                         if(userInput.Length > 1)
                         {
                            staffContact[ID] = userInput;
                         }
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
             /*
                args: boolean titleShow
                des: This function takes user input as staff information (ID or name), search staff and remove if user confirm by ID.
                     When done, this function call the "StaffShow" function for displaying all data you entered.
                return: null
             */
             if(filled > 0)
             {
                 if(titleShow)
                 {
                    Console.WriteLine("\n=========================EDITING==========================");
                 }
                 StaffSearch(false);
                 if(findout > 0)
                 {
                     Console.Write("\n-----------------------------------------\n");
                     Console.Write("|Re-enter staff ID to confirm: ");
                     int ID = Convert.ToInt32(GetNumber(Console.ReadLine()));
                     Console.Write("\n-----------------------------------------\n");
                     if(ID >= 0 && Array.IndexOf(staffMatch, ID.ToString()) != -1)
                     {
                         for(int i = ID; i <= filled; i++){
                             staffName[i] = staffName[i+1];
                             staffAge[i] = staffAge[i+1];
                             staffContact[i] = staffContact[i+1];
                         } 
                         staffID[filled-1] = "";
                         staffName[filled-1] = "";
                         staffAge[filled-1] = "";
                         staffContact[filled-1] = "";
                         filled -= 1;
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
                     StaffRemove(false);
                 }
             }
             else
             {
                 Console.WriteLine("You need to add students first");
                 StaffAdd(true);
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
                if ((c >= 'A' && c <= 'Z')|| (c >= 'a' && c <= 'z'))
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
            if(input.Length < sample.Length)
            {
                char[] except = sample.Except(input).ToArray();
                matching = 1 - 1.0*except.Length / sample.Length;
            }
            else
            {
                char[] except = input.Except(sample).ToArray();
                matching = 1 - 1.0*except.Length / input.Length;
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
            if(filled > 0)
            {
                if(titleShow)
                {
                    Console.WriteLine("\n=========================SEARCHING==========================");
                }
                Console.Write("\n-----------------------------------------\n");
                Console.Write("|Enter staff name or ID for searching: ");
                string staffInfo = Console.ReadLine();
                string staffInfoID = GetNumber(staffInfo);
                Console.Write("\n-----------------------------------------\n");
                findout = 0;
                for(int i = 0; i < filled; i++)
                {
                    double nameMatching = StringMatch(staffInfo,staffName[i]);
                    if( nameMatching > 0.5 || staffID[i] == staffInfoID)
                    {
                        staffMatch[i] = staffID[i];
                        if(staffInfoID == "-1")
                        {
                            Console.WriteLine($"ID: {staffID[i]}, Name: {staffName[i]}, Age: {staffAge[i]}, Contact: {staffContact[i]}, Day start: {staffDate[i]} (Matched: {Math.Round(nameMatching*100,2)}%)");
                        }
                        else
                        {
                            Console.WriteLine($"ID: {staffID[i]}, Name: {staffName[i]}, Age: {staffAge[i]}, Contact: {staffContact[i]}, Day start: {staffDate[i]} (Matched: 100%)");
                        }
                        findout += 1;
                    }
                }
                if(findout == 0)
                {
                    Console.Write("Sorry, couldn't found. Press enter to continue");
                    Console.ReadLine();
                }
                else
                {
                    Console.Write("\n-----------------------------------------\n");
                    Console.Write("We found {0} staff that matched your input. Press enter to continue", findout);
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("You need to add students first");
                StaffAdd(true);
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
            if(userInput.Length > 0)
            {
                return userInput;
            }
            else
            {
                return "-1";
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
            if(Convert.ToInt32(userChoice) <= 6 && Convert.ToInt32(userChoice) > 0)
            {
                return userChoice;               
            }
            else
            {
                return "Your choice doesn't exist";
            }
        }
    }
}
