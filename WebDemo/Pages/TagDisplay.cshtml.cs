using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebDemo.Pages
{
    public class TagDisplayModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public Domain.Invoice Invoice { get; set; } = new Domain.Invoice
        {
            Amount = 478.25,
            IsPaid = false,
            Name = "John Smith",
            StoreBranch = "Springfield - 8745",
            InvoiceStatus = Domain.InvoiceStatusEnum.Active
        };


        public TagDisplayModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}