using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibje_Omer_Akdeniz
{
    internal class NewsPaper : ReadingRoomItem
    {
		private DateTime date;
        public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}

        public override string Category
        {
            get 
            {
                return "Krant";
            }
        }

        public override string Identification
        {
            get
            {

                string[] words = Title.Split(" "); 
                string letters = "";

                foreach (string word in words)
                {
                    if (word != "" && letters.Length < 3)
                    {
                        letters += word[0]; // pakt elke eerste letter => voegt toe aan afkorting
                    }
                }

                letters = letters.ToUpper();


                string date = Date.ToString("ddMMyyyy");

                return $"{letters}{date}";
            }
        }

        public NewsPaper(string title, string publisher, DateTime date) : base(title, publisher)
        {
            Date = date;
        }
    }
}
