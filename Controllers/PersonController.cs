//Created by: Jordan Hurst
//On: 11/3/2020
//This program makes CRUD calls to a local SQL database
//This program acts as a backend for a ReactJS app
using NABABackend.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace NABABackend.Controllers
{
    public class PersonController : ApiController
    {
        [RoutePrefix("Api/User")]
        public class UserController : ApiController
        {
            NABAPersonEntities objEntity = new NABAPersonEntities();

            [HttpGet]
            [Route("GetUserDetails")]
            public IQueryable<UserDetail> GetEmaployee()
            {
                try
                {
                    return objEntity.UserDetails;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            [HttpGet]
            [Route("GetUserDetailsById/{userId}")]
            public IHttpActionResult GetUserById(string userId)
            {
                UserDetail objUser = new UserDetail();
                int ID = Convert.ToInt32(userId);
                try
                {
                    objUser = objEntity.UserDetails.Find(ID);
                    if (objUser == null)
                    {
                        return NotFound();
                    }

                }
                catch (Exception)
                {
                    throw;
                }

                return Ok(objUser);
            }

            [HttpPost]
            [Route("InsertUserDetails")]
            public IHttpActionResult PostUser(UserDetail data)
            {
                string message = "";
                if (data != null)
                {

                    try
                    {
                        objEntity.UserDetails.Add(data);
                        int result = objEntity.SaveChanges();
                        if (result > 0)
                        {
                            message = "Person has been successfully added";
                        }
                        else
                        {
                            message = "Person has failed to be added";
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                return Ok(message);
            }

            [HttpPut]
            [Route("UpdateEmployeeDetails")]
            public IHttpActionResult PutUserMaster(UserDetail user)
            {
                string message = "";
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    UserDetail objUser = new UserDetail();
                    objUser = objEntity.UserDetails.Find(user.UserId);
                    if (objUser != null)
                    {
                        objUser.FirstName = user.FirstName;
                        objUser.LastName = user.LastName;
                        objUser.EmailId = user.EmailId;
                        objUser.MobileNo = user.MobileNo;
                    }

                    int result = objEntity.SaveChanges();
                    if (result > 0)
                    {
                        message = "Person has been successfully updated";
                    }
                    else
                    {
                        message = "Person has failed to be updated";
                    }

                }
                catch (Exception)
                {
                    throw;
                }

                return Ok(message);
            }

            [HttpDelete]
            [Route("DeleteUserDetails/{id}")]
            public IHttpActionResult DeleteUserDelete(int id)
            {
                string message = "";
                UserDetail user = objEntity.UserDetails.Find(id);
                if (user == null)
                {
                    return NotFound();
                }

                objEntity.UserDetails.Remove(user);
                int result = objEntity.SaveChanges();
                if (result > 0)
                {
                    message = "Person has been successfully deleted";
                }
                else
                {
                    message = "Person has failed to be deleted";
                }

                return Ok(message);
            }
        }
    }
}
