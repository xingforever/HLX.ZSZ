using Autofac;
using CaptchaGen;
using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Filters.Watermarks;
using HLX.ZSZ.CommonMVC;
using log4net;
using MyBLLImpl;
using MyIBll;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZSZTest
{
    class Program
    {
        static void Main(string[] args)
        {

            // Log4NET();

            //Console.WriteLine("ok");

            //Console.ReadKey();
            //try
            //{
            //    Quartz();
            //    SqlConnection conn = new SqlConnection();
            //    conn.Open();//出错
            //}
            //catch (Exception)
            //{

            //    Log4NET();
            //}

            //AutoFac();


            SendMsg2();
            Console.WriteLine("ok");
            Console.ReadKey();


        }

        //发送邮件
        static void SendMail() {

            using (MailMessage mailMessage = new MailMessage())
            using (SmtpClient smtpClient = new SmtpClient("smtp.qq.com"))
            {
                mailMessage.To.Add("2945873057@qq.com");             
                mailMessage.Body = "HLX，学习NET,Come On ";
                mailMessage.From = new MailAddress("1003863495@qq.com");
                mailMessage.Subject = "HLX,ComeOn";
                smtpClient.EnableSsl = true;//如果邮箱需要开启SSL访问
                smtpClient.Credentials =
                    new System.Net.NetworkCredential("1003863495@qq.com", "tlufpojrqetqbfij");//如果启用了“客户端授权码”，要用授权码代替密码
                smtpClient.Send(mailMessage);
            }
        }

        //缩略图一
        static void Thumbnail() {
            ImageProcessingJob job = new ImageProcessingJob();
            job.Filters.Add(new FixedResizeConstraint(200, 200));
            job.SaveProcessedImageToFileSystem(@"E:\Program\NET高级培训\NET掌上租\TEST\Image\1.jpg", @"E:\Program\NET高级培训\NET掌上租\TEST\Image\2.jpg",
                new JpegFormatEncoderParams());

            //job.SaveProcessedImageToStream();


         
        }
        //水印
        //CodeCarvings.Piczard
        static void ImgWaterMark() {
            ImageWatermark imgWatermark = new ImageWatermark(@"E:\Program\NET高级培训\NET掌上租\TEST\Image\bg.jpg");
            imgWatermark.ContentAlignment = System.Drawing.ContentAlignment.BottomRight;//水印位置
            imgWatermark.Alpha = 50;//透明度，需要水印图片是背景透明的png图片
            ImageProcessingJob jobNormal = new ImageProcessingJob();
            jobNormal.Filters.Add(imgWatermark);//添加水印
            jobNormal.Filters.Add(new FixedResizeConstraint(300, 300));//限制图片的大小，避免生成大图。如果想原图大小处理，就不用加这个Filter
            jobNormal.SaveProcessedImageToFileSystem(@"E:\Program\NET高级培训\NET掌上租\TEST\Image\1.jpg", @"E:\Program\NET高级培训\NET掌上租\TEST\Image\3.jpg");

            
        }

        //生成图片验证码
        //安装 CaptchaGen
        static void ImageFactorys() {
            using (MemoryStream ms =
              ImageFactory.GenerateImage("a123fdasfa32", 80, 150,
              30, 10))
            using (FileStream fs = File.OpenWrite(@"E:\Program\NET高级培训\NET掌上租\TEST\Image\4.jpg"))
            {
                ms.CopyTo(fs);
            }
           

        }

        //Log4NET 使用
        static void Log4NET() {

            log4net.Config.XmlConfigurator.Configure();
            log4net.Config.XmlConfigurator.Configure();
            //传入类名
            ILog log = LogManager.GetLogger(typeof(Program));
            //log.Debug("飞行高度10000米");
            //log.Warn("油压不足");
            //log.Error("引擎失灵");
            //性能 优化
            //log.DebugFormat("当前数据有问题,{0}", "aaaaaaaa");
            //log.Debug("当前数据有问题,"+ "aaaaaaaa");
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.Open();
            }
            catch (Exception ex)
            {
                log.Error("连接数据库失败", ex);
            }
            

        }

        //Quartz 使用
        static void Quartz()
        {
            //如果出了异常 不会报错 所以用TryCatch

            IScheduler sched = new StdSchedulerFactory().GetScheduler();
            JobDetailImpl jdBossReport = new JobDetailImpl("jdTest", typeof(TestJob));
            //IMutableTrigger triggerBossReport = CronScheduleBuilder.DailyAtHourAndMinute(19, 38).Build();//每天23.45
            var bulider= CalendarIntervalScheduleBuilder.Create();
            bulider.WithInterval(3, IntervalUnit.Second);
            IMutableTrigger triggerBossReport=    bulider.Build();
            triggerBossReport.Key = new TriggerKey("triggerTest");
            sched.ScheduleJob(jdBossReport, triggerBossReport);
            sched.Start();
            Console.WriteLine("ok"); ;
            Console.ReadKey();
        }

        //使用ICO  AutoFac
        static  void AutoFac()
        {
            ContainerBuilder bulider = new ContainerBuilder();
          //bulider.RegisterType<UserBll>().AsImplementedInterfaces();//
          //  bulider.RegisterType<DogBll>().AsImplementedInterfaces();//

            Assembly asm = Assembly.Load("MyBLLImpl");//加载这个类库中类
            bulider.RegisterAssemblyTypes(asm).AsImplementedInterfaces();//

            var container = bulider.Build();
          IUserBll bll= container.Resolve<IUserBll>();
            bll.Addone("aaa", "23");


            IDogBll dogbll = container.Resolve<IDogBll>();
            dogbll.Bark();

        }
        //模拟发短信 利用如鹏网模拟短信平台 http://sms.rupeng.cn/doc.html
        //http://sms.rupeng.cn/Index.aspx
        static void SendMsg()
        {
            string userName = "hlxForNext";
            string appKey = "4643878b073baa968bd870";
            string templateId = "596";
            string code = "12356";
            string phoneNum = "15779708281";

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string url=  wc.DownloadString("http://sms.rupeng.cn/SendSms.ashx?userName="+Uri.EscapeDataString(userName)+"&appKey="
                +Uri.EscapeDataString(appKey)+ "&templateId="+ templateId+"&code="+Uri.EscapeDataString(code)
                +"&phoneNum="+phoneNum);
        
            string resp = wc.DownloadString(url);
            Console.WriteLine(resp);
        }

        static  void SendMsg2()
        {
            string userName = "hlxForNext";
            string appKey = "4643878b073baa968bd870";
            string templateId = "596";
            string code = "12356";
            string phoneNum = "15779708281";
            RuPengSMSSender  sender= new RuPengSMSSender();
            sender.UserName = userName;
            sender.AppKey = appKey;
            var result = sender.SendSMS(templateId, code, phoneNum);
            Console.WriteLine("返回码："+result.code+"消息"+result.msg);
        }
    }
}
