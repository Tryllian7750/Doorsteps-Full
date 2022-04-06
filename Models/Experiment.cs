using System.Text.Json.Serialization;
namespace ExperimentApi.Models
{
    public class Experiment
    {
        public Experiment()
        {
           Questions = new HashSet<Question>();
        }
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }      
        public bool enabled {get;set;} = true;
     
        public virtual ICollection<Question>? Questions { get; set; }
        //public virtual ICollection<FormResponse>? FormResponses { get; set; }
    }


  
}