using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ActivityAnalysis.Domain.Models;
using Activity = ActivityAnalysis.Domain.Models.Activity;

namespace ActivityAnalysis.Domain.Services.ProcessService
{
    public class ProcessService : IProcessService
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(IntPtr ptr, out int pid);

        private readonly int[] _blockedSystemProcesses = {0, 4};
        private readonly IActivityService _activityService;
        private Account _loggedAccount;

        public ProcessService(IActivityService activityService)
        {
            _activityService = activityService;
        }

        public async Task Start(Account loggedAccount)
        {
            _loggedAccount = loggedAccount;
            while (_loggedAccount != null)
            {
                DateTime programStartTime = DateTime.Now;
                string programTitle = GetActiveWindowTitle();
                if (!string.IsNullOrWhiteSpace(programTitle))
                {
                    await Task.Delay(1000);
                    string dayOfUse = DateTime.Now.ToString("d");
                    TimeSpan spendTime = DateTime.Now - programStartTime;
                    Activity activity = new Activity(_loggedAccount.AccountHolder.Id, programTitle, dayOfUse, spendTime);
                    await AddOrUpdate(activity);
                }
            }
        }

        private string GetActiveWindowTitle()
        {
            GetWindowThreadProcessId(GetForegroundWindow(), out int windowId);

            if (Array.Exists(_blockedSystemProcesses, element => element == windowId))
            {
                return String.Empty;
            }

            Process currentProcess = Process.GetProcessById(windowId);
            return currentProcess.MainModule?.FileVersionInfo.FileDescription;
        }

        private async Task AddOrUpdate(Activity activity)
        {
            Activity activityContext = _loggedAccount.Activities.FirstOrDefault(a =>
                a.ProgramTitle.Equals(activity.ProgramTitle) && a.DayOfUse.Equals(activity.DayOfUse));
            if (activityContext == null)
            {
                _loggedAccount.Activities.Add(activity);
                await _activityService.Create(activity);
            }
            else
            {
                activityContext.TimeSpent += activity.TimeSpent;
                await _activityService.Update(activityContext);
            }
        }

        public void Stop()
        {
            _loggedAccount = null;
        }
    }
}