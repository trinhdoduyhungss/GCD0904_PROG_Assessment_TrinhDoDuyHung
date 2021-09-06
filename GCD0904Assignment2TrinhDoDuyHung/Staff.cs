using System;

namespace GCD0904Assignment2TrinhDoDuyHung
{
    class Staff : IStaff
    {
        string staffName;
        public string Name
        {
            get
            {
                return staffName;
            }
            set
            {
                staffName = value;
            }
        }

        string staffID;
        public string ID
        {
            get
            {
                return staffID;
            }
            set
            {
                staffID = value;
            }
        }

        string staffContact;
        public string Contact
        {
            get
            {
                return staffContact;
            }
            set
            {
                staffContact = value;
            }
        }

        string staffAge;
        public string Age
        {
            get
            {
                return staffAge;
            }
            set
            {
                staffAge = value;
            }
        }

        string staffDate = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
        public string StartDate
        {
            get
            {
                return StartDate;
            }
        }

        public void Display()
        {
            Console.WriteLine($"ID: {staffID}, Name: {staffName}, Age: {staffAge}, Contact: {staffContact}, Day start: {staffDate}");
        }

    }
}
