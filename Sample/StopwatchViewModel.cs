/*
ヘッダテスト
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Mvvm;
using System.Timers;
using System.Collections.ObjectModel;

namespace SampleStopwatch
{
    /// <summary>
    /// ストップウォッチViewModel
    /// 表示部分で使用するプロパティを記述する。
    /// </summary>
    public class StopwatchViewModel : BindableBase
    {
        /// <summary>
        /// ストップウォッチModel
        /// </summary>
        StopwatchModel stopwatchModel = new StopwatchModel();

        /// <summary>
        /// ストップウォッチが起動中かどうか
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return this.stopwatchModel.IsRunning;
            }
        }

        /// <summary>
        /// lapタイム
        /// </summary>
        public ObservableCollection<string> LapTimes
        {
            get
            {
                return this.stopwatchModel.LapTimes;
            }
        }

        string _currentTime = "00:00:00:00";
        /// <summary>
        /// 表示用の時間
        /// </summary>
        public string CurrentTime
        {
            get
            {
                return this._currentTime;
            }

            set
            {
                this.SetProperty(ref this._currentTime, value);
            }
        } 

        private StartCommand _startCommand;
        /// <summary>
        /// スタートボタンコマンド
        /// </summary>
        public StartCommand StartCommand
        {
            get
            {
                return this._startCommand ?? (this._startCommand = new StartCommand(this, this.stopwatchModel.RunTimeCount));
            }
        }

        private ICommand _stopCommand;
        /// <summary>
        /// ストップボタンコマンド
        /// </summary>
        public ICommand StopCommand
        {
            get
            {
                return this._stopCommand ?? (this._stopCommand = new StopCommand(this, this.stopwatchModel.StopTimeCount));
            }
        }

        private ResetCommand _resetCommand;
        /// <summary>
        /// リセットボタンコマンド
        /// </summary>
        public ResetCommand ResetCommand
        {
            get
            {
                return this._resetCommand ?? (this._resetCommand = new ResetCommand(this, this.stopwatchModel.Reset));
            }
        }

        private LapCommand _lapCommand;
        /// <summary>
        /// ラップボタンコマンド
        /// </summary>
        public LapCommand LapCommand
        {
            get
            {
                return this._lapCommand ?? (this._lapCommand = new LapCommand(this, this.stopwatchModel.RecordLap));
            }
        }

        private OutputLogButtonCommand _outputLogButtonCommand;
        /// <summary>
        /// ラップボタンコマンド
        /// </summary>
        public OutputLogButtonCommand OutputLogButtonCommand
        {
            get
            {
                return this._outputLogButtonCommand ?? 
                    (this._outputLogButtonCommand = new OutputLogButtonCommand(this, this.stopwatchModel.OutputCsv));
            }
        }

        //以下ストップウォッチ画面表示の更新処理
        Timer _timer = new Timer();

        /// <summary>
        /// 表示画面リフレッシュ用メソッド
        /// ストップウォッチスタート時に定期的に表示画面のリフレッシュを行う。
        /// </summary>
        public void RunRefresh()
        {
            _timer.Elapsed += new ElapsedEventHandler(OnElapsed_TimersTimer);
            _timer.Interval = 10;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnElapsed_TimersTimer(object sender, ElapsedEventArgs e)
        {
            CurrentTime = this.stopwatchModel.CurrentTime.ToString(@"hh\:mm\:ss\:ff");
        }

        /// <summary>
        /// 表示画面リフレッシュ停止メソッド
        /// ストップウォッチ停止時に定期リフレッシュを停止する。
        /// </summary>
        public void StopRefresh()
        {
            this._timer.Stop();
        }
    }
}
