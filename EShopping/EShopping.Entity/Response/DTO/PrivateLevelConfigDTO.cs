using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Entity.Response.DTO
{
    public class PrivateLevelConfigDTO
    {
        public string currentExpense { get; set; }

        public int currentLevel { get; set; }

        public string nextLevelNeedExpense { get; set; }

        public List<LiveConfigDTO> levelConfigVOs { get; set; }
    }
}
