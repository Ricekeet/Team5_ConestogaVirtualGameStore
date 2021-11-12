﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Team5_ConestogaVirtualGameStore.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int ReviewListId { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int? Pending { get; set; }

        public virtual ReviewList ReviewList { get; set; }
    }
}
