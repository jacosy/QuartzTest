using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuartzLearning.Quartz.Jobs;

namespace QuartzLearning.Quartz
{
    public class SchedulerFactory
    {
        public static void CreateSchedule()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
               .UsingJobData("LastExecutionTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
               .UsingJobData("UserName", "Longoria")
               .WithIdentity("myJob", "group1")
               .Build();

            // Trigger the job to run now, and then every 5 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("myTrigger", "group1")
              //.ForJob(job)
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(5)
                  .RepeatForever())
              .Build();

            sched.ScheduleJob(job, trigger);
        }
    }
}