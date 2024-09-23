using InvoiceDataLayer;
using InvoiceDataLayer.DataModels;
using Moq;

namespace InvoiceTestLayer
{
    public class InvoiceUseCasesTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void CreateInvoiceHeader()
        {
            var mockDataAcess = new Mock<InvoiceHeaderRepository>();
            DO_InvoiceHeader header = new DO_InvoiceHeader();
            mockDataAcess.Setup(x => x.CreateInvoiceHeaderAsync(It.IsAny<DO_InvoiceHeader>()).Result).Returns(header);

            InvoiceHeaderRepository invoiceHeaderRepo = mockDataAcess.Object;
            invoiceHeaderRepo.CreateInvoiceHeaderAsync(new DO_InvoiceHeader());
            Assert.IsNotNull(invoiceHeaderRepo);
        }

        [Test]
        public async Task GetInvoiceHeaderById()
        {
            // do I put a header in the database?
            Guid id = System.Guid.Parse("a know guid");
            // do I use mock again
            var mockDataAcess = new Mock<InvoiceHeaderRepository>();
            InvoiceHeaderRepository invoiceHeaderRepository = mockDataAcess.Object;

            DO_InvoiceHeader invoiceHeader = await invoiceHeaderRepository.GetInvoiceHeaderAsync(id);
            Assert.IsNotNull(invoiceHeader);
        }

        //[Test]
        //public async Task TestUpdateInvoiceHeader()

        //{
        //	Guid Id = System.Guid.Parse("GUID ID");

        //	InvoiceHeaderRepository invoiceHeaderRepository = new InvoiceHeaderRepository();

        //	DO_InvoiceHeader invoiceHeader = await invoiceHeaderRepository.GetInvoiceHeaderAsync(Id);

        //	DateTime? updateDT = invoiceHeader.UpdatedOn;

        //	invoiceHeaderRepository.UpdateInvoiceHeaderAsync(invoiceHeader);

        //	invoiceHeader = await invoiceHeaderRepository.GetInvoiceHeaderAsync(Id);

        //	DateTime? updateDT2 = invoiceHeader.UpdatedOn;

        //	Assert.AreNotEqual(updateDT, updateDT2);

        //}
    }
}