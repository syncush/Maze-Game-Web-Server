using MazeWebServer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MazeLib;
using System.Net.Http;
using MazeWebServer.Models;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace MazeWebServer.Controllers
{
    public class GenerateMazeController : ApiController
    {
        private GenerateMazeModel gmModel;
        public GenerateMazeController()
        {
            this.gmModel = new GenerateMazeModel();
        }

        [HttpPost]
        [Route("api/GenerateMaze/Get")]
        public IHttpActionResult Get(MazeParams value)
        {
            Maze maze = this.gmModel.GenerateMaze(value.Name, value.Rows, value.Cols);
            JObject jobj = JObject.Parse(maze.ToJSON());
            return Ok(jobj);
        }
    }
}
