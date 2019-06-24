using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScientificCalculator_Josh_Fox
{
    public class EquationObject
    {
        double num1;
        double num2;
        string operation;

        public EquationObject(double n1, string op, double n2)
        {
            this.num1 = n1;
            this.num2 = n2;
            this.operation = op;
        }

        public EquationObject(string op, double num)
        {
            this.num1 = num;
            this.operation = op;
        }

        public double SolveLeftToRight(List<EquationObject> eo)
        {
            double result = 0;

            for (int i = 0; i < eo.Count; i++)
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

        protected double performOperation()
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
                case "%":
                    return this.num1 % this.num2;
                case "^":
                    //Math lib only takes doubles and only RETURNS DOUBLES. Got to convert it back to a decimal for our return type.
                    return Convert.ToDouble(Math.Pow(Convert.ToDouble(this.num1), Convert.ToDouble(this.num2)));

            }
            return 0;
        }

        protected double performOperation(double result, EquationObject eo)
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
                case "%":
                    return result % eo.num1;
                case "^":
                    //Math lib only takes doubles and only RETURNS DOUBLES. Got to convert it back to a decimal for our return type.
                    return Math.Pow(Convert.ToDouble(result), Convert.ToDouble(num1));
            }
            return 0;
        }
    }

}
