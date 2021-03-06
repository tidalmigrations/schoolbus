/*
 * REST API Documentation for the MOTI School Bus Application
 *
 * The School Bus application tracks that inspections are performed in a timely fashion. For each school bus the application tracks information about the bus (including data from ICBC, NSC, etc.), it's past and next inspection dates and results, contacts, and the inspector responsible for next inspecting the bus.
 *
 * OpenAPI spec version: v1
 * 
 * 
 */

using Microsoft.AspNetCore.Mvc;
using SchoolBusAPI.Models;
using SchoolBusAPI.Services;
using SchoolBusAPI.Authorization;

namespace SchoolBusAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _service;

        /// <summary>
        /// Create a controller and set the service
        /// </summary>
        public NoteController(INoteService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <response code="201">Note created</response>
        [HttpPost]
        [Route("/api/notes/bulk")]
        [RequiresPermission(Permissions.SchoolBusWrite, Permissions.OwnerWrite)]
        public virtual IActionResult NotesBulkPost([FromBody]Note[] items)
        {
            return this._service.NotesBulkPostAsync(items);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of Note to delete</param>
        /// <response code="200">OK</response>
        /// <response code="404">Note not found</response>
        [HttpPost]
        [Route("/api/notes/{id}/delete")]
        [RequiresPermission(Permissions.SchoolBusWrite, Permissions.OwnerWrite)]
        public virtual IActionResult NotesIdDeletePost([FromRoute]int id)
        {
            return this._service.NotesIdDeletePostAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of Note to fetch</param>
        /// <response code="200">OK</response>
        /// <response code="404">Note not found</response>
        [HttpGet]
        [Route("/api/notes/{id}")]
        [RequiresPermission(Permissions.SchoolBusRead, Permissions.OwnerRead)]
        public virtual IActionResult NotesIdGet([FromRoute]int id)
        {
            return this._service.NotesIdGetAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of Note to fetch</param>
        /// <param name="item"></param>
        /// <response code="200">OK</response>
        /// <response code="404">Note not found</response>
        [HttpPut]
        [Route("/api/notes/{id}")]
        [RequiresPermission(Permissions.SchoolBusWrite, Permissions.OwnerWrite)]
        public virtual IActionResult NotesIdPut([FromRoute]int id, [FromBody]Note item)
        {
            return this._service.NotesIdPutAsync(id, item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <response code="201">Note created</response>
        [HttpPost]
        [Route("/api/notes")]
        [RequiresPermission(Permissions.SchoolBusWrite, Permissions.OwnerWrite)]
        public virtual IActionResult NotesPost([FromBody]Note item)
        {
            return this._service.NotesPostAsync(item);
        }
    }
}
