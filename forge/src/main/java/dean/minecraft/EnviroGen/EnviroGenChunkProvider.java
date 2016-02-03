package dean.minecraft.EnviroGen;

import java.util.Collections;
import java.util.List;

import net.minecraft.block.Block;
import net.minecraft.block.state.IBlockState;
import net.minecraft.entity.EnumCreatureType;
import net.minecraft.util.BlockPos;
import net.minecraft.util.IProgressUpdate;
import net.minecraft.world.World;
import net.minecraft.world.biome.BiomeGenBase;
import net.minecraft.world.biome.BiomeGenBase.SpawnListEntry;
import net.minecraft.world.chunk.Chunk;
import net.minecraft.world.chunk.IChunkProvider;
import net.minecraftforge.fml.common.registry.GameData;

public class EnviroGenChunkProvider implements IChunkProvider
{
	private World world;
	
	public EnviroGenChunkProvider(World world) 
	{
		this.world = world;
	}
	
	@Override
	public boolean chunkExists(int x, int z) 
	{
		return (EnviroGenMod.WORLD_CREATED 
				&& x >= 0 
				&& z >= 0
				&& x < EnviroGenMod.CHUNK_X 
				&& z < EnviroGenMod.CHUNK_Z);
	}

	@Override
	public Chunk provideChunk(int x, int z) 
	{
		byte[] blockIDs = EnviroGenMod.instance
									.PipeClient
									.requestChunkFromEnviroGen((byte)x, (byte)z);
		
		if (blockIDs == null)
		{
			System.err.println("A null chunk was returned by EnviroGen");
		}
		
		//Using height like this in case we change the EnviroGen's height limit
		//This is throwing a null pointer exception....
		int height = blockIDs.length / (16 * 16);
		
		Chunk chunk = new Chunk(world, x, z);
		for (int k = 0; k < 16; k++)
		{
			for (int i = 0; i < 16; i++)
			{
				for (int j = 0; j < height; j++)
				{
					//The +1 is for the RECEIVE_CHUNK byte, it's not a block ID
					Block block = Block.getBlockById((int)blockIDs[j + i*height + k*16*height + 1]);
					chunk.setBlockState(new BlockPos(i, j, k), block.getDefaultState());
				}
			}
		}
		
		chunk.generateSkylightMap();
		
		return chunk;
	}

	@Override
	public Chunk provideChunk(BlockPos blockPosIn) {
		return this.provideChunk(blockPosIn.getX() >> 4, blockPosIn.getZ() >> 4);
	}

	@Override
	public void populate(IChunkProvider p_73153_1_, int p_73153_2_, int p_73153_3_) {
		//DO nothing, we don't populate through this function with EnviroGen
	}

	@Override
	public boolean func_177460_a(IChunkProvider p_177460_1_, Chunk p_177460_2_, int p_177460_3_, int p_177460_4_) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean saveChunks(boolean p_73151_1_, IProgressUpdate progressCallback) {
		// Seems weird - just copying ChunkProviderFlat here
		return true;
	}

	@Override
	public boolean unloadQueuedChunks() {
		// Seems weird - just copying ChunkProviderFlat here
		return false;
	}

	@Override
	public boolean canSave() {
		return true;
	}

	@Override
	public String makeString() {
		return "EnviroGenLevelSource";
	}

	@Override
	public List<SpawnListEntry> getPossibleCreatures(EnumCreatureType creatureType, BlockPos pos) {
		return Collections.<SpawnListEntry>emptyList();
	}

	@Override
	public BlockPos getStrongholdGen(World worldIn, String structureName, BlockPos position) {
		return null;
	}

	@Override
	public int getLoadedChunkCount() {
		// Seems weird - just copying ChunkProviderFlat here
		return 0;
	}

	@Override
	public void recreateStructures(Chunk p_180514_1_, int p_180514_2_, int p_180514_3_) 
	{
		//Never had structures in the first place
	}

	@Override
	public void saveExtraData() 
	{
		//Not event implemented in Minecraft so... yeah
	}

}
