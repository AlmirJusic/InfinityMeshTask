using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfinityMeshTask.ViewModels
{
    public class CityVM
    {
        public string Name { get; set; }
        public List<CandidateVM> Candidate { get; set; }
    }
}
