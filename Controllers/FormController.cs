using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExperimentApi.Models;
using Newtonsoft.Json;
using System.Web;
using System.Net.Http;
using System.Text;

namespace ExperimentApi.Controllers
{
    public class questiondata{
            public string ID {get; set;}
            public string Name {get;set;}
            public string Answer {get;set;} 
        }
    
    public class FormController : Controller
   
    {
        //private readonly ExperimentContext _context;
          private readonly ExperimentContext _context;

        public FormController(ExperimentContext context)
        {
            _context = context;
         
        }

      
         public string Index()
        {
       
            return "This is my default action...";
        }

        

        public async Task<ActionResult> DisplayForm(long id) 
        {
           
            if (Request.Method == "POST")
            {
                FormResponse res = new FormResponse();
                try {
                res.Name = HttpContext.Request.Form["Name"];
                res.Email = HttpContext.Request.Form["Email"];
                res.Phone = HttpContext.Request.Form["Phone"];
                res.ExpId = id;

                 
                List<questiondata> qlist = new List<questiondata>();
                foreach(string key in HttpContext.Request.Form.Keys) {
                   
                    if (key.StartsWith("Quest")){
                         questiondata qdata= new questiondata();
                        qdata.ID = key.Replace("Quest","");
                        qdata.Name = key;
                        qdata.Answer =  HttpContext.Request.Form[key];
                        qlist.Add(qdata);
                    }
                
                }


                res.QuestionAnswers = JsonConvert.SerializeObject(qlist);
                var json = JsonConvert.SerializeObject(res);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "http://localhost:5000/api/Experiment/AddAnswers";
                using var client = new HttpClient();

                var response = await client.PostAsync(url, data);

                var result = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex){
                    return View("~/Views/Shared/Error.cshtml");
                }

               
                return View("~/Views/Form/Thanks.cshtml");
            // The action is a POST.
            }
            else{  
                 Experiment exp = new Experiment();  
            try
            {

                       
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5000/api/Experiment/GetExperiment/"+id))
                    {
                        Console.Write(response);
                        if (response!=null)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            exp = JsonConvert.DeserializeObject<Experiment>(apiResponse);
                        }
                 
                    
                }
            }
            }
            catch (Exception ex)
            {
                 return View("~/Views/Form/Disabled.cshtml");
            }
            return View(exp);
           
            
            }
            
            
        }


         [HttpPost]
         public async Task<ActionResult> DisplayFormPost(long id) 
        {
            Experiment exp = new Experiment();
            using (var httpClient = new HttpClient())
            {
               // using (var response = await httpClient.GetAsync("http://localhost:5000/api/Experiment/"+id))
               // {
               //     string apiResponse = await response.Content.ReadAsStringAsync();
               //     exp = JsonConvert.DeserializeObject<Experiment>(apiResponse);
               //     Console.Write(JsonConvert.SerializeObject(exp));
               // }
            }
            Console.Write(HttpContext.Request.Form["Name"]);
            return View(exp);
            
        }
    }

}