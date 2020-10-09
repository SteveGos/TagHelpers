using System.ComponentModel.DataAnnotations;
using WebDemo.Attributes;

namespace WebDemo.Domain
{
    public class Invoice
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} required.", AllowEmptyStrings = false)]
        [MaxLength(50, ErrorMessage = "The {0} can not have more than {1} characters")]
        public string Name { get; set; }

        [Display(Name = "Store Branch")]
        [Required(ErrorMessage = "{0} required.", AllowEmptyStrings = false)]
        [MaxLength(50, ErrorMessage = "The {0} can not have more than {1} characters")]
        public string StoreBranch { get; set; }

        [Display(Name = "Paid")]
        [Required(ErrorMessage = "{0} required.", AllowEmptyStrings = false)]
        public bool? IsPaid { get; set; }

        [Display(Name = "OverDue")]
        public bool? IsOverDue { get; set; }

        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [ComputedOptionally]
        public double Amount { get; set; }

        [Display(Name = "Tax")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Computed]
        public double Tax { get; set; }

        [Display(Name = "Status")]
        public InvoiceStatusEnum InvoiceStatus { get; set; }

        [Display(Name = "Payment Options")]
        public PaymentOptionsEnum? PaymentOptions { get; set; }
    }

    /// <summary>
    /// Status
    /// </summary>
    public enum InvoiceStatusEnum
    {
        /// <summary>
        /// Active
        /// </summary>
        [Display(Order = 0001, Name = "Active", ShortName = "Active", Description = "Active")]
        Active = 1,

        /// <summary>
        /// Past Due
        /// </summary>
        [Display(Order = 0002, Name = "Past Due", ShortName = "Past Due", Description = "Past Due")]
        PastDue = 2,

        /// <summary>
        /// Paid
        /// </summary>
        [Display(Order = 0003, Name = "Paid", ShortName = "Paid", Description = "Paid")]
        Paid = 3
    }

    /// <summary>
    /// Status
    /// </summary>
    public enum PaymentOptionsEnum
    {
        /// <summary>
        /// Cash
        /// </summary>
        [Display(Order = 100, Name = "Cash", ShortName = "Cash", Description = "Cash")]
        Cash = 100,

        /// <summary>
        /// Cash
        /// </summary>
        [Display(Order = 200, Name = "Card", ShortName = "Card", Description = "Card")]
        Card = 200,

        /// <summary>
        /// Cash
        /// </summary>
        [Display(Order = 300, Name = "Bill", ShortName = "Invoice", Description = "Invoice")]
        Bill = 300,
    }
}