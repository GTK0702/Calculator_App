using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    /// <summary>
    /// 計算を行うModelクラスです。
    /// </summary>
    class CalculateModel
    {
        /// <summary>
        /// 計算結果
        /// </summary>
        private double _result;

        /// <summary>
        /// 計算結果を設定・取得します。
        /// </summary>
        public double Result
        {
            get { return _result; }
            private set { _result = value; }
        }

        /// <summary>
        /// 計算式
        /// </summary>
        private string _formula = "";

        /// <summary>
        /// 計算式を取得します。
        /// </summary>
        public string Formula
        {
            get { return _formula; }
            private set { _formula = value; }
        }

        public void Clear()
        {
            this.Formula = "";
            this.Result = 0;

        }

        /// <summary>
        /// 式に数字文字列を追加します。
        /// </summary>
        /// <param name="param">数字文字列</param>
        public void AddNumber( string param)
        {
            string formula = this.Formula;
            var operations = new char[] { '+', '-', '*', '/' };

            //計算過程に演算子含まれているかどうかで判別。
            if (formula.IndexOfAny(operations) == -1)
            {
                //最初の数字入力時の処理
                switch (formula.Substring(0))
                {
                    case ("0"):
                        switch (param)
                        {
                            case ("00"):
                                formula = "";
                                param = "0";
                                break;
                            case ("."):
                                break;
                            default:
                                formula = "";
                                break;
                        }
                        break;
                    case (""):
                        if (param == "00")
                        {
                            param = "0";
                        }
                        break;
                    default:
                        break;
                }

                //小数点入力時の処理
                if(formula.Length==0 && param == ".")
                {
                    param = "0.";
                }
            }
            else
            {
                //演算子入力後、数値入力時の処理
                int index = formula.LastIndexOfAny(operations);
                switch (formula.Substring(index+1))
                {
                    case ("0"):
                        switch (param)
                        {
                            case ("00"):
                                formula = formula.Remove(index + 1);
                                param = "0";
                                break;
                            case ("."):
                                if (param == "00")
                                {
                                    param = "0";
                                }
                                break;
                            default:
                                formula = formula.Remove(index + 1);
                                break;
                        }
                        break;
                    case (""):
                        if (param == "00")
                        {
                            param = "0";
                        }
                        break;
                    default:
                        break;
                }

                //小数点入力時の処理
                if (formula.Substring(index + 1)=="" && param == ".")
                {
                    param = "0.";
                }
            }

            formula += param;
            this.Formula = formula;
            
        }

        /// <summary>
        /// 式に演算子を追加します。
        /// </summary>
        /// <param name="param">演算子文字列</param>
        public void AddOperate( string param)
        {
            // 計算式がない場合
            if( this.Formula.Length == 0)
            {
                this.Formula += param;
                return;
            }
            // 最後が演算子の場合
            string formula = this.Formula;
            string lastword = formula[formula.Length - 1].ToString();
            switch (lastword)
            {
                case "+":
                    // 次に来るものが-の場合
                    if (param == "-")
                    {
                        // 最後を置き換える
                        formula = formula.TrimEnd(new char[] { '+' }) + param;
                    }
                    // そのほかの場合は何もしない
                    break;
                case "-":
                    // 次に来るものが-の場合
                    if( param == "-")
                    {
                        // 最後を置き換える
                        formula = formula.TrimEnd(new char[] { '-' }) + "+";
                    }
                    // そのほかの場合は何もしない
                    break;
                default:
                    // そのまま追加
                    formula += param;
                    break;
            }
            this.Formula = formula;
        }

        public bool IsNumberCommand(string param)
        {
            string formula = this.Formula;
            var operations = new char[] { '+', '-', '*', '/' };

            //小数点連打によるエラー発生を防ぐ。
            //計算過程に演算子含まれているかどうかで判別。
            if (formula.IndexOfAny(operations) == -1)
            {
                if (formula.Contains("."))
                {
                    return !(param == ".");
                }
            }
            else
            {
                int index = formula.LastIndexOfAny(operations);
                if(formula.Substring(index + 1).Contains("."))
                {
                    return !(param == ".");
                }
            }

            return true;
        }

        /// <summary>
        /// 次に演算子文字列を追加できるかを取得します。
        /// </summary>
        /// <param name="param">演算子文字列</param>
        /// <returns>追加できる場合は、true。その他false。</returns>
        public bool IsOperateCommand( string param)
        {
            // 計算式がない場合
            if( this.Formula.Length == 0)
            {
                // +と-のみOKとする
                return (param == "+" || param == "-");
            }

            // 最後の1文字を取得
            string lastword = this.Formula[this.Formula.Length - 1].ToString();
            // 最後の演算子が*/の場合、+-はOK
            // 最後の演算子が+-の場合、+-はOK
            if( "*/+-".IndexOf(lastword) >= 0)
            {
                return !(param == "*" || param == "/");
            }
            return true;
        }

        /// <summary>
        /// 計算を行います。
        /// </summary>
        public void Calculate()
        {
            // 計算式を解析
            string formula = this.Formula;
            // 先頭の+を削除、最後に演算子がある場合は、削除
            formula = formula.TrimStart('+').TrimEnd(new char[] { '+', '-', '*', '/' });
            // -の前に+を付与
            // 先頭の+を削除
            formula = formula.Replace("-", "+-").TrimStart('+');
            // +でSplit
            string[] formulas = formula.Split('+');
            // ばらした中身に*/がある場合は、計算する
            // ただし、? * -xx の場合は、複数つに分かれているので注意
            List<double> list = new List<double>();
            string prev = "";
            foreach (string work in formulas)
            {
                if (work.EndsWith("*") == true || work.EndsWith("/") == true)
                {
                    prev += work;
                }
                else
                {
                    if (prev.Length == 0)
                    {
                        if (work.Contains("*") == true || work.Contains("/") == true)
                        {
                            list.Add(CalcMulDiv(prev + work));
                        }
                        else
                        {
                            list.Add(Convert.ToDouble(work));
                        }
                    }
                    else
                    {
                        list.Add(CalcMulDiv(prev + work));
                        prev = "";
                    }
                }
            }
            // Listを全部足す
            this.Result = list.Sum();
        }


        private double CalcMulDiv(string formula)
        {
            // *と/の前後に#を入れる
            formula = formula.Replace("*", "#*#").Replace("/", "#/#");
            // #でsplit
            string[] formulas = formula.Split('#');

            // 先頭を取得
            double result = Convert.ToDouble(formulas[0]);

            // 2番目から順番に計算していく
            for (int i = 1; i < formulas.Length - 1; i++)
            {
                if (formulas[i] == "*")
                {
                    result *= Convert.ToDouble(formulas[i + 1]);
                }
                else if (formulas[i] == "/")
                {
                    result /= Convert.ToDouble(formulas[i + 1]);
                }
            }
            return result;
        }

        public void BackSpace()
        {
            string formula = this.Formula;
            var lastword = formula[formula.Length - 1];

            formula = formula.TrimEnd(lastword);
            this.Formula = formula;

        }
    }
}
