using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Web.Http;
using MazeWebServer.Models;
using Newtonsoft.Json.Linq;

namespace MazeWebServer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ListMultiController : ApiController
    {
        /// <summary>
        /// The mp model
        /// </summary>
        public static MultiplayerModel mpModel = new MultiplayerModel();
        public ListMultiController()
        {

        }
        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ListMulti/List")]
        public IHttpActionResult List()
        {
            List<string> temp = mpModel.ListAllGames();
            return Ok(JsonConvert.SerializeObject(temp, Formatting.Indented));
        }

    }
}
