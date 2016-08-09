using EShopping.Entity.Response.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response
{
    public class QueryNewsListResponse
    {
        public List<ActivityDTO> responseData { get; set; }
    }

    public class QueryNewsListResponseDTO{
        public List<ActivityDTO> Activitys { get; set; }
    }
}
