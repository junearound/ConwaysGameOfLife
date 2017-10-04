using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConwaysGameOfLife.UI
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute = null;
        private readonly Predicate<T> _canExecute = null;

        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

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

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }


//    private Action<object> execute;
//    private Func<object, bool> canExecute;

//    public event EventHandler CanExecuteChanged
//    {
//        add { CommandManager.RequerySuggested += value; }
//        remove { CommandManager.RequerySuggested -= value; }
//    }

//    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
//    {
//        this.execute = execute;
//        this.canExecute = canExecute;
//    }

//    public bool CanExecute(object parameter)
//    {
//        return this.canExecute == null || this.canExecute(parameter);
//    }

//    public void Execute(object parameter)
//    {
//        this.execute(parameter);
//    }
//}
}

