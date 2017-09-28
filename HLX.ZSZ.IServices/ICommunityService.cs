using HLX.ZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLX.ZSZ.IServices
{
    public interface ICommunityService : IServiceSupport
    {
        //获取区域regionId下的所有小区
        CommunityDTO[] GetByRegionId(long regionId);
    }

}
