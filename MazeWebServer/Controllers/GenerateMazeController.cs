using Server.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MazeLib;
using System.Net.Http;
using Server.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/GenerateMaze")]
    public class GenerateMazeController : Controller
    {
        private GenerateMazeModel gmModel;
        public GenerateMazeController()
        {
            this.gmModel = new GenerateMazeModel();
        }

        // POST: api/GenerateMaze
        [HttpPost]
        public IActionResult Post(MazeParams value)
        {
            Maze maze = this.gmModel.GenerateMaze(value.Name, value.Rows, value.Cols);
            //JObject jobj = JObject.Parse(maze.ToJSON());
            return new ObjectResult(maze);
        }
        [HttpGet]
        public IActionResult Get(MazeParams value)
        {
            Maze maze = this.gmModel.GenerateMaze(value.Name, value.Rows, value.Cols);
            JObject jobj = JObject.Parse(maze.ToJSON());
            return Ok(jobj);
        }


    }
}
