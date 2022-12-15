using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheWarehouse.Web.Models
{
    public class AuctionViewModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string ProductName { get; set; }
        public string Winner { get; set; }
        public int InitialPrice { get; set; }
       // [DataType(DataType.Time)]
        public DateTime Date { get; set; }
        [Required]
        public IFormFile UploadPicture { get; set; }
    }
}
