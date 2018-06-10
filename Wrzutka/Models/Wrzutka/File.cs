using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wrzutka.Models.Wrzutka
{
    public class File
    {
        public int FileID { get; set; }

        [DisplayName("Data dodania")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreationTime { get; set; }
        [DisplayName("Nazwa pliku")]
        [Required(ErrorMessage = "Nazwa pliku jest wymagana.")]
        public string FileName { get; set; }
        public string AuthorUserID { get; set; }
        [DisplayName("Autor")]
        public string AuthorUserName { get; set; }
        public List<Rating> ratings { get; set; }
        [DisplayName("Średnia")]
        public double Average { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        public string ShortDescription
        {
            get
            {
                if (Description == null)
                    return "";
                else
                    return Description.Length <= 20 ? Description : Description.Substring(0, 17) + "...";
            }
        }


    }
}