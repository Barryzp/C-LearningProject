
namespace Common
{
    public enum OperationCode:byte
    {
        LogginOperation,
        RegisterOperation,
        DefaultOperation
    }

    public enum ParameterCode:byte
    {
        Username,
        Password
    }

    public enum EventCode:byte
    {

    }

    public enum ReturnCode:short
    {
        Success,
        Failed,
        AccountExist,
        RegisterSuccess
    }
}
