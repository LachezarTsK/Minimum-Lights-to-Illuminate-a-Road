
import kotlin.math.ceil
import kotlin.math.min
import kotlin.math.max

class Solution {

    private companion object {
        const val PLACEHOLDER_VALUE_PREVIOUS_END = -1
        const val SIZE_ILLUMINATED_INTERVAL_PER_ADDITIONAL_BULB = 3
    }

    fun minLights(lights: IntArray): Int {
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
    private fun editLightsByMarkingIntervalsAtTheirStart(lights: IntArray) {
        for (i in lights.indices) {
            if (lights[i] == 0) {
                continue
            }

            val startIndex = max(0, i - lights[i])
            val endIndex = min(lights.size - 1, i + lights[i])

            lights[startIndex] = max(lights[i], endIndex)
            if (startIndex != i) {
                lights[i] = 0
            }
        }
    }

    private fun findMinLightsToIlluminateRoad(lights: IntArray): Int {
        var minNumberOfAdditionalLightBulbsToIlluminateRoad = 0
        var previousEnd = PLACEHOLDER_VALUE_PREVIOUS_END
        for (i in lights.indices) {
            if (lights[i] == 0) {
                continue
            }
            if (previousEnd + 1 < i) {
                val end = if (previousEnd == PLACEHOLDER_VALUE_PREVIOUS_END) 0 else previousEnd + 1
                minNumberOfAdditionalLightBulbsToIlluminateRoad +=
                    ceil((i - end).toDouble() / SIZE_ILLUMINATED_INTERVAL_PER_ADDITIONAL_BULB).toInt()
            }
            previousEnd = max(lights[i], previousEnd)
        }

        if (previousEnd + 1 < lights.size) {
            val end = if (previousEnd == PLACEHOLDER_VALUE_PREVIOUS_END) 0 else previousEnd + 1
            minNumberOfAdditionalLightBulbsToIlluminateRoad +=
                ceil((lights.size - end).toDouble() / SIZE_ILLUMINATED_INTERVAL_PER_ADDITIONAL_BULB).toInt()
        }

        return minNumberOfAdditionalLightBulbsToIlluminateRoad
    }
}
