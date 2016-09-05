using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyFirstWebsite.Models
{
    public class StokPengecers
    {
        [Key]
        [Display(Name = "Nomor Stok Pengecer")]
        public int StokPengecer_Id { get; set; }
        [Display(Name = "Kode Assign Pengecer")]
        public int Assign_Id { get; set; }
        [Display(Name = "Last Edited By")]
        public string Edited_by { get; set; }
        [Display(Name = "Last Date Edited")]
        public string Date_Edited { get; set; }
        [Display(Name = "Last Time Edited")]
        public string Time_Edited { get; set; }
        [Display(Name = "Stok Urea")]
        public Nullable<int> Urea { get; set; }
        [Display(Name = "Stok NPK")]
        public Nullable<int> NPK { get; set; }
        [Display(Name = "Stok SP36")]
        public Nullable<int> SP36 { get; set; }
        [Display(Name = "Stok ZA")]
        public Nullable<int> ZA { get; set; }
        [Display(Name = "Stok Organik")]
        public Nullable<int> Organik { get; set; }

        public virtual Assigns Assign { get; set; }
    }
}