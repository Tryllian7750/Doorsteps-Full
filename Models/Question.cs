  namespace ExperimentApi.Models
{
  
  public class Question
    {
        
        public long QuestionId { get; set; }      
        public string? QuestionName { get; set; }
        public string? QuestionType { get; set; }
        public string? QuestionItemList { get; set; }
        public long ExpId {get; set;}
        public virtual Experiment Exp { get; set; }
        
    }
}