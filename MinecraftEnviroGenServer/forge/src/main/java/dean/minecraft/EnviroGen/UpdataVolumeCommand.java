package dean.minecraft.EnviroGen;

import java.util.ArrayList;
import java.util.List;

import net.minecraft.command.ICommand;
import net.minecraft.command.ICommandSender;
import net.minecraft.util.BlockPos;
import net.minecraft.util.ChatComponentText;

public class UpdataVolumeCommand implements ICommand
{
	private List aliases;
	
	public UpdataVolumeCommand()
	{
		aliases = new ArrayList();
		aliases.add("EGUpdateVolume");
		aliases.add("EGUV");
		aliases.add("eguv");
	}
	
	@Override
	public String getCommandName()
	{
		return "EGUpdateVolume";
	}
	
	@Override
	public String getCommandUsage(ICommandSender icommandsender)
	{
		return "EGUpdateVolume <integer (0-255)>";
	}
	
	@Override
	public List getCommandAliases()
	{
		return aliases;
	}
	
	@Override
	public void processCommand(ICommandSender sender, String[] astring)
	{
		if (astring.length != 1)
		{
			sender.addChatMessage(new ChatComponentText("Invalid Arguments"));
		}
		
		try
		{
			byte volume = Byte.parseByte(astring[0]);
			if (volume < 1)
			{
				throw new NumberFormatException();
			}
			
			EnviroGenMod.instance.PipeClient.UpdateVolume = volume;
			
			sender.addChatMessage(new ChatComponentText("Set EnviroGen Update Volume to: " + volume));
		}
		catch (NumberFormatException e)
		{
			sender.addChatMessage(new ChatComponentText("Argument must be an integer greater than 0 and less than 256"));
		}
	}
	
	@Override
	public boolean canCommandSenderUseCommand(ICommandSender sender)
	{
		return true;
	}
	
	@Override
	public boolean isUsernameIndex(String[] astring, int i)
	{
		return false;
	}
	
	@Override
	public int compareTo(ICommand c)
	{
		return 0;
	}

	@Override
	public List<String> addTabCompletionOptions(ICommandSender sender, String[] astring, BlockPos pos) 
	{
		return null;
	}
}
