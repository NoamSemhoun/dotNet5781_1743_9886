using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlAPI
{
    public class BLFactory
    {
        public static IBL GetBL()
        {
            return new BL.BLImp();
        }
    }
}
