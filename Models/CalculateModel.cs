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
            string formula = this.Formula;
            //計算回数
            int OpCnt = 0;
            //括弧処理の優先度
            int nest = 0;
            //小数点以下の位
            int bit = 0;
            //数値を格納
            var Value = new List<Double>();
            //演算子を格納
            var Operator = new List<String>();
            //優先度を格納
            var Priority = new List<int>();
            //文字
            string chr;
            //小数点以下の入力となるか判別
            bool IsDecimal = false;

            //辞書。数字と演算子。
            var Number = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var Operations = new char[] { '+', '-', '*', '/' };

            //リストの初期化
            int i = 0;
            while (i <= formula.Length)
            {
                Value.Add(0);
                Operator.Add("");
                Priority.Add(0);
                i += 1;
            }

            //計算過程の解析処理
            i = 0;
            while (i < formula.Length)
            {
                chr = formula.Substring(i, 1);

                //chrが数字である場合の処理
                if (chr.IndexOfAny(Number) != -1)
                {
                    //文字列に小数点が含まれるかどうかで判別。
                    if (IsDecimal)
                    {
                        bit += 1;
                        Value[OpCnt] += SmallPart(chr, bit);
                    }
                    else
                    {
                        Value[OpCnt] = 10 * Value[OpCnt] + Convert.ToDouble(chr);
                    }
                }

                //chrが演算子である場合の処理
                if (chr.IndexOfAny(Operations) != -1)
                {
                    Operator[OpCnt] = chr;
                    if (chr == "+" || chr == "-")
                    {
                        Priority[OpCnt] = nest + 1;
                    }
                    else
                    {
                        Priority[OpCnt] = nest + 2;
                    }
                    OpCnt += 1;
                    Value[OpCnt] = 0;
                    IsDecimal = false;
                    bit = 0;
                }

                //chrが括弧であるときの処理。括弧で囲まれる数式の優先度を上げる。
                if (chr == "(")
                {
                    nest += 10;
                }

                if (chr == ")")
                {
                    nest -= 10;
                }

                //chrが小数点であるときの処理。
                if (chr == ".")
                {
                    IsDecimal = true;
                }

                i += 1;
            }

            //計算処理
            while (OpCnt > 0)
            {
                int ip = 0;

                int j = 1;
                while (j < OpCnt)
                {
                    if (Priority[ip] < Priority[j])
                    {
                        ip = j;
                    }
                    j += 1;
                }

                chr = Operator[ip];
                if (chr == "+")
                {
                    Value[ip] += Value[ip + 1];
                }
                if (chr == "-")
                {
                    Value[ip] -= Value[ip + 1];
                }
                if (chr == "*")
                {
                    Value[ip] *= Value[ip + 1];
                }
                if (chr == "/")
                {
                    Value[ip] /= Value[ip + 1];
                }

                j = ip + 1;
                while (j < OpCnt)
                {
                    Value[j] = Value[j + 1];
                    Operator[j - 1] = Operator[j];
                    Priority[j - 1] = Priority[j];
                    j += 1;
                }
                OpCnt -= 1;
            }
            this.Result += Value[0];
        }

        /// <summary>
        /// 受取った数字を小数にします。
        /// </summary>
        private double SmallPart(string s,int i)
        {
            int j = 1;
            double place = 1;

            while (j <= i)
            {
                place *= 0.1;
                j += 1;
            }
            return Convert.ToDouble(s) * place;
        }

        //private int CountChar(string s,string t)
        //{
        //    return s.Length - s.Replace(s, "").Length;
        //}


        //private double CalcMulDiv(string formula)
        //{
        //    // *と/の前後に#を入れる
        //    formula = formula.Replace("*", "#*#").Replace("/", "#/#");
        //    // #でsplit
        //    string[] formulas = formula.Split('#');

        //    // 先頭を取得
        //    double result = Convert.ToDouble(formulas[0]);

        //    // 2番目から順番に計算していく
        //    for (int i = 1; i < formulas.Length - 1; i++)
        //    {
        //        if (formulas[i] == "*")
        //        {
        //            result *= Convert.ToDouble(formulas[i + 1]);
        //        }
        //        else if (formulas[i] == "/")
        //        {
        //            result /= Convert.ToDouble(formulas[i + 1]);
        //        }
        //    }
        //    return result;
        //}

        //計算過程の末尾を1文字消す.
        public void BackSpace()
        {
            string formula = this.Formula;
            var lastword = formula[formula.Length - 1];

            formula = formula.TrimEnd(lastword);
            this.Formula = formula;

        }
    }
}
