using MyIBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBLLImpl
{
    public  class UserBll : IUserBll
    {
        public void Addone(string username, string pwd)
        {
            Console.WriteLine("新增加了"+username);
        }

        public bool Check(string username, string pwd)
        {
            return true;
        }
    }
}
