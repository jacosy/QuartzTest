using Quartz;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuartzLearning.Quartz.Jobs
{
    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    public class HelloJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                JobKey key = context.JobDetail.Key;
                JobDataMap dataMap = context.JobDetail.JobDataMap;

                string lastExecutionTime = dataMap.GetString("LastExecutionTime");
                string userName = dataMap.GetString("UserName");
                
                Debug.WriteLine($"Instance {key}: Current Time is {now} and the last execution time is {lastExecutionTime} HelloJob is executing by {userName}.");

                // update JobDataMap by replacing the original job with the given name and group
                //dataMap["LastExecutionTime"] = now;
                dataMap.Put("LastExecutionTime", now);
            }
            catch (JobExecutionException ex)
            {
                Debug.WriteLine($"Error Message: {ex.Message}");
            }
        }
    }
}