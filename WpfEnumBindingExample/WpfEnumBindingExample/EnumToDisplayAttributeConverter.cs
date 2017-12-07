using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfEnumBindingExample {
    public class EnumToDisplayAttributeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            if (!value.GetType().IsEnum) {
                throw new ArgumentException("Value must be an enumeration type");
            }

            var fieldInfo = value.GetType().GetField(value.ToString());
            var array = fieldInfo.GetCustomAttributes(false);

            foreach(var attrib in array) {
                if(attrib is DisplayAttribute) {
                    var displayAttrib = attrib as DisplayAttribute;

                    if(displayAttrib.ResourceType == null) { return displayAttrib.Name; }

                    ResourceManager resourceManager = new ResourceManager(
                        displayAttrib.ResourceType.FullName,
                        displayAttrib.ResourceType.Assembly);
                    var entry = resourceManager.GetResourceSet(
                        System.Threading.Thread.CurrentThread.CurrentCulture, true, true)
                        .OfType<DictionaryEntry>()
                        .FirstOrDefault(p => p.Key.ToString() == displayAttrib.Name);

                    var key = entry.Value.ToString();
                    return key;
                }
            }

            string name = Enum.GetName(value.GetType(), value);
            return name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
