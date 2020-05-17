using System;

namespace Noyau
{
    [Serializable]
    class EmptyListException : Exception
    {
        public EmptyListException() { }
    }
}
