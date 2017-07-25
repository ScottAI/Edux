﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edux.Models
{
    public class PageComponent : BaseEntity
    {
        public PageComponent() : base()
        {
            CreateDate = DateTime.Now;
            CreatedBy = "username";
            UpdateDate = DateTime.Now;
            UpdatedBy = "username";
        }
        public string PageId { get; set; }
        [ForeignKey("PageId")]
        public Page Page { get; set; }
        public string ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public Component Component { get; set; }
        public int Position { get; set; }
    }
}
