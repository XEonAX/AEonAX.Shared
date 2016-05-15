using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AEonAX.Shared
{

    public class NotifyBase : INotifyPropertyChanged
    {
        /// <summary>
        /// This is the implementation of INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property. 
        // The CallerMemberName Attribute below is new to .NET Framework 4.5.  
        //It determines the calling property name automatically!

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }





        //DoSomethingCommand = new SimpleCommand
        //            {
        //                ExecuteDelegate = this.RunCommand
        //            };


        //DoSomethingCommand = new SimpleCommand
        //            {
        //                ExecuteDelegate = o => this.SelectedItem = o,
        //                CanExecuteDelegate = o => o != null
        //            };

    public class SimpleCommand : SimpleCommand<object> { };
    public class SimpleCommand<T> : ICommand
    {

        /// <summary>
        /// Bool Function to Call
        /// </summary>
        public Predicate<T> CanExecuteDelegate { get; set; }

        /// <summary>
        /// Call Bool Function
        /// </summary>
        public Action<T> ExecuteDelegate { get; set; }

        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
                return CanExecuteDelegate((T)parameter);
            return true;// if there is no can execute default to true
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null)
                ExecuteDelegate((T)parameter);
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="X">Predicate<X></typeparam>
    /// <typeparam name="Y">Action<Y></typeparam>
    public class ComplexCommand<X,Y> : ICommand
    {
        public Predicate<X> CanExecuteDelegate { get; set; }
        public Action<Y> ExecuteDelegate { get; set; }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
                return CanExecuteDelegate((X)parameter);
            return true;// if there is no can execute default to true
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null)
                ExecuteDelegate((Y)parameter);
        }
        #endregion
    }


       

}
