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
                        IO.Alert("Your choice doesn't exist, please press Enter to continue", true);
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

            // Takes user input
            string userChoice = IO.Menu();

            // Gets numbers in the user input
            userChoice = Tool.GetNumber(userChoice);

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
                    IO.Header("SEARCHING");
                }
                IO.BreakLine();
                string staffInfo = IO.GetInput("|Enter staff name or ID for searching: "); 
                string staffInfoID = Tool.GetNumber(staffInfo);
                IO.BreakLine();
                foreach (Staff i in listDictionary.Values)
                {
                    double nameMatching = Tool.StringMatch(staffInfo, i.Name);
                    if (nameMatching > 0.5 || staffInfoID == i.ID)
                    {
                        i.Display();
                        staffMatch += i.ID+",";
                    }
                }
                if (staffMatch.Length == 0)
                {
                    IO.Alert("Sorry, couldn't found. Press enter to continue", true);
                }
                else
                {
                    IO.BreakLine();
                    IO.Alert($"We found {staffMatch.Split(',').Length - 1} staff that matched your input. Press enter to continue", true);
                }
            }
            else
            {
                IO.Alert("You need to add students first", false);
                StaffAdd(true);
            }
        } 

        // Displays staff
        static void StaffDisplay()
        {
            if(listDictionary.Count > 0)
            {
                IO.Header("DISPLAYING");
                foreach(Staff i in listDictionary.Values)
                {
                    i.Display();
                }
                IO.Footer("Press Enter to continue", true);
            }
            else
            {
                IO.Alert("You need to add students first", false);
                StaffAdd(true);
            }
        }

        // Adds staff
        static void StaffAdd(bool titleShow)
        {
            if (titleShow) // To disable the display of a function's title when it has been called again.
            {
                IO.Header("ADDING");
            }
            int n = Convert.ToInt32(Tool.GetNumber(IO.GetInput("Enter number of staff you want to add: ")));
            int count = 0;
            if (n > 0)
            {
                while (count < n)
                {
                    Staff staffNew = new Staff();
                    string name = IO.GetInput("\n|Enter staff name: ");
                    if (Convert.ToInt32(Tool.GetNumber(name)) > 0)
                    {
                        IO.Alert("Your input isn't valid, enter again please", false);
                    }
                    else
                    {
                        string age = Tool.GetNumber(IO.GetInput("|Enter staff age: "));
                        if (Convert.ToInt32(age) > 0)
                        {
                            string contact = Tool.GetNumber(IO.GetInput("|Enter staff contact: "));
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
                                IO.Alert("Your input isn't valid", false);
                            }
                        }
                        else
                        {
                            IO.Alert("Your input isn't valid", false);
                        }
                    }
                }
                IO.Footer("", false);
                StaffDisplay();
            }
            else
            {
                IO.Alert("Your input isn't valid", false);
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
                    IO.Header("EDITING");
                }
                StaffSearch(false);
                if (staffMatch.Length > 0)
                {
                    IO.BreakLine();
                    string userInputID = Tool.GetNumber(IO.GetInput("|Re-enter staff ID to confirm: "));
                    if (userInputID != "-1" && Array.IndexOf(staffMatch.Split(','), userInputID) != -1)
                    { 
                        Staff staffEdit = listDictionary[userInputID];
                        IO.BreakLine();
                        string userInputName = IO.GetInput($"Rename for ID: {userInputID} or press Enter to skip: ");
                        if (userInputName.Length > 1)
                        {
                            staffEdit.Name = userInputName;
                        }
                        string userInputAge = IO.GetInput("Change age or press Enter to skip: ");
                        if (userInputAge.Length > 1)
                        {
                            staffEdit.Age = userInputAge;
                        }
                        string userInputContact = IO.GetInput("Change contact or press Enter to skip: ");
                        if (userInputContact.Length > 1)
                        {
                            staffEdit.Contact = userInputContact;
                        }
                        IO.Footer("", false);
                        StaffDisplay();
                    }
                    else
                    {
                        IO.Alert("Your input isn't valid, the process has been aborted. Try later, press enter to comback the menu", true);
                    }
                }
                else
                {
                    StaffEdit(false);
                }
            }
            else
            {
                IO.Alert("You need to add students first", false);
                StaffAdd(true);
            }
            staffMatch = "";
        }

        // Remove staff
        static void StaffRemove(bool titleShow)
        {
            if (listDictionary.Count > 0)
            {
                if (titleShow)
                {
                    IO.Header("EDITING");
                }
                StaffSearch(false);
                if (staffMatch.Length > 0)
                {
                    IO.BreakLine();
                    string userInputID = Tool.GetNumber(IO.GetInput("|Re-enter staff ID to confirm: "));
                    if (userInputID != "-1" && Array.IndexOf(staffMatch.Split(','), userInputID) != -1)
                    {
                        listDictionary.Remove(userInputID);
                        StaffDisplay();
                    }
                    else
                    {
                        IO.Alert("Your input isn't valid, the process has been aborted. Try later, press enter to comback the menu", true);
                    }
                }
                else
                {
                    StaffRemove(false);
                }
            }
            else
            {
                IO.Alert("You need to add students first", false);
                StaffAdd(true);
            }
            staffMatch = "";
        }
    }
}
