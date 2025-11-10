using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibje_Omer_Akdeniz
{
    internal class InvalidIsbnException : Exception
    {
        public InvalidIsbnException(string message) : base(message) { }
    }
}
