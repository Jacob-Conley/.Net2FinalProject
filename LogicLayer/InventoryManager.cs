using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class InventoryManager
    {
        public int CreateEquipment(string name, string description, int defense)
        {
            int result = 0;

            try
            {
                result = InventoryAccessor.AddEquipment(name, description, defense);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
        public List<Equipment> RetrieveEquipmentList()
        {
            List<Equipment> equipmentList = null;

            try
            {
                equipmentList = InventoryAccessor.RetrieveEquipment();
            }
            catch (Exception)
            {
                throw;
            }

            return equipmentList;
        }
        public int UpdateEquipment(int equipmentID, string oldName, string newName, string oldDescription,
            string newDescription, int oldDefense, int newDefense)
        {
            int result = 0;

            try
            {
                result = InventoryAccessor.UpdateEquipment(equipmentID, oldName, newName, oldDescription,
                    newDescription, oldDefense, newDefense);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
        public int DeleteEquipment(int equipmentID)
        {
            int result = 0;

            try
            {
                result = InventoryAccessor.DeleteEquipment(equipmentID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }

        public int CreateWeapon(string name, string description, int attack)
        {
            int result = 0;

            try
            {
                result = InventoryAccessor.AddWeapon(name, description, attack);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
        public List<Weapon> RetrieveWeaponList()
        {
            List<Weapon> weaponList = null;

            try
            {
                weaponList = InventoryAccessor.RetrieveWeapons();
            }
            catch (Exception)
            {
                throw;
            }

            return weaponList;
        }
        public int UpdateWeapon(int weaponID, string oldName, string newName, string oldDescription,
            string newDescription, int oldAttack, int newAttack)
        {
            int result = 0;

            try
            {
                result = InventoryAccessor.UpdateWeapon(weaponID, oldName, newName, oldDescription,
                    newDescription, oldAttack, newAttack);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
        public int DeleteWeapon(int weaponID)
        {
            int result = 0;

            try
            {
                result = InventoryAccessor.DeleteWeapon(weaponID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }


        public int CreateItem(string name, string description, float attackBoost, float defenseBoost)
        {
            int result = 0;

            try
            {
                result = InventoryAccessor.AddItem(name, description, attackBoost, defenseBoost);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
        public List<Item> RetrieveItemList()
        {
            List<Item> itemList = null;

            try
            {
                itemList = InventoryAccessor.RetrieveItems();
            }
            catch (Exception)
            {
                throw;
            }

            return itemList;
        }
        public int UpdateItem(int itemID, string oldName, string newName, string oldDescription,
            string newDescription, float oldAttackBoost, float newAttackBoost, float oldDefenseBoost, float newDefenseBoost)
        {
            int result = 0;

            try
            {
                result = InventoryAccessor.UpdateItem(itemID, oldName, newName, oldDescription,
                    newDescription, oldAttackBoost, newAttackBoost ,oldDefenseBoost, newDefenseBoost);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
        public int DeleteItem(int itemID)
        {
            int result = 0;

            try
            {
                result = InventoryAccessor.DeleteItem(itemID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }

    }
}
