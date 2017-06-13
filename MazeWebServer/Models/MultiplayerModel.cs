using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using MazeLib;
using MazeWebServer.Entitys;
using MazeGeneratorLib;
using SearchAlgorithmsLib;

namespace MazeWebServer.Models
{
   
    public class MultiplayerModel
    {
        static Dictionary<string, MultiPlayerInfoPackage> mpDB = new Dictionary<string, MultiPlayerInfoPackage>();
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
        }
        public Maze Start(string host, string name, int rows, int cols)
        {
            if (!mpDB.ContainsKey(name))
            {
                IMazeGenerator generator = new DFSMazeGenerator();   
                Maze maze = generator.Generate(rows, cols);
                maze.Name = name;
                mpDB.Add(name,new MultiPlayerInfoPackage(host, maze));
                while(mpDB[name].Guest == null)
                {
                    Thread.Sleep(500);
                }
                return maze;
            } else
            {
                return null;
            }
        }
        public List<string> ListAllGames()
        {
            List<string> gamesList = new List<string>();
            foreach(string maze in mpDB.Keys)
            {
                if(mpDB[maze].Guest == null)
                {
                    gamesList.Add(maze);
                }
            }
            return gamesList;
        }
        public Maze Join(string guest, string game)
        {
            mpDB[game].Guest = guest;
            return mpDB[game].Maze;
        }
    } 
}