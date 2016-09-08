using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleStopwatch
{
    /// <summary>
    /// ストップウォッチModel
    /// 表示以外で使用するステートやビジネスロジックを記述
    /// </summary>
    public class StopwatchModel
    {
        Stopwatch stopwatch = new Stopwatch();
        
        /// <summary>
        /// ストップウォッチが起動中かどうか
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return this.stopwatch.IsRunning;
            }
        }

        ObservableCollection<string> _lapTimes = new ObservableCollection<string>();
        /// <summary>
        /// Lapタイム
        /// </summary>
        public ObservableCollection<string> LapTimes
        {
            get
            {
                return this._lapTimes;
            }
        }

        /// <summary>
        /// 表示用の時間
        /// </summary>
        public TimeSpan CurrentTime
        {
            get
            {
                return this.stopwatch.Elapsed;
            }
        }

        /// <summary>
        /// スタート
        /// </summary>
        public void RunTimeCount()
        {
            if (!this.stopwatch.IsRunning)
            {
                this.stopwatch.Start();
            }
        }

        /// <summary>
        /// ストップ
        /// </summary>
        public void StopTimeCount()
        {
            if (this.stopwatch.IsRunning)
            {
                this.stopwatch.Stop();
            }
        }

        /// <summary>
        /// リセット
        /// </summary>
        public void Reset()
        {
            this.stopwatch.Reset();
            this.LapTimes.Clear();
        }

        /// <summary>
        /// lap記録
        /// </summary>
        public void RecordLap()
        {
            var stringLapTime = this.stopwatch.Elapsed.ToString(@"hh\:mm\:ss\:ff");
            var addItem = string.Format("[{0}]{1}", this._lapTimes.Count, stringLapTime);
            _lapTimes.Add(addItem);
        }

        /// <summary>
        /// ログ出力
        /// </summary>
        public void OutputCsv()
        {
            using (var sw = new System.IO.StreamWriter(@"LapLog.csv", true))
            {
                foreach (string outputItem in _lapTimes)
                {
                    sw.WriteLine(outputItem);
                }
            }
        }
    }
}
