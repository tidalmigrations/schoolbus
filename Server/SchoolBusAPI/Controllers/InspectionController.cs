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
    public class InspectionController : ControllerBase
    {
        private readonly IInspectionService _service;

        /// <summary>
        /// Create a controller and set the service
        /// </summary>
        public InspectionController(IInspectionService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <response code="201">Inspection created</response>
        [HttpPost]
        [Route("/api/inspections/bulk")]
        [RequiresPermission(Permissions.SchoolBusWrite)]
        public virtual IActionResult InspectionsBulkPost([FromBody]Inspection[] items)
        {
            return this._service.InspectionsBulkPostAsync(items);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of Inspection to delete</param>
        /// <response code="200">OK</response>
        /// <response code="404">Inspection not found</response>
        [HttpPost]
        [Route("/api/inspections/{id}/delete")]
        [RequiresPermission(Permissions.SchoolBusWrite)]
        public virtual IActionResult InspectionsIdDeletePost([FromRoute]int id)
        {
            return this._service.InspectionsIdDeletePostAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of Inspection to fetch</param>
        /// <response code="200">OK</response>
        /// <response code="404">Inspection not found</response>
        [HttpGet]
        [Route("/api/inspections/{id}")]
        [RequiresPermission(Permissions.SchoolBusRead)]
        public virtual IActionResult InspectionsIdGet([FromRoute]int id)
        {
            return this._service.InspectionsIdGetAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of Inspection to fetch</param>
        /// <param name="item"></param>
        /// <response code="200">OK</response>
        /// <response code="404">Inspection not found</response>
        [HttpPut]
        [Route("/api/inspections/{id}")]
        [RequiresPermission(Permissions.SchoolBusWrite)]
        public virtual IActionResult InspectionsIdPut([FromRoute]int id, [FromBody]Inspection item)
        {
            return this._service.InspectionsIdPutAsync(id, item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <response code="201">Inspection created</response>
        [HttpPost]
        [Route("/api/inspections")]
        [RequiresPermission(Permissions.SchoolBusWrite)]
        public virtual IActionResult InspectionsPost([FromBody]Inspection item)
        {
            return this._service.InspectionsPostAsync(item);
        }
    }
}
