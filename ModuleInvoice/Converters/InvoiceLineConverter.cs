using ModuleInvoice.Models.Input;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace ModuleInvoice.Converters
{
    [ValueConversion(typeof(CreateInvoiceLineInput), typeof(string))]
    public class InvoiceLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CreateInvoiceLineInput singleItem)
            {
                CreateInvoiceLineInput invoiceLine = (CreateInvoiceLineInput)value;
                return invoiceLine.ToString();
            }
            else if (value is IList list && list.Count > 0)
            {
                List<string> strings = new();
                foreach (var item in list)
                {
                    strings.Add(item.ToString());
                }

                return strings;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}