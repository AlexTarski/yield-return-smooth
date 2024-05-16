using System.Collections.Generic;

namespace yield;

public static class ExpSmoothingTask
{
    public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
	{
        bool isFirstPoint = true;
        double currentYPoint;
        double previousYPoint = 0;

        foreach (var point in data)
		{
            if (isFirstPoint)
            {
                previousYPoint = point.OriginalY;
                currentYPoint = point.OriginalY;
                isFirstPoint = false;
            }
            else
            {
                currentYPoint = alpha * point.OriginalY + (1 - alpha) * previousYPoint;
                previousYPoint = currentYPoint;
            }
            DataPoint newPoint = point.WithExpSmoothedY(currentYPoint);
            yield return newPoint;
		}
	}
}
