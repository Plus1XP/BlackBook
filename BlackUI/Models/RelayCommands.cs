using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlackUI.Models
{
    public class Command : ICommand
    {
        protected readonly Func<Boolean> canExecute;

        protected readonly Action execute;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public Command(Action execute, Func<Boolean> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public Command(Action execute)
            : this(execute, null)
        {
        }

        public virtual Boolean CanExecute(Object parameter)
        {
            return this.canExecute == null ? true : this.canExecute();
        }

        public virtual void Execute(Object parameter)
        {
            this.execute();
        }
    }

    //public class Command<T> : ICommand
    //{
    //    protected readonly Func<Boolean> canExecute;

    //    protected readonly Action<T> execute;

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add
    //        {
    //            if (this.canExecute != null)
    //            {
    //                CommandManager.RequerySuggested += value;
    //            }
    //        }

    //        remove
    //        {
    //            if (this.canExecute != null)
    //            {
    //                CommandManager.RequerySuggested -= value;
    //            }
    //        }
    //    }

    //    public Command(Action<T> execute, Func<Boolean> canExecute)
    //    {
    //        if (execute == null)
    //        {
    //            throw new ArgumentNullException("execute");
    //        }
    //        this.execute = execute;
    //        this.canExecute = canExecute;
    //    }

    //    public Command(Action<T> execute)
    //        : this(execute, null)
    //    {
    //    }

    //    public virtual Boolean CanExecute(Object parameter)
    //    {
    //        return this.canExecute == null ? true : this.canExecute();
    //    }

    //    public virtual void Execute(Object parameter)
    //    {
    //        this.execute((T)parameter);
    //    }
    //}

    public class Command<T> : ICommand
    {
        protected readonly Func<T, Boolean> canExecute;

        protected readonly Action<T> execute;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public Command(Action<T> execute, Func<T, Boolean> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public Command(Action<T> execute)
            : this(execute, null)
        {
        }

        public virtual Boolean CanExecute(Object parameter)
        {
            return this.canExecute == null ? true : this.canExecute((T)parameter);
        }

        public virtual void Execute(Object parameter)
        {
            this.execute((T)parameter);
        }
    }

    public class AsyncCommand : Command
    {
        private bool isExecuting = false;

        public event EventHandler Started;

        public event EventHandler Ended;

        public bool IsExecuting
        {
            get { return this.isExecuting; }
        }

        public AsyncCommand(Action execute, Func<Boolean> canExecute)
            : base(execute, canExecute)
        {
        }

        public AsyncCommand(Action execute)
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
                    this.execute();
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
