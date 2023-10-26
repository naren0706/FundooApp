using FundooModel.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModel.Labels
{
    public class label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string EmailId { get; set; }

        [Required]
        public string LabelName { get; set; }

       /* public int id { get; set; }
        * 
        [ForeignKey("userid")]
        public Register Register{ get; set; }*/
    }
}
