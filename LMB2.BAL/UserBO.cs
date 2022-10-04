using LMB2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMB2.BAL
{
    public class UserBO
    {
        #region Private Members
        // Private Members
        private LMDBDataContext dc;
        #endregion

        // Public Properties

        // Constructor's
        public UserBO()
        {
            dc = new LMDBDataContext();
        }

        // Get Methods (Select queries)
        public List<User> GetUsers()
        {
            // SELECT * FROM Users
            return dc.Users.ToList();
        }

        public User GetUser(int userId)
        {
            // SELECT * FROM Users WHERE UserId = @userId
            return dc.Users.SingleOrDefault(u => u.UserId == userId);
        }

        public List<User> GetUsersBySearch()
        {
            // TODO
            return new List<User>();
        }

        // Insert Methods
        public int Save(User user)
        {
            // Check if the (user) is existed or not
            
            bool isInsert = false;
            var _user = dc.Users.SingleOrDefault(u => u.UserId == user.UserId);

            if (_user == null) // if the user is not exist in Db => insert
            {
                _user = new User();
                _user.CreatedOn = DateTime.UtcNow;
                _user.CreatedBy = user.CreatedBy;
                isInsert = true;
            }
            else // If the user exists in DB,      => update
            {
                _user.UpdatedOn = DateTime.UtcNow;
                _user.UpdatedBy = user.UpdatedBy;
            }

            _user.FirstName = user.FirstName;
            _user.LastName = user.LastName;
            _user.Email = user.Email;
            // etc

            if (isInsert == true)
                dc.Users.InsertOnSubmit(_user); // UserId

            dc.SubmitChanges();

            return _user.UserId;

        }


        // Delete Methods
    }
}

// LINQ Methods
// - Single
// - SingleOrDefault
// - First
// - FirstOrDefault
// - Last ..
// - Where
// - Select
// - Join
// - Max, Min, Average
// - OrderBy