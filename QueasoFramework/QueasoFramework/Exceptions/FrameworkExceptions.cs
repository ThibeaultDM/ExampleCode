using QueasoFramework.Enums;
using System;

namespace QueasoFramework.Exceptions;

public class FrameworkException : Exception
{
    #region Properties

    public Guid Id { get; set; } = Guid.NewGuid();

    public FrameworkExceptionType Type { get; set; }
    public string Namespace { get; set; }
    public string UseCase { get; set; }
    public string InputParameters { get; set; }

    #endregion Properties

    #region Constructors

    public FrameworkException(object classobject, string useCaseName, string message, FrameworkExceptionType type) : base(message)
    {
        var myType = classobject.GetType();
        var n = myType.Namespace;
        var properties = myType.GetProperties();
        string parameters = "";
        foreach (var prop in properties)
        {
            var value = prop.GetValue(classobject, null);
            parameters += $"{prop.Name}:{value}";
        }
        this.Type = type;
        this.Namespace = n;
        this.InputParameters = parameters;
        this.UseCase = useCaseName;
    }

    public FrameworkException(string useCaseName, string message, Exception innerException, FrameworkExceptionType type) : base(message, innerException)
    {
        this.UseCase = useCaseName;
        this.Type = type;
        this.Namespace = innerException.Source;
    }

    #endregion Constructors
}