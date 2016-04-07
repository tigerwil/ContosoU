using System.ComponentModel.DataAnnotations;

namespace ContosoUDemo.Models
{
    //Grade enum
    public enum Grade
    {
        A, B, C, D, F
    }
    public class Enrollment
    {
        public int EnrollmentID { get; set; }//PK

        /*
            The CourseID property is a foreign key, and the corresponding navigation property is Course.
            An Enrollment entity is associated with one Course entity.
        */
        public int CourseID { get; set; }//FK
        /*
            The StudentID property is a foreign key, and the corresponding navigation property is Student. 
            An Enrollment entity is associated with one Student entity, so the property can only hold a single Student entity 
            (unlike the Student.Enrollments navigation property you saw earlier, which can hold multiple Enrollment entities).
        */
        public int StudentID { get; set; }//FK

        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }//? nullable

        //Navigation properties
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }


    }
}