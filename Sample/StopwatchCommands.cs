using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Timers;
using System.Windows;

namespace SampleStopwatch
{
    /// <summary>
    /// スタートボタン押下時
    /// </summary>
    public class StartCommand : ICommand
    {
        StopwatchViewModel _vm;
        Action _action;

        public StartCommand(StopwatchViewModel vm, Action action)
        {
            _vm = vm;
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return !_vm.IsRunning;
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
        public void Execute(object parameter)
        {
            //リセットボタンのCanExecuteの条件が変更されたことを通知する。a
            _vm.ResetCommand.RaiseCanExecuteChanged();
            this._action();
            _vm.RunRefresh();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    /// <summary>
    /// ストップボタン押下時
    /// </summary>
    public class StopCommand : ICommand
    {
        StopwatchViewModel _vm;
        Action _action;

        public StopCommand(StopwatchViewModel vm,Action action)
        {
            this._vm = vm;
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return _vm.IsRunning;
        }

        public void Execute(object parameter)
        {
            this._action();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    /// <summary>
    /// リセットボタン押下時
    /// </summary>
    public class ResetCommand : ICommand
    {
        StopwatchViewModel _vm;
        Action _action;

        public ResetCommand(StopwatchViewModel vm,Action action)
        {
            _vm = vm;
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return _vm.CurrentTime == ("00:00:00:00") ? false : true;
        }

        public void Execute(object parameter)
        {
            _action();
            this.RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    /// <summary>
    /// ラップボタン押下時
    /// </summary>
    public class LapCommand : ICommand
    {
        StopwatchViewModel _vm;
        Action _action;

        public LapCommand(StopwatchViewModel vm, Action action)
        {
            this._vm = vm;
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return _vm.IsRunning;
        }

        public void Execute(object parameter)
        {
            this._action();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    /// <summary>
    /// ログ出力コマンド
    /// </summary>
    public class OutputLogButtonCommand : ICommand
    {
        StopwatchViewModel _vm;
        Action _action;

        public OutputLogButtonCommand(StopwatchViewModel vm, Action action)
        {
            this._vm = vm;
            this._action = action;
        } 

        public bool CanExecute(object parameter) 
        {
            return _vm.LapTimes.Count == 0 ? false : true;
        }

        public void Execute(object parameter)
        {
            this._action();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

}
