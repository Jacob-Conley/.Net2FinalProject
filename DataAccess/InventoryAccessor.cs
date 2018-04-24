using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class InventoryAccessor
    {
        public static List<Equipment> RetrieveEquipment()
        {
            List<Equipment> equipmentList = new List<Equipment>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_retrieve_active_equipment_list";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // no parameters

            // all set? need a try-catch
            try
            {
                // open a connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                // check for results
                if (reader.HasRows)
                {
                    // multiple rows are possible, so use a while loop
                    while (reader.Read())
                    {
                        // create a character object
                        var equipment = new Equipment()
                        {
                            EquipmentID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Defense = reader.GetInt32(3)
                        };
                        // add character to the list
                        equipmentList.Add(equipment);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }


            return equipmentList;
        }
        public static int AddEquipment(string name, string description, int defense)
        {
            int results = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_create_equipment";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 40);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@Defense", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@Name"].Value = name;
            cmd.Parameters["@Description"].Value = description;
            cmd.Parameters["@Defense"].Value = defense;

            // execute the command surrounded by try-catch
            try
            {
                // open the connection
                conn.Open();
                // execute the command
                results = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // close the connection
                conn.Close();
            }

            return results;
        }
        public static int UpdateEquipment(int equipmentID, string oldName, string newName, string oldDescription,
            string newDescription, int oldDefense, int newDefense)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_update_equipment_by_equipmentid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@EquipmentID", SqlDbType.Int);
            cmd.Parameters.Add("@OldName", SqlDbType.NVarChar, 40);
            cmd.Parameters.Add("@NewName", SqlDbType.NVarChar, 40);
            cmd.Parameters.Add("@OldDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@NewDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@OldDefense", SqlDbType.Int);
            cmd.Parameters.Add("@NewDefense", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@EquipmentID"].Value = equipmentID;
            cmd.Parameters["@OldName"].Value = oldName;
            cmd.Parameters["@NewName"].Value = newName;
            cmd.Parameters["@OldDescription"].Value = oldDescription;
            cmd.Parameters["@NewDescription"].Value = newDescription;
            cmd.Parameters["@OldDefense"].Value = oldDefense;
            cmd.Parameters["@NewDefense"].Value = newDefense;

            // all set? need a try-catch
            try
            {
                // open a connection
                conn.Open();

                // execute the command
                result = cmd.ExecuteNonQuery();

                // display error if there are no rows affected
                if (result == 0)
                {
                    throw new ApplicationException("Equipment update failed.");
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        public static int DeleteEquipment(int EquipmentID)
        {
            int result = 0;


            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_delete_equipment_by_equipmentid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@EquipmentID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@EquipmentID"].Value = EquipmentID;

            // all set? need a try-catch
            try
            {
                // open a connection
                conn.Open();

                // execute the command
                result = cmd.ExecuteNonQuery();

                // display error if there are no rows affected
                if (result == 0)
                {
                    throw new ApplicationException("Equipment deletion failed.");
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public static List<Weapon> RetrieveWeapons()
        {
            List<Weapon> weaponList = new List<Weapon>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_retrieve_active_weapon_list";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // no parameters

            // all set? need a try-catch
            try
            {
                // open a connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                // check for results
                if (reader.HasRows)
                {
                    // multiple rows are possible, so use a while loop
                    while (reader.Read())
                    {
                        // create a character object
                        var weapon = new Weapon()
                        {
                            WeaponID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Attack = reader.GetInt32(3)
                        };
                        // add character to the list
                        weaponList.Add(weapon);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }


            return weaponList;
        }
        public static int AddWeapon(string name, string description, int attack)
        {
            int results = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_create_weapon";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 40);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@Attack", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@Name"].Value = name;
            cmd.Parameters["@Description"].Value = description;
            cmd.Parameters["@Attack"].Value = attack;

            // execute the command surrounded by try-catch
            try
            {
                // open the connection
                conn.Open();
                // execute the command
                results = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // close the connection
                conn.Close();
            }

            return results;
        }
        public static int UpdateWeapon(int weaponID, string oldName, string newName, string oldDescription,
            string newDescription, int oldAttack, int newAttack)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_update_weapon_by_weaponid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@WeaponID", SqlDbType.Int);
            cmd.Parameters.Add("@OldName", SqlDbType.NVarChar, 40);
            cmd.Parameters.Add("@NewName", SqlDbType.NVarChar, 40);
            cmd.Parameters.Add("@OldDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@NewDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@OldAttack", SqlDbType.Int);
            cmd.Parameters.Add("@NewAttack", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@WeaponID"].Value = weaponID;
            cmd.Parameters["@OldName"].Value = oldName;
            cmd.Parameters["@NewName"].Value = newName;
            cmd.Parameters["@OldDescription"].Value = oldDescription;
            cmd.Parameters["@NewDescription"].Value = newDescription;
            cmd.Parameters["@OldAttack"].Value = oldAttack;
            cmd.Parameters["@NewAttack"].Value = newAttack;

            // all set? need a try-catch
            try
            {
                // open a connection
                conn.Open();

                // execute the command
                result = cmd.ExecuteNonQuery();

                // display error if there are no rows affected
                if (result == 0)
                {
                    throw new ApplicationException("Weapon update failed.");
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        public static int DeleteWeapon(int WeaponID)
        {
            int result = 0;


            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_delete_weapon_by_weaponid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@WeaponID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@WeaponID"].Value = WeaponID;

            // all set? need a try-catch
            try
            {
                // open a connection
                conn.Open();

                // execute the command
                result = cmd.ExecuteNonQuery();

                // display error if there are no rows affected
                if (result == 0)
                {
                    throw new ApplicationException("Weapon deletion failed.");
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public static List<Item> RetrieveItems()
        {
            List<Item> itemList = new List<Item>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_retrieve_active_item_list";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // no parameters

            // all set? need a try-catch
            try
            {
                // open a connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                // check for results
                if (reader.HasRows)
                {
                    // multiple rows are possible, so use a while loop
                    while (reader.Read())
                    {
                        // create a character object
                        var item = new Item()
                        {
                            ItemID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            AttackBoost = (float)reader.GetDouble(3),
                            DefenseBoost = (float)reader.GetDouble(4)
                        };
                        // add character to the list
                        itemList.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }


            return itemList;
        }
        public static int AddItem(string name, string description, float attackBoost, float defenseBoost)
        {
            int results = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_create_item";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 40);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@AttackBoost", SqlDbType.Float);
            cmd.Parameters.Add("@DefenseBoost", SqlDbType.Float);

            // parameter values
            cmd.Parameters["@Name"].Value = name;
            cmd.Parameters["@Description"].Value = description;
            cmd.Parameters["@AttackBoost"].Value = attackBoost;
            cmd.Parameters["@DefenseBoost"].Value = defenseBoost;

            // execute the command surrounded by try-catch
            try
            {
                // open the connection
                conn.Open();
                // execute the command
                results = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // close the connection
                conn.Close();
            }

            return results;
        }
        public static int UpdateItem(int itemID, string oldName, string newName, string oldDescription,
            string newDescription, float oldAttackBoost, float newAttackBoost, float oldDefenseBoost, float newDefenseBoost)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_update_item_by_itemid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@ItemID", SqlDbType.Int);
            cmd.Parameters.Add("@OldName", SqlDbType.NVarChar, 40);
            cmd.Parameters.Add("@NewName", SqlDbType.NVarChar, 40);
            cmd.Parameters.Add("@OldDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@NewDescription", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@OldAttackBoost", SqlDbType.Float);
            cmd.Parameters.Add("@NewAttackBoost", SqlDbType.Float);
            cmd.Parameters.Add("@OldDefenseBoost", SqlDbType.Float);
            cmd.Parameters.Add("@NewDefenseBoost", SqlDbType.Float);

            // parameter values
            cmd.Parameters["@ItemID"].Value = itemID;
            cmd.Parameters["@OldName"].Value = oldName;
            cmd.Parameters["@NewName"].Value = newName;
            cmd.Parameters["@OldDescription"].Value = oldDescription;
            cmd.Parameters["@NewDescription"].Value = newDescription;
            cmd.Parameters["@OldAttackBoost"].Value = oldAttackBoost;
            cmd.Parameters["@NewAttackBoost"].Value = newAttackBoost;
            cmd.Parameters["@OldDefenseBoost"].Value = oldDefenseBoost;
            cmd.Parameters["@NewDefenseBoost"].Value = newDefenseBoost;

            // all set? need a try-catch
            try
            {
                // open a connection
                conn.Open();

                // execute the command
                result = cmd.ExecuteNonQuery();

                // display error if there are no rows affected
                if (result == 0)
                {
                    throw new ApplicationException("Item update failed.");
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        public static int DeleteItem(int ItemID)
        {
            int result = 0;


            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_delete_item_by_itemid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@ItemID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@ItemID"].Value = ItemID;

            // all set? need a try-catch
            try
            {
                // open a connection
                conn.Open();

                // execute the command
                result = cmd.ExecuteNonQuery();

                // display error if there are no rows affected
                if (result == 0)
                {
                    throw new ApplicationException("Item deletion failed.");
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

    }
}
