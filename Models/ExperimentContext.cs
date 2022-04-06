using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ExperimentApi.Models
{
    public class ExperimentContext : DbContext
    {
        public ExperimentContext(DbContextOptions<ExperimentContext> options)
            : base(options)
        {
        }

        public DbSet<Experiment>? Experiments { get; set; }
        public DbSet<Question>? Questions { get; set; }
        public DbSet<FormResponse>? ForemResponses { get; set; }
      

         protected override void OnModelCreating(ModelBuilder builder)
        {

              builder.Entity<Question>(entity => {

                entity.HasOne(d => d.Exp)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.ExpId)
                    .HasConstraintName("FK__QUestion__Experiment_id__asdE");
        
              });
            
            builder.Entity<Experiment>().HasData(new Experiment {Id = 1, Name = "Experiment 1", Description = "This is description for Experiment 1"});

           builder.Entity<Question>().HasData(
                new Question{ QuestionId = 1, ExpId = 1, QuestionName = "Make up Sum Question 1", QuestionType = "text", QuestionItemList = "Answer Here" },
                new Question{ QuestionId = 2, ExpId = 1, QuestionName = "Make up Sum Question 2", QuestionType  = "text/multi", QuestionItemList = "Answer Here" },
                new Question{ QuestionId = 3, ExpId = 1, QuestionName = "Make up Sum Question 3", QuestionType  = "text/select", QuestionItemList = "item 1, item 2, item 3" }
                );
            
        }
    }
}
