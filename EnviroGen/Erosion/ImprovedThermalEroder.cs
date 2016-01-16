using EnviroGen.HeightMaps;

namespace EnviroGen.Erosion
{
    public class ImprovedThermalEroder : ThermalEroder
    {
        public override void Erode(HeightMap heightMap)
        {
            for (var i = 0; i < Iterations; i++)
            {
                for (var y = 0; y < heightMap.Size.Y; y++)
                {
                    for (var x = 0; x < heightMap.Size.X; x++)
                    {
                        var neighbors = heightMap.GetVonNeumannNeighbors(x, y);
                        float highestSlope;
                        var highestSlopedNeighbor = GetHighestSlopedNeighbor(heightMap, neighbors, heightMap[x, y], out highestSlope);

                        //This if condition is the only difference between ImprovedThermalErosion and ThermalErosion
                        if (highestSlope <= TalusAngle)
                        {
                            BalanceHeightsAtPoints(heightMap, x, y, highestSlopedNeighbor.X, highestSlopedNeighbor.Y);
                        }
                    }
                }
            }
        }
    }
}
