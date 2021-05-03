using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ActivityAnalysis.Domain.Models;
using ActivityAnalysis.WPF.Helpers;

namespace ActivityAnalysis.WPF.ViewModels.ProgramsAnalysisViewModels
{
    public class DaysOfUseRatingViewModel: ViewModelBase
    {
        public ObservableCollection<DaysOfUseRatingItemViewModel> Programs { get; }
        
        public DaysOfUseRatingViewModel(ICollection<Activity> activities)
        {
            Programs = GetProgramViewModels(activities);
        }
        
        private ObservableCollection<DaysOfUseRatingItemViewModel> GetProgramViewModels(ICollection<Activity> activities)
        {
            ObservableCollection<DaysOfUseRatingItemViewModel> resultCollection = new ObservableCollection<DaysOfUseRatingItemViewModel>();
            foreach (string program in activities.Select(a=>a.ProgramTitle).Distinct())
            {
                resultCollection.Add(
                    new DaysOfUseRatingItemViewModel
                    {
                        Title = program,
                        DaysOfUse =  activities.Where(a => a.ProgramTitle == program).Count()
                    }
                );
            }

            resultCollection.Sort((p1, p2) => p2.DaysOfUse.CompareTo(p1.DaysOfUse));

            return resultCollection;
        }
    }
}