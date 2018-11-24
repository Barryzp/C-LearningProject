
namespace Common
{
    public enum OperationCode:byte
    {
        LogginOperation,
        RegisterOperation,
        DefaultOperation,
        SyncPositionOp,
        SyncPlayerOp
    }

    public enum ParameterCode:byte
    {
        Username,
        Password,
        Position,
        X,
        Y,
        Z,
        UsernameList,
        PlayerDataList
    }

    public enum EventCode:byte
    {
        NewPlayer,
        SyncPlayerPosition
    }

    public enum ReturnCode:short
    {
        Success,
        Failed,
        AccountExist,
        RegisterSuccess
    }
}
