using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestIservice;

namespace TestService
{
    public class UserService : IUserService
    {
        public INewsService NewService { get; set; }
        public bool CheckLogin(string userName, string pwd)
        {
            NewService.AddNews("fd", "fdf");
            return true;
        }
    }
}
