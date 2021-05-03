using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ActivityAnalysis.WPF.Helpers
{
    public static class ObservableCollectionHelper
    {
        public static void Sort<T>(this ObservableCollection<T> collection, Comparison<T> comparison)
        {
            List<T> sortableList = new List<T>(collection);
            sortableList.Sort(comparison);
            
            for (int i = 0; i < sortableList.Count; i++)
            {
                collection.Move(collection.IndexOf(sortableList[i]), i);
            }
        }
    }
}