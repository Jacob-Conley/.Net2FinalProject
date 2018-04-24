using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class CharacterItem
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float AttackBoost { get; set; }
        public float DefenseBoost { get; set; }
        public int Quantity { get; set; }
    }
}
