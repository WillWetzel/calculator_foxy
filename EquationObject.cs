using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScientificCalculator_Josh_Fox
{
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
                case "%":
                    return this.num1 % this.num2;
                case "^":
                    //Math lib only takes doubles and only RETURNS DOUBLES. Got to convert it back to a decimal for our return type.
                    return Convert.ToDecimal(Math.Pow(Convert.ToDouble(this.num1), Convert.ToDouble(this.num2)));

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
                case "%":
                    return result % eo.num1;
                case "^":
                    //Math lib only takes doubles and only RETURNS DOUBLES. Got to convert it back to a decimal for our return type.
                    return Convert.ToDecimal(Math.Pow(Convert.ToDouble(result), Convert.ToDouble(num1)));
            }
            return 0;
        }
    }

}
