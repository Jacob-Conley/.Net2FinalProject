using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class CharacterManager
    {
        public List<Character> RetrieveCharacterList(int gameUserID)
        {
            List<Character> characterList = null;

            try
            {
                characterList = UserAccessor.RetrieveCharactersByGameUserID(gameUserID);
            }
            catch (Exception)
            {

                throw;
            }

            return characterList;
        }

        public int CreateCharacter(int gameUserID, int fileNumber, string name, string race, string playerClass,
                                    string image)
        {
            int result = 0;

            try
            {
                result = UserAccessor.CreateCharacter(gameUserID, fileNumber, name, race, playerClass, image);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return result;
        }

        public int UpdateCharacter(int playerCharacterID, string oldClass, string newClass)
        {
            int results = 0;

            try
            {
                results = UserAccessor.UpdateCharacter(playerCharacterID, oldClass, newClass);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return results;
        }

        public int DeleteCharacter(int playerCharacterID)
        {
            int results = 0;

            try
            {
                results = UserAccessor.DeleteCharacter(playerCharacterID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return results;
        }
    }
}
