using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeCommonLayer
{
    public class EmployeeModel
    {
        [Required]
        public int EmployeeId { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 3,
         ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 3,
         ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string MobileNumber { get; set; }

        public string EmployeeAddress { get; set; }

    }
}

