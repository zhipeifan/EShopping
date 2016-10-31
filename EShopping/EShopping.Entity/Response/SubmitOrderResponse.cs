using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
    public class SubmitOrderResponse
    {
        public ResponseStatus Status { get; set; }

        public SubmitOrderDTO responseData { get; set; }
    }

    public class SubmitOrderDTO
    {
        public decimal needPayMoney { get; set; }

        public string orderCode { get; set; }
    }
}
