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
    public class CharacterInventoryAccessor
    {
        public static List<CharacterEquipment> RetrieveEquipmentByCharacterID(int PlayerCharacterID)
        {
            List<CharacterEquipment> characterEquipment = new List<CharacterEquipment>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_retrieve_equipment_by_playercharacterid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;

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
                        var characterEq = new CharacterEquipment()
                        {
                            EquipmentID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Defense = reader.GetInt32(3),
                            Quantity = reader.GetInt32(4)
                        };
                        // add character to the list
                        characterEquipment.Add(characterEq);
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


            return characterEquipment;
        }
        public static int AddCharacterEquipment(int PlayerCharacterID, int EquipmentID, int quantity)
        {
            int results = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_add_equipment_to_character_equipment_inventory";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);
            cmd.Parameters.Add("@EquipmentID", SqlDbType.Int);
            cmd.Parameters.Add("@Quantity", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;
            cmd.Parameters["@EquipmentID"].Value = EquipmentID;
            cmd.Parameters["@Quantity"].Value = quantity;

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
        public static int AddDefaultEquipment(int PlayerCharacterID)
        {
            int results = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_create_default_character_equipment_inventory";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;

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
        public static int UpdateCharacterEquipment(int PlayerCharacterID, int EquipmentID, int oldQuantity, int newQuantity)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_update_equipment_by_playercharacterid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);
            cmd.Parameters.Add("@EquipmentID", SqlDbType.Int);
            cmd.Parameters.Add("@OldQuantity", SqlDbType.Int);
            cmd.Parameters.Add("@NewQuantity", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;
            cmd.Parameters["@EquipmentID"].Value = EquipmentID;
            cmd.Parameters["@OldQuantity"].Value = oldQuantity;
            cmd.Parameters["@NewQuantity"].Value = newQuantity;

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
                    throw new ApplicationException("Equipment quantity update failed.");
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
        public static int DeleteCharacterEquipment(int PlayerCharacterID, int EquipmentID)
        {
            int result = 0;


            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_delete_equipment_from_character_equipment_inventory";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);
            cmd.Parameters.Add("@EquipmentID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;
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

        public static List<CharacterWeapon> RetrieveWeaponsByCharacterID(int PlayerCharacterID)
        {
            List<CharacterWeapon> characterWeapons = new List<CharacterWeapon>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_retrieve_weapons_by_playercharacterid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;

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
                        var characterWeapon = new CharacterWeapon()
                        {
                            WeaponID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Attack = reader.GetInt32(3),
                            Quantity = reader.GetInt32(4)
                        };
                        // add character to the list
                        characterWeapons.Add(characterWeapon);
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


            return characterWeapons;
        }
        public static int AddCharacterWeapon(int PlayerCharacterID, int WeaponID, int quantity)
        {
            int results = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_add_weapon_to_weapon_character_inventory";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);
            cmd.Parameters.Add("@WeaponID", SqlDbType.Int);
            cmd.Parameters.Add("@Quantity", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;
            cmd.Parameters["@WeaponID"].Value = WeaponID;
            cmd.Parameters["@Quantity"].Value = quantity;

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
        public static int AddDefaultWeapon(int PlayerCharacterID)
        {
            int results = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_create_default_weapon_inventory";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;

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
        public static int UpdateCharacterWeapon(int PlayerCharacterID, int WeaponID, int oldQuantity, int newQuantity)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_update_weapon_quantity_by_playercharacterid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);
            cmd.Parameters.Add("@WeaponID", SqlDbType.Int);
            cmd.Parameters.Add("@OldQuantity", SqlDbType.Int);
            cmd.Parameters.Add("@NewQuantity", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;
            cmd.Parameters["@WeaponID"].Value = WeaponID;
            cmd.Parameters["@OldQuantity"].Value = oldQuantity;
            cmd.Parameters["@NewQuantity"].Value = newQuantity;

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
                    throw new ApplicationException("Weapon quantity update failed.");
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
        public static int DeleteCharacterWeapon(int PlayerCharacterID, int WeaponID)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_delete_weapon_from_character_weapon_inventory";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);
            cmd.Parameters.Add("@WeaponID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;
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

        public static List<CharacterItem> RetrieveItemsByCharacterID(int PlayerCharacterID)
        {
            List<CharacterItem> characterItems = new List<CharacterItem>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_retrieve_items_by_playercharacterid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;

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
                        var characterItem = new CharacterItem()
                        {
                            ItemID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                             AttackBoost = (float) reader.GetDouble(3),
                            DefenseBoost = (float) reader.GetDouble(4),
                            Quantity = reader.GetInt32(5)
                        };
                        // add character to the list
                        characterItems.Add(characterItem);
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


            return characterItems;
        }
        public static int AddCharacterItem(int PlayerCharacterID, int ItemID, int Quantity)
        {
            int results = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_add_item_to_character_item_inventory";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);
            cmd.Parameters.Add("@ItemID", SqlDbType.Int);
            cmd.Parameters.Add("@Quantity", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;
            cmd.Parameters["@ItemID"].Value = ItemID;
            cmd.Parameters["@Quantity"].Value = Quantity;

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
        public static int AddDefaultItems(int PlayerCharacterID)
        {
            int results = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_create_default_item_inventory";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;

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
        public static int UpdateCharacterItem(int PlayerCharacterID, int ItemID, int OldQuantity, int NewQuantity)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_update_item_quantity";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);
            cmd.Parameters.Add("@ItemID", SqlDbType.Int);
            cmd.Parameters.Add("@OldQuantity", SqlDbType.Int);
            cmd.Parameters.Add("@NewQuantity", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;
            cmd.Parameters["@ItemID"].Value = ItemID;
            cmd.Parameters["@OldQuantity"].Value = OldQuantity;
            cmd.Parameters["@NewQuantity"].Value = NewQuantity;

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
                    throw new ApplicationException("Item quantity update failed.");
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
        public static int DeleteCharacterItem(int PlayerCharacterID, int ItemID)
        {
            int result = 0;


            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_delete_item_from_character_item_inventory";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);
            cmd.Parameters.Add("@ItemID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;
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

