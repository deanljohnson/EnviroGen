using EnviroGen.HeightMaps;

namespace EnviroGen.Erosion
{
    public interface IEroder
    {
        void Erode(HeightMap heightMap);
    }
}
