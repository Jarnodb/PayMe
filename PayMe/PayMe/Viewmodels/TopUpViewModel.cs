using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PayMe.Viewmodels
{
    public class TopUpViewModel
    {
        public string Id { get; set; }

        public decimal Money { get; set; }
    }
}
