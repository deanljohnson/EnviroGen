using System;
using EnviroGen.Continents;
using EnviroGen.Erosion;
using EnviroGen.HeightMaps;
using EnviroGen.Noise.Modifiers;

namespace EnviroGen
{
    public class Environment
    {
        public Terrain Terrain { get; set; }

        public Environment(Terrain terrain)
        {
            Terrain = terrain;
        }

        public void GenerateTerrain(GenerationOptions options)
        {
            var terrainHeightMap = HeightMapGenerator.GenerateHeightMap(options);

            if (terrainHeightMap == null)
            {
                throw new NullReferenceException($"Error in terrain height map generation, {nameof(GenerateTerrain)}");
            }

            if (options.CombineWithExisting && Terrain != null)
            {
                Terrain.CombineWith(terrainHeightMap);
                Terrain.UpdateImage();
            }
            else
            {
                Terrain = new Terrain(terrainHeightMap);
            }
        }

        public void GenerateContinents(IContinentGenerator generator)
        {
            generator.GenerateContinents(Terrain);
            Terrain.UpdateImage();
        }

        public void ErodeTerrain(IEroder eroder)
        {
            eroder.Erode(Terrain);
            Terrain.UpdateImage();
        }

        public void ApplyTerrainModifier(IModifier modifier)
        {
            modifier.Modify(Terrain);
            Terrain.UpdateImage();
        }

        public void ApplyTerrainModifierInverted(IInvertableModifier modifier)
        {
            modifier.InvertModify(Terrain);
            Terrain.UpdateImage();
        }
    }
}
