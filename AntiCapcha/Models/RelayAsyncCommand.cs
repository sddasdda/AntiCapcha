using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiCapcha.Models
{
    public class RelayAsyncCommand : RelayCommand
    {
        private bool isExecuting = false;

        public event EventHandler Started;

        public event EventHandler Ended;

        public bool IsExecuting
        {
            get { return this.isExecuting; }
        }

        public RelayAsyncCommand(Action<object> execute, Func<Boolean> canExecute)
            : base(execute, canExecute)
        {
        }

        public RelayAsyncCommand(Action<object> execute)
            : base(execute)
        {
        }

        public override Boolean CanExecute(Object parameter)
        {
            return ((base.CanExecute(parameter)) && (!this.isExecuting));
        }

        public override void Execute(object parameter)
        {
            try
            {
                this.isExecuting = true;
                if (this.Started != null)
                {
                    this.Started(this, EventArgs.Empty);
                }

                Task task = Task.Factory.StartNew(() =>
                {
                    this.execute(parameter);
                });
                task.ContinueWith(t =>
                {
                    this.OnRunWorkerCompleted(EventArgs.Empty);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                this.OnRunWorkerCompleted(new RunWorkerCompletedEventArgs(null, ex, true));
            }
        }

        private void OnRunWorkerCompleted(EventArgs e)
        {
            this.isExecuting = false;
            if (this.Ended != null)
            {
                this.Ended(this, e);
            }
        }
    }
}
