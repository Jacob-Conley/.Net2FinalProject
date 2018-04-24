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
    public static class PlayerAccessor
    {
        public static int CreateUser(string username, string passwordHash, string email)
        {
            var result = 0; // Variable to return, should be one user that matches

            // get the connection to the database
            var conn = DBConnection.GetDBConnection();

            // Set the command text to the stored procedure's name
            var cmdText = @"sp_create_gameuser";

            // Create a command using the command text and the connection
            var cmd = new SqlCommand(cmdText, conn);

            // Set the command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameters 
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 16);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 200);

            // Set the parameter values
            cmd.Parameters["@Username"].Value = username;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;
            cmd.Parameters["@Email"].Value = email;


            // execute the command surrounded by try-catch
            try
            {
                // open the connection
                conn.Open();
                // execute the command
                result = (int)cmd.ExecuteNonQuery();
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

            return result;
        }

        public static int VerifyUserNameAndPassword(string username, string passwordHash)
        {
            var result = 0; // Variable to return, should be one user that matches

            // get the connection to the database
            var conn = DBConnection.GetDBConnection();

            // Set the command text to the stored procedure's name
            var cmdText = @"sp_authenticate_user";

            // Create a command using the command text and the connection
            var cmd = new SqlCommand(cmdText, conn);

            // Set the command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameters 
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 16);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // Set the parameter values
            cmd.Parameters["@Username"].Value = username;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;


            // execute the command surrounded by try-catch
            try
            {
                // open the connection
                conn.Open();
                // execute the command
                result = (int)cmd.ExecuteScalar();
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

            return result;
        }

        public static User RetrieveUserByUsername(string username)
        {
            User user = null;

            // connection first
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_retrieve_gameuser_by_username";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 100);

            // parameters values
            cmd.Parameters["@Username"].Value = username;

            // try-catch to execute command
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    reader.Read(); // reads the next line in the result

                    // create a new employee object
                    user = new User()
                    {
                        // [GameUserID], [Username], [PasswordHash], [Email], [GameEditor], [Active]
                        GameUserID = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        PasswordHash = reader.GetString(2),
                        Email = reader.GetString(3),
                        GameEditor = reader.GetBoolean(4),
                        Active = reader.GetBoolean(5)
                    };
                    if (user.Active != true)
                    {
                        throw new ApplicationException("Not an active user.");
                    }
                }
                else
                {
                    throw new ApplicationException("User not found!");
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

            return user;
        }

        public static int UpdateAccount(int gameUserID, string oldEmail, string newEmail, string oldPasswordHash, string newPasswordHash)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_update_account";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            
            // parameters
            cmd.Parameters.Add("@GameUserID", SqlDbType.Int);
            cmd.Parameters.Add("@OldEmail", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@oldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newPasswordHash", SqlDbType.NVarChar, 100);

            // parameter values
            cmd.Parameters["@GameUserID"].Value = gameUserID;
            cmd.Parameters["@OldEmail"].Value = oldEmail;
            cmd.Parameters["@NewEmail"].Value = newEmail;
            cmd.Parameters["@oldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@newPasswordHash"].Value = newPasswordHash;

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
                    throw new ApplicationException("Account update failed.");
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

        public static int UpgradeAccount(int gameUserID, string oldEmail, string newEmail, string oldPasswordHash, string newPasswordHash)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_upgrade_account";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@GameUserID", SqlDbType.Int);
            cmd.Parameters.Add("@OldEmail", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@oldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newPasswordHash", SqlDbType.NVarChar, 100);

            // parameter values
            cmd.Parameters["@GameUserID"].Value = gameUserID;
            cmd.Parameters["@OldEmail"].Value = oldEmail;
            cmd.Parameters["@NewEmail"].Value = newEmail;
            cmd.Parameters["@oldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@newPasswordHash"].Value = newPasswordHash;

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
                    throw new ApplicationException("Account upgrade failed.");
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

        public static int DeleteAccount(int gameUserID)
        {
            int result = 0;


            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_deactivate_gameuser_by_gameuserid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@GameUserID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@GameUserID"].Value = gameUserID;

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
                    throw new ApplicationException("Account deletion failed.");
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
