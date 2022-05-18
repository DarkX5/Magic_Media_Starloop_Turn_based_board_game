using System;

[Serializable]
public class DiceRange
{
    public DiceRange(int newMin, int newMax)
    {
        min = newMin;
        max = newMax;
    }
    public int min = 0;
    public int max = 0;
}
