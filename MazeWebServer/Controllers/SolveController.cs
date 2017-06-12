using MazeWebServer.Entitys;
using MazeWebServer.Models;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using System.Web.Http;

namespace MazeWebServer.Controllers
{
    public class SolveController : ApiController
    {
        private SolveMazeModel smModel;
        public SolveController()
        {
            this.smModel = new SolveMazeModel();
        }
        [HttpPost]
        [Route("api/Solve/Get")]
        public IHttpActionResult Get(SolveParams value)
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
