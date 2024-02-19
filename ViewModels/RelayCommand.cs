using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Textile.ViewModels
{
    public class RelayCommand<T> : ICommand
    {
        #region Fields

        private readonly Action<T> _execute = null;
        private readonly Predicate<object> _canExecute = null;

        #endregion


        #region Constructors

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion


        #region ICommand Implementation

        [DebuggerStepThrough]
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object? parameter)
        {
            if (parameter is T)
            {
                var typedParameter = (T)parameter;
                _execute(typedParameter);
            }
        }

        #endregion
    }
    public class  RelayCommand : ICommand
    {
        #region Fields

        private readonly Action<object> execute = null;
        private readonly Func<object, bool> canExecute;

        #endregion

        #region Constructors

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public  RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region ICommand Implementation

        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }
          public void Execute(object? parameter)
        {
                execute(parameter);
            
        }

        #endregion
    }
}
