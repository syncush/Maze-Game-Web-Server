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

namespace MazeWebServer.Controllers
{
    [Route("api/GenerateMaze")]
    public class GenerateMazeController : ApiController
    {
        private GenerateMazeModel gmModel;
        public GenerateMazeController()
        {
            this.gmModel = new GenerateMazeModel();
        }

        // POST: api/GenerateMaze
        [HttpPost]
        public IHttpActionResult Post(MazeParams value)
        {
            Maze maze = this.gmModel.GenerateMaze(value.Name, value.Rows, value.Cols);
            //JObject jobj = JObject.Parse(maze.ToJSON());
            return new (maze);
        }
        [HttpGet]
        public IHttpActionResult Get(MazeParams value)
        {
            Maze maze = this.gmModel.GenerateMaze(value.Name, value.Rows, value.Cols);
            JObject jobj = JObject.Parse(maze.ToJSON());
            return Ok(jobj);
        }


    }
}
