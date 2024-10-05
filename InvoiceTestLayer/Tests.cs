using InvoiceDataLayer;
using InvoiceDataLayer.DataModels;
using InvoiceDataLayer.Interfaces;
using Moq;

namespace InvoiceTestLayer
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateInvoiceHeader()
        {
            DO_InvoiceHeader invoiceHeader = new();
            var mockDataAccess = new Mock<IInvoiceHeaderRepository>();
            mockDataAccess.Setup(m => m.CreateInvoiceHeaderAsync(invoiceHeader)).ReturnsAsync(new DO_InvoiceHeader());
            var invoiceHeaderRepo = mockDataAccess.Object;

            invoiceHeader = invoiceHeaderRepo.CreateInvoiceHeaderAsync(invoiceHeader).Result;
            Assert.NotNull(invoiceHeader);
        }

        [Test]
        public void GetInvoiceHeaderById()
        {
            Guid id = System.Guid.Parse("6ED4F1C3-8348-4090-CA2B-08DB4FC383E9");
            var mockDataAccess = new Mock<IInvoiceHeaderRepository>();
            mockDataAccess.Setup(m => m.GetInvoiceHeaderAsync(id)).ReturnsAsync(new DO_InvoiceHeader() { Id = id });
            var invoiceHeaderRepo = mockDataAccess.Object;

            DO_InvoiceHeader invoiceHeader = invoiceHeaderRepo.GetInvoiceHeaderAsync(id).Result;
            Assert.NotNull(invoiceHeader);
        }

        [Test]
        public void UpdateInvoiceHeader()
        {
            Guid id = System.Guid.Parse("6ED4F1C3-8348-4090-CA2B-08DB4FC383E9");
            DateTime originalDate = new(2023, 10, 1);
            DateTime updatedDate = new(2023, 10, 2);

            var mockDataAccess = new Mock<IInvoiceHeaderRepository>();
            mockDataAccess.Setup(m => m.GetInvoiceHeaderAsync(id))
                          .ReturnsAsync(new DO_InvoiceHeader() { Id = id, UpdatedOn = originalDate });

            var invoiceHeaderRepo = mockDataAccess.Object;

            DO_InvoiceHeader invoiceHeader = invoiceHeaderRepo.GetInvoiceHeaderAsync(id).Result;
            invoiceHeader.UpdatedOn = updatedDate;
            mockDataAccess.Setup(m => m.UpdateInvoiceHeaderAsync(invoiceHeader))
                          .ReturnsAsync(new DO_InvoiceHeader() { Id = id, UpdatedOn = updatedDate });

            invoiceHeader = invoiceHeaderRepo.UpdateInvoiceHeaderAsync(invoiceHeader).Result;

            Assert.AreEqual(updatedDate, invoiceHeader.UpdatedOn);
            Assert.AreNotEqual(originalDate, invoiceHeader.UpdatedOn);
        }
    }
}