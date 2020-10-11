using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebDemo.Pages
{
    public class IndexModel : PageModel
    {
        [Display(Name = "Boolean")]
        public bool BoolValue { get; set; } = true;

        [Display(Name = "Page Link")]
        public string PageLink { get; set; }

        public Domain.Invoice Invoice { get; set; } = new Domain.Invoice
        {
            Amount = 478.25,
            IsPaid = true,
            Name = "John Smith",
            StoreBranch = "Springfield - 8745",
            InvoiceStatus = Domain.InvoiceStatusEnum.Active,
            //IsOverDue = false
        };

        public IndexModel()
        {
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            var retValue_ = TryUpdateModelAsync(Invoice, "Invoice",
                                s => s.Name,
                                s => s.StoreBranch,
                                s => s.Tax,
                                s => s.InvoiceStatus,
                                s => s.PaymentOptions,
                                s => s.IsPaid,
                                s => s.IsOverDue
                            ).Result;

            ModelState.AddModelError(string.Empty, "Demo - Stop Post");
        }
    }
}