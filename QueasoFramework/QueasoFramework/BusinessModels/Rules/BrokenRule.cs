namespace QueasoFramework.BusinessModels.Rules
{
    public class BrokenRule
    {
        #region Properties

        public string PropertyName { get; set; }
        public string FailedMessage { get; set; }

        #endregion Properties

        #region Constructors

        public BrokenRule()
        {
            this.PropertyName = string.Empty;
            this.FailedMessage = string.Empty;
        }

        public BrokenRule(string failedMessage)
        {
            this.PropertyName = string.Empty;
            this.FailedMessage = failedMessage;
        }

        public BrokenRule(string failedMessage, string propertyName)
        {
            this.PropertyName = propertyName;
            this.FailedMessage = failedMessage;
        }

        #endregion Constructors
    }
}