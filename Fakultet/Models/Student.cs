using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fakultet.Models
{
    public class Student
    {
        [Key, Required, StringLength(13, MinimumLength = 13, ErrorMessage = "JMBG mora biti tacno 13 karaktera")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string JMBG { get; set; }
        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = "Ime mora biti duzine izmedju 2 i 20 karaktera")]
        public string Ime { get; set; }
        [Required, StringLength(30, MinimumLength = 4, ErrorMessage = "Prezime mora biti duzine izmedju 4 i 30 karaktera")]
        public string Prezime { get; set; }
        [Required, Range(18, 100, ErrorMessage = "Godine moraju biti izmedju 18 i 100")]
        public int Godine { get; set; }

        public virtual ICollection<Predmet> Predmeti { get; set; }
    }
}