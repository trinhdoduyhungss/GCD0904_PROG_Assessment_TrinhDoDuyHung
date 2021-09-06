using System;

namespace GCD0904Assignment2TrinhDoDuyHung
{
    interface IStaff
    {
		string Name { get; set; }
		string ID { get; set; }
		string Contact { get; set; }
		string Age { get; set; }
		string StartDate { get; }

		void Display();
	}
}
