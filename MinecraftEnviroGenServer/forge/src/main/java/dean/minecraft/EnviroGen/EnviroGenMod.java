package dean.minecraft.EnviroGen;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.RandomAccessFile;
import java.util.NoSuchElementException;
import java.util.Scanner;
import java.util.logging.Logger;

import net.minecraft.block.Block;
import net.minecraft.init.Blocks;
import net.minecraft.util.BlockPos;
import net.minecraft.world.World;
import net.minecraft.world.WorldType;
import net.minecraftforge.common.DimensionManager;
import net.minecraftforge.common.MinecraftForge;
import net.minecraftforge.event.world.WorldEvent;
import net.minecraftforge.fml.common.FMLCommonHandler;
import net.minecraftforge.fml.common.Mod;
import net.minecraftforge.fml.common.Mod.EventHandler;
import net.minecraftforge.fml.common.Mod.Instance;
import net.minecraftforge.fml.common.event.FMLInitializationEvent;
import net.minecraftforge.fml.common.event.FMLPostInitializationEvent;
import net.minecraftforge.fml.common.event.FMLPreInitializationEvent;
import net.minecraftforge.fml.common.event.FMLServerStartedEvent;
import net.minecraftforge.fml.common.event.FMLServerStartingEvent;
import net.minecraftforge.fml.common.eventhandler.SubscribeEvent;
import net.minecraftforge.fml.common.gameevent.TickEvent.ClientTickEvent;
import net.minecraftforge.fml.common.gameevent.TickEvent.ServerTickEvent;
import net.minecraftforge.fml.common.gameevent.TickEvent.WorldTickEvent;
import net.minecraftforge.fml.common.registry.GameRegistry;

@Mod(modid = EnviroGenMod.MODID, name = EnviroGenMod.NAME, version = EnviroGenMod.VERSION, 
	serverSideOnly = true, clientSideOnly = false, acceptableRemoteVersions = "*")
public class EnviroGenMod
{
	private Logger logger;
	private int m_InputCounter = 1;
	
	public static final byte CHUNK_X = 20;
	public static final byte CHUNK_Z = 20;
	public static boolean WORLD_CREATED = false;
	
	public static final String NAME = "EnviroGen";
    public static final String MODID = "dean.minecraft.EnviroGen";
    public static final String VERSION = "1.0";
    
    @Instance(value = EnviroGenMod.MODID) //Tell Forge what instance to use.
    public static EnviroGenMod instance;
    
    public static final EnviroGenWorld EnviroGenWorld = new EnviroGenWorld();
    
    public static EnviroGenPipeClient PipeClient;
    
    public int UpdateRate = 2;
    
    
    @EventHandler
    public void preInit(FMLPreInitializationEvent event)
    {
    	MinecraftForge.EVENT_BUS.register(instance);
    	logger = Logger.getLogger(NAME);
    	
    	PipeClient = new EnviroGenPipeClient("\\\\.\\pipe\\EnviroGenOutput");
    }
    
    @EventHandler
    public void init(FMLInitializationEvent event)
    {
    	PipeClient.startEnviroGenWorldGen();
    }
    
    @EventHandler
    public void serverStarting(FMLServerStartingEvent event)
    {
    	event.registerServerCommand(new UpdateRateCommand());
    	event.registerServerCommand(new UpdataVolumeCommand());
    }

    @SubscribeEvent
    public void onWorldTick(WorldTickEvent event) 
    {
    	if (m_InputCounter % UpdateRate != 0){
			m_InputCounter++;
    		return;
    	}
		m_InputCounter = 1;

    	PipeClient.requestUpdateFromServer();
    }
    
    public void DeleteBlock(byte cx, byte cz, byte x, byte y, byte z)
    {
    	World world = DimensionManager.getWorld(0);
    	BlockPos pos = new BlockPos((cx * 16) + x, y, (cz * 16) + z);
    	world.destroyBlock(pos, false);
    }
    
    public void SetBlock(byte cx, byte cz, byte x, byte y, byte z, byte id)
    {
    	World world = DimensionManager.getWorld(0);
    	BlockPos pos = new BlockPos((cx * 16) + x, y, (cz * 16) + z);
    	world.setBlockState(pos, Block.getBlockById(id).getDefaultState(), 3);
    }
}
