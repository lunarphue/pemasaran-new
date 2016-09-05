using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstWebsite.Models
{
    public class Perusahaans
    {
        public Perusahaans()
        {
            this.Users = new HashSet<Users>();
        }
        [Key]
        [Display(Name = "Kode Perusahaan")]
        public string Perusahaan_Id { get; set; }
        [Display(Name = "Nama Perusahaan")]
        public string Nama { get; set; }
    
        public virtual ICollection<Users> Users { get; set; }


    }
    
}