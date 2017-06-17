using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class RayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;
        //khi khởi tạo thì truyền điều kiện ủy thá và hàm ủy thác
        public RayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            if (execute == null)
            {
                throw new ArgumentException("execute");
            }
            _canExecute = canExecute;
            _execute = execute;
        }
        //tạo 1 event có tên tương ứng để ủy thác
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //điều kiện chạy commad
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }
        //hàm ủy thác khi gọi commad
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
