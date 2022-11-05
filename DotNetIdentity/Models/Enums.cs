namespace DotNetIdentity.Models
{
    /// <summary>
    /// Gender enum
    /// </summary>
    public enum Gender { 
        ///<summary>gender unknown</summary>
        Unknown, 
        ///<summary>gender male</summary>
        Male, 
        ///<summary>gender female</summary>
        Female }
    /// <summary>
    /// Department enum
    /// </summary>
    public enum Department { 
        ///<summary>Department Software</summary>
        Software, 
        ///<summary>Department HR</summary>
        HR, 
        ///<summary>Department Accounting</summary>
        Accounting, 
        ///<summary>Department Management</summary>
        Management }
    /// <summary>
    /// TwoFactorType enum
    /// </summary>
    public enum TwoFactorType { 
        ///<summary>2fa type none</summary>
        None, 
        ///<summary>2fa type email</summary>
        Email, 
        ///<summary>2fa type Authenticator app</summary>
        Authenticator
    }
}