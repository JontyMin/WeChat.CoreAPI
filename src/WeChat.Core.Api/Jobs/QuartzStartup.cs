using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using WeChat.Core.Api.Log;

namespace WeChat.Core.Api.Jobs
{
    /// <summary>
    /// 定时任务
    /// </summary>
    public class QuartzStartup
    {
        private readonly ILoggerHelper _logger;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _iocJobfactory;
        private IScheduler _scheduler;
        public QuartzStartup(IJobFactory iocJobfactory, ILoggerHelper logger, ISchedulerFactory schedulerFactory)
        {
            this._logger = logger;
            //1、声明一个调度工厂
            this._schedulerFactory = schedulerFactory;
            this._iocJobfactory = iocJobfactory;
        }
        public async Task<string> Start()
        {

            //2、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            _scheduler.JobFactory = this._iocJobfactory;//  替换默认工厂
            #region 任务调度-已废弃

            /*
        
            //3、开启调度器
            await _scheduler.Start();
            
            
            
            //4、创建一个触发器
            var trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())//每2h执行一次
                .Build();
            //5、创建任务
            var jobDetail = JobBuilder.Create<TokenRefreshJob>()
                .WithIdentity("job", "group")
                .Build();
            
            
            
            //6、将触发器和任务器绑定到调度器中
            await _scheduler.ScheduleJob(jobDetail, trigger);

            
            //await CreateJob<StartLogDebugJob>("_StartLogDebugJob", "_StartLogDebugJob", " 0/5 * * * * ? ");


            return await Task.FromResult("将触发器和任务器绑定到调度器中完成");
            */
            #endregion


            await StartJob();


            return await Task.FromResult("将触发器和任务器绑定到调度器中完成");
        }


        public async Task StartJob()
        {
            
            // 开始调度器
            await _scheduler.Start();
            
            await CreateJob<TokenRefreshJob>("TokenRefreshJob", "TokenRefreshJob", 7200);
            
            await CreateJob<DynamicsDataJob>("DynamicsDataJob", "DynamicsDataJob", 360);

        }

        /// <summary>
        /// 创建运行的调度器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task CreateJob<T>(string name, string group, int time) where T : IJob
        {
            
            // 创建作业
            var job = JobBuilder.Create<T>()
                .WithIdentity($"name{name}", $"group{group}")
                .Build();

            // 创建触发器
            var trigger =TriggerBuilder.Create()
                .WithIdentity($"name{name}", $"group{group}")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(time).RepeatForever())
                .Build();

            // 放入调度器
            await _scheduler.ScheduleJob(job, trigger);
        }
        
        
        /// <summary>
        /// 停止调度器
        /// </summary>
        public void Stop()
        {
            if (_scheduler == null) return;

            if (_scheduler.Shutdown(true).Wait(30000))
            {
                _scheduler = null;
            }
            else
            {
            }
            _logger.Debug(nameof(Stop),"Schedule job upload as application stopped");
        }
    }
}