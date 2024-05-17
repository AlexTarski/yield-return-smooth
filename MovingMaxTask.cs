using System;
using System.Collections.Generic;
using System.Linq;

namespace yield;

public static class MovingMaxTask
{
	public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
	{
        Queue<double> previousYs = new();
        LinkedList<double> max = new();
        foreach (DataPoint point in data)
        {
            previousYs.Enqueue(point.OriginalY);
            if (previousYs.Count > windowWidth && previousYs.Dequeue() == max.First.Value)
            {
                max.RemoveFirst();
            }
            while(max.Count > 0 && max.Last.Value < point.OriginalY)
            {
                max.RemoveLast();
            }
            max.AddLast(point.OriginalY);

            DataPoint newPoint = point.WithMaxY(max.First.Value);
            yield return newPoint;
        }
    }
}