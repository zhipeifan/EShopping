using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
    public class AddSpellbuyproductResponse
    {
        public AddSpellbuyproductResponseDTO responseData { get; set; }
    }

    public class AddSpellbuyproductResponseDTO
    {

      public int spellbuyproductId { get; set; }

        public string licensingCode { get; set; }

        public int spellbuyLimit { get; set; }
    }
}
