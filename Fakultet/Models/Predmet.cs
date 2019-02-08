using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fakultet.Models
{
    public class Predmet
    {
        [Key, Required, StringLength(30, MinimumLength = 4, ErrorMessage = "Naziv mora biti duzine izmedju 4 i 30 karaktera")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Naziv { get; set; }
        [Required, Range(1000, 20000, ErrorMessage = "Cena mora biti izmedju 1000 i 6000 dinara")]
        [DataType(DataType.Currency)]
        public int Cena { get; set; }
        [StringLength(13, MinimumLength = 13, ErrorMessage = "JMBG mora biti tacno 13 karaktera")]
        [Display(Name = "JMBG nastavnika")]
        [Required, ForeignKey("Nastavnik")]
        public string JMBGNastavnika { get; set; }

        public virtual Nastavnik Nastavnik { get; set; }
        public virtual ICollection<Student> Studenti { get; set; }
    }
}