using System.Collections.Generic;

namespace _Project.Scripts.Memento.Command
{
    public class CommandManager
    {
        private Stack<ICommand> _commands = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _commands.Push(command);
        }

        public void UndoLastCommand()
        {
            if (_commands.Count > 0)
            {
                var command = _commands.Pop();
                command.Undo();
            }
        }
    }

}