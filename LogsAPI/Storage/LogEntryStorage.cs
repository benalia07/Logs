using LogsAPI.Models;
using Microsoft.Data.SqlClient;

namespace LogsAPI.Storage;


    public class LogEntryStorage : ILogEntryStorage
{
    private readonly string _connectionString;

    public LogEntryStorage(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<LogEntry>> GetAllLogs()
    {
        var list = new List<LogEntry>();
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("SELECT * FROM log.ENTRIES", connection);
        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new LogEntry
            {
                Id = reader.GetGuid(0),
                UserId = reader.GetGuid(1),
                ActionId = reader.GetInt32(2),
                EntityId = reader.GetGuid(3),
                Message = reader.IsDBNull(4) ? null : reader.GetString(4),
                Date = reader.GetDateTime(5)
            });
        }
        return list;
    }

    public async Task<LogEntry?> GetLogById(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("SELECT * FROM log.ENTRIES WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);
        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new LogEntry
            {
                Id = reader.GetGuid(0),
                UserId = reader.GetGuid(1),
                ActionId = reader.GetInt32(2),
                EntityId = reader.GetGuid(3),
                Message = reader.IsDBNull(4) ? null : reader.GetString(4),
                Date = reader.GetDateTime(5)
            };
        }
        return null;
    }

    public async Task AddLog(LogEntry entry)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(
                "INSERT INTO log.ENTRIES (Id, UserId, ActionId, EntityId, Message, Date) VALUES (@Id, @UserId, @ActionId, @EntityId, @Message, @Date)",
                connection);

            command.Parameters.AddWithValue("@Id", entry.Id);
            command.Parameters.AddWithValue("@UserId", entry.UserId);
            command.Parameters.AddWithValue("@ActionId", entry.ActionId);
            command.Parameters.AddWithValue("@EntityId", entry.EntityId);
            command.Parameters.AddWithValue("@Message", (object?)entry.Message ?? DBNull.Value);
            command.Parameters.AddWithValue("@Date", entry.Date);

            await command.ExecuteNonQueryAsync();
        }
        catch (SqlException ex)
        {
            // Log the detailed error message somewhere, ex.Message
            throw; // Ou gérer autrement
        }
    }


    public async Task UpdateLog(LogEntry entry)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand(
            @"UPDATE log.ENTRIES SET UserId = @UserId, Action = @Action, 
              EntityId = @EntityId, Message = @Message, Date = @Date WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", entry.Id);
        command.Parameters.AddWithValue("@UserId", entry.UserId);
        command.Parameters.AddWithValue("@Action", entry.ActionId);
        command.Parameters.AddWithValue("@EntityId", entry.EntityId);
        command.Parameters.AddWithValue("@Message", (object?)entry.Message ?? DBNull.Value);
        command.Parameters.AddWithValue("@Date", entry.Date);
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteLog(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("DELETE FROM log.ENTRIES WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }
    public async Task<List<LogEntry>> GetLogsByDate(DateTime date)
{
    var list = new List<LogEntry>();
    using var connection = new SqlConnection(_connectionString);
    var command = new SqlCommand("SELECT * FROM log.ENTRIES WHERE Date = @Date", connection);
    command.Parameters.AddWithValue("@Date", date);
    await connection.OpenAsync();
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        list.Add(ReadLogEntry(reader));
    }
    return list;
}


public async Task<List<LogEntry>> GetLogsBetweenDates(DateTime? start, DateTime? end)
{
    var list = new List<LogEntry>();
    using var connection = new SqlConnection(_connectionString);

    var query = "SELECT * FROM log.ENTRIES WHERE 1=1";
    var command = new SqlCommand();
    command.Connection = connection;

    if (start.HasValue)
    {
        query += " AND Date >= @Start";
        command.Parameters.AddWithValue("@Start", start.Value);
    }

    if (end.HasValue)
    {
        query += " AND Date <= @End";
        command.Parameters.AddWithValue("@End", end.Value);
    }

    command.CommandText = query;

    await connection.OpenAsync();
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        list.Add(ReadLogEntry(reader));
    }

    return list;
}

public async Task<List<LogEntry>> GetLogsByUserId(Guid userId)
{
    var list = new List<LogEntry>();
    using var connection = new SqlConnection(_connectionString);
    var command = new SqlCommand("SELECT * FROM log.ENTRIES WHERE UserId = @UserId", connection);
    command.Parameters.AddWithValue("@UserId", userId);
    await connection.OpenAsync();
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        list.Add(ReadLogEntry(reader));
    }
    return list;
}

public async Task<List<LogEntry>> GetLogsByActionId(int actionId)
{
    var list = new List<LogEntry>();
    using var connection = new SqlConnection(_connectionString);
    var command = new SqlCommand("SELECT * FROM log.ENTRIES WHERE ActionId = @ActionId", connection);
    command.Parameters.AddWithValue("@ActionId", actionId);
    await connection.OpenAsync();
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        list.Add(ReadLogEntry(reader));
    }
    return list;
}

private LogEntry ReadLogEntry(SqlDataReader reader)
{
    return new LogEntry
    {
        Id = reader.GetGuid(0),
        UserId = reader.GetGuid(1),
        ActionId = reader.GetInt32(2),
        EntityId = reader.GetGuid(3),
        Message = reader.IsDBNull(4) ? null : reader.GetString(4),
        Date = reader.GetDateTime(5)
    };
}


  public async Task<List<LogAction>> GetAllLogActions()
    {
        var list = new List<LogAction>();
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("SELECT * FROM log.ACTIONS", connection);
        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new LogAction
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }
        return list;
    }

    public async Task<LogAction?> GetLogActionById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("SELECT * FROM log.ACTIONS WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);
        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new LogAction
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
        }
        return null;
    }

    public async Task AddLogAction(LogAction action)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("INSERT INTO log.ACTIONS (Id, Name) VALUES (@Id, @Name)", connection);
        command.Parameters.AddWithValue("@Id", action.Id);
        command.Parameters.AddWithValue("@Name", action.Name);
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateLogAction(LogAction action)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("UPDATE log.ACTIONS SET Name = @Name WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", action.Id);
        command.Parameters.AddWithValue("@Name", action.Name);
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteLogAction(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("DELETE FROM log.ACTIONS WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

}

