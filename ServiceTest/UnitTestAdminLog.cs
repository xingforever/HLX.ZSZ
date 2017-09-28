using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service;

namespace ServiceTest
{
  public   class UnitTestAdminLog
    {
        [TestMethod]
        public void TestAddNew()
        {

            new AdminLogService().AddNew(1, "测试信息");

        }
    }
}
