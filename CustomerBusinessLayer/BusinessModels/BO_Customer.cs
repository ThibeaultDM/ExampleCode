using CustomerDataLayer.DataModels.Enums;
using QueasoFramework.BusinessModels;
using QueasoFramework.BusinessModels.Rules;

namespace CustomerBusinessLayer.BusinessModels
{
    // TODO See how this maps
    public class BO_Customer : BusinessObjectBase
    {
        public BO_Customer()
        {
            Addresses = new List<BO_Address>();
        }

        #region Properties

        public Guid Id { get; set; }

        /// <summary>
        /// MaxLength(100)
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// MaxLength(50)
        /// </summary>
        public string FirstName { get; set; }

        public Gender Gender { get; set; }

        public List<BO_Address> Addresses { get; set; }

        public bool IsActive { get; set; }

        public BO_Company Company { get; set; }

        public BO_CreditInfo CreditInfo { get; set; }

        #endregion Properties

        public override bool AddBusinessRules()
        {
            BusinessRules.Add(new BusinessRule().IsRequired(nameof(FamilyName), FamilyName));
            BusinessRules.Add(new BusinessRule().MaxLength(nameof(FamilyName), FamilyName, 100));
            BusinessRules.Add(new BusinessRule().IsRequired(nameof(FirstName), FirstName));
            BusinessRules.Add(new BusinessRule().MaxLength(nameof(FirstName), FirstName, 50));
            BusinessRules.Add(new BusinessRule().IsRequired(nameof(Gender), Gender));
            BusinessRules.Add(new BusinessRule().IsRequired(nameof(Addresses), Addresses));
            //BusinessRules.Add(new BusinessRule().IsRequired(nameof(CreditInfo), CreditInfo));

            return base.AddBusinessRules();
        }
    }
}