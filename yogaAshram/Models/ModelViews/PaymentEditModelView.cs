﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace yogaAshram.Models.ModelViews
{
    public class PaymentEditModelView
    {
        public string Comment { get; set; }
        [Remote(action: "CheckMembership", controller: "Validation")]
        public long MembershipId { get; set; }
        [Remote(action: "CheckClient", controller: "Validation")]
        public long PaymentId { get; set; }
        public int Debts { get; set; }
        public Membership Membership { get; set; }
    }
}