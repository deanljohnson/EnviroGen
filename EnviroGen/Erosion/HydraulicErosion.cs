using EnviroGen.HeightMaps;
using SFML.Window;

namespace EnviroGen.Erosion
{
    public static class HydraulicErosion
    {
        /// <summary>
        /// Erodes the given HeightMap based on the supplied data in such a way as to simulate the process of
        /// rain water picking up sediment at higher elevations and moving it to lower elevations.
        /// </summary>
        public static void Erode(HeightMap heightMap, HydraulicErosionData data)
        {
            var waterMap = new float[heightMap.Size.X, heightMap.Size.Y];
            var sedimentMap = new float[heightMap.Size.X, heightMap.Size.Y];

            for (var i = 0; i < data.Iterations; i++)
            {
                DoRainfallAndErosion(heightMap, ref waterMap, ref sedimentMap, data.RainAmount, data.Solubility);
                DoMovement(heightMap, ref waterMap);
                DoEvaporation(heightMap, ref waterMap, ref sedimentMap, data.Evaporation, data.Capacity);
            }
        }

        /// <summary>
        /// Performs the Rainfall and Erosion steps of Hydraulic Erosion simultaneously.
        /// Yes this function has multiple responsibilities, usually a bad design, but it
        /// is more efficient this way and still understandable.
        /// </summary>
        private static void DoRainfallAndErosion(HeightMap heightMap, ref float[,] waterMap, ref float[,] sedimentMap,
            float rainAmount, float solubility)
        {
            for (var y = 0; y < heightMap.Size.Y; y++)
            {
                for (var x = 0; x < heightMap.Size.X; x++)
                {
                    //Rainfall Steps
                    waterMap[x, y] += rainAmount;

                    //Erosion Steps
                    heightMap[x, y] -= solubility;
                    sedimentMap[x, y] += solubility;
                }
            }
        }

        /// <summary>
        /// Moves water downhill
        /// </summary>
        private static void DoMovement(HeightMap heightMap, ref float[,] waterMap)
        {
            for (var y = 0; y < heightMap.Size.Y; y++)
            {
                for (var x = 0; x < heightMap.Size.X; x++)
                {
                    var lowestNeighbor = GetLowestNeighbor(heightMap, x, y);

                    //If current cell is the lowest, water cant flow from the current cell
                    if (lowestNeighbor.X == x && lowestNeighbor.Y == y)
                    {
                        continue;
                    }

                    var heightOfNeighborWithAllWater = waterMap[x, y] + waterMap[lowestNeighbor.X, lowestNeighbor.Y] +
                                                       heightMap[lowestNeighbor];

                    if (heightOfNeighborWithAllWater < heightMap[x, y])
                    {
                        MoveAllWater(ref waterMap, x, y, lowestNeighbor.X, lowestNeighbor.Y);
                    }
                    else
                    {
                        LevelWater(heightMap, ref waterMap, x, y, lowestNeighbor.X, lowestNeighbor.Y);
                    }
                }
            }
        }

        /// <summary>
        /// Evaporates water and deposits sediment if needed.
        /// </summary>
        private static void DoEvaporation(HeightMap heightMap, ref float[,] waterMap, ref float[,] sedimentMap, float evaporation, float capacity)
        {
            for (var y = 0; y < heightMap.Size.Y; y++)
            {
                for (var x = 0; x < heightMap.Size.X; x++)
                {
                    waterMap[x, y] *= (1f - evaporation);
                    var thisPointsCapacity = capacity * waterMap[x, y];

                    if (sedimentMap[x, y] > thisPointsCapacity)
                    {
                        var dif = sedimentMap[x, y] - thisPointsCapacity;
                        heightMap[x, y] += dif / 2f;
                        sedimentMap[x, y] -= dif / 2f;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the lowest neighboring cell, or the current cell if the current cell is the lowest.
        /// </summary>
        private static Vector2i GetLowestNeighbor(HeightMap heightMap, int x, int y)
        {
            var neighbors = heightMap.GetVonNeumannNeighbors(x, y);

            var lowest = new Vector2i(x, y);
            foreach (var neighbor in neighbors)
            {
                if (heightMap[neighbor] < heightMap[lowest])
                {
                    lowest = neighbor;
                }
            }

            return lowest;
        }

        /// <summary>
        /// Moves all water from the index (x1,y1) to (x2,y2)
        /// </summary>
        private static void MoveAllWater(ref float[,] waterMap, int x1, int y1, int x2, int y2)
        {
            waterMap[x2, y2] += waterMap[x1, y1];
            waterMap[x1, y1] = 0f;
        }

        /// <summary>
        /// Moves water from (x1,y1) to (x2,y2) until the ground height plus the water height are level.
        /// </summary>
        private static void LevelWater(HeightMap heightMap, ref float[,] waterMap, int x1, int y1, int x2, int y2)
        {
            var heightDif = (heightMap[x1, y1] + waterMap[x1, y1]) - (heightMap[x2, y2] + waterMap[x2, y2]);
            waterMap[x1, y1] -= heightDif / 2f;
            waterMap[x2, y2] += heightDif / 2f;
        }
    }
}
