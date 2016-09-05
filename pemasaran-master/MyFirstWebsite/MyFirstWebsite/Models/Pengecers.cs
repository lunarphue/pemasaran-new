using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyFirstWebsite.Models
{
    public class Pengecers
    {
         public Pengecers()
        {
            this.Assigns = new HashSet<Assigns>();
        }
        [Key]
        [Display(Name = "Kode Pengecer")]
        public string Pengecer_Id { get; set; }
        [Display(Name = "Nama")]
        public string Name { get; set; }
        public string Alamat { get; set; }
        public string Kecamatan { get; set; }
        public string Telpon { get; set; }
        public string Faks { get; set; }
        public string NPWP { get; set; }
        [Display(Name = "Status")]
        public string Aktif { get; set; }
        [Display(Name = "Created On")]
        public string Createdon { get; set; }
        [Display(Name = "Created By")]
        public string Createdby { get; set; }
        public virtual ICollection<Assigns> Assigns { get; set; }
        
    }
}