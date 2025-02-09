using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._2._Booking
{
    public class Table
    {
        public int Id { get; private set; }
        public string Location { get; set; }
        public int Seats { get; set; }
        public Dictionary<int, Book> Schedule { get; private set; }

        public Table(int ID, string loc, int Seats1)
        {
            Id = ID;
            Location = loc;
            Seats = Seats1;




            Schedule = new Dictionary<int, Book>();
            for (int hours = 9; hours < 18; hours++)
            {
                Schedule[hours] = null;
            }


        }
        public void changeOfInfoAboveTale(string NewTableLoc, int newSeats)
        {
            Location = NewTableLoc;
            Seats = newSeats;
        }

        public void printInfoAboveTable()
        {
            Console.WriteLine($"ID: ..................................................{(Id < 10 ? "0" + Id : Id)}");
            Console.WriteLine($"Расположение: ..................................................\"{Location}\"");
            Console.WriteLine($"Количество мест: ..................................................{Seats}");
            Console.WriteLine($"Расписание:");
            for (int i = 9; i < 18; i++)
            {
                var booking = Schedule[i];
                if (Schedule[i] != null)
                {
                    Console.WriteLine($"{i}:00 - {i + 1}:00 --------------------------ID: {booking.id}, {booking.name}, {booking.telephone}");
                }
                else
                {
                    Console.WriteLine($"{i}:00 - {i + 1}:00 --------------------------------------------------");
                }
            }
        }
    }
}
