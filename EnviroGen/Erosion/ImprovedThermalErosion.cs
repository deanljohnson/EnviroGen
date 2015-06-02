namespace EnviroGen.Erosion
{
    public class ImprovedThermalErosion : ThermalErosion
    {
        public new static void Erode(HeightMap heightMap, float talusAngle, int iterations)
        {
            for (var i = 0; i < iterations; i++)
            {
                for (var y = 0; y < heightMap.Size.Y; y++)
                {
                    for (var x = 0; x < heightMap.Size.X; x++)
                    {
                        var neighbors = heightMap.GetVonNeumannNeighbors(x, y);
                        float highestSlope;
                        var highestSlopedNeighbor = GetHighestSlopedNeighbor(heightMap, neighbors, heightMap[x, y], out highestSlope);

                        if (highestSlope <= talusAngle)
                        {
                            BalanceHeightsAtPoints(heightMap, x, y, highestSlopedNeighbor.X, highestSlopedNeighbor.Y);
                        }
                    }
                }
            }
        }
    }
}
