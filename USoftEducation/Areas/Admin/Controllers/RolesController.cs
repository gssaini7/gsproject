using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using USoftEducation.Models;

namespace USoftEducation.Areas.Admin.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Roles")]
    public class RolesController : ApiController
    {
       
        public RolesController()
        {}

        

        // GET api/roles
        public IHttpActionResult Get()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var result = roleManager.Roles;
            return Ok(result);
        }

        // GET api/roles/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/roles
        public IHttpActionResult Post(RolesModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            IdentityResult result;
            if (roleManager.RoleExists(model.RoleName) == false)
            {
                Guid guid = Guid.NewGuid();
                result = roleManager.Create(new IdentityRole() { Id = guid.ToString(), Name = model.RoleName });
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
            }
            else {
                return BadRequest("Role Already Existed.");
            }


            //    var role = new IdentityRole(model.RoleName);
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            //if (roleManager.RoleExists(model.RoleName) == false)
            //{
            //    Guid guid = Guid.NewGuid();
            //    var roleresult=  roleManager.Create(new IdentityRole() { Id = guid.ToString(), Name = model.RoleName });
            //    if (!roleresult.Succeeded)
            //    {
            //        return GetErrorResult(roleresult);
            //    }
            //}
            return Ok();


            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //var user = new ApplicationUser() { UserName = model.RoleName};

            //IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            //if (!result.Succeeded)
            //{
            //    return GetErrorResult(result);
            //}

            //return Ok();
        }

        // PUT api/roles/5
        public IHttpActionResult Put(string id, RolesModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            IdentityResult result = null;

            var currentrole = roleManager.FindById(id);
            if (currentrole.Name != model.RoleName)
            {
                if (roleManager.RoleExists(model.RoleName) == false)
                {
                    result = roleManager.Update(new IdentityRole() { Name = model.RoleName, Id = id });
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
                else
                {
                    return BadRequest("Role Already Existed.");
                }
            }

            return GetErrorResult(result);
        }

        // DELETE api/roles/5
        public void Delete(int id)
        {
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
