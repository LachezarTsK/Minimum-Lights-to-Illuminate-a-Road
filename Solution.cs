
using System;

public class Solution
{
    private static readonly int PLACEHOLDER_VALUE_PREVIOUS_END = -1;
    private static readonly int SIZE_ILLUMINATED_INTERVAL_PER_ADDITIONAL_BULB = 3;

    public int MinLights(int[] lights)
    {
        EditLightsByMarkingIntervalsAtTheirStart(lights);
        return FindMinLightsToIlluminateRoad(lights);
    }

    /*
    Notes about method "editLightsByMarkingIntervalsAtTheirStart"

    The original marking of the lighted intervals is not very convenient to process the input greedily 
    in style "merge intervals" since the information about the size of the lighted interval can be found 
    only at the middle of it.

    If the input, for example, is as follows
    0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0
    then the size of the interval is 4 (proceeding) + 4 (following) + 1 (the place of the value itself) = 9 
    and the complete interval looks like this
    0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 

    This can be corrected by moving information about the interval at its start. In this example, it would mean
    0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    and the complete interval would be again 
    0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0

    Once this editing of the input is done, it can be processed greedily without any complications 
    in style "merge intervals". This modification is just at the cost of one iteration through the input.
     */
    private void EditLightsByMarkingIntervalsAtTheirStart(int[] lights)
    {
        for (int i = 0; i < lights.Length; ++i)
        {
            if (lights[i] == 0)
            {
                continue;
            }

            int startIndex = Math.Max(0, i - lights[i]);
            int endIndex = Math.Min(lights.Length - 1, i + lights[i]);

            lights[startIndex] = Math.Max(lights[i], endIndex);
            if (startIndex != i)
            {
                lights[i] = 0;
            }
        }
    }

    private int FindMinLightsToIlluminateRoad(int[] lights)
    {
        int minNumberOfAdditionalLightBulbsToIlluminateRoad = 0;
        int previousEnd = PLACEHOLDER_VALUE_PREVIOUS_END;
        for (int i = 0; i < lights.Length; ++i)
        {
            if (lights[i] == 0)
            {
                continue;
            }
            if (previousEnd + 1 < i)
            {
                int end = (previousEnd == PLACEHOLDER_VALUE_PREVIOUS_END) ? 0 : previousEnd + 1;
                minNumberOfAdditionalLightBulbsToIlluminateRoad
                        += (int)Math.Ceiling(((double)i - end) / SIZE_ILLUMINATED_INTERVAL_PER_ADDITIONAL_BULB);
            }
            previousEnd = Math.Max(lights[i], previousEnd);
        }

        if (previousEnd + 1 < lights.Length)
        {
            int end = (previousEnd == PLACEHOLDER_VALUE_PREVIOUS_END) ? 0 : previousEnd + 1;
            minNumberOfAdditionalLightBulbsToIlluminateRoad
                    += (int)Math.Ceiling(((double)lights.Length - end) / SIZE_ILLUMINATED_INTERVAL_PER_ADDITIONAL_BULB);
        }

        return minNumberOfAdditionalLightBulbsToIlluminateRoad;
    }
}
