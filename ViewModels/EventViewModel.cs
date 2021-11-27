using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_ConestogaVirtualGameStore.ViewModels
{
    public class EventViewModel
    {
        public int EventId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile EventPic { get; set; }
    }
}
