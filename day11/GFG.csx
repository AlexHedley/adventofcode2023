using System;
using System.Collections.Generic;

// https://www.geeksforgeeks.org/shortest-distance-two-cells-matrix-grid/

// C# Code implementation for above problem

// QItem for current location and distance
// from source location
public class QItem
{
    public int row;
    public int col;
    public int dist;

    public QItem(int x, int y, int w)
    {
        this.row = x;
        this.col = y;
        this.dist = w;
    }
}

public static class GFG
{
    // static int N = 4;
    // static int M = 4;

    public static int minDistance(string[,] grid)
    {
        int N = grid.GetLength(0); // = rows;
        int M = grid.GetLength(1); // = cols;

        QItem source = new QItem(0, 0, 0);

        // To keep track of visited QItems. Marking
        // blocked cells as visited.
        bool[,] visited = new bool[N, M];
        
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if (grid[i, j] == "0")
                {
                    visited[i, j] = true;
                }
                else
                {
                    visited[i, j] = false;
                }

                // Finding source
                if (grid[i, j] == "s")
                {
                    source.row = i;
                    source.col = j;
                }
            }
        }

        // applying BFS on matrix cells starting from source
        Queue<QItem> q = new Queue<QItem>();
        q.Enqueue(source);
        visited[source.row, source.col] = true;
        while (q.Count > 0)
        {
            QItem p = q.Peek();
            q.Dequeue();

            // Destination found;
            if (grid[p.row, p.col] == "d")
            {
                return p.dist;
            }

            // moving up
            if (p.row - 1 >= 0
                && visited[p.row - 1, p.col] == false)
            {
                q.Enqueue(new QItem(p.row - 1, p.col,
                                    p.dist + 1));
                visited[p.row - 1, p.col] = true;
            }

            // moving down
            if (p.row + 1 < N
                && visited[p.row + 1, p.col] == false)
            {
                q.Enqueue(new QItem(p.row + 1, p.col,
                                    p.dist + 1));
                visited[p.row + 1, p.col] = true;
            }

            // moving left
            if (p.col - 1 >= 0
                && visited[p.row, p.col - 1] == false)
            {
                q.Enqueue(new QItem(p.row, p.col - 1,
                                    p.dist + 1));
                visited[p.row, p.col - 1] = true;
            }

            // moving right
            if (p.col + 1 < M
                && visited[p.row, p.col + 1] == false)
            {
                q.Enqueue(new QItem(p.row, p.col + 1,
                                    p.dist + 1));
                visited[p.row, p.col + 1] = true;
            }
        }
        return -1;
    }

    // // Driver code
    // public static void Main()
    // {
    //     char[,] grid = { { '0', '*', '0', 's' },
    //                  { '*', '0', '*', '*' },
    //                  { '0', '*', '*', '*' },
    //                  { 'd', '*', '*', '*' } };

    //     Console.Write(minDistance(grid));
    // }
}

// This code is contributed by Aarti_Rathi