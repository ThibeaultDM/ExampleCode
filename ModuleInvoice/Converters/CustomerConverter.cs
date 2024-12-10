using ModuleInvoice.Models.Response;
using System.Globalization;
using System.Text;
using System.Windows.Data;

[ValueConversion(typeof(CustomerDetailResponse), typeof(string))]
public class CustomerConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CustomerDetailResponse customerDetailResponse)
        {
            var builder = new StringBuilder();

            // Add basic customer details
            builder.AppendLine($"First Name: {customerDetailResponse.FirstName}");
            builder.AppendLine($"Family Name: {customerDetailResponse.FamilyName}");
            builder.AppendLine($"Gender: {customerDetailResponse.Gender}");
            builder.AppendLine($"Is Active: {customerDetailResponse.IsActive}");

            // Add company details
            if (customerDetailResponse.Company != null)
            {
                builder.AppendLine($"Company ID: {customerDetailResponse.Company.Id}");
                builder.AppendLine($"Company Name: {customerDetailResponse.Company.PublicName}");
                builder.AppendLine($"Company Active: {customerDetailResponse.Company.IsActive}");

                if (customerDetailResponse.Company.Addresses != null)
                {
                    builder.AppendLine("Company Addresses:");
                    foreach (var address in customerDetailResponse.Company.Addresses)
                    {
                        builder.AppendLine(FormatAddress(address));
                    }
                }
            }

            // Add addresses
            if (customerDetailResponse.Addresses != null)
            {
                builder.AppendLine("Addresses:");
                foreach (var address in customerDetailResponse.Addresses)
                {
                    builder.AppendLine(FormatAddress(address));
                }
            }

            // Add credit info
            if (customerDetailResponse.CreditInfo != null)
            {
                builder.AppendLine($"Credit ID: {customerDetailResponse.CreditInfo.Id}");
                builder.AppendLine($"To Spend: {customerDetailResponse.CreditInfo.ToSpend:C}");
            }

            return builder.ToString();
        }

        // Return empty string for invalid input
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string inputString)
        {
            try
            {
                var lines = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                var customer = new CustomerDetailResponse();

                foreach (var line in lines)
                {
                    var parts = line.Split(new[] { ':' }, 2, StringSplitOptions.TrimEntries);
                    if (parts.Length < 2) continue;

                    var key = parts[0];
                    var val = parts[1];

                    switch (key)
                    {
                        case "First Name":
                            customer.FirstName = val;
                            break;

                        case "Family Name":
                            customer.FamilyName = val;
                            break;

                        case "Gender":
                            customer.Gender = val;
                            break;

                        case "Is Active":
                            customer.IsActive = bool.Parse(val);
                            break;

                        case "Company ID":
                            customer.Company ??= new CompanyResponse();
                            customer.Company.Id = Guid.Parse(val);
                            break;

                        case "Company Name":
                            customer.Company ??= new CompanyResponse();
                            customer.Company.PublicName = val;
                            break;

                        case "Company Active":
                            customer.Company ??= new CompanyResponse();
                            customer.Company.IsActive = bool.Parse(val);
                            break;

                        case "Credit ID":
                            customer.CreditInfo ??= new CreditResponse();
                            customer.CreditInfo.Id = Guid.Parse(val);
                            break;

                        case "To Spend":
                            customer.CreditInfo ??= new CreditResponse();
                            customer.CreditInfo.ToSpend = decimal.Parse(val, culture);
                            break;

                        default:
                            // Handle nested collections like Addresses or Company Addresses
                            if (key.StartsWith("- ID"))
                            {
                                var addressParts = ParseAddress(line);
                                if (line.Contains("Company Addresses"))
                                {
                                    customer.Company ??= new CompanyResponse();
                                    customer.Company.Addresses ??= new List<AddressResponse>();
                                    customer.Company.Addresses.Add(addressParts);
                                }
                                else
                                {
                                    customer.Addresses ??= new List<AddressResponse>();
                                    customer.Addresses.Add(addressParts);
                                }
                            }
                            break;
                    }
                }

                return customer;
            }
            catch (Exception)
            {
                // Return null or throw an exception if parsing fails
                return null;
            }
        }

        return null;
    }

    private AddressResponse ParseAddress(string line)
    {
        var address = new AddressResponse();
        var parts = line.Split(new[] { ',' }, StringSplitOptions.TrimEntries);

        foreach (var part in parts)
        {
            var keyVal = part.Split(new[] { ':' }, 2, StringSplitOptions.TrimEntries);
            if (keyVal.Length < 2) continue;

            var key = keyVal[0].Trim('-').Trim();
            var val = keyVal[1];

            switch (key)
            {
                case "ID":
                    address.Id = Guid.Parse(val);
                    break;

                case "City":
                    address.City = val;
                    break;

                case "Postcode":
                    address.Postcode = int.Parse(val);
                    break;

                case "Street":
                    var streetParts = val.Split(' ', StringSplitOptions.TrimEntries);
                    address.StreetName = string.Join(' ', streetParts.Take(streetParts.Length - 1));
                    address.Number = int.Parse(streetParts.Last());
                    break;

                case "Is Default":
                    address.IsDefault = bool.Parse(val);
                    break;
            }
        }

        return address;
    }

    private string FormatAddress(AddressResponse address)
    {
        return $"- ID: {address.Id}, City: {address.City}, Postcode: {address.Postcode}, " +
               $"Street: {address.StreetName} {address.Number}, Is Default: {address.IsDefault}";
    }
}