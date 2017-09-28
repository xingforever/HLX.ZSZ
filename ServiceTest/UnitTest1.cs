using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZSZ.Service;
using ZSZ.Service.Entities;
using HLX.ZSZ.DTO;

namespace ServiceTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            new AdminLogService().AddNew(3, "测试信息");
        }

        //public AdminLogDTO GetById(long id)
        //{
        //    using (ZSZDbContext ctx=new ZSZDbContext ())
        //    {
        //        BaseService<AdminLogEntity> bs = new ZSZ.Service.BaseService<AdminLogEntity>(ctx);
        //      var log=  bs.GetById(id);
        //        if (log==null )
        //        {
        //            return null;
        //        }
        //        AdminLogDTO dto = new AdminLogDTO();
        //        dto.AdminUserId = log.AdminUserId;
        //        dto.AdminUserName = log.AdminUser.Name;
        //    }

        //}
    }
}
