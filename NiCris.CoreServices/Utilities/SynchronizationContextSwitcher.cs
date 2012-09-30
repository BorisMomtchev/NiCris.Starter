using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NiCris.CoreServices.Utilities
{
    public class SynchronizationContextSwitcher
      : IDisposable
    {
        private ExecutionContext _executionContext;
        private readonly SynchronizationContext _oldContext;
        private readonly SynchronizationContext _newContext;

        public SynchronizationContextSwitcher()
            : this(new SynchronizationContext())
        {
        }

        public SynchronizationContextSwitcher(SynchronizationContext context)
        {
            _newContext = context;
            _executionContext = Thread.CurrentThread.ExecutionContext;
            _oldContext = SynchronizationContext.Current;
            SynchronizationContext.SetSynchronizationContext(context);
        }

        public void Dispose()
        {
            if (null != _executionContext)
            {
                if (_executionContext != Thread.CurrentThread.ExecutionContext)
                    throw new InvalidOperationException("Dispose called on wrong thread.");

                if (_newContext != SynchronizationContext.Current)
                    throw new InvalidOperationException("The SynchronizationContext has changed.");

                SynchronizationContext.SetSynchronizationContext(_oldContext);
                _executionContext = null;
            }
        }
    }
}
