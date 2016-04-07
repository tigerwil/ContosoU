using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUDemo.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public int? InstructorID { get; set; }

        public virtual Instructor Administrator { get; set; }
        public virtual ICollection<Course> Courses { get; set; }


        //mwilliams:  Add Optimistic Concurrency  Property
        /*
        Entity Framework supports Optimistic Concurrency by default. In the optimistic concurrency, EF saves the entity to the database, 
        assuming that the same data has not changed since the entity was loaded. If it determines that the data has changed, 
        then an exception is thrown and you must resolve the conflict before attempting to save it again.


        The Timestamp attribute specifies that this column will be included in the Where clause of Update and Delete commands 
        sent to the database. The attribute is called Timestamp because previous versions of SQL Server used a SQL timestamp data type 
        before the SQL rowversion replaced it. The .Net type for rowversion is a byte array. 

        Also check out:  ConcurrencyCheck Attribute:
        http://www.entityframeworktutorial.net/code-first/concurrencycheck-dataannotations-attribute-in-code-first.aspx

        ConcurrencyCheck attribute can be applied to a property of a domain class. Code First takes the value of a column in "where" clause
        when EF executes update command for the table. You can use ConcurrencyCheck attribute when you want to use existing column 
        for concurrency check and not a separate timestamp column for concurrency.

        for example:
        [ConcurrencyCheck]
        public string StudentName { get; set; }

        */
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}