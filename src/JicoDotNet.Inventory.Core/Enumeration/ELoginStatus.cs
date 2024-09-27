namespace JicoDotNet.Inventory.Core.Enumeration
{
    public enum ELoginStatus
    {
        Error = 0,

        UserNameOrPasswordInvalid = 1,
        UserNotInIPRange = 2,
        IPAddressFormatError = 3,
        DuplicateLogin = 4,

        Success = 6
    }
}
