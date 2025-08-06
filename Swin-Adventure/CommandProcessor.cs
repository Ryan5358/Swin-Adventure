using System;

namespace Swin_Adventure
{
    public class CommandProcessor
    {
        private List<Command> _commands;
       
        public CommandProcessor(Command[] commands) 
        {
            _commands = new List<Command>();
            foreach (Command command in commands)
            {
                _commands.Add(command); 
            }
        }
        
        public string Execute(Player p, string input)
        {
            string[] text = input.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            foreach (Command command in _commands)
            {
                if (command.AreYou(text[0]))
                {
                    return command.Execute(p, text);
                }
            }
            return $"I don't understand {input}.";
        }
    }
}
