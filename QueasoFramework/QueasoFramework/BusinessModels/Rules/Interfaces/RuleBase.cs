namespace QueasoFramework.BusinessModels.Rules.Interfaces;

public interface IRuleBase
{
    #region Properties

    string PropertyName { get; set; }
    bool Passed { get; set; }
    string FailedMessage { get; set; }

    #endregion Properties
}