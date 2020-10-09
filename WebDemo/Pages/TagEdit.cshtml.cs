using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebDemo.Pages
{
    public class TagEditModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public Domain.Invoice Invoice { get; set; } = new Domain.Invoice
        {
            Amount = 478.25,
            IsPaid = false,
            Name = "John Smith",
            StoreBranch = "Springfield - 8745",
            InvoiceStatus = Domain.InvoiceStatusEnum.Active,
            //IsOverDue = false
        };


        public TagEditModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}