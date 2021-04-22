using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPIAss.Models;

namespace WebAPIAss.Controllers
{
    public class EmployeeController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Employees
        public IQueryable<EmployeesTable> GetEmployeesTables()
        {
            return db.EmployeesTables;
        }

        // GET: api/Employees/5
        [ResponseType(typeof(EmployeesTable))]
        public IHttpActionResult GetEmployeesTable(int id)
        {
            EmployeesTable employeesTable = db.EmployeesTables.Find(id);
            if (employeesTable == null)
            {
                return NotFound();
            }

            return Ok(employeesTable);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeesTable(int id, EmployeesTable employeesTable)
        {
          
            if (id != employeesTable.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employeesTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [ResponseType(typeof(EmployeesTable))]
        public IHttpActionResult PostEmployeesTable(EmployeesTable employeesTable)
        {
            

            db.EmployeesTables.Add(employeesTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeesTable.EmployeeID }, employeesTable);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(EmployeesTable))]
        public IHttpActionResult DeleteEmployeesTable(int id)
        {
            EmployeesTable employeesTable = db.EmployeesTables.Find(id);
            if (employeesTable == null)
            {
                return NotFound();
            }

            db.EmployeesTables.Remove(employeesTable);
            db.SaveChanges();

            return Ok(employeesTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeesTableExists(int id)
        {
            return db.EmployeesTables.Count(e => e.EmployeeID == id) > 0;
        }
    }
}