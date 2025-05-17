using LogsAPI.Models;

namespace LogsAPI.Service;

public interface ILogEntryService
{
    Task<List<LogEntry>> GetAllLogs();
    Task<LogEntry?> GetLogById(Guid id);
    Task AddLog(LogEntry entry);
    Task UpdateLog(LogEntry entry);
    Task DeleteLog(Guid id);

    Task<List<LogEntry>> GetLogsByDate(DateTime date);
    Task<List<LogEntry>> GetLogsBetweenDates(DateTime start, DateTime end);
    Task<List<LogEntry>> GetLogsByUserId(Guid userId);
    Task<List<LogEntry>> GetLogsByActionId(int actionId);
    Task<List<LogAction>> GetAllLogActions();
    Task<LogAction?> GetLogActionById(int id);
    Task AddLogAction(LogAction action);
    Task UpdateLogAction(LogAction action);
    Task DeleteLogAction(int id);
}
