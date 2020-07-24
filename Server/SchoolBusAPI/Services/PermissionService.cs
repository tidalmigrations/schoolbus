/*
 * REST API Documentation for the MOTI School Bus Application
 *
 * The School Bus application tracks that inspections are performed in a timely fashion. For each school bus the application tracks information about the bus (including data from ICBC, NSC, etc.), it's past and next inspection dates and results, contacts, and the inspector responsible for next inspecting the bus.
 *
 * OpenAPI spec version: v1
 * 
 * 
 */

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SchoolBusAPI.Models;
using SchoolBusAPI.ViewModels;
using SchoolBusAPI.Mappings;

namespace SchoolBusAPI.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <response code="201">Permission created</response>
        IActionResult PermissionsBulkPostAsync(Permission[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200">OK</response>
        IActionResult PermissionsGetAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of Permission to delete</param>
        /// <response code="200">OK</response>
        /// <response code="404">Permission not found</response>
        IActionResult PermissionsIdDeletePostAsync(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of Permission to fetch</param>
        /// <response code="200">OK</response>
        /// <response code="404">Permission not found</response>
        IActionResult PermissionsIdGetAsync(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of Permission to fetch</param>
        /// <param name="item"></param>
        /// <response code="200">OK</response>
        /// <response code="404">Permission not found</response>
        IActionResult PermissionsIdPutAsync(int id, PermissionViewModel item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <response code="201">Permission created</response>
        IActionResult PermissionsPostAsync(PermissionViewModel item);
    }

    /// <summary>
    /// 
    /// </summary>
    public class PermissionService : IPermissionService
    {
        private readonly DbAppContext _context;

        /// <summary>
        /// Create a service and set the database context
        /// </summary>
        public PermissionService(DbAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <response code="201">Permissions created</response>
        public virtual IActionResult PermissionsBulkPostAsync(Permission[] items)
        {
            if (items == null)
            {
                return new BadRequestResult();
            }
            foreach (Permission item in items)
            {
                var exists = _context.Permissions.Any(a => a.Id == item.Id);
                if (exists)
                {
                    _context.Permissions.Update(item);
                }
                else
                {
                    _context.Permissions.Add(item);
                }
            }

            // Save the changes
            _context.SaveChanges();
            return new NoContentResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Returns a collection of permissions</remarks>
        /// <response code="200">OK</response>
        public virtual IActionResult PermissionsGetAsync()
        {
            var result = _context.Permissions.Select(x => x.ToViewModel()).ToList();
            return new ObjectResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of Permission to delete</param>
        /// <response code="200">OK</response>
        /// <response code="404">Permission not found</response>
        public virtual IActionResult PermissionsIdDeletePostAsync(int id)
        {
            var permission = _context.Permissions.FirstOrDefault(x => x.Id == id);
            if (permission == null)
            {
                // Not Found
                return new StatusCodeResult(404);
            }
            // remove any user role associations.            
            var toRemove = _context.RolePermissions.Where(x => x.Permission.Id == id).ToList();
            toRemove.ForEach(x => _context.RolePermissions.Remove(x));

            _context.Permissions.Remove(permission);
            _context.SaveChanges();
            return new ObjectResult(permission.ToViewModel());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Returns a permission</remarks>
        /// <param name="id">id of Permission to fetch</param>
        /// <response code="200">OK</response>
        /// <response code="404">Permission not found</response>
        public virtual IActionResult PermissionsIdGetAsync(int id)
        {
            var permission = _context.Permissions.FirstOrDefault(x => x.Id == id);
            if (permission == null)
            {
                // Not Found
                return new StatusCodeResult(404);
            }
            return new ObjectResult(permission.ToViewModel());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of Permission to update</param>
        /// <param name="item"></param>
        /// <response code="200">OK</response>
        /// <response code="404">Permission not found</response>
        public virtual IActionResult PermissionsIdPutAsync(int id, PermissionViewModel item)
        {
            var permission = _context.Permissions.FirstOrDefault(x => x.Id == id);
            if (permission == null)
            {
                // Not Found
                return new StatusCodeResult(404);
            }

            permission.Code = item.Code;
            permission.Description = item.Code;
            permission.Name = item.Name;

            // Save changes
            _context.Permissions.Update(permission);
            _context.SaveChanges();
            return new ObjectResult(permission.ToViewModel());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <response code="201">Permission created</response>
        public virtual IActionResult PermissionsPostAsync(PermissionViewModel item)
        {
            if (item == null)
            {
                // Not Found
                return new StatusCodeResult(400);
            }
            var permission = new Permission();

            permission.Code = item.Code;
            permission.Description = item.Code;
            permission.Name = item.Name;

            // Save changes
            _context.Permissions.Add(permission);
            _context.SaveChanges();
            return new ObjectResult(permission);
        }
    }
}