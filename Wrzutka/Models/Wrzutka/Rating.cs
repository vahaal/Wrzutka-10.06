using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wrzutka.Models.Wrzutka
{
    public class Rating
    {
        public int RatingID { get; set; }
        public int FileID { get; set; }
        public string UserID { get; set; }
        public int Value { get; set; }
    }
}