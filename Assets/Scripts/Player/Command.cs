using UnityEngine;

namespace Midterm
{
    public abstract class Command
    {
        // Execution Method
        public abstract void Execute();

        // Checks if the current command has been executed or not
        public abstract bool isComplete { get; } // abstract classes need gets

    }
}
