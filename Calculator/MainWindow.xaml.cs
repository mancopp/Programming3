﻿using System;
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

namespace Calculator
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

        public string? prevOperand;
        public string? currOperand;
        public string? operation;
        public bool secondOperandInput = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var keyword = (e.Source as Button).Content.ToString();

            string[] operations = {"+", "-", "/", "*", "%"};
            string[] digits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "," };

            if (digits.Contains(keyword))
            {
                if (secondOperandInput)
                {
                    TextDisplay.Content = "";
                    secondOperandInput = false;
                }
                else if (TextDisplay.Content != null)
                {
                    if (TextDisplay.Content.ToString().Contains(",") && keyword == ",") return;
                }

                TextDisplay.Content += keyword;
            }
            else if (operations.Contains(keyword))
            {
                //TODO: add handling operator to evaluate calculation
                prevOperand = TextDisplay.Content.ToString();
                operation = keyword;
                TextDisplay.Content = operation;
                secondOperandInput = true;
            }
            else if(keyword == "=")
            {
                // TODO: check for null TextDisplay.Content
                currOperand = TextDisplay.Content.ToString();
                if (prevOperand == null || currOperand == null || operation == null)
                {
                    MessageBox.Show(String.Format("Error: operands and operations can't be null: \n{0} \n{1} \n{2}", prevOperand, currOperand, operation));
                }
                else
                {
                    TextDisplay.Content = Evaluate(prevOperand, currOperand, operation);
                }
            }
            else if (keyword == "Clear")
            {
                TextDisplay.Content = "";
            }
        }

        private string Evaluate(string num1, string num2, string operation)
        { 
            float num1_s = float.Parse(num1);
            float num2_s = float.Parse(num2);
            float result_f = 0;

            switch (operation)
            {
                case "+":
                    result_f = num1_s + num2_s;
                    break;
                case "-":
                    result_f = num1_s - num2_s;
                    break;
                case "/":
                    result_f = num1_s / num2_s;
                    break;
                case "*":
                    result_f = num1_s * num2_s;
                    break;
                case "%":
                    result_f = num1_s % num2_s;
                    break;
            }

            return result_f.ToString();
        }
    }
}
