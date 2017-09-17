using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIservice
{
   public    interface IUserService
    {

        bool CheckLogin(string userName,string pwd);
    }
}
