using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace MyFirstWebsite.Models
{
    public class Assigns
    {
         public Assigns()
        {
            this.StokPengecers = new HashSet<StokPengecers>();
        }
         [Key]
         [Display(Name = "Nomor Assign")]
        public int Assign_Id { get; set; }
        [Display(Name = "ID AE/Assisten AE")]
        public int User_Id { get; set; }
        [Display(Name = "Status")]
        public string Aktif { get; set; }
        [Display(Name = "ID Pengecer")]
        public string Pengecer_Id { get; set; }
        [Display(Name = "Last Edited By")]
        public string Edited_by { get; set; }
        [Display(Name = "Last Date Edited")]
        public string Date_Edited { get; set; }
        [Display(Name = "Last Time Edited")]
        public string Time_Edited { get; set; }
         
        [Display (Name="Assign For Urea?")]
        public string Urea { get; set; }
        [Display(Name = "Assign For NPK?")]
        public string NPK { get; set; }
        [Display(Name = "Assign For ZA?")]
        public string ZA { get; set; }
        [Display(Name = "Assign For SP36?")]
        public string SP36 { get; set; }
        [Display(Name = "Assign For Organik?")]
        public string Organik { get; set; }
       
        public virtual Users User { get; set; }
        public virtual Pengecers Pengecer { get; set; }
        public virtual ICollection<StokPengecers> StokPengecers { get; set; }
          
        
    }

}