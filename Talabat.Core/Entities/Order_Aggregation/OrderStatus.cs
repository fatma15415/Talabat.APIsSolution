﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregation
{
    public enum OrderStatus
    {

        //int 
        [EnumMember(Value = "Pending") ]
        Pending,

        [EnumMember(Value = "PaymentReceived")]
        PaymentReceived,

        [EnumMember(Value = "PaymentFaild")]
        PaymentFaild


    }
}
