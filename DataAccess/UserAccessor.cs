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
    public static class UserAccessor
    {
        public static List<Character> RetrieveCharactersByGameUserID(int GameUserID)
        {
            List<Character> characters = new List<Character>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_retrieve_characters_by_gameuserid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@GameUserID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@GameUserID"].Value = GameUserID;

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
                        var character = new Character()
                        {
                            PlayerCharacterID = reader.GetInt32(0),
                            PlayerName = reader.GetString(1),
                            PlayerRace = reader.GetString(2),
                            PlayerClass = reader.GetString(3),
                            PlayerImage = reader.GetString(4),
                            PlayerSlot = reader.GetInt32(5),
                            StatID = reader.GetInt32(6)
                        };
                        // add character to the list
                        characters.Add(character);
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


            return characters;
        }

        public static int CreateCharacter(int gameUserID, int fileNumber, string name, string race, string playerClass,
            string image)
        {
            int results = 0;

            // connection
            var conn = DBConnection.GetDBConnection();
            
            // cmdtext
            var cmdText = @"sp_create_character_by_gameuserid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@GameUserID", SqlDbType.Int);
            cmd.Parameters.Add("@PlayerName", SqlDbType.NVarChar, 40);
            cmd.Parameters.Add("@PlayerRace", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@PlayerClass", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@PlayerImage", SqlDbType.NVarChar, 40);
            cmd.Parameters.Add("@PlayerSlot", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@GameUserID"].Value = gameUserID;
            cmd.Parameters["@PlayerName"].Value = name;
            cmd.Parameters["@PlayerRace"].Value = race;
            cmd.Parameters["@PlayerClass"].Value = playerClass;
            cmd.Parameters["@PlayerImage"].Value = image;
            cmd.Parameters["@PlayerSlot"].Value = fileNumber;


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

        public static int UpdateCharacter(int PlayerCharacterID, string oldClass, string newClass)
        {
            int result = 0;


            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_update_character_by_playercharacterid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@PlayerCharacterID", SqlDbType.Int);
            cmd.Parameters.Add("@OldClass", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@NewClass", SqlDbType.NVarChar, 20);

            // parameter values
            cmd.Parameters["@PlayerCharacterID"].Value = PlayerCharacterID;
            cmd.Parameters["@OldClass"].Value = oldClass;
            cmd.Parameters["@NewClass"].Value = newClass;

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
                    throw new ApplicationException("Character update failed.");
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

        public static int DeleteCharacter(int PlayerCharacterID)
        {
            int result = 0;


            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_delete_character_by_playercharacterid";

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
                result = cmd.ExecuteNonQuery();

                // display error if there are no rows affected
                if (result == 0)
                {
                    throw new ApplicationException("Character deletion failed.");
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
