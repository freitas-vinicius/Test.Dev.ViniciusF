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
    [Route("v1/breakfest")]

    public class BreakfestController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Breakfest>>> Get([FromServices] DataContext context)
        {
            var breakfest = await context.Breakfests.AsNoTracking().ToListAsync();
            return breakfest;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Breakfest>> GetById([FromServices] DataContext context, int id)
        {
            var breakfest = await context.Breakfests.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return breakfest;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Breakfest>> Post(
            [FromServices] DataContext context,
            [FromBody] Breakfest model)
        {
            if (ModelState.IsValid)
            {
                context.Breakfests.Add(model);
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

        public async Task<ActionResult<Breakfest>> Delete([FromServices] DataContext context,
            int id)
        {
            var breakfest = await context.Breakfests.FirstOrDefaultAsync(x => x.Id == id);
            if (breakfest == null)
                return NotFound(new { message = "Espaço não encontrada!" });

            try
            {
                context.Breakfests.Remove(breakfest);
                await context.SaveChangesAsync();
                return breakfest;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover este espaço!" });

            }
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<ActionResult<Breakfest>> Put(
            [FromServices] DataContext context,
            int id,
            [FromBody] Breakfest model)
        {
            if (id != model.Id)
                return NotFound(new { message = "Espaço não encontrado!" });

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
