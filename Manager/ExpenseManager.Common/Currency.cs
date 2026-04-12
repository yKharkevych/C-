using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Manager.ExpenseManager.Common
{
    public enum Currency
    {
        [Display(Name = "UAH")]
        UAH,
        [Display(Name = "USD")]
        USD,
        [Display(Name = "EUR")]
        EUR,
        [Display(Name = "PLN")]
        PLN,
        [Display(Name = "GBP")]
        GBP
    }
}
