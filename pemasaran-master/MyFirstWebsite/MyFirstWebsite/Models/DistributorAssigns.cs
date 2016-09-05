using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstWebsite.Models
{
    public class DistributorAssigns
    {
        public DistributorAssigns()
        {
            this.StokDistributors = new HashSet<StokDistributors>();
        }
        [Key]
        [Display(Name = "Nomor Assign")]
        public int DistributorAssign_Id { get; set; }
        [Display(Name = "ID AE/Assisten AE")]
        public int User_Id { get; set; }
        [Display(Name = "Status")]
        public string Aktif { get; set; }
        [Display(Name = "ID Distributor")]
        public string Distributor_Id { get; set; }
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
        public virtual Distributors Distributor { get; set; }
        public virtual ICollection<StokDistributors> StokDistributors { get; set; }

    }
    
}