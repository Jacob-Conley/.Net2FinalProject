using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class CharacterInventory
    {
        public List<CharacterWeapon> Weapons { get; set; }
        public List<CharacterEquipment> Equipment { get; set; }
        public List<CharacterItem> Items { get; set; }

        public CharacterInventory(List<CharacterWeapon> weapons, 
            List<CharacterEquipment> equipment, List<CharacterItem> items)
        {
            this.Weapons = weapons;
            this.Equipment = equipment;
            this.Items = items;
        }
    }
}
