using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SchneiderMilkManagement.BusinessLayer.BusinessObjects.BusinessRules;

namespace SchneiderMilkManagement.BusinessLayer.BusinessObjects
{
    /// <summary>
    /// Abstract base class for business objects.
    /// Contains basic business rule infrastructure.
    /// </summary>
    public abstract class BusinessObject
    {
        // List of business rules
        private IList<BusinessRule> _businessRules = new List<BusinessRule>();

        // List of validation errors (following validation failure)
        private IList<string> _validationErrors = new List<string>();

        /// <summary>
        /// Gets list of validations errors.
        /// </summary>
        public IList<string> ValidationErrors
        {
            get { return _validationErrors; }
        }

        /// <summary>
        /// Adds a business rule to the business object.
        /// </summary>
        /// <param name="rule"></param>
        protected void AddRule(BusinessRule rule)
        {
            _businessRules.Add(rule);
        }

        /// <summary>
        /// Determines whether business rules are valid or not.
        /// Creates a list of validation errors when appropriate.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            bool isValid = true;

            _validationErrors.Clear();

            foreach (BusinessRule rule in _businessRules)
            {
                if (!rule.Validate(this))
                {
                    isValid = false;
                    _validationErrors.Add(rule.ErrorMessage);
                }
            }
            return isValid;
        }
    }
}
