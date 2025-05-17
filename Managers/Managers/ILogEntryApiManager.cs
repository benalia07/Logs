using Managers.Models;

namespace Managers.Managers;

public interface ILogEntryApiManager
{
    Task<List<LogEntry>?> GetAllLogsAsync();
    Task<LogEntry?> GetLogByIdAsync(Guid id);
    Task<bool> AddLogAsync(LogEntry entry);
    Task<bool> UpdateLogAsync(Guid id, LogEntry entry);
    Task<bool> DeleteLogAsync(Guid id);
    Task<List<LogEntry>?> GetLogsByDateAsync(DateTime date);
    Task<List<LogEntry>?> GetLogsBetweenDatesAsync(DateTime start, DateTime end);
    Task<List<LogEntry>?> GetLogsByUserIdAsync(Guid userId);
    Task<List<LogEntry>?> GetLogsByActionIdAsync(int actionId);

    Task<List<LogAction>?> GetAllLogActionsAsync();
    Task<LogAction?> GetLogActionByIdAsync(int id);
    Task<bool> AddLogActionAsync(LogAction action);
    Task<bool> UpdateLogActionAsync(int id, LogAction action);
    Task<bool> DeleteLogActionAsync(int id);
}