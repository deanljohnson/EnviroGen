using EnviroGen.HeightMaps;

namespace EnviroGen.Erosion
{
    public class ImprovedThermalErosion : ThermalErosion
    {
        /// <summary>
        /// Erodes the given HeightMap based on the given data. Will preserve steep declines while flattening out most areas.
        /// </summary>
        public new static void Erode(HeightMap heightMap, ThermalErosionData data)
        {
            for (var i = 0; i < data.Iterations; i++)
            {
                for (var y = 0; y < heightMap.Size.Y; y++)
                {
                    for (var x = 0; x < heightMap.Size.X; x++)
                    {
                        var neighbors = heightMap.GetVonNeumannNeighbors(x, y);
                        float highestSlope;
                        var highestSlopedNeighbor = GetHighestSlopedNeighbor(heightMap, neighbors, heightMap[x, y], out highestSlope);

                        //This if condition is the only difference between ImprovedThermalErosion and ThermalErosion
                        if (highestSlope <= data.TalusAngle)
                        {
                            BalanceHeightsAtPoints(heightMap, x, y, highestSlopedNeighbor.X, highestSlopedNeighbor.Y);
                        }
                    }
                }
            }
        }
    }
}
