﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
//using System.Web;

namespace CCICNVSqlDb.Models
{
    public class Family
    {
        public int ID { get; set; }
        public string FamilyName { get; set; }

        [Display(Name = "First Name")]
        public string Parent { get; set; }
        [Display(Name = "姓名")]
        public string ChineseName { get; set; }

        public string Children { get; set; }
        public string ChildrenChineseName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string eMail { get; set; }

        public string Congregation { get; set; }
        public string Fellowship { get; set; }
        public Byte[] FamilyPicture { get; set; }

        public string Done { get; set; }
        public string Description { get; set; }


        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
    }
}