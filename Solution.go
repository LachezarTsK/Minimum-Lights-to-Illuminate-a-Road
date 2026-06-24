
package main
import "math"

const PLACEHOLDER_VALUE_PREVIOUS_END = -1
const SIZE_ILLUMINATED_INTERVAL_PER_ADDITIONAL_BULB = 3

func minLights(lights []int) int {
    editLightsByMarkingIntervalsAtTheirStart(lights)
    return findMinLightsToIlluminateRoad(lights)
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
func editLightsByMarkingIntervalsAtTheirStart(lights []int) {
    for i := range lights {
        if lights[i] == 0 {
            continue
        }

        startIndex := max(0, i - lights[i])
        endIndex := min(len(lights) - 1, i + lights[i])

        lights[startIndex] = max(lights[i], endIndex)
        if startIndex != i {
            lights[i] = 0
        }
    }
}

func findMinLightsToIlluminateRoad(lights []int) int {
    minNumberOfAdditionalLightBulbsToIlluminateRoad := 0
    previousEnd := PLACEHOLDER_VALUE_PREVIOUS_END
    for i := range lights {
        if lights[i] == 0 {
            continue
        }
        if previousEnd + 1 < i {
            end := previousEnd + 1
            if previousEnd == PLACEHOLDER_VALUE_PREVIOUS_END {
                end = 0
            }

            minNumberOfAdditionalLightBulbsToIlluminateRoad += int(math.Ceil(float64(i - end) / SIZE_ILLUMINATED_INTERVAL_PER_ADDITIONAL_BULB))
        }
        previousEnd = max(lights[i], previousEnd)
    }

    if previousEnd + 1 < len(lights) {
        end := previousEnd + 1
        if previousEnd == PLACEHOLDER_VALUE_PREVIOUS_END {
            end = 0
        }
        minNumberOfAdditionalLightBulbsToIlluminateRoad +=
            int(math.Ceil(float64(len(lights) - end) / SIZE_ILLUMINATED_INTERVAL_PER_ADDITIONAL_BULB))
    }

    return minNumberOfAdditionalLightBulbsToIlluminateRoad
}
