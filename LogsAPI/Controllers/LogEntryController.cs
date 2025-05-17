using LogsAPI.Models;
using LogsAPI.Service;


using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogsAPI.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class LogEntryController : ControllerBase
    {
        private readonly ILogEntryService _service;

        public LogEntryController(ILogEntryService service)
        {
            _service = service;
        }

        // GET: api/logentries
        [HttpGet]
        public async Task<ActionResult<List<LogEntry>>> GetAll()
        {
            var logs = await _service.GetAllLogs();
            return Ok(logs);
        }

        // GET: api/logentries/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LogEntry>> GetById(Guid id)
        {
            var log = await _service.GetLogById(id);
            if (log == null) return NotFound();
            return Ok(log);
        }

        // POST: api/logentries
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] LogEntry entry)
        {
            await _service.AddLog(entry);
            return CreatedAtAction(nameof(GetById), new { id = entry.Id }, entry);
        }

        // PUT: api/logentries/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] LogEntry entry)
        {
            if (id != entry.Id) return BadRequest("Id mismatch");
            await _service.UpdateLog(entry);
            return NoContent();
        }

        // DELETE: api/logentries/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _service.DeleteLog(id);
            return NoContent();
        }

        // GET: api/logentries/bydate/{date}
        [HttpGet("bydate/{date}")]
        public async Task<ActionResult<List<LogEntry>>> GetByDate(DateTime date)
        {
            var logs = await _service.GetLogsByDate(date);
            return Ok(logs);
        }

        // GET: api/logentries/betweendates?start=yyyy-MM-dd&end=yyyy-MM-dd
        [HttpGet("betweendates")]
        public async Task<ActionResult<List<LogEntry>>> GetBetweenDates([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var logs = await _service.GetLogsBetweenDates(start, end);
            return Ok(logs);
        }

        // GET: api/logentries/byuserid/{userId}
        [HttpGet("byuserid/{userId:guid}")]
        public async Task<ActionResult<List<LogEntry>>> GetByUserId(Guid userId)
        {
            var logs = await _service.GetLogsByUserId(userId);
            return Ok(logs);
        }

        // GET: api/logentries/byactionid/{actionId}
        [HttpGet("byactionid/{actionId:int}")]
        public async Task<ActionResult<List<LogEntry>>> GetByActionId(int actionId)
        {
            var logs = await _service.GetLogsByActionId(actionId);
            return Ok(logs);
        }


        // ----------- LogAction endpoints -----------

        // GET: api/logentries/actions
        [HttpGet("actions")]
        public async Task<ActionResult<List<LogAction>>> GetAllLogActions()
        {
            var actions = await _service.GetAllLogActions();
            return Ok(actions);
        }

        // GET: api/logentries/actions/{id}
        [HttpGet("actions/{id:int}")]
        public async Task<ActionResult<LogAction>> GetLogActionById(int id)
        {
            var action = await _service.GetLogActionById(id);
            if (action == null) return NotFound();
            return Ok(action);
        }

        // POST: api/logentries/actions
        [HttpPost("actions")]
        public async Task<ActionResult> AddLogAction([FromBody] LogAction action)
        {
            await _service.AddLogAction(action);
            return CreatedAtAction(nameof(GetLogActionById), new { id = action.Id }, action);
        }

        // PUT: api/logentries/actions/{id}
        [HttpPut("actions/{id:int}")]
        public async Task<ActionResult> UpdateLogAction(int id, [FromBody] LogAction action)
        {
            if (id != action.Id) return BadRequest("Id mismatch");
            await _service.UpdateLogAction(action);
            return NoContent();
        }

        // DELETE: api/logentries/actions/{id}
        [HttpDelete("actions/{id:int}")]
        public async Task<ActionResult> DeleteLogAction(int id)
        {
            await _service.DeleteLogAction(id);
            return NoContent();
        }
    }

