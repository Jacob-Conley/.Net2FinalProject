using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class StatManager
    {
        public int CreateStats(int strength, int stamina, int dexterity, int intelligence)
        {
            int result = 0;

            try
            {
                result = StatAccessor.CreateStats(strength, stamina, dexterity, intelligence);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }

        public Stat RetrieveStats(int statID)
        {
            Stat stats = null;

            try
            {
                stats = StatAccessor.RetrieveStatsByStatID(statID);
            }
            catch (Exception)
            {

                throw;
            }

            return stats;
        }

        public int ResetStats(int statID, int strength, int stamina, int dexterity, int intelligence)
        {
            int results = 0;

            try
            {
                results = StatAccessor.ResetStats(statID, strength, stamina, dexterity, intelligence);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return results;
        }

        public int UpdateStats(int statID, int oldStrength, int strength, int oldStamina, int stamina, int oldDexterity,
            int dexterity, int oldIntelligence, int intelligence, int pointsRemaining)
        {
            int results = 0;

            try
            {
                results = StatAccessor.UpdateStats(statID, oldStrength, strength, oldStamina, stamina, oldDexterity,
                    dexterity, oldIntelligence, intelligence, pointsRemaining);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return results;
        }
    }
}
