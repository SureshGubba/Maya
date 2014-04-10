using System;
using System.Collections.Generic;
using System.Text;
using SchneiderMilkManagement.BusinessLayer.BusinessObjects.BusinessRules;

namespace SchneiderMilkManagement.BusinessLayer.BusinessObjects
{
    public class User : BusinessObject
    {
        public User()
        {
            AddRule(new ValidateId("UserId"));
            AddRule(new ValidateRequired("Email"));
            AddRule(new ValidateRequired("Password"));
            AddRule(new ValidateEmail("Email"));            
        }
        /// <summary>
        /// Gets or sets the unique User identifier.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique User First Name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the unique User Last Name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the unique User Email Id.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the unique UserName.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the unique User Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets the New Password in change password function.
        /// </summary>
        public string NewPassword { get; set; }
        
        /// <summary>
        /// Gets or sets the unique Is Super Admin.
        /// </summary>
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// Gets or sets the MobileNo.
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// Gets or sets the Role.
        /// </summary>
        public string Role { get; set; }
    }
}

