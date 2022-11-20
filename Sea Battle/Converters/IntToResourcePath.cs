using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Sea_Battle.Converters
{
    public class IntToResourcePath : IValueConverter
    {
        //We can have these states for each slot:
        //0 - empty slot
        //1 - slot with ship
        //2 - hit slot with ship
        //3 - hit empty slot
        //4 - fully destroyed ship tile
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 0:
                    return "/Resources/empty.png";
                case 1:
                    return "/Resources/ship.png";
                case 2:
                    return "/Resources/ship_hit.png";
                case 3:
                    return "/Resources/empty_hit.png";
                case 4:
                    return "/Resources/ship_destr.png";
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
