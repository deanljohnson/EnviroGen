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
                        var highestSlopedNeighbor = GetHighestSlopedNeighbor(heightMap, x, y, neighbors, out highestSlope);

                        if (highestSlope <= talusAngle)
                        {
                            var heightDif = heightMap[x, y] - heightMap[highestSlopedNeighbor];
                            heightMap[x, y] -= heightDif / 2f;
                            heightMap[highestSlopedNeighbor] += heightDif / 2f;
                        }
                    }
                }
            }
        }
    }
}
