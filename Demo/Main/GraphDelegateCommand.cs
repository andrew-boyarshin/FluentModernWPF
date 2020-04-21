using System;
using System.Diagnostics;
using System.Windows.Input;

namespace CodeWanda.UI.Main
{
    public sealed class GraphDelegateCommand<T> : ICommand
    {
        private readonly T _parameter;
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public GraphDelegateCommand(T parameter, Action<T> execute, Predicate<T> canExecute = null)
        {
            _parameter = parameter;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// EventHandler to re-evaluate whether this command can execute or not
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }

            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        /// <inheritdoc />
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T) parameter ?? _parameter) ?? true;
        }

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            _execute((T) parameter ?? _parameter);
        }
    }
}