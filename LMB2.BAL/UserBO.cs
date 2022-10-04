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

        #region Ctors..
        // Constructor's
        public UserBO()
        {
            dc = new LMDBDataContext();
        }
        #endregion

        #region Get Methods
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

        public User GetUser2(int userId)
        {
            //var Query = from u in dc.Users
            //            where u.UserId == userId
            //            select u;

            //return Query.SingleOrDefault();

            return (
                from u in dc.Users
                join l in dc.LeaveApplications on u.UserId equals l.UserId
                where u.UserId == userId
                select u
                ).SingleOrDefault();


        }

        // write a method that retrieve a user based on username
        public List<User> GetUser3(string userName)
        {
            return dc.Users.Where(c => c.Username == userName).ToList();
        }

        public List<User> GetUsersBySearch()
        {
            // TODO
            return new List<User>();
        }
        #endregion

        #region Insert/Update Methods
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
        #endregion

        #region Delete Methods
        public void Delete(int userId)
        {
            var delUser = dc.Users.SingleOrDefault(u => u.UserId == userId);

            if (delUser == null) return;

            dc.Users.DeleteOnSubmit(delUser);
            dc.SubmitChanges();
        }

        public void FakeDelete(int userId)
        {
            var delUser = dc.Users.SingleOrDefault(u => u.UserId == userId);

            if (delUser == null) return;

            delUser.IsDeleted = true;
            dc.SubmitChanges();
        }
        #endregion

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