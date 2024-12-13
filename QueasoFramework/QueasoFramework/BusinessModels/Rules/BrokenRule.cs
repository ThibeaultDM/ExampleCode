namespace QueasoFramework.BusinessModels.Rules;

public class BrokenRule
{
    #region Properties

    public string PropertyName { get; set; }
    public string FailedMessage { get; set; }

    #endregion Properties

    #region Constructors

    public BrokenRule()
    {
        PropertyName = string.Empty;
        FailedMessage = string.Empty;
    }

    public BrokenRule(string failedMessage)
    {
        PropertyName = string.Empty;
        FailedMessage = failedMessage;
    }

    public BrokenRule(string failedMessage, string propertyName)
    {
        PropertyName = propertyName;
        FailedMessage = failedMessage;
    }

    #endregion Constructors
}