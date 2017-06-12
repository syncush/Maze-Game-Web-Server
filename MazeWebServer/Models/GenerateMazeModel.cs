using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MazeLib;
using MazeGeneratorLib;

namespace MazeWebServer.Models
{
    public class GenerateMazeModel
    {
       public static Dictionary<string, Maze> mazes = new Dictionary<string, Maze>();
       public Maze GenerateMaze(string name , int rows, int cols)
        {
            IMazeGenerator iMaze = new MazeGeneratorLib.DFSMazeGenerator();
            Maze result = iMaze.Generate(rows, cols);
            if (mazes.ContainsKey(name)) {
                return mazes[name];
            }
            mazes.Add(name, result);
            result.Name = name;
            return result;
        }
    }
}