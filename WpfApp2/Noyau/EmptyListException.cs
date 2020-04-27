using System;

namespace logisimConsole
{
    [Serializable]
    class EmptyListException : Exception
    {
        public EmptyListException() { }
    }
}
