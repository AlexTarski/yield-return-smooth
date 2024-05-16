using Avalonia;
using System.Collections.Generic;
using System.Drawing;

namespace yield;

public static class MovingAverageTask
{
	public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
	{
		Queue<double> previousYs= new();
		double averageSum = 0;
		foreach (DataPoint point in data)
		{
            Average(previousYs, windowWidth, point.OriginalY, ref averageSum);
            DataPoint newPoint = point.WithAvgSmoothedY(averageSum / previousYs.Count);
			yield return newPoint;
        }
	}

	public static void Average(Queue<double> previousYs, int windowWidth, double currentY, ref double averageSum)
	{
        previousYs.Enqueue(currentY);
        averageSum += currentY;
        if (previousYs.Count > windowWidth)
        {
            averageSum -= previousYs.Dequeue();
        }
    }
}