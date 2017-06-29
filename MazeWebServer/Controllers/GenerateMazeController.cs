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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class GenerateMazeController : ApiController
    {
        /// <summary>
        /// The gm model
        /// </summary>
        private GenerateMazeModel gmModel;
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateMazeController"/> class.
        /// </summary>
        public GenerateMazeController()
        {
            this.gmModel = new GenerateMazeModel();
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
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
