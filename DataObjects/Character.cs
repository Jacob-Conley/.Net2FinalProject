using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Character
    {

        public int PlayerCharacterID { get; set; }
        public string PlayerName { get; set; }
        public string PlayerRace { get; set; }
        public string PlayerClass { get; set; }
        public string PlayerImage { get; set; }
        public int PlayerSlot { get; set; }
        public int StatID{ get; set; }

    }
}
