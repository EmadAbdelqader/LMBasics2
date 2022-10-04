using LMB2.BAL;
using LMB2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMB2.TestingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserBO userBO = new UserBO();

            // Get all users
            //var users = userBO.GetUsers();

            // Get single used based on userId
            //var user = userBO.GetUser(5);

            // Insert user
            //User newUser = new User()
            //{
            //    
            //    FirstName = "Islam",
            //    LastName = "Mohammed",
            //    Email = "xx@xc.c"
            //};

            //userBO.Save(newUser);


            // Update User
            //var user = userBO.GetUser(12);

            //user.FirstName = "Emad";
            //userBO.Save(user);

            userBO.Delete(12);

        }
    }
}
