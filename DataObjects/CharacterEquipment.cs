using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class CharacterEquipment
    {
        public int EquipmentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Defense { get; set; }
        public int Quantity { get; set; }
    }
}
