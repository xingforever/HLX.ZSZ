
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIBll
{
   public  interface IUserBll
    {
        bool Check(string username, string pwd);
        void Addone(string username, string pwd);

    }
}
