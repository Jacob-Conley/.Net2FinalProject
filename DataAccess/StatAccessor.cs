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
    public class StatAccessor
    {
        public static int CreateStats(int strength, int stamina, int dexterity, int intelligence)
        {
            int results = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_create_stat";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Strength", SqlDbType.Int);
            cmd.Parameters.Add("@Stamina", SqlDbType.Int);
            cmd.Parameters.Add("@Dexterity", SqlDbType.Int);
            cmd.Parameters.Add("@Intelligence", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@Strength"].Value = strength;
            cmd.Parameters["@Stamina"].Value = stamina;
            cmd.Parameters["@Dexterity"].Value = dexterity;
            cmd.Parameters["@Intelligence"].Value = intelligence;

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

        public static Stat RetrieveStatsByStatID(int StatID)
        {
            Stat stats = null;

            // connection
            var conn = DBConnection.GetDBConnection();

            // cmdtext
            var cmdText = @"sp_select_stat_by_statid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@StatID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@StatID"].Value = StatID;

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
                    reader.Read();
                    // create a Stat object
                    stats = new Stat()
                    {
                        PlayerLevel = reader.GetInt32(0),
                        Strength = reader.GetInt32(1),
                        Stamina = reader.GetInt32(2),
                        Dexterity = reader.GetInt32(3),
                        Intelligence = reader.GetInt32(4),
                        Experience = reader.GetInt32(5),
                        PointsRemaining = reader.GetInt32(6)
                    };

                }
                else // no rows!
                {
                    throw new ApplicationException("Record not found");
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


            return stats;
        }

        public static int ResetStats(int StatID, int strength, int stamina, int dexterity, int intelligence)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_reset_stats";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@StatID", SqlDbType.Int);
            cmd.Parameters.Add("@OldStrength", SqlDbType.Int);
            cmd.Parameters.Add("@OldStamina", SqlDbType.Int);
            cmd.Parameters.Add("@OldDexterity", SqlDbType.Int);
            cmd.Parameters.Add("@OldIntelligence", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@StatID"].Value = StatID;
            cmd.Parameters["@OldStrength"].Value = strength;
            cmd.Parameters["@OldStamina"].Value = stamina;
            cmd.Parameters["@OldDexterity"].Value = dexterity;
            cmd.Parameters["@OldIntelligence"].Value = intelligence;

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
                    throw new ApplicationException("Stat reset failed.");
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

        public static int UpdateStats(int StatID, int oldStrength, int strength, int oldStamina, int stamina,
            int oldDexterity, int dexterity, int oldIntelligence, int intelligence, int pointsRemaining)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_update_stats_by_statid";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@StatID", SqlDbType.Int);
            cmd.Parameters.Add("@OldStrength", SqlDbType.Int);
            cmd.Parameters.Add("@NewStrength", SqlDbType.Int);
            cmd.Parameters.Add("@OldStamina", SqlDbType.Int);
            cmd.Parameters.Add("@NewStamina", SqlDbType.Int);
            cmd.Parameters.Add("@OldDexterity", SqlDbType.Int);
            cmd.Parameters.Add("@NewDexterity", SqlDbType.Int);
            cmd.Parameters.Add("@OldIntelligence", SqlDbType.Int);
            cmd.Parameters.Add("@NewIntelligence", SqlDbType.Int);
            cmd.Parameters.Add("@PointsRemaining", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@StatID"].Value = StatID;
            cmd.Parameters["@OldStrength"].Value = oldStrength;
            cmd.Parameters["@NewStrength"].Value = strength;
            cmd.Parameters["@OldStamina"].Value = oldStamina;
            cmd.Parameters["@NewStamina"].Value = stamina;
            cmd.Parameters["@OldDexterity"].Value = oldDexterity;
            cmd.Parameters["@NewDexterity"].Value = dexterity;
            cmd.Parameters["@OldIntelligence"].Value = oldIntelligence;
            cmd.Parameters["@NewIntelligence"].Value = intelligence;
            cmd.Parameters["@PointsRemaining"].Value = pointsRemaining;

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
                    throw new ApplicationException("Stat reset failed.");
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
