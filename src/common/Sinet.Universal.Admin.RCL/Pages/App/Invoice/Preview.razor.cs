using Sinet.Universal.Admin.RCL.Data.App.Invoice;
using Sinet.Universal.Admin.RCL.Data.App.Invoice.Dto;

namespace Sinet.Universal.Admin.RCL.Pages.App.Invoice
{
    public partial class Preview
    {
        private bool? _showSendInvoice;
        private bool? _showAddPayment;
        private InvoiceRecordDto? _invoiceRecord;

        [Parameter]
        public int? Id { get; set; }

        public InvoiceRecordDto InvoiceRecord => _invoiceRecord ??= InvoiceService.GetInvoiceRecordList().FirstOrDefault(i => i.Id == Id) ?? InvoiceService.GetInvoiceRecordList().First();

        private void NavigateToEdit()
        {
            NavigationManager.NavigateTo($"apps/invoice/edit/{Id ?? 0}");
        }
    }
}
