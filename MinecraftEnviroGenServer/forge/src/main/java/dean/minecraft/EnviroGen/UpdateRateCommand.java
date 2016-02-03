package dean.minecraft.EnviroGen;

import java.util.ArrayList;
import java.util.List;

import net.minecraft.command.ICommand;
import net.minecraft.command.ICommandSender;
import net.minecraft.util.BlockPos;
import net.minecraft.util.ChatComponentText;

public class UpdateRateCommand implements ICommand 
{
	private List aliases;
	
	public UpdateRateCommand()
	{
		aliases = new ArrayList();
		aliases.add("EGUpdateRate");
		aliases.add("EGUR");
	}
	
	@Override
	public String getCommandName()
	{
		return "EGUpdateRate";
	}
	
	@Override
	public String getCommandUsage(ICommandSender icommandsender)
	{
		return "EGUpdateRate <integer>";
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
			int rate = Integer.parseInt(astring[0]);
			
			EnviroGenMod.instance.UpdateRate = rate;
			
			sender.addChatMessage(new ChatComponentText("Set EnviroGen Update Rate to: " + rate));
		}
		catch (NumberFormatException e)
		{
			sender.addChatMessage(new ChatComponentText("Argument must be an integer"));
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
