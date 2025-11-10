using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibje_Omer_Akdeniz
{
    internal class Magazine : ReadingRoomItem
    {
		private byte month;
		public byte Month
		{
			get { return month; }
			set 
			{
                if (value > 12 || value < 1)
                {
                    Console.WriteLine("De maand moet tussen 1 en 12 liggen.");
                }
                month = value; 
			}
		}

		private uint year;
        public uint Year
		{
			get { return year; }
			set 
			{
                if (value > 2500)
                {
                    Console.WriteLine("Het jaartal is maximaal 2500");
                }
                year = value; 
			}
		}

        public override string Identification
        {
            get 
            {
                string[] words = Title.Split(" ");
                string letters = "";

                foreach (var word in words)
                {
                    if (word != "" && letters.Length < 3)
                    {
                        letters += word[0];
                    }
                }

                letters = letters.ToUpper();
                string date = new DateTime((int)Year, Month, 1).ToString("MMyyyy");

                return $"{letters}{date}";
            }
        }

        public override string Category
        {
            get
            {
                return "Maandblad";
            }
        }

        public Magazine(string title, string publisher, byte month, uint year) : base(title, publisher)
        {
            Month = month;
            Year = year;
        }


    }
}
