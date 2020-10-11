using System;
using Windows.UI.Xaml.Data;

namespace XChecklistReader.UI.Main.Converter {
    public class SpaceToNonbreakingSpaceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (value is string text) {
                return text.Replace(' ', System.Convert.ToChar(160));
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}