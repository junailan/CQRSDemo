using Command;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandService
{
    public class CommandService
    {
        public void CommitCommand(ICommand command)
        {
            CommandHandler commandHandler = new CommandHandler();
            commandHandler.Handle(command);
        }
    }
}
