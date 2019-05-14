﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScientificCalculator_Josh_Fox
{
    public partial class Form1 : Form
    {
        decimal num;
        string operation;
        List<String> equation = new List<String>();

        public Form1()
        {
            InitializeComponent();
        }

        private void input(string a)
        {
            if (textBox1.Text == "0")
                textBox1.Text = a;
            else
                textBox1.Text += a;
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            input("1");
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            input("2");
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            input("3");
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            input("4");
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            input("5");
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            input("6");
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            input("7");
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            input("8");
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            input("9");
        }

        private void btn_0_Click(object sender, EventArgs e)
        {
            input("0");
        }

        private void btn_dot_Click(object sender, EventArgs e)
        {
            textBox1.Text += ".";
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            num = decimal.Parse(textBox1.Text);
            operation = "+";
            textBox1.Text = "0";

            equation.Add(num.ToString());
            equation.Add(operation);
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            num = decimal.Parse(textBox1.Text);
            operation = "-";
            textBox1.Text = "0";

            equation.Add(num.ToString());
            equation.Add(operation);
        }

        private void btn_multiply_Click(object sender, EventArgs e)
        {
            num = decimal.Parse(textBox1.Text);
            operation = "*";
            textBox1.Text = "0";

            equation.Add(num.ToString());
            equation.Add(operation);
        }

        private void btn_div_Click(object sender, EventArgs e)
        {
            num = decimal.Parse(textBox1.Text);
            operation = "/";
            textBox1.Text = "0";

            equation.Add(num.ToString());
            equation.Add(operation);
        }

        private void btn_equals_Click(object sender, EventArgs e)
        {
            equation.Add(textBox1.Text);

            List<EquationObject> subEquations = new List<EquationObject>();
            decimal result;

            //First sort them into a list of equations.
            //Then apply bidmass to the sub equations.
            //Then solve in the new order.
            for (int i = 0; i < equation.Count; i++)
            {
                if (i == 0)
                {
                    EquationObject eo = new EquationObject(decimal.Parse(equation[i]), equation[i + 1], decimal.Parse(equation[i + 2]));

                    subEquations.Add(eo);
                    i = i + 2;
                }
                else
                {
                    EquationObject eo = new EquationObject(equation[i], decimal.Parse(equation[i + 1]));
                    subEquations.Add(eo);
                    i++;
                }

                //Add 1 to i again, so we add again in the loop and start at n2 of the previous equation.

            }

            //Needs to solve for each one.
            result = subEquations.FirstOrDefault().SolveLeftToRight(subEquations);
            textBox1.Text = result.ToString();

            equation.Clear();

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            num = 0;
            equation.Clear();

            textBox1.Text = "0";
        }
    }

    public class EquationObject
    {
        decimal num1;
        decimal num2;
        string operation;

        public EquationObject(decimal n1, string op, decimal n2)
        {
            this.num1 = n1;
            this.num2 = n2;
            this.operation = op;
        }

        public EquationObject(string op, decimal num)
        {
            this.num1 = num;
            this.operation = op;
        }

        public decimal SolveLeftToRight(List<EquationObject> eo)
        {
            decimal result = 0;

            for(int i = 0; i < eo.Count; i++)
            {
                if (i == 0)
                {
                    result = eo.FirstOrDefault().performOperation();
                }
                else
                {
                    result = performOperation(result, eo[i]);
                }
            }

            return result;

        }



        protected decimal performOperation()
        {
            switch (operation)
            {
                case "+":
                    return this.num1 + this.num2;
                case "-":
                    return this.num1 - this.num2;
                case "*":
                    return this.num1 * this.num2;
                case "/":
                    return this.num1 / this.num2;

            }
            return 0;
        }

        protected decimal performOperation(decimal result, EquationObject eo)
        {
            switch (eo.operation)
            {
                case "+":
                    return result + eo.num1;
                case "-":
                    return result - eo.num1;
                case "*":
                    return result * eo.num1;
                case "/":
                    return result / eo.num1;
            }
            return 0;
        }
    }

}