using InfinityMeshTask.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InfinityMeshTask.Controllers
{
    public class InfinityMeshTaskController : Controller
    {
        private IHostingEnvironment Environment;
        public InfinityMeshTaskController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult Index()
        {
            return View(new List<CityVM>());
        }
        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            string path = null;
            if (file != null)
            {
                string fileName = $"{this.Environment.WebRootPath}\\files\\{file.FileName}";
                using (FileStream fileStream = System.IO.File.Create(fileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }


                path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + file.FileName;
            }




            List<CityVM> gradovi = new List<CityVM>();


            if (path != null)
            {
                string[] lines = System.IO.File.ReadAllLines(path);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] line = lines[i].Split(",");
                    gradovi.Add(new CityVM
                    {
                        Name = line[0]
                    });



                    List<CandidateVM> kandidati = new List<CandidateVM>();
                    decimal prosjek = 0;


                    for (int j = 1; j < line.Length; j++)
                    {
                        decimal suma = 0;



                        kandidati.Add(new CandidateVM()
                        {
                            Votes = line[j++],
                            Name = line[j],
                            Average = suma

                        });


                        foreach (var a in kandidati)
                        {
                            FullName(a);
                            suma += decimal.Parse(a.Votes);
                        }


                        prosjek = suma;

                        foreach (var e in kandidati)
                        {
                            e.Average = decimal.Parse(((decimal.Parse(e.Votes) / prosjek) * 100).ToString("0"));
                        }
                        gradovi[i].Candidate = kandidati;

                    }



                }
            }



            return View(gradovi);

        }

        private void FullName(CandidateVM e)
        {
            if (e.Name.Contains("DT"))
                e.Name = "Donald Trump";
            else if (e.Name.Contains("JB"))
                e.Name = "Joe Biden";
            else if (e.Name.Contains("HC"))
                e.Name = "Hillary Clinton";
            else if (e.Name.Contains("JFK"))
                e.Name = "John F. Kennedy";
            else if (e.Name.Contains("JR"))
                e.Name = "Jack Randall";
        }
    }
}

  