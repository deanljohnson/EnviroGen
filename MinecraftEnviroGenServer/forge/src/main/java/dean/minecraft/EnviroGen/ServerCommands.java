package dean.minecraft.EnviroGen;

import java.util.Hashtable;

public final class ServerCommands 
{
	public static Hashtable<Byte, Integer> CommandLengths = new Hashtable<Byte, Integer>();
	
	public static Hashtable<Byte, String> CommandNames = new Hashtable<Byte, String>();
	
    public final static byte NULL = 0;

    public final static byte START_WORLD_GEN = 1;

    public final static byte UPDATE_REQUEST = 2;

    public final static byte START_SIMULATING = 3;
    
    
    /**
     * This command gets the byte id's of all blocks in a certain chunk.
     * The block id's should be read by a triple nested for-loop
     * with z as the outer most, x as the middle, and y as the inner.
     * Also, note that EnviroGen only generates to height 128
     */
    public final static byte GET_CHUNK = 4;
    
    /**
     * This is the command returned from EnviroGen when sent GET_CHUNK
     */
    public final static byte RECEIVE_CHUNK = 5;
    
    static {
    	CommandLengths.put(NULL, 0);
    	CommandLengths.put(START_WORLD_GEN, 2);
    	CommandLengths.put(UPDATE_REQUEST, 0);
    	CommandLengths.put(START_SIMULATING, 0);
    	CommandLengths.put(GET_CHUNK, 2);
    	CommandLengths.put(RECEIVE_CHUNK, 32768);
    	
    	CommandNames.put(NULL, "NULL");
    	CommandNames.put(START_WORLD_GEN, "START_WORLD_GEN");
    	CommandNames.put(UPDATE_REQUEST, "UPDATE_REQUEST");
    	CommandNames.put(START_SIMULATING, "START_SIMULATING");
    	CommandNames.put(GET_CHUNK, "GET_CHUNK");
    	CommandNames.put(RECEIVE_CHUNK, "RECEIVE_CHUNK");
    }
}
