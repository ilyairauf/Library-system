using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_sys
{
    internal class Users
    {
        private int userID = 10000;

        public List<int> bookdata = new List<int>();
        public int UserID { get { return userID; } }
        public string UserName { get; set; }
        public dynamic Password { get; set; }
        public string Fullname { get; set; }


        public Users()
        {
            userID++;

        }






    }
}
