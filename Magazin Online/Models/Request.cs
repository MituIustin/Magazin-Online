﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazin_Online.Models
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }
        public string Message { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}
