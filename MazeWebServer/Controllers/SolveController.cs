using Server.Entitys;
using Server.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using SearchAlgorithmsLib;

namespace Server.Controllers
{
    [Route("api/Solve")]
    public class SolveController : Controller
    {
        private SolveMazeModel smModel;
        public SolveController()
        {
            this.smModel = new SolveMazeModel();
        }
        [HttpGet]
        public IActionResult Get(SolveParams value)
        {
            Algorithm algo = Algorithm.BFS;
            if(value.AlgoSelector == 1)
            {
                algo = Algorithm.BFS;
            } else
            {
                algo = Algorithm.DFS;
            }
            Solution<MazeLib.Position> sol = this.smModel.Solve(value.Name, algo);
            string directions = MazeAdapter.AdaptSolution.ToDirection(sol);
            JObject jobj = new JObject();
            jobj["Name"] = value.Name;
            jobj["Solution"] = directions.ToString();
            jobj["NodesEvaluated"] = sol.EvaluatedNodes;
            return Ok(jobj);
        }
    }
}
