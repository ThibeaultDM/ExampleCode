using NewInvoiceBusinessLayer.Enums;

namespace NewInvoiceBusinessLayer.Objects
{
    public class BO_InvoiceException
    {
        public Guid Id { get; set; }

        public InvoiceExceptionTypes Type { get; set; }

        public string NameSpace { get; set; }

        public string Message { get; set; }

        public string InputParameters { get; set; }
    }
}