using System.Reflection;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Calculator.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// キーが押された時のイベントハンドラ
        /// </summary>
        /// <param name="e">キーイベント情報</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.Key)
            {
                case (Key.Delete):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(AllClearButton, new object[0]);
                    break;
                case (Key.D0):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Zero, new object[0]);
                    break;
                case (Key.D1):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(One, new object[0]);
                    break;
                case (Key.D2):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Two, new object[0]);
                    break;
                case (Key.D3):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Three, new object[0]);
                    break;
                case (Key.D4):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Four, new object[0]);
                    break;
                case (Key.D5):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Five, new object[0]);
                    break;
                case (Key.D6):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Six, new object[0]);
                    break;
                case (Key.D7):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Seven, new object[0]);
                    break;
                case (Key.D8):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Eight, new object[0]);
                    break;
                case (Key.D9):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Nine, new object[0]);
                    break;
                case (Key.A):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Add, new object[0]);
                    break;
                case (Key.S):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Sub, new object[0]);
                    break;
                case (Key.M):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Mul, new object[0]);
                    break;
                case (Key.D):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Div, new object[0]);
                    break;
                case (Key.E):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(Equ, new object[0]);
                    break;
                case (Key.Back):
                    typeof(ButtonBase)
                        .GetMethod("OnClick",
                            BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(BackSpace, new object[0]);
                    break;
            }
            if (e.Key == Key.L)
            {
             typeof(ButtonBase)
             .GetMethod("OnClick",
             BindingFlags.Instance | BindingFlags.NonPublic)
             .Invoke(LeftBracket, new object[0]);
            }
            if (e.Key == Key.R)
            {
                typeof(ButtonBase)
                .GetMethod("OnClick",
                BindingFlags.Instance | BindingFlags.NonPublic)
                .Invoke(RightBracket, new object[0]);
            }

        }
        /// <summary>
        /// キーが離されたときにイベントハンドラ
        /// </summary>
        /// <param name="e">キーイベント情報</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }
    }
}
