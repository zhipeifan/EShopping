using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Request
{
    public class QuerySpellbuyrecordListRequest
    {
     //   {"payload":"{'spellbuyproductId':1,'currentPage':1,'pageSize':10}"}

        public AttendUserRequestDTO payload { get; set; }
    }

    public class AttendUserRequestDTO
    {
        public int spellbuyproductId { get; set; }

        public int currentPage { get; set; }


        public int pageSize { get; set; }
    }
}
