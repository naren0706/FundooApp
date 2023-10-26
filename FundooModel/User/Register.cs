using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooModel.User
{
    public class Register
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is null")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is null")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is null")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is null")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[0-9a-zA-Z]+[.+-_]{0,1}[0-9a-zA-Z]+[@][a-zA-Z]+[.][a-zA-Z]{2,3}([.][a-zA-Z]{2,3}){0,1}", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is null")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
