using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class CharacterInventoryManager
    {
        public int CreateDefaultInventory(int playerCharacterID)
        {
            int result = 0;

            try
            {
                result = CharacterInventoryAccessor.AddDefaultEquipment(playerCharacterID);
                result = CharacterInventoryAccessor.AddDefaultWeapon(playerCharacterID);
                result = CharacterInventoryAccessor.AddDefaultItems(playerCharacterID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }

        public int CreateEquipment(int playerCharacterID, int equipmentID, int quantity)
        {
            int result = 0;

            try
            {
                result = CharacterInventoryAccessor.AddCharacterEquipment(playerCharacterID, equipmentID, quantity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
        public List<CharacterEquipment> RetrieveCharacterEquipmentList(int playerCharacterID)
        {
            List<CharacterEquipment> characterEquipmentList = null;

            try
            {
                characterEquipmentList = CharacterInventoryAccessor.RetrieveEquipmentByCharacterID(playerCharacterID);
            }
            catch (Exception)
            {

                throw;
            }

            return characterEquipmentList;
        }
        public int UpdateEquipment(int playerCharacterID, int equipmentID, int oldQuantity, int newQuantity)
        {
            int result = 0;

            try
            {
                result = CharacterInventoryAccessor.UpdateCharacterEquipment(playerCharacterID, equipmentID, oldQuantity, newQuantity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
        public int DeleteEquipment(int playerCharacterID, int equipmentID)
        {
            int result = 0;

            try
            {
                result = CharacterInventoryAccessor.DeleteCharacterEquipment(playerCharacterID, equipmentID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }

        public int CreateWeapon(int playerCharacterID, int weaponID, int quantity)
        {
            int result = 0;

            try
            {
                result = CharacterInventoryAccessor.AddCharacterWeapon(playerCharacterID, weaponID, quantity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
        public List<CharacterWeapon> RetrieveCharacterWeaponList(int playerCharacterID)
        {
            List<CharacterWeapon> characterWeaponList = null;

            try
            {
                characterWeaponList = CharacterInventoryAccessor.RetrieveWeaponsByCharacterID(playerCharacterID);
            }
            catch (Exception)
            {

                throw;
            }

            return characterWeaponList;
        }
        public int UpdateWeapon(int playerCharacterID, int weaponID, int oldQuantity, int newQuantity)
        {
            int result = 0;

            try
            {
                result = CharacterInventoryAccessor.UpdateCharacterWeapon(playerCharacterID, weaponID, oldQuantity, newQuantity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
        public int DeleteWeapon(int playerCharacterID, int weaponID)
        {
            int result = 0;

            try
            {
                result = CharacterInventoryAccessor.DeleteCharacterWeapon(playerCharacterID, weaponID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
          
        public int CreateItem(int playerCharacterID, int itemID, int quantity)
        {
            int result = 0;

            try
            {
                result = CharacterInventoryAccessor.AddCharacterItem(playerCharacterID, itemID, quantity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
        public List<CharacterItem> RetrieveCharacterItemsList(int playerCharacterID)
        {
            List<CharacterItem> characterItemList = null;

            try
            {
                characterItemList = CharacterInventoryAccessor.RetrieveItemsByCharacterID(playerCharacterID);
            }
            catch (Exception)
            {

                throw;
            }

            return characterItemList;
        }
        public int UpdateItem(int playerCharacterID, int itemID, int oldQuantity, int newQuantity)
        {
            int result = 0;

            try
            {
                result = CharacterInventoryAccessor.UpdateCharacterItem(playerCharacterID, itemID, oldQuantity, newQuantity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
        public int DeleteItem(int playerCharacterID, int itemID)
        {
            int result = 0;

            try
            {
                result = CharacterInventoryAccessor.DeleteCharacterItem(playerCharacterID, itemID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }
    }
}
