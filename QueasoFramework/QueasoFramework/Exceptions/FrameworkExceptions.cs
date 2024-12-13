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
        Type myType = classobject.GetType();
        string n = myType.Namespace;
        System.Reflection.PropertyInfo[] properties = myType.GetProperties();
        string parameters = "";
        foreach (System.Reflection.PropertyInfo prop in properties)
        {
            object value = prop.GetValue(classobject, null);
            parameters += $"{prop.Name}:{value}";
        }
        Type = type;
        Namespace = n;
        InputParameters = parameters;
        UseCase = useCaseName;
    }

    public FrameworkException(string useCaseName, string message, Exception innerException, FrameworkExceptionType type) : base(message, innerException)
    {
        UseCase = useCaseName;
        Type = type;
        Namespace = innerException.Source;
    }

    #endregion Constructors
}