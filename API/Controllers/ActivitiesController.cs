using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Application.Activities;
using System.Threading;

namespace API.Controllers
{
      public class ActivitiesController : BaseApiController
      {
            [HttpGet]  //get all activities in the database
            public async Task<ActionResult<List<Activity>>> GetActivities()
            {
                  return await Mediator.Send(new List.Query());
            }

            [HttpGet("{id}")] //activities by id
            public async Task<ActionResult<Activity>> GetActivity(Guid id)
            {
                  return await Mediator.Send(new Details.Query{Id = id});
            }

            [HttpPost]  //create a new activity

            public async Task<IActionResult> CreateActivity(Activity activity)
            {
                  return Ok(await Mediator.Send(new Create.Command {Activity = activity}));
            }

            [HttpPut("{id}")] //update a particular activity

            public async Task<IActionResult> EditActivity(Guid id, Activity activity)
            {
                  activity.Id = id;
                  return Ok(await Mediator.Send(new Edit.Command {Activity = activity})); 
            }

            [HttpDelete("{id}")]    //delete a particular activity
            public async Task<IActionResult> DeleteActivity(Guid id)
            {
                  return Ok(await Mediator.Send(new Delete.Command {Id = id}));
            }
      }
}
