using System.Collections.Generic;

namespace _Project.Scripts.Memento
{
    public class GameCaretaker
    {
        private Stack<GameMemento> _mementos = new Stack<GameMemento>();

        public void Save(GameMemento memento)
        {
            _mementos.Push(memento);
        }

        public GameMemento LoadLast()
        {
            return _mementos.Count > 0 ? _mementos.Pop() : null;
        }

        public void Clear()
        {
            _mementos.Clear();
        }
    }

}