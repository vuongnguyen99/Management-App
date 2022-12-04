using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Common.Exception
{
    public class NotFoundException: SystemException
    {
        public NotFoundException()
        {

        }
        public NotFoundException(string message): base(message)
        {

        }
        public NotFoundException(string message, SystemException inner): base(message, inner)
        {

        }
    }
}
