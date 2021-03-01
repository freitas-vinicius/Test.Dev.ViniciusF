using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Dev.ViniciusF.Data;
using Test.Dev.ViniciusF.Models;

namespace Test.Dev.ViniciusF.Controllers
{
    [ApiController]
    [Route("v1/room")]

    public class RoomController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Room>>> Get([FromServices] DataContext context)
        {
            var room = await context.Rooms.AsNoTracking().ToListAsync();
            return room;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Room>> GetById([FromServices] DataContext context, int id)
        {
            var room = await context.Rooms.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return room;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Room>> Post(
            [FromServices] DataContext context,
            [FromBody] Room model)
        {
            if (ModelState.IsValid)
            {
                context.Rooms.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<ActionResult<Room>> Delete([FromServices] DataContext context,
            int id)
        {
            var room = await context.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            if (room == null)
                return NotFound(new { message = "Sala não encontrada!" });

            try
            {
                context.Rooms.Remove(room);
                await context.SaveChangesAsync();
                return room;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover esta sala!" });

            }
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<ActionResult<Room>> Put(
            [FromServices] DataContext context,
            int id,
            [FromBody] Room model)
        {
            if (id != model.Id)
                return NotFound(new { message = "Sala não encontrada!" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Não foi possível atualizar este cadastro!" });

            }
        }
    }
}
