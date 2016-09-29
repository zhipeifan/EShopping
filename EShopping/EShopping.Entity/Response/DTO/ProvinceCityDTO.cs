using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{

    public class ProvoinceDTO
    {
        public string provinceName { get; set; }

        public List<CityDTO> cityVOs { get; set; }
    }

   public class CityDTO
    {
       public string cityName { get; set; }

       public List<string> regions { get; set; }
    }
}
