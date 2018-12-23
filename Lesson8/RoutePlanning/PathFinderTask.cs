using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
			var permutation = new int[checkpoints.Length];
            var permutations = new List<int[]>();
		    permutations.Add(MakeTrivialPermutation(checkpoints.Length));

            MakeMinPermutations(checkpoints, permutation, 1, permutations);
		    var minPathLength = GetMinPathLength(checkpoints, permutations);
            return permutations.Find(
                x => Math.Abs(checkpoints.GetPathLength(x) - minPathLength) < 0.001);
        }

	    private static double GetFirstNPathLength(Point[] checkpoints, int n, int[] order)
	    {
	        return checkpoints.GetPathLength(order.Take(n).ToArray());
	    }

	    private static double GetMinPathLength(Point[] checkpoints, List<int[]> permutations)
	    {
	        return permutations.Select(checkpoints.GetPathLength).Min();
	    }

	    private static void MakeMinPermutations(
	        Point[] checkpoints, int[] permutation, int position, List<int[]> permutations) 
	    {
	        var currentPrice = position == 1 ? 0 : GetFirstNPathLength(checkpoints, position, permutation);
	        if (position != 1 && currentPrice > GetMinPathLength(checkpoints, permutations))
	            return;

	        if (position == permutation.Length)
	        {
	            permutations.Add(permutation.ToArray());
	            return;
	        }

	        for (var i = 0; i < permutation.Length; i++)
	        {
	            var index = Array.IndexOf(permutation, i, 0, position);
	            if (index != -1) continue;
	            permutation[position] = i;
	            MakeMinPermutations(checkpoints, permutation.ToArray(), position + 1, permutations);
            }
	    }

        private static int[] MakeTrivialPermutation(int size)
		{
			var bestOrder = new int[size];
			for (var i = 0; i < bestOrder.Length; i++)
				bestOrder[i] = i;
			return bestOrder;
		}
	}
}