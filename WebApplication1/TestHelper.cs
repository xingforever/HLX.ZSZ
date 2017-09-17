using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestIservice;
using System.Web.Mvc;

namespace WebApplication1
{
    public class TestHelper
    {
      

        public   static void Test()
        {

            IUserService svc = DependencyResolver.Current.GetService<IUserService>();
            bool b=    svc.CheckLogin("1", "1");

        }
    }
}