using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Models
{
    public class Users
    {
        [Key]
        [Display(Name = "ID User")]
        public int User_Id { get; set; }
        

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Nama")]
        public string Name { get; set; }
        [Display(Name = "Alamat")]
        public string Country { get; set; }
         [Display(Name = "Perusahaan")]
        public string Perusahaan_Id { get; set; }
        public string Kecamatan { get; set; }
        public string Telpon { get; set; }
        public string Faks { get; set; }
        public string Kategori { get; set; }
        public string Aktif { get; set; }
        [Display(Name = "ID AE")]
        public int Id_Atasan { get; set; }
        [Display(Name = "Created On")]
        public string Createdon { get; set; }
        [Display(Name = "Created By")]
        public string Createdby { get; set; }

        public virtual ICollection<Assigns> Assigns { get; set; }
        public virtual ICollection<DistributorAssigns> DistributorAssigns { get; set; }
        public virtual Perusahaans Perusahaans { get; set; }
    }
   
}