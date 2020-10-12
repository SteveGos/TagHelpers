using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebDemo.Domain;

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
            StoreBranch = "McCall - 5",
            InvoiceStatus = Domain.InvoiceStatusEnum.Active,
            //IsOverDue = false
        };

        public List<SelectListItem> StoreBranches { get; set; }
        public List<SelectListItem> InvoiceStatusList { get; set; }
        public List<SelectListItem> PaymentOptionsList { get; set; }

        public IndexModel()
        {
            StoreBranches = new List<SelectListItem>
            {
                new SelectListItem{ Value = "Springfield - 1", Text = "Springfield - 1"},
                new SelectListItem{ Value = "Meridian - 2", Text = "Meridian - 2"},
                new SelectListItem{ Value = "Boise - 3", Text = "Boise - 3"},
                new SelectListItem{ Value = "Nampa - 4", Text = "Nampa - 4"},
                new SelectListItem{ Value = "McCall - 5", Text = "McCall - 5"},
            };
        }

        public void OnGet()
        {
            InvoiceStatusList = GetEnumSelect(type: typeof(InvoiceStatusEnum), isNullable: false, value: (int)Invoice.InvoiceStatus).ToList();
            PaymentOptionsList = GetEnumSelect(type: typeof(PaymentOptionsEnum), isNullable: false, value: (int?)Invoice.PaymentOptions).ToList();
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


            InvoiceStatusList = GetEnumSelect(type: typeof(InvoiceStatusEnum), isNullable: false, value: (int)Invoice.InvoiceStatus).ToList();
            PaymentOptionsList = GetEnumSelect(type: typeof(PaymentOptionsEnum), isNullable: false, value: (int?)Invoice.PaymentOptions).ToList();

            ModelState.AddModelError(string.Empty, "Demo - Stop Post");
        }

        private IEnumerable<SelectListItem> GetEnumSelect(Type type, bool isNullable, int? value)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            var coll = new List<EnumerationData>();

            var fields = type.GetFields().ToList();

            var enumArr = Enum.GetValues(type);

            if (enumArr.Length <= 0)
            {
                return retVal;
            }

            int? maxOrder = null;

            foreach (var item_ in enumArr)
            {
                var itm1_ = item_;
                var fi_ = fields.FirstOrDefault(o => o.Name == itm1_.ToString());

                var enumData = new EnumerationData
                {
                    Id = (int)fi_.GetRawConstantValue(),
                    Code = item_.ToString(),
                    Name = GetEnumAttributeValueString<DisplayAttribute>(item_ as Enum, y => y.Name),
                };

                if (string.IsNullOrWhiteSpace(enumData.Name))
                {
                    enumData.Name = item_.ToString();
                }

                enumData.Order = GetEnumAttributeValueIntegerNullable<DisplayAttribute>(item_ as Enum, y => y.GetOrder());

                if (enumData.Order.HasValue)
                {
                    if (maxOrder.HasValue)
                    {
                        maxOrder = maxOrder.Value < enumData.Order.Value ? enumData.Order.Value : maxOrder.Value;
                    }
                    else
                    {
                        maxOrder = enumData.Order.Value;
                    }
                }

                coll.Add(enumData);
            }

            foreach (var item_ in coll.Where(o => !o.Order.HasValue).OrderBy(o => o.Name))
            {
                item_.Order = int.MaxValue;
            }

            coll = coll.OrderBy(o => o.Order).ThenBy(o => o.Name).ToList();

            var xxx = value;

            if (isNullable)
            {
                retVal.Add(new SelectListItem { Text = "Pick One", Value = string.Empty });
            }
            else if (value == null)
            {
                retVal.Add(new SelectListItem { Text = "Pick One", Value = string.Empty });
            }
            else if (!coll.Any(o => o.Id == value))
            {
                retVal.Add(new SelectListItem { Text = "Pick One", Value = string.Empty });
            }

            foreach (var item in coll.OrderBy(o => o.Order).ThenBy(o => o.Name).ToList())
            {
                if (value != null && value == item.Id)
                {
                    retVal.Add(new SelectListItem { Text = item.Name, Value = item.Code, Selected = true });
                }
                else
                {
                    retVal.Add(new SelectListItem { Text = item.Name, Value = item.Code });
                }
            }

            return retVal;
        }

        // Private Classes

        private class EnumerationData
        {
            /// <summary>
            /// int value of ENUM.
            /// <para/>
            /// Example. Corresponds to 1 or 2 in public ENUM Gender { Male = 0, Female = 1 }.
            /// </summary>
            /// <value>Enumeration Integer Value</value>
            public int Id { get; set; }

            /// <summary>
            /// Code value of enumeration.
            /// <para/>
            /// Example. Corresponds to 'Male' or 'Female' in public ENUM Gender { Male = 0, Female = 1 }.
            /// </summary>
            /// <value>Code value of enumeration.</value>
            public string Code { get; set; }

            /// <summary>
            /// Name - Defaults to Code if Name isn't present in data annotation.
            /// </summary>
            /// <value>Name - Defaults to Code if Name isn't present in data annotation.</value>
            public string Name { get; set; }

            ///// <summary>
            ///// Group Name. From Data Annotation.
            ///// </summary>
            ///// <value>Group Name. From Data Annotation.</value>
            //[Display(Name = "Group Name")]
            //public string GroupName { get; set; }

            /// <summary>
            /// Order. From Data Annotation.
            /// </summary>
            /// <value>Order. From Data Annotation.</value>
            public int? Order { get; set; }
        }

        private static string GetEnumAttributeValueString<T>(Enum e, Func<T, object> selector) where T : Attribute
        {
            var value_ = GetEnumAttributeValue(e, selector);
            return (string)value_;
        }

        private static int? GetEnumAttributeValueIntegerNullable<T>(Enum e, Func<T, object> selector) where T : Attribute
        {
            var value_ = GetEnumAttributeValue(e, selector);

            return (int?)value_;
        }

        private static object GetEnumAttributeValue<T>(Enum e, Func<T, object> selector) where T : Attribute
        {
            var output_ = e.ToString();
            var member_ = e.GetType().GetMember(output_).First();
            var attributes_ = member_.GetCustomAttributes(typeof(T), false);

            if (attributes_.Length > 0)
            {
                return selector((T)attributes_[0]);
            }

            return null;
        }
    }
}