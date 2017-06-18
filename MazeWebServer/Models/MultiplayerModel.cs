using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using MazeLib;
using MazeWebServer.Entitys;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using MazeWebServer.Entitys;

namespace MazeWebServer.Models
{

    public class MultiplayerModel
    {
        static Dictionary<string, MultiPlayerInfoPackage> mpDB = new Dictionary<string, MultiPlayerInfoPackage>();
        private readonly IMazeGenerator generator;
        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns>
        /// The maze by the properties.
        /// </returns>
        public MultiplayerModel()
        {
            generator = new DFSMazeGenerator();
        }
        /// <summary>
        /// Starts the specified host.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        public Maze Start(string name, int rows, int cols)
        {
            if (!mpDB.ContainsKey(name))
            {
                Maze maze = generator.Generate(rows, cols);
                maze.Name = name;
                mpDB.Add(name, new MultiPlayerInfoPackage());
                mpDB[name].Maze = maze;
                mpDB[name].IsStarted = false;
                return maze;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Lists all games.
        /// </summary>
        /// <returns></returns>
        public List<string> ListAllGames()
        {
            List<string> gamesList = new List<string>();
            foreach (string maze in mpDB.Keys)
            {
                if (!mpDB[maze].IsStarted)
                {
                    gamesList.Add(maze);
                }

            }
            return gamesList;
        }
        /// <summary>
        /// Joins the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public Maze Join(string game)
        {
            if (mpDB.ContainsKey(game))
            {
                mpDB[game].IsStarted = true;
                return mpDB[game].Maze;
            }
            else
            {
                return null;
            }
        }

        public void Close(string gameToBeDeleted)
        {
            mpDB.Remove(gameToBeDeleted);
        }
    }
}
