using LogsAPI.Models;
using LogsAPI.Storage;

namespace LogsAPI.Service;

public class LogEntryService : ILogEntryService
{
    private readonly ILogEntryStorage _storage;

    public LogEntryService(ILogEntryStorage storage)
    {
        _storage = storage;
    }

    public async Task<List<LogEntry>> GetAllLogs()
    {
        return await _storage.GetAllLogs();
    }

    public async Task<LogEntry?> GetLogById(Guid id)
    {
        return await _storage.GetLogById(id);
    }

    public async Task AddLog(LogEntry entry)
    {
        await _storage.AddLog(entry);
    }

    public async Task UpdateLog(LogEntry entry)
    {
        await _storage.UpdateLog(entry);
    }

 

    public async Task DeleteLog(Guid id)
    {
        await _storage.DeleteLog(id);
    }

    public async Task<List<LogEntry>> GetLogsByDate(DateTime date)
    {
        return await _storage.GetLogsByDate(date);
    }

    public async Task<List<LogEntry>> GetLogsBetweenDates(DateTime start, DateTime end)
    {
        return await _storage.GetLogsBetweenDates(start, end);
    }

    public async Task<List<LogEntry>> GetLogsByUserId(Guid userId)
    {
        return await _storage.GetLogsByUserId(userId);
    }

    public async Task<List<LogEntry>> GetLogsByActionId(int actionId)
    {
        return await _storage.GetLogsByActionId(actionId);
    }
    
    public async Task<List<LogAction>> GetAllLogActions()
    {
        return await _storage.GetAllLogActions();
    }

    public async Task<LogAction?> GetLogActionById(int id)
    {
        return await _storage.GetLogActionById(id);
    }

    public async Task AddLogAction(LogAction action)
    {
        await _storage.AddLogAction(action);
    }

    public async Task UpdateLogAction(LogAction action)
    {
        await _storage.UpdateLogAction(action);
    }

    public async Task DeleteLogAction(int id)
    {
        await _storage.DeleteLogAction(id);
    }
}
