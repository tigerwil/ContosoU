using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUDemo.ViewModels
{
    public class EnrollmentDateGroup
    {
        /*
            Data Annotations Datatype property 
            - for formatting date like 9/1/2005 within view
            - without it: 9/1/2001 12:00:00 AM
        
        */
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }

        public int StudentCount { get; set; }
    }
}