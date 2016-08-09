using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    public class UpdateSpellbuyproductRequest
    {
        public int spellbuyproductId { get; set; }

        public int privateMemberCount { get; set; }
        public string privateCode { get; set; }
    }
}
