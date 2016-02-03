package dean.minecraft.EnviroGen;

import java.io.EOFException;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.logging.Logger;

public class EnviroGenPipeClient {
	private String m_EnviroGenOutputName;
	
	private RandomAccessFile m_EnviroGenOutputPipe;
	
	private Logger m_Logger;
	
	public EnviroGenPipeClient(String egOutput)
	{
		m_EnviroGenOutputName = egOutput;
		m_Logger = Logger.getLogger(EnviroGenMod.NAME);
	}
	
	public void startEnviroGenWorldGen()
	{
		byte[] cmd = new byte[]{ServerCommands.START_WORLD_GEN, 
				EnviroGenMod.CHUNK_X, 
				EnviroGenMod.CHUNK_Z};
		
		//For some reason, the first 
		//pipe connections tend to fail, 
		//so we try till it succeeds
		while(true)
		{
			byte[] input = sendRequestToEnviroGen(cmd);
			
			if (input == null) continue;
			
			processEnviroGenOutput(input);
			break;
		}
	}
	
	public void requestUpdateFromServer()
	{
		byte[] input = sendRequestToEnviroGen(new byte[]{ServerCommands.UPDATE_REQUEST});
		
		processEnviroGenOutput(input);
	}
	
	public byte[] requestChunkFromEnviroGen(byte cx, byte cz)
	{
		byte[] cmd = new byte[]{ServerCommands.GET_CHUNK, cx, cz};
		
		while(true)
		{
			byte[] response = sendRequestToEnviroGen(cmd);
			if (response == null) continue;
			
			return response;
		}
	}
	
	private byte[] sendRequestToEnviroGen(byte[] cmd)
	{
		openPipe(false);
		sendToPipe(cmd);
		
		byte[] input = readInputFromPipe(m_EnviroGenOutputPipe);
		
		closePipe();
		return input;
	}
	
	private void openPipe(boolean throwOnBusy)
	{
		while (true)
		{
			try 
			{
				m_EnviroGenOutputPipe = new RandomAccessFile(m_EnviroGenOutputName, "rw");
				//If no exception, we are done and can return.
				return;
			}
			catch (FileNotFoundException e)
			{
				if (throwOnBusy)
				{
					System.err.println(e.getMessage() + ", occured while opening the pipe");
					e.printStackTrace();
					return;
				}
			}
		}
	}
	
	private void closePipe()
	{
		try 
		{
			m_EnviroGenOutputPipe.close();
		} 
		catch (Exception e) 
		{
		}
	}
	
	private byte[] readInputFromPipe(RandomAccessFile pipe)
	{
		try
		{
			byte[] commandRead = new byte[1];
			pipe.read(commandRead, 0, 1);
			
			m_Logger.info("Raw command read: " + commandRead[0]);
			
			int commandLength = ServerCommands.CommandLengths.get(commandRead[0]);
			byte[] input = new byte[1 + commandLength];
			
			input[0] = commandRead[0];
			
			if (commandLength > 0){
				pipe.read(input, 1, commandLength);
			}
			
			return input;
		}
		catch(EOFException e)
		{
			System.err.println(e.getMessage() + ", occured while reading the pipe");
			e.printStackTrace();
		} 
		catch (IOException e) {
			System.err.println(e.getMessage() + ", occured while reading the pipe");
			e.printStackTrace();
		}
		
		return null;
	}
	
	private void sendToPipe(byte[] cmd)
	{
		if ((cmd.length - 1) != ServerCommands.CommandLengths.get(cmd[0]))
		{
			System.err.println("Command tried to be sent with the wrong number of arguments");
			return;
		}
		
		try {
			m_EnviroGenOutputPipe.write(cmd, 0, cmd.length);
		} catch (IOException e) {
			System.err.println(e.getMessage() + ", occured while writing to the pipe.");
			e.printStackTrace();
		}
	}
	
	private void processEnviroGenOutput(byte[] cmd)
	{
		if (cmd[0] == ServerCommands.START_WORLD_GEN)
		{
			EnviroGenMod.WORLD_CREATED = true;
		}
		m_Logger.info(String.format("Received %s command from EnviroGen", ServerCommands.CommandNames.get(cmd[0])));
	}
}
