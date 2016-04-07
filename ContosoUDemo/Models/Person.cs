using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContosoUDemo.Models
{
    public abstract class Person
    {
        public int ID { get; set; }//PK

        [Required]
        [StringLength(65)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        /*
        You can put multiple attributes on one line, so you could also write the instructor class as follows:
        [Display(Name = "Last Name"),StringLength(50, MinimumLength=1)]
         public string LastName { get; set; }

        [DataType(DataType.Date),Display(Name = "Hire Date"),DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }
       */

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /*mwilliams:  
            FullName is a calculated property that returns a value that's created by concatenating two other properties. 
            Therefore it has only a get accessor, and no FullName column will be generated in the database. 
        */
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

    }
}