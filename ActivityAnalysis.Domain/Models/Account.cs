using System.Collections.Generic;

namespace ActivityAnalysis.Domain.Models
{
    public class Account
    {
        public User AccountHolder { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}