using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Manager.ExpenseManager.Common
{
    public enum Category
    {
        [Display(Name = "Products")]
        Products,
        [Display(Name = "Cafe")]
        Cafe,
        [Display(Name = "Transport")]
        Transport,
        [Display(Name = "Entertainment")]
        Entertainment,
        [Display(Name = "House")]
        House,
        [Display(Name = "Healthcare")]
        Healthcare,
        [Display(Name = "Education")]
        Education,
        [Display(Name = "Clothes")]
        Clothes,
        [Display(Name = "Travel")]
        Travel,
        [Display(Name = "Salary")]
        Salary,
        [Display(Name = "Investment")]
        Investment,
        [Display(Name = "Gift")]
        Gift,
        [Display(Name = "Other")]
        Other
    }
}
