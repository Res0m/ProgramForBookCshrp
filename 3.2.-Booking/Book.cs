using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._2._Booking
{
    public class Book
    {
        public int id { get; private set; }
        public string name { get; set; }
        public string telephone { get; set; }
        public int beginingOfBook { get; set; }
        public int endingOfBook { get; set; }
        public string comment { get; set; }
        public Table table { get; private set; }


        public Book(int clientID, string clientNAME, string clientNumber, int start, int end, string clientComment, Table tb)
        {
            this.id = clientID;
            this.name = clientNAME;
            this.telephone = clientNumber;
            this.beginingOfBook = start;
            this.endingOfBook = end;
            this.comment = clientComment;
            this.table = tb;



            for (int hour = start; hour < end; hour++)
            {
                if (table.Schedule[hour] == null)
                {
                    table.Schedule[hour] = this;
                }
                
            }
        }
        public void ChangedOfBook(int newBegin, int newEnd, string Comment)
        {
            for (int old = beginingOfBook; old < endingOfBook; old++)
            {
                table.Schedule[old] = null;
            }
            beginingOfBook = newBegin;
            endingOfBook = newEnd;
            for (int newSchd = beginingOfBook; newSchd < endingOfBook; newSchd++)
            {
                table.Schedule[newSchd] = this;

            }

            comment = Comment;
        }
        public void CancelOfBook()
        {
            for (int hour = beginingOfBook; hour < endingOfBook; hour++)
            {
                table.Schedule[hour] = null;
            }
        }
    }
}
