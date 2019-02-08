using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fakultet.Models
{
    public class Nastavnik
    {
        [Key, Required, StringLength(13, MinimumLength = 13, ErrorMessage = "JMBG mora biti tacno 13 karaktera")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string JMBG { get; set; }
        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = "Ime mora biti duzine izmedju 2 i 20 karaktera")]
        public string Ime { get; set; }
        [Required, StringLength(30, MinimumLength = 4, ErrorMessage = "Prezime mora biti duzine izmedju 4 i 30 karaktera")]
        public string Prezime { get; set; }
        [Required, DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum zaposljavanja")]
        public DateTime DatumZaposljavanja { get; set; }

        public virtual ICollection<Predmet> Predmeti { get; set; }
    }
}