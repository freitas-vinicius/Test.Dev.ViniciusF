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
    [Route("v1/studant")]

    public class StudantController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Studant>>> Get([FromServices] DataContext context)
        {
            var studant = await context.Studants.AsNoTracking().ToListAsync();
            return studant;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Studant>> GetById([FromServices] DataContext context, int id)
        {
            var studant = await context.Studants.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return studant;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Studant>> Post(
            [FromServices] DataContext context,
            [FromBody] Studant model)
        {
            if (ModelState.IsValid)
            {
                context.Studants.Add(model);
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

        public async Task<ActionResult<Studant>> Delete([FromServices] DataContext context,
            int id)
        {
            var studant = await context.Studants.FirstOrDefaultAsync(x => x.Id == id);
            if (studant == null)
                return NotFound(new { message = "Aluno não encontrado!" });

            try
            {
                context.Studants.Remove(studant);
                await context.SaveChangesAsync();
                return studant;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover este aluno!" });

            }
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<ActionResult<Studant>> Put(
            [FromServices] DataContext context,
            int id,
            [FromBody] Studant model)
        {
            if (id != model.Id)
                return NotFound(new { message = "Aluno não encontrado!" });

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
