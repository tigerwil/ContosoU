using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUDemo.Models
{
    public class Student : Person
    {
        //mwilliams:  Data Annotations
        /*
            DataType.Date:  we only want to keep track of the date, not the date and time.
                            Does not specify the format of the date that is displayed. 
                            By default, the data field is displayed according to the default formats based on 
                            the server's CultureInfo

            DisplayFormat:  Attribute is used to explicitly specify the date format, for example:  yyyy-MM-dd
                            The browser can enable HTML5 features (for example to show a calendar control, the locale-appropriate currency
                            symbol, email links, some client-side input validation, etc.).
            Display(Name=""): Gets or sets a value that is used for display in the UI overriding default EnrollmentDate.
        */
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }//1 student : many enrollments
        /*
        The Enrollments property is a navigation property. Navigation properties hold other entities that are related to this entity. 
        In this case, the Enrollments property of a Student entity will hold all of the Enrollment entities that are related to that Student entity. 
        In other words, if a given Student row in the database has two related Enrollment rows (rows that contain that student's primary key value 
        in their StudentID foreign key column), that Student entity's Enrollments navigation property will contain those two Enrollment entities.

        Navigation properties are typically defined as virtual so that they can take advantage of certain Entity Framework functionality such as lazy loading. 
        */


    }
}