using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchAlgorithmsLib;
using MazeLib;

namespace Server.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum Algorithm {DFS,BFS};
    /// <summary>
    /// 
    /// </summary>
    public class SolveMazeModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolveMazeModel"/> class.
        /// </summary>
        public SolveMazeModel()
        {

        }
        /// <summary>
        /// Solves the specified maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algorithm">The algorithm.</param>
        /// <returns>
        /// solves the maze and returns the solution
        /// </returns>
        /// <exception cref="System.Exception">Failed at Solve function in model class</exception>
        public Solution<Position> Solve(string name, Algorithm algorithm)
        {
            Solution<MazeLib.Position> sol;
            MazeAdapter mazeAdapter = new MazeAdapter(GenerateMazeModel.mazes[name]);
            // if the maze is exists
            switch (algorithm)
            {
                case Algorithm.BFS:
                    {
                        WeightForEdges<MazeLib.Position> we = new WeightsForShortestWay<MazeLib.Position>();
                        ISearcher<Position> bfs = new BFS<MazeLib.Position>(we);
                        // solve the maze with bfs.
                        sol = bfs.Search(mazeAdapter);
                    }
                    break;
                case Algorithm.DFS:
                    { 
                        ISearcher<Position> dfs = new DFS<Position>();
                        // solves the maze with dfs.
                        sol = dfs.Search(mazeAdapter);
                    } break;
                default:
                    return null;
            }
            return sol;
        }
    }
}