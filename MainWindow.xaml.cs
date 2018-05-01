using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator2._0
{
    public partial class MainWindow : Window
    {
        private string input;
        private char cur_op;
        private bool _have_dot, _have_0_first;
        public MainWindow()
        {
            _init();
            InitializeComponent();
        }
        private void _init()
        {
            input = "";
            _have_dot = _have_0_first = false;
        }

        private void push_number()
        {
            Cal.Num_list = Convert.ToDouble(input);
            input = "";
        }

        private void Add_num_input(int num)
        {
            if (_have_0_first == true) { return; }
            if (input == "")
            {
                if (cur_op != '\0')
                {
                    Cal.Op_list = cur_op;
                }
            }
            input += num.ToString();
            OutputScreen.Text += num.ToString();
        }

        private void Add_op_input(char op)
        {
            _have_0_first = false;
            cur_op = op;
            if (input == "")
            {
                OutputScreen.Text = OutputScreen.Text.Remove(OutputScreen.Text.Length - 3, 3);
                OutputScreen.Text += " " + op + " ";
                return;
            }
            push_number();
            input = "";
            OutputScreen.Text += " " + op + " ";
        }

        private void Num1_Click(object sender, RoutedEventArgs e)
        {
            Add_num_input(1);
        }

        private void Num2_Click(object sender, RoutedEventArgs e)
        {
            Add_num_input(2);
        }

        private void Num3_Click(object sender, RoutedEventArgs e)
        {
            Add_num_input(3);
        }

        private void Num4_Click(object sender, RoutedEventArgs e)
        {
            Add_num_input(4);
        }

        private void Num5_Click(object sender, RoutedEventArgs e)
        {
            Add_num_input(5);
        }

        private void Num6_Click(object sender, RoutedEventArgs e)
        {
            Add_num_input(6);
        }

        private void Num7_Click(object sender, RoutedEventArgs e)
        {
            Add_num_input(7);
        }

        private void Num8_Click(object sender, RoutedEventArgs e)
        {
            Add_num_input(8);
        }

        private void Num9_Click(object sender, RoutedEventArgs e)
        {
            Add_num_input(9);
        }

        private void Num0_Click(object sender, RoutedEventArgs e)
        {
            if (input.Length == 1 && input == "0") { return; }
            if (input.Length == 0)
            {
                Add_num_input(0);
                _have_0_first = true;
                return;
            }
            Add_num_input(0);
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Add_op_input('+');
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            Add_op_input('-');
        }

        private void Mul_Click(object sender, RoutedEventArgs e)
        {
            Add_op_input('x');
        }

        private void Div_Click(object sender, RoutedEventArgs e)
        {
            Add_op_input('/');
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            if (input != "") push_number();
            Cal.Calulate();
            ResultOutputScreen.Text = Cal.Result.ToString();
            OutputScreen.Text += " =";
        }

        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            if (_have_dot) { return; }
            if (_have_0_first)
            {
                _have_0_first = false;
            }
            input += ".";
            OutputScreen.Text += ".";
            _have_dot = true;
        }
    }

    public static class Cal
    {
        static Cal()
        {
            num_list = new List<double>();
            operator_list = new List<int>();
        }
        private static List<double> num_list;
        private static List<int> operator_list; // 1: +; 2: -; 3: *; 4: /;
        private static double cur_num;
        private static double result;
        public static double Num_list
        {
            set => num_list.Add(value);
        }
        public static char Op_list
        { 
            set
            {
                switch (value)
                {
                    case '+':
                        operator_list.Add(1);
                        break;
                    case '-':
                        operator_list.Add(2);
                        break;
                    case 'x':
                        operator_list.Add(3);
                        break;
                    case '/':
                        operator_list.Add(4);
                        break;
                    default: return;
                }
            }
        }
        public static void Remove_last_op()
        {
            operator_list.RemoveAt(operator_list.Count - 1);
        }
        public static double Result
        {
            get => result;
        }
        public static void Calulate()
        {
            if (operator_list.Count == 0)
            {
                if (num_list.Count == 0) { return; }
                result = num_list[0];
                return;
            }
            if (operator_list.Count == num_list.Count) { operator_list.RemoveAt(operator_list.Count - 1); }
            int index = 0;
            for (; index < operator_list.Count; ++index)
            {
                if ((operator_list[index] == 3) || (operator_list[index] == 4))
                {
                    if (operator_list[index] == 3)
                    {
                        num_list[index] *= num_list[index + 1];
                        num_list.RemoveAt(index + 1);
                        operator_list.RemoveAt(index--);
                    } else {
                        num_list[index] /= num_list[index + 1];
                        num_list.RemoveAt(index + 1);
                        operator_list.RemoveAt(index--);
                    }
                }
            }
            index = 0;
            while (operator_list.Count != 0)
            {
                if (operator_list[index] == 1)
                {
                    num_list[index] += num_list[index + 1];
                    num_list.RemoveAt(index + 1);
                    operator_list.RemoveAt(index);
                }
                else if (operator_list[index] == 2)
                {
                    num_list[index] -= num_list[index + 1];
                    num_list.RemoveAt(index + 1);
                    operator_list.RemoveAt(index);
                }
            }
            result = num_list[0];
        }
    }
}
