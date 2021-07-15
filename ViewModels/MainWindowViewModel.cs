using Calculator.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    /// <summary>
    /// CalcutatorのViewModelクラスです。
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// タイトル
        /// </summary>
        private string _title = "WPFで電卓！";

        /// <summary>
        /// タイトルを設定・取得します。
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// 計算式
        /// </summary>
        private string _formula = "";

        /// <summary>
        /// 計算式文字列を設定・取得します。
        /// </summary>
        public string Formula
        {
            get { return _formula; }
            set { SetProperty(ref _formula, value); }
        }

        /// <summary>
        /// 計算結果
        /// </summary>
        private string _result = "";

        /// <summary>
        /// 計算結果を設定・取得します。
        /// </summary>
        public string Result
        {
            get { return _result; }
            set { SetProperty(ref _result, value); }
        }

        /// <summary>
        /// クリアコマンドを設定・取得します。
        /// </summary>
        public DelegateCommand AllClearCommand { get; set; }
        /// <summary>
        /// 数字コマンドを設定・取得します。
        /// </summary>
        public DelegateCommand<string> NumberCommand { get; set; }
        /// <summary>
        /// 演算子コマンドを設定・取得します。
        /// </summary>
        public DelegateCommand<string> OperateCommand { get; set; }
        /// <summary>
        /// 計算コマンドを設定・取得します。
        /// </summary>
        public DelegateCommand CalculateCommand { get; set; }

        /// <summary>
        /// 計算コマンドを設定・取得します。
        /// </summary>
        public DelegateCommand BackSpaceCommand { get; set; }

        /// <summary>
        /// 計算を行うModelクラスです。
        /// </summary>
        private CalculateModel model = new CalculateModel();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            // コマンドの初期化を行います。
            AllClearCommand = new DelegateCommand(AllClearCommandExecute, CanAllClearCommandExecute);
            NumberCommand = new DelegateCommand<string>(NumberCommandExecute, CanNumberCommandExecute);
            OperateCommand = new DelegateCommand<string>(OperateCommandExecute, CanOperateCommandExecute);
            CalculateCommand = new DelegateCommand(CalculateCommandExecute, CanCalculateCommandExecute);
            BackSpaceCommand = new DelegateCommand(BackSpaceCommandExcute, CanBackSpaceCommandExcute);
        }

        /// <summary>
        /// 計算式、結果をクリアします。
        /// </summary>
        private void AllClearCommandExecute()
        {
            model.Clear();
            ReDisplay();
        }
        /// <summary>
        /// 計算式、結果をクリアできるかを取得します。
        /// </summary>
        /// <returns>クリアできる場合は、true。その他false。</returns>
        private bool CanAllClearCommandExecute()
        {
            return true;
        }
        /// <summary>
        /// 渡された数字文字列を計算式に追加します。
        /// </summary>
        /// <param name="param">数字文字列(0-9 or .)</param>
        private void NumberCommandExecute(string param)
        {
            model.AddNumber(param);
            ReDisplay();
        }
        /// <summary>
        /// 計算式に数字文字列を追加できるかを取得します。
        /// </summary>
        /// <param name="param">数字文字列(0-9 or .)</param>
        /// <returns>追加できる場合は、true。その他false。</returns>
        private bool CanNumberCommandExecute(string param)
        {
            return model.IsNumberCommand(param);
        }
        /// <summary>
        /// 演算子文字列を計算式に追加します。
        /// </summary>
        /// <param name="param">演算子文字列(*/+-)</param>
        private void OperateCommandExecute(string param)
        {
            model.AddOperate(param);
            ReDisplay();
        }
        /// <summary>
        /// 演算子文字列を計算式に追加できるかを取得します。
        /// </summary>
        /// <param name="param">演算子文字列(*/+-)</param>
        /// <returns>追加できる場合は、true。その他false。</returns>
        private bool CanOperateCommandExecute(string param)
        {
            return model.IsOperateCommand(param);
        }
        /// <summary>
        /// 計算します。
        /// </summary>
        private void CalculateCommandExecute()
        {
            model.Calculate();
            ReDisplay();
        }
        /// <summary>
        /// 計算できるかを取得します。
        /// </summary>
        /// <returns>計算できる場合は、true。その他false。</returns>
        private bool CanCalculateCommandExecute()
        {
            return true;
        }

        /// <summary>
        /// 計算過程の末尾を1文字消す。
        /// </summary>
        private void BackSpaceCommandExcute()
        {
            model.BackSpace();
            ReDisplay();
        }

        /// <summary>
        /// 計算過程の末尾を1文字消せるか取得。
        /// </summary>
        /// <returns>計算過程を消せる場合は、true。その他false。</returns>
        private bool CanBackSpaceCommandExcute()
        {
            return true;
        }

        /// <summary>
        /// 計算式・計算結果を再描画します。
        /// </summary>
        private void ReDisplay()
        {
            this.Formula = model.Formula;
            if (this.Formula.Length == 0)
            {
                this.Result = model.Result == 0 ? "" : model.Result.ToString();
            }
            else
            {
                this.Result = model.Result.ToString();
            }
            this.OperateCommand.RaiseCanExecuteChanged();
        }
    }
}
