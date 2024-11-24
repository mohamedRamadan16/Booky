﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booky.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; } 
        public int ProductId {  get; set; }
        public string ApplicationUserId {  get; set; }
        [ForeignKey(nameof(ProductId))]
        [ValidateNever]
        public Product Product { get; set; }
        [Range(1, 100, ErrorMessage = "Enter a value between 1 and 1000")]
        public int Count {  get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public double Price {  get; set; }

    }

}
