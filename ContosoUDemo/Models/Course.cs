using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUDemo.Models
{
    public class Course
    {
        /* 
            You can turn off the identity by using DatabaseGeneratedOption.None

            DatabaseGeneratedOption enum has three members(see documentation):
                Computed: The database generates a value when a row is inserted or updated.
                Identity: The database generates a value when a row is inserted.
                None:    The database does not generate values.
        */
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Number")]
        public int CourseID { get; set; }//PK with no Identity

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0,5)]
        public int Credits { get; set; }
        public int DepartmentID { get; set; }//FK 1:deparment : many courses

        //Navigation Property:  1 course : many enrollments
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        //Navigation Property:  1 course : many instructors
        public virtual ICollection<Instructor> Instructors { get; set; }

        //Nagigation Property:  
        public virtual Department Department { get; set; }

        /*mwilliams:  
         CourseIdTitle is a calculated property that returns a value that's created by concatenating two other properties. 
         Therefore it has only a get accessor, and no CourseIdTitle column will be generated in the database. 
      */
        public string CourseIdTitle
        {
            get
            {
                return CourseID + ": " + Title;
            }
        }
    }
}