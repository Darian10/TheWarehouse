using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheWarehouse.Web.Models
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Cant { get; set; }
        [Display(Name = "Choose a picture")]

        public IFormFile UploadPicture { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime VigencyDate { get; set; }

    }
}
