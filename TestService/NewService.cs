using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestIservice;

namespace TestService
{
    public class NewService : INewsService
    {
        public void AddNews(string title, string body)
        {
            title = "dfdfd";
        }
    }
}
