using System;

public static class Randomiser
{
    static Random random = new Random();

    public static int GetRandomInt(int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue + 1);
    }

    public static double GetRandomFloat(double minValue, double maxValue)
    {
        return (random.NextDouble() * maxValue) - minValue;
    }
}