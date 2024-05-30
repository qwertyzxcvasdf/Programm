using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programm.Model
{
    public static class AppData
    {
        public static businessBDEntities db = new businessBDEntities();
        public static Client currentUser;

    }
}
