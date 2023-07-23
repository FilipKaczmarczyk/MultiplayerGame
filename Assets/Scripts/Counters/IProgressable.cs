using System;

namespace Counters
{
    public interface IProgressable 
    {
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public class OnProgressChangedEventArgs : EventArgs
        {
            public float progressNormalized;
        }
    }
}

