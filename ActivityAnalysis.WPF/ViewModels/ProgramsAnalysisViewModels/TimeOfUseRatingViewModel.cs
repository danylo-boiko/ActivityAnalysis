using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ActivityAnalysis.Domain.Models;
using ActivityAnalysis.WPF.Helpers;

namespace ActivityAnalysis.WPF.ViewModels.ProgramsAnalysisViewModels
{
    public class TimeOfUseRatingViewModel : ViewModelBase
    {
        public ObservableCollection<TimeOfUseRatingItemViewModel> Programs { get; }

        public TimeOfUseRatingViewModel(ICollection<Activity> activities)
        {
            Programs = GetProgramViewModels(activities);
        }
        
        private ObservableCollection<TimeOfUseRatingItemViewModel> GetProgramViewModels(ICollection<Activity> activities)
        {
            ObservableCollection<TimeOfUseRatingItemViewModel> resultCollection = new ObservableCollection<TimeOfUseRatingItemViewModel>();
            TimeSpan totalTime = new TimeSpan(activities.Sum(a => a.TimeSpent.Ticks));
            
            foreach (string programTitle in activities.Select(a => a.ProgramTitle).Distinct())
            {
                TimeSpan timeSpent = new TimeSpan(activities.Where(a => a.ProgramTitle == programTitle).Sum(a => a.TimeSpent.Ticks));
                resultCollection.Add(
                    new TimeOfUseRatingItemViewModel
                    {
                        ProgramTitle = programTitle,
                        TimeSpent = timeSpent,
                        PercentageOfTotal = timeSpent / totalTime * 100
                    }
                );
            }

            resultCollection.Sort((p1, p2) => p2.TimeSpent.CompareTo(p1.TimeSpent));
            return resultCollection;
        }
    }
}