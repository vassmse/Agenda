using AgendaContracts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AgendaFE.UI.Converters
{
    public class TaskStateConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = ((IEnumerable)value).Cast<object>().ToList();
            ArrayList collection = new ArrayList(result);
            ObservableCollection<TaskDto> retCollection = new ObservableCollection<TaskDto>();

            foreach(var task in collection)
            {
                if (((TaskDto)(task)).State == int.Parse(parameter.ToString()))
                    retCollection.Add(((TaskDto)(task)));
            }
            return retCollection;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //var result = ((IEnumerable)value).Cast<object>().ToList();
            //ArrayList collection = new ArrayList(result);
            //ObservableCollection<TaskDto> retCollection = new ObservableCollection<TaskDto>();

            //foreach (var task in collection)
            //{
            //    if (((TaskDto)(task)).State == int.Parse(parameter.ToString()))
            //        retCollection.Add(((TaskDto)(task)));
            //}
            return value;
        }
    }
}
