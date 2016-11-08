using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
    public class QueryTopSearchResponse
    {
        public ResponseStatus Status { get; set; }

        public List<SearData> responseData { get; set; }
    }

    public class SearData{
        public int Id { get; set; }

        public string keyword { get; set; }
    }
}
