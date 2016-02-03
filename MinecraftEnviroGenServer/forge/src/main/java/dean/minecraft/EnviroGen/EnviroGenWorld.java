package dean.minecraft.EnviroGen;

import net.minecraft.world.World;
import net.minecraft.world.WorldType;

public class EnviroGenWorld extends WorldType
{
	public EnviroGenWorld() 
	{
		super("EnviroGen");
	}

	@Override
	public void onGUICreateWorldPress()
	{
		
	}
	
	@Override
	public net.minecraft.world.biome.WorldChunkManager getChunkManager(World world)
    {
        net.minecraft.world.gen.FlatGeneratorInfo flatgeneratorinfo = net.minecraft.world.gen.FlatGeneratorInfo.createFlatGeneratorFromString(world.getWorldInfo().getGeneratorOptions());
        return new net.minecraft.world.biome.WorldChunkManagerHell(net.minecraft.world.biome.BiomeGenBase.getBiomeFromBiomeList(flatgeneratorinfo.getBiome(), net.minecraft.world.biome.BiomeGenBase.field_180279_ad), 0.5F);
    }
	
	@Override
	public net.minecraft.world.chunk.IChunkProvider getChunkGenerator(World world, String generatorOptions)
    {
		return new EnviroGenChunkProvider(world);
        //return new net.minecraft.world.gen.ChunkProviderFlat(world, world.getSeed(), world.getWorldInfo().isMapFeaturesEnabled(), generatorOptions);
    }
}
