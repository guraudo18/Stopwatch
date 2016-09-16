using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.Windows;
using Codeer.Friendly.Windows.Grasp;
using RM.Friendly.WPFStandardControls;
using System.Threading;
using Codeer.Friendly.Windows.NativeStandardControls;

namespace FrendrySample
{
    [TestClass]
    public class UnitTest1
    {
        WindowsAppFriend _app;
        WPFButtonBase _messageBoxButton;
        WPFButtonBase _startButton;
        WPFButtonBase _stopButton;
        WPFButtonBase _resetButton;
        WPFButtonBase _lapButton;
        dynamic mainCore;

        [TestInitialize]
        public void TestInitialize()
        {
            _app = new WindowsAppFriend(Process.Start("C:\\Users\\t-yamazaki\\Desktop\\Sample\\Sample\\bin\\Debug\\Sample.exe"));
      
            mainCore = _app.Type<Application>().Current.MainWindow;
            _messageBoxButton = new WPFButtonBase(mainCore.MessageBoxButton);
            _startButton = new WPFButtonBase(mainCore.StartButton);
            _stopButton = new WPFButtonBase(mainCore.StopButton);
            _resetButton = new WPFButtonBase(mainCore.ResetButton);
            _lapButton = new WPFButtonBase(mainCore.LapButton); 
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void TestMethod1()
        {       
            var main = new WindowControl(mainCore);
            var CurrentText = new WPFTextBlock(mainCore.CurrentText);

            //各ボタンの初期状態を確認
            Assert.AreEqual(true, _startButton.IsEnabled);
            Assert.AreEqual(false, _stopButton.IsEnabled);
            Assert.AreEqual(false, _resetButton.IsEnabled);
            Assert.AreEqual(false, _lapButton.IsEnabled);

            //ストップウォッチ画面の初期表示
            Assert.AreEqual(CurrentText.Text, "00:00:00:00");

            //ストップウォッチ起動(非同期)～停止まで
            var async = new Async();
            _startButton.EmulateClick(async);

            Thread.Sleep(1000);

            _stopButton.EmulateClick();
            async.WaitForCompletion();

            //ストップウォッチ画面が更新されているか
            Assert.AreNotEqual(CurrentText.Text, "00:00:00:00");
        }

        [TestMethod]
        public void MessageBoxTest()
        {
            var async = new Async();
            _messageBoxButton.EmulateClick(async);

            //メッセージボックスを取得
            var main = new WindowControl(mainCore);
            var childWindow = main.WaitForNextModal();
            var msg = new NativeMessageBox(childWindow);

            //メッセージを取得
            Assert.AreEqual("msg", msg.Message);

            //テキストからボタンを検索して押す
            msg.EmulateButtonClick("OK");

            //非同期処理の完了待ち
            async.WaitForCompletion();
        }
    }
}
