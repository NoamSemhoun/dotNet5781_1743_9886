
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    class ExceptionTarguil2 : Exception
    {
        public ExceptionTarguil2()
        {

        }
        public ExceptionTarguil2(string msg) : base(msg) { }
    }
}