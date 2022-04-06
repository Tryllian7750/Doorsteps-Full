using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExperimentApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ExperimentApi.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExperimentController : ControllerBase
   
    {
        private readonly ExperimentContext _context;

        public ExperimentController(ExperimentContext context)
        {
            _context = context;
         
        }

        // GET: api/Experiment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Experiment>>> GetExperiment()
        {   

            
            //show only enable questions
            return await _context.Experiments.Where(s => s.enabled == true).ToListAsync();
            
            
        }

        // GET: api/Experiment/5
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Experiment>> GetExperiment(long id)
        {
            var Experiment = await _context.Experiments.SingleAsync(p => p.Id == id && p.enabled) ;

            _context.Entry(Experiment)
                    .Collection(s => s.Questions)
                    .Load();

            if (Experiment == null)
            {
                return NotFound();
            }

            return Experiment;
        }
        

        // Edit and experiment
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperiment(long id, Experiment Experiment)
        {
            if (id != Experiment.Id)
            {
                return BadRequest();
            }

            _context.Entry(Experiment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExperimentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        

        // POST: api/Experiment
        // add a new Experiment
        
        [HttpPost]
        public async Task<ActionResult<Experiment>> PostExperiment([FromQuery] Experiment experiment)
        {
           
             try
            {
            _context.Experiments.Add(experiment);
            await _context.SaveChangesAsync();
            }
             catch (DbUpdateConcurrencyException ex)
            {
                return CreatedAtAction("Databaseerror", new { error = ex.StackTrace });
            }
            return CreatedAtAction(nameof(GetExperiment), new { id = experiment.Id }, experiment);
        }
       


        //Add Responses to Questions
         [HttpPost]
        public async Task<ActionResult<Experiment>> AddAnswers(FormResponse Formresponse)
        {
              try
            {
            _context.ForemResponses.Add(Formresponse);
            await _context.SaveChangesAsync();
            } 
            catch (DbUpdateConcurrencyException ex)
            {
                return CreatedAtAction("Databaseerror", new { error = ex.StackTrace });
            }
            return CreatedAtAction("Response", new { id =  Formresponse.Id}, Formresponse);
        }

        // DELETE: api/experiment/5
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperiment(long id)
        {
            var Experiment = await _context.Experiments.FindAsync(id);
            if (Experiment == null)
            {
                return NotFound();
            }
           
           //not deleting the record just flagging it disable 
           //weird condition statement gave error here on compile
            if (Experiment.enabled == true )
            { 
                Experiment.enabled = false; 
            }
            else
            { 
                Experiment.enabled = true;             
            }


            _context.Entry(Experiment).State = EntityState.Modified;
           // _context.Experiments.Delete(Experiment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
       

        private bool ExperimentExists(long id)
        {
            return _context.Experiments.Any(e => e.Id == id);
        }
    }
}