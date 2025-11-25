using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
        private string lastResult = "";
        private bool errorState = false;

        public Form1()
        {
            InitializeComponent();
            txtDisplay.Text = "";
        }

        private void Btn_Click(object sender, EventArgs e) 
        {
           
            Button btn = sender as Button;
            if (btn == null) return;

            if (errorState) { txtDisplay.Text = ""; errorState = false; }

            
            if (txtDisplay.Text == "" && "+−×÷%^".Contains(btn.Text))
                return;

            string t = btn.Text;

            try
            {
                switch (t) 
                {
                    case "C":
                        txtDisplay.Text = "";
                        lastResult = "";
                        break;

                    case "CE":
                        Backspace();
                        break;

                    case "=":
                        EvaluateExpression();
                        break;

                    case "π":
                        InsertConstant(Math.PI);
                        break;

                    case "e":
                        InsertConstant(Math.E);
                        break;

                    case "^":
                        AppendOperator("^");
                        break;

                    case "÷": AppendOperator("/"); break;
                    case "×": AppendOperator("*"); break;
                    case "−": AppendOperator("-"); break;
                    case "+": AppendOperator("+"); break;

                    case "%":
                        ApplyPercentage();
                        break;

                    case "(":
                        txtDisplay.Text += "(";
                        break;

                    case ")":
                        txtDisplay.Text += ")";
                        break;

                    case ".":
                        AppendDecimal();
                        break;

                    case "n!":
                        ApplyFactorial();
                        break;

                    case "√":
                        ApplyFunction("sqrt");
                        break;

                    case "sin":
                    case "cos":
                    case "tan":
                    case "ln":
                    case "log":
                        ApplyFunction(t);
                        break;

                    default:
                        if (char.IsDigit(t[0]))
                            InsertNumber(t);
                        break;
                }
            }
            catch (Exception ex) 
            {
                ShowError("Input Error!");
               
            }
        }

        private void AppendOperator(string op)
        {
            if (IsResultShown())
                txtDisplay.Clear();

            
            if (Regex.IsMatch(txtDisplay.Text, @"[+\-*/^]$"))
            {
                txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1) + op;
            }
            else
            {
                txtDisplay.Text += op;
            }
        }

        private void AppendDecimal()
        {
            string text = txtDisplay.Text;

            if (text.EndsWith(".")) return;

            string num = GetCurrentNumber();

            
            if (num.Contains(".")) return;

            txtDisplay.Text += ".";
        }


        private void InsertNumber(string digit)
        {
            if (IsResultShown())
                txtDisplay.Text = digit;
            else
                txtDisplay.Text += digit;
        }


        private void InsertConstant(double value)
        {
            string str = value.ToString("G15", CultureInfo.InvariantCulture);
            if (IsResultShown() || txtDisplay.Text == "0")
                txtDisplay.Text = str;
            else
                txtDisplay.Text += str;
        }

        private void Backspace()
        {
            if (IsResultShown() || errorState)
            {
                txtDisplay.Text = "";
                return;
            }

            if (txtDisplay.Text.Length > 0) 
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1); 
            else
                txtDisplay.Text = "";
        }

        private void ApplyPercentage()
        {
            string num = GetCurrentNumber();

            if (double.TryParse(num, NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
            {
                
                Match opMatch = Regex.Match(txtDisplay.Text, @"[+\-*/](?!.*[+\-*/])");

                if (opMatch.Success)
                {
                    int index = opMatch.Index;
                    
                    double baseVal = double.Parse(txtDisplay.Text.Substring(0, index), CultureInfo.InvariantCulture);

                    double result = (baseVal * val) / 100.0;

                    ReplaceCurrentNumber(result.ToString("G15", CultureInfo.InvariantCulture));
                }
                else
                {
                    ReplaceCurrentNumber((val / 100.0).ToString("G15", CultureInfo.InvariantCulture));
                }
            }
        }

        private string ProcessPower(string expr)
        {
            
            var regex = new Regex(@"(\d*\.?\d+)\s*\^\s*(\d*\.?\d+)");

            while (regex.IsMatch(expr))
            {
                var m = regex.Match(expr);

                double a = double.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
                double b = double.Parse(m.Groups[2].Value, CultureInfo.InvariantCulture);

                double result = Math.Pow(a, b);

                expr = expr.Replace(m.Value, result.ToString("G15", CultureInfo.InvariantCulture));
            }

            return expr;
        }

        private void EvaluateExpression()
        {
            if (string.IsNullOrWhiteSpace(txtDisplay.Text) || txtDisplay.Text == "0") return;

            try
            {
                string expr = txtDisplay.Text
                    .Replace("−", "-")
                    .Replace("×", "*")
                    .Replace("÷", "/");

                expr = ProcessPower(expr);

                var result = new DataTable().Compute(expr, "");

                string output = FormatResult(Convert.ToDouble(result));

                txtDisplay.Text = output;
                lastResult = output;
            }
            catch
            {
                ShowError("Invalid Expression");
            }
        }

        private double DegToRad(double deg) => deg * Math.PI / 180.0;

        private void ApplyFunction(string func)
        {
            string num = GetCurrentNumber();
            if (!double.TryParse(num, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                return;

            double result;

            try
            {
               
                switch (func)
                {
                    case "sin":
                        result = Math.Sin(DegToRad(value));
                        break;
                    case "cos":
                        result = Math.Cos(DegToRad(value));
                        break;
                    case "tan":
                        
                        if (Math.Abs(Math.Cos(DegToRad(value))) < 0.00000001)
                            throw new Exception("Tangent undefined");
                        result = Math.Tan(DegToRad(value));
                        break;
                    case "ln":
                        if (value <= 0) throw new Exception("Log domain error");
                        result = Math.Log(value);
                        break;
                    case "log":
                        if (value <= 0) throw new Exception("Log domain error");
                        result = Math.Log10(value);
                        break;
                    case "sqrt":
                        if (value < 0) throw new Exception("Square root of negative");
                        result = Math.Sqrt(value);
                        break;
                    default:
                        result = value;
                        break;
                }

                
                if (Math.Abs(result) < 1e-15)
                    result = 0;
            }
            catch
            {
                ShowError("Math Error!"); 
                return;
            }

            ReplaceCurrentNumber(FormatResult(result));
        }

        private void ApplyFactorial()
        {
            string num = GetCurrentNumber();
            
            if (!int.TryParse(num, out int n) || n < 0 || n > 170)
            {
                ShowError("Invalid!");
                return;
            }

            double f = 1;
            for (int i = 2; i <= n; i++) f *= i;

            ReplaceCurrentNumber(f.ToString("G15", CultureInfo.InvariantCulture));
        }

        private string GetCurrentNumber()
        {
            
            var match = Regex.Match(txtDisplay.Text, @"[+-]?\d*\.?\d+[Ee]?[+-]?\d*$");
            return match.Success ? match.Value : "";
        }

        private void ReplaceCurrentNumber(string newValue)
        {
            string current = GetCurrentNumber();
            if (string.IsNullOrEmpty(current))
                txtDisplay.Text += newValue;
            else
                
                txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - current.Length) + newValue;
        }

        private bool IsResultShown()
        {
            return txtDisplay.Text == lastResult && !string.IsNullOrEmpty(lastResult);
        }

        private string FormatResult(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value)) return "Error";
            return value.ToString("G15", CultureInfo.InvariantCulture);
        }

        private void ShowError(string msg)
        {
            txtDisplay.Text = msg; 
            errorState = true;
        }
    }
}