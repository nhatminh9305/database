﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace B3.Models
{
    public class NewModel
    {
        public int Id { get; set; }
        [StringLength(30 ,ErrorMessage = "Title cannot be longer than 30 characters.")]
        public string Title { get; set; }
        [StringLength(200,ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; }
        public string Image { get; set; }
        public byte Type { get; set; }
        public byte Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? EditedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
