using System;

namespace Counter
{
    public interface IHasProgress
    {
        public event EventHandler<ProgressChangedEventArgs> OnProgressChanged;

        public class ProgressChangedEventArgs : EventArgs
        {
            public float ProgressNormalized;
            public bool ProgressStart = false;
            public bool ProgressStop = false;
        }
    }
}