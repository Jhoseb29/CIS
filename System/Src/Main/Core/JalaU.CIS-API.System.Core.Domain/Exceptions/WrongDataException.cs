namespace JalaU.CIS_API.System.Core.Domain;

public class WrongDataException(string message, List<MessageLogDTO> messageLogDTOs)
    : ApplicationException(message)
{
    public readonly List<MessageLogDTO> MessageLogs = messageLogDTOs;
}
