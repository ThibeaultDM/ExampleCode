using QueasoFramework.BusinessModels.Rules;
using QueasoFramework.BusinessModels.Rules.Interfaces;
using System.Collections.Generic;

namespace QueasoFramework.BusinessModels
{
    public abstract class BusinessObjectBase
    {
        #region Properties

        public bool Valid
        { get { return IsValid(); } }

        public List<BusinessRule> BusinessRules { get; set; }
        public List<BrokenRule> BrokenRules { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default empty constructor
        /// </summary>
        public BusinessObjectBase()
        {
            this.BusinessRules = new List<BusinessRule>();
            this.BrokenRules = new List<BrokenRule>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Checks if the rules are valid
        /// </summary>
        /// <returns>returns true or false</returns>
        private bool IsValid()
        {
            this.BrokenRules = new List<BrokenRule>(); //always reset the list to prevent duplicates

            bool rulesPassed = AddBusinessRules();

            return rulesPassed;
        }

        #endregion Methods

        #region Virtual Methods

        public virtual bool AddBusinessRules()
        {
            return CheckRules<BusinessRule>(BusinessRules);
        }

        /// <summary>
        /// Checks for each item in the businessrules list if they are valid
        /// </summary>
        /// <returns>true or false</returns>
        protected virtual bool CheckRules<T>(List<T> rules) where T : IRuleBase
        {
            if (rules != null && rules.Count > 0)
            {
                foreach (var item in rules)
                {
                    if (!item.Passed)
                    {
                        this.BrokenRules.Add(new BrokenRule(item.FailedMessage, item.PropertyName));
                    }
                }
            }

            if (this.BrokenRules == null || this.BrokenRules.Count == 0)
            {
                return true;
            }

            return false;
        }

        #endregion Virtual Methods
    }
}