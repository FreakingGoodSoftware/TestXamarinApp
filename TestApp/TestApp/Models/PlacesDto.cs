using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class ResultDto
    {
        public string icon { get; set; }
        public string name { get; set; }

        public string[] types { get; set; }

        public string formatted_address  { get; set; }

        public string rating { get; set; }

    }
    public class PlacesDto
    {
        public string next_page_token { get; set; }

        public ResultDto[] results { get; set; }
    }
}
