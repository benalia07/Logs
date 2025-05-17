using Managers.Models;
namespace Managers.Managers;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class LogEntryApiManager : ILogEntryApiManager
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:5216/api/logentry"; // ajuste selon l'URL réelle

    public LogEntryApiManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET all log entries
    public async Task<List<LogEntry>?> GetAllLogsAsync()
    {
        var response = await _httpClient.GetAsync(BaseUrl);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<List<LogEntry>>();

        return null;
    }

    // GET log by id
    public async Task<LogEntry?> GetLogByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<LogEntry>();

        return null;
    }

    // POST log
    public async Task<bool> AddLogAsync(LogEntry entry)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, entry);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    // PUT log
    public async Task<bool> UpdateLogAsync(Guid id, LogEntry entry)
    {
        if (id != entry.Id)
        {
            return false;
        }

        var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", entry);
        if (response.IsSuccessStatusCode)
            return true;

        return false;
    }

    // DELETE log
    public async Task<bool> DeleteLogAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        if (response.IsSuccessStatusCode)
            return true;

        return false;
    }

    // GET logs by date
    public async Task<List<LogEntry>?> GetLogsByDateAsync(DateTime date)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/bydate/{date:yyyy-MM-dd}");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<List<LogEntry>>();

        return null;
    }

    // GET logs between dates
    public async Task<List<LogEntry>?> GetLogsBetweenDatesAsync(DateTime start, DateTime end)
    {
        var url = $"{BaseUrl}/betweendates?start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}";
        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<List<LogEntry>>();

        return null;
    }

    // GET logs by user ID
    public async Task<List<LogEntry>?> GetLogsByUserIdAsync(Guid userId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/byuserid/{userId}");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<List<LogEntry>>();

        return null;
    }

    // GET logs by action ID
    public async Task<List<LogEntry>?> GetLogsByActionIdAsync(int actionId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/byactionid/{actionId}");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<List<LogEntry>>();

        return null;
    }

    // GET all log actions
    public async Task<List<LogAction>?> GetAllLogActionsAsync()
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/actions");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<List<LogAction>>();

        return null;
    }

    // GET log action by ID
    public async Task<LogAction?> GetLogActionByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/actions/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<LogAction>();

        return null;
    }

    // POST log action
    public async Task<bool> AddLogActionAsync(LogAction action)
    {
        var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/actions", action);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    // PUT log action
    public async Task<bool> UpdateLogActionAsync(int id, LogAction action)
    {
        if (id != action.Id)
        {
            return false;
        }

        var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/actions/{id}", action);
        if (response.IsSuccessStatusCode)
            return true;

        return false;
    }

    // DELETE log action
    public async Task<bool> DeleteLogActionAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/actions/{id}");
        if (response.IsSuccessStatusCode)
            return true;

        return false;
    }
}
