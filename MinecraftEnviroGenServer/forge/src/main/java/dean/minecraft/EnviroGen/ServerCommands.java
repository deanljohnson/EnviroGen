package dean.minecraft.EnviroGen;

import java.util.Hashtable;

public final class ServerCommands 
{
	public static Hashtable<Byte, Integer> CommandLengths = new Hashtable<Byte, Integer>();
	
	public static Hashtable<Byte, String> CommandNames = new Hashtable<Byte, String>();
	
	/**
    * Sent by the EnviroGen pipe client to signify an empty command.
    * Args: {}
    */
    public static final byte NULL = 0;

    /**
    * Sent by the java EnviroGen pipe client when world gen is starting, 
    * so that initial terrain can all be generated. 
    * Args: {cx, cy}
    */
    public static final byte START_WORLD_GEN = 1;

    /**
    * Sent by the java EnviroGen pipe client when it is 
    * ready to accept the next block update.
    * Args: {numUpdates}
    */
    public static final byte UPDATE_REQUEST = 2;

    /**
    * Sent by the EnviroGen pipe client when world gen is completed(and it has received the data),
    * and things such as erosion can begin to be simulated.
    * Args: {}
    */
    public static final byte START_SIMULATING = 3;

    /**
    * This command gets the byte id's of all blocks in a certain chunk.
    * BlockIDs are flattened into 1d array by a triple nested for-loop
    * in the order z, x, y.
    * Args: {cx, cy}
    */
    public static final byte GET_CHUNK = 4;

    /**
    * This is the command returned from EnviroGen when sent GET_CHUNK.
    * BlockIDs are flattened into 1d array by a triple nested for-loop
    * in the order z, x, y.
    * Args: {blockID[0]...blockID[32767]}
    */
    public static final byte RECEIVE_CHUNK = 5;

    /**
    * Sent by EnviroGen as a response to an UPDATE_REQUEST.
    * Args: {cx, cy, x, y, z}
    */
    public static final byte DELETE_BLOCK = 6;

    /**
    * Sent by EnviroGen as a response to an UPDATE_REQUEST.
    * Args: {cx, cy, x, y, z, id}
    */
    public static final byte SET_BLOCK = 7;
    
    static {
    	CommandLengths.put(NULL, 0);
    	CommandLengths.put(START_WORLD_GEN, 2);
    	CommandLengths.put(UPDATE_REQUEST, 1);
    	CommandLengths.put(START_SIMULATING, 0);
    	CommandLengths.put(GET_CHUNK, 2);
    	CommandLengths.put(RECEIVE_CHUNK, 32768);
    	CommandLengths.put(DELETE_BLOCK, 5);
    	CommandLengths.put(SET_BLOCK, 6);
    	
    	CommandNames.put(NULL, "NULL");
    	CommandNames.put(START_WORLD_GEN, "START_WORLD_GEN");
    	CommandNames.put(UPDATE_REQUEST, "UPDATE_REQUEST");
    	CommandNames.put(START_SIMULATING, "START_SIMULATING");
    	CommandNames.put(GET_CHUNK, "GET_CHUNK");
    	CommandNames.put(RECEIVE_CHUNK, "RECEIVE_CHUNK");
    	CommandNames.put(DELETE_BLOCK, "DELETE_BLOCK");
    	CommandNames.put(SET_BLOCK, "SET_BLOCK");
    }
}
