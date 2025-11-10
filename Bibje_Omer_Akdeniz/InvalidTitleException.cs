using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibje_Omer_Akdeniz
{
    internal class InvalidTitleException : Exception
    {
        public InvalidTitleException(string message) : base(message) { }
    }
}
