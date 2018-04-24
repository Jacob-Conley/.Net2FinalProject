using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Security.Cryptography;

namespace LogicLayer
{
    public class UserManager
    {
        public int CreateAccount(string username, string email, string password)
        {
            int results = 0;

            var passwordHash = HashSha256(password);

            try
            {
                results = PlayerAccessor.CreateUser(username, passwordHash, email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return results;

        }

        public User AuthenticateUser(string username, string password)
        {
            User user = null; // user token to build

            // we need to hash the password first
            var passwordHash = HashSha256(password);

            try
            {

                var validationResults = PlayerAccessor.VerifyUserNameAndPassword(username, passwordHash);

                if (validationResults == 1) // user is validated
                {

                    user = PlayerAccessor.RetrieveUserByUsername(username);

                }
                else // User was not validated
                {
                    // we can throw an exception here.
                    throw new ApplicationException("Login failed. Bad username or password");
                }
            }
            catch (ApplicationException) // rethrow the application exception?
            {
                throw;
            }
            catch (Exception ex) // wrap and throw other types of exceptions 
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return user;
        }

        // Function to apply a SHA256 hash algorithm 
        // to a password to store or compare with the 
        // user's passwordHash in the database
        private string HashSha256(string source)
        {
            var result = "";

            // create a byte array
            byte[] data;

            // create a .NET Hash provider object
            using (SHA256 sha256hash = SHA256.Create())
            {
                // hash the input
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));

            }


            // now to build the result string
            var s = new StringBuilder();
            // loop through the byte array creating letters
            // to add to the StringBuilder
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            // get a string from the stringbuilder
            result = s.ToString();

            return result;
        }

        public User UpdateAccount(User user, string oldEmail, string newEmail,  string oldPasswordHash, string newPassword)
        {
            User newUser = null;
            int rowsAffected = 0;
            
            string newPasswordHash = HashSha256(newPassword);

            // try to invoke the access method
            try
            {
                rowsAffected = PlayerAccessor.UpdateAccount(user.GameUserID, oldEmail, newEmail, oldPasswordHash, newPasswordHash);
                if (rowsAffected == 1)
                {
                    newUser = user;
                }
                else
                {
                    throw new ApplicationException("Update returned 0 rows affected.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Account update failed.", ex);
            }

            return newUser;
        }

        public User UpdateAccountSamePassword(User user, string oldEmail, string newEmail, string oldPasswordHash)
        {
            User newUser = null;
            int rowsAffected = 0;
            

            // try to invoke the access method
            try
            {
                rowsAffected = PlayerAccessor.UpdateAccount(user.GameUserID, oldEmail, newEmail, oldPasswordHash, oldPasswordHash);
                if (rowsAffected == 1)
                {
                    newUser = user;
                }
                else
                {
                    throw new ApplicationException("Update returned 0 rows affected.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Account update failed.", ex);
            }

            return newUser;
        }

        public User UpgradeAccount(User user, string oldEmail, string newEmail, string oldPasswordHash, string newPassword)
        {
            User newUser = null;
            int rowsAffected = 0;
           
            string newPasswordHash = HashSha256(newPassword);

            // try to invoke the access method
            try
            {
                rowsAffected = PlayerAccessor.UpgradeAccount(user.GameUserID, oldEmail, newEmail, oldPasswordHash, newPasswordHash);
                if (rowsAffected == 1)
                {
                    newUser = user;
                }
                else
                {
                    throw new ApplicationException("Update returned 0 rows affected.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Account upgrade failed.", ex);
            }

            return newUser;
        }

        public User UpgradeAccountSamePassword(User user, string oldEmail, string newEmail, string oldPasswordHash)
        {
            User newUser = null;
            int rowsAffected = 0;
            

            // try to invoke the access method
            try
            {
                rowsAffected = PlayerAccessor.UpgradeAccount(user.GameUserID, oldEmail, newEmail, oldPasswordHash, oldPasswordHash);
                if (rowsAffected == 1)
                {
                    newUser = user;
                }
                else
                {
                    throw new ApplicationException("Update returned 0 rows affected.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Account upgrade failed.", ex);
            }

            return newUser;
        }

        public int DeleteAccount(int gameUserID)
        {
            int results = 0;

            try
            {
                results = PlayerAccessor.DeleteAccount(gameUserID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return results;
        }
    }
}
