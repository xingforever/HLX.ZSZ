using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZTest
{
    class TestJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("执行当前任务了"+DateTime.Now);
        }
    }
}
