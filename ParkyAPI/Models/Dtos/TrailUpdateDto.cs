using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Models.Dtos
{
    public class TrailUpdateDto
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }

        [Required] public double Distance { get; set; }




        public Trail.DifficultType Difficulty { get; set; }

        [Required]
        public int NationalParkId { get; set; }
        [Required]
        public double Elevation { get; set; }


    }

}
