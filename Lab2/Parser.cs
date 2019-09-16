using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public class Parser
    {
        enum Types { NONE, DELIMITER, NUMBER };
        string exp;
        int expIdx;
        string token;
        Types tokType;
        public string str_error = "";

        public Parser()
        {

        }

        public double ExpressionStart(string exprStr)
        {
            double result;
            exp = exprStr;
            int l = exp.Length;

            if (exprStr.Equals(""))
            {
                str_error = "Порожній вираз!";
                //MessageBox.Show(str_error);
                return 0.0;
            }

            if (exp[l - 1].Equals('+') || exp[l - 1].Equals('-') || exp[l - 1].Equals('/') || exp[l - 1].Equals('*') || exp[l - 1].Equals('^') || exp[l - 1].Equals('%') || exp[l - 1].Equals('|'))
            {
                MessageBox.Show("Остання лексема повинна бути виразом");
                str_error = "invalid expression";
            }
            expIdx = 0;
            try
            {
                GetToken();
                if (token.Equals(""))
                {
                    str_error = "Неправильно введений вираз!";
                    MessageBox.Show(str_error);
                    return 0.0;
                }
                ExpPlusMin(out result);
                if (!token.Equals(""))
                {
                    str_error = "Остання лексема повинна бути )";
                }
                return result;
            }
            catch (Exception)
            {
                MessageBox.Show(str_error);
                return 0.0;
            }
        }

        void Expr(out double result)
        {
            ExpPlusMin(out result);
        }

        void ExpPlusMin(out double result)
        {
            string op;
            double partialResult;

            ExpMultDiv(out result);
            while ((op = token).Equals("+") || op.Equals("-"))
            {
                GetToken();
                ExpMultDiv(out partialResult);
                switch (op)
                {
                    case "+":
                        result = result + partialResult;
                        break;
                    case "-":
                        result = result - partialResult;
                        break;
                }
            }
        }

        void ExpMultDiv(out double result)
        {
            string op;
            double partialResult = 0.0;
            ExpStepin(out result);
            while ((op = token).Equals("*") || op.Equals("/") || op.Equals("%") || op.Equals("|"))
            {
                GetToken();
                ExpStepin(out partialResult);
                switch (op)
                {
                    case "|":
                        if (partialResult.Equals(0.0))
                        {
                            MessageBox.Show("Ділення на нуль!");
                            str_error = "div by 0";
                        }
                        else
                            result = (int)(result) / (int)(partialResult);
                        break;
                    case "*":
                        result = result * partialResult;
                        break;
                    case "/":
                        if (partialResult.Equals(0.0))
                        {
                            MessageBox.Show("Ділення на нуль!");
                            str_error = "div by 0";
                        }
                        else
                            result = result / partialResult;
                        break;
                    case "%":
                        if (partialResult.Equals(0.0))
                        {
                            MessageBox.Show("Ділення на нуль!");
                            str_error = "div by 0";
                        }
                        else
                            result = (int)(result) % (int)(partialResult);
                        break;
                }
            }
        }

        void ExpStepin(out double result)
        {
            double partialResult;
            double ex;

            int t;
            ExpIncDec(out result);
            if (token.Equals("^"))
            {
                GetToken();
                ExpStepin(out partialResult);
                ex = result;
                if (partialResult.Equals(0.0))
                {
                    result = 1.0;
                    return;
                }

                for (t = (int)partialResult - 1; t > 0; t--)
                {
                    result = result * (double)ex;
                }
            }
        }

        void ExpIncDec(out double result)
        {
            string op;
            op = "";
            if (tokType.Equals(Types.DELIMITER) && token.Equals("+") || token.Equals("-"))
            {
                op = token;
                GetToken();
            }

            ExpDuzhka(out result);
            if (op.Equals("+"))
            {
                ++result;
            }
            if (op.Equals("-"))
            {
                --result;
            }
        }

        void ExpDuzhka(out double result)
        {
            if (token.Equals("("))
            {
                GetToken();
                ExpPlusMin(out result);
                if (!token.Equals(")"))
                {
                    MessageBox.Show("Різна кількість дужок!");
                    str_error = "invalid expression";
                }
                GetToken();
            }
            else
            {
                Atom(out result);
            }
        }

        void Atom(out double result)
        {
            switch (tokType)
            {
                case Types.NUMBER:
                    try
                    {
                        result = Double.Parse(token);
                    }
                    catch (FormatException)
                    {
                        result = 0.0;
                        MessageBox.Show("Синтаксична помилка!");
                        str_error = "synrax error";
                    }
                    GetToken();
                    return;
                default:
                    result = 0.0;
                    break;
            }
        }

        void GetToken()
        {
            tokType = Types.NONE;
            token = "";
            if (expIdx.Equals(exp.Length)) // пропускаємо пробіл (кінець виразу)
            {
                return;
            }

            while (expIdx < exp.Length && Char.IsWhiteSpace(exp[expIdx]))
                ++expIdx;
            // Хвостовий пробіл
            if (expIdx.Equals(exp.Length))
            {
                return;
            }

            if (IsDelim(exp[expIdx]))
            {
                token += exp[expIdx];
                expIdx++;
                tokType = Types.DELIMITER;
            }
            else if (Char.IsDigit(exp[expIdx]))
            {
                while (!IsDelim(exp[expIdx]))
                {
                    token += exp[expIdx];
                    expIdx++;
                    if (expIdx >= exp.Length)
                    {
                        tokType = Types.NUMBER;
                        break;
                    }
                    else
                        tokType = Types.NUMBER;
                }
            }
        }

        // Метод повертає true, якщо с - роздільник
        bool IsDelim(char c)
        {
            if (!("+-/*%^|()<>".IndexOf(c).Equals)(-1))
            {
                return true;
            }
            else
                return false;
        }
    }
}
