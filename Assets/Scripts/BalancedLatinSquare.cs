using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancedLatinSquare 
{
    // A = 1, B = 2, C = 3, D = 4, E = 5, F = 6
    public static int[,] Shapes = new int[6,6]{
        {1, 2, 6, 3, 5, 4},
        {2, 3, 1, 4, 6, 5},
        {3, 4, 2, 5, 1, 6},
        {4, 5, 3, 6, 2, 1},
        {5, 6, 4, 1, 3, 2},
        {6, 1, 5, 2, 4, 3}
    };

    // If two interaction methods
    public static int[,] Methods2 = new int[2,2] {
        {1, 2},
        {2, 1}
    };

    // If three interaction methods
    public static int[,] Methods3 = new int[6,3] {
        {1, 2, 3},
        {1, 3, 2},
        {3, 1, 2},
        {3, 2, 1},
        {2, 3, 1},
        {2, 1, 3}
    }; 
}

