﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Models
{
    public class Customers : CommonProps
    {
        public BigInteger CustomerID { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public BigInteger InsurancePolicyID { get; set; }

    }
}
