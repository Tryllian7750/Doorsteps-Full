namespace ExperimentApi.Models
{
  
  public class FormResponse
    {
        
        public long Id { get; set; }      
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? QuestionAnswers {get; set;}
        public long ExpId {get; set;}
       //public virtual Experiment? Exp { get; set; }
        
    }
}