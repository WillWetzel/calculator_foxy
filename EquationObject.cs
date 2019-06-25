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
        string[] operators = new string[6] { "(", "^", "x", "/", "+", "-" };

        public EquationObject(double n1, string op, double n2)
        {
            this.num1 = n1;
            this.num2 = n2;
            this.operation = op;
        }

        /*
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
        */

        public double SolveBidmass(List<EquationObject> eo)
        {
            double result = 0;
            int operatorIndex = -1;
            List<string> NumOfOps = new List<string>();
            bool operatorCheck = false;

            //Scan through and get our operators.
            foreach (var sum in eo)
            {
                NumOfOps.Add(sum.operation);
            }

            //While thereare multiplications...
            //Find index and perform equation at index.
            //Then replace numbers in ajacent equations with answer.
            //Continue.
            //When done with mutliplications, go to division... etc
            do
            {
                int i = 0;

                operatorIndex = NumOfOps.IndexOf(operators[i]);     //IndexOf returns -1 if element is not found
                result = eo.ElementAt(operatorIndex).performOperation();

                eo = refreshEquations(eo, operatorIndex, result);

                i++;


                //What about an array of strings with each operator?
                //Then we can have a if not end of array = true, if end of array = false?


            } while (operatorIndex != -1);


            //Solve for that operator with 
            return 0;

        }

        private List<EquationObject> refreshEquations(List<EquationObject> eo, int operatorIndex, double result)
        {
            if (operatorIndex > 0)
            {
                eo.ElementAt(operatorIndex - 1).num2 = result;
            }
            if (operatorIndex < eo.Count)
            {
                eo.ElementAt(operatorIndex + 1).num1 = result;
            }

            return eo;
        }


        /*
                if (sum.operation == "(")
                {
                    //Add to new list.
                }
                else if (sum.operation == "^")
                {

                }
                else if (sum.operation == "/")
                {

                }
                else if (sum.operation == "x")
                {

                }
                else if (sum.operation == "+")
                {

                }
                else if (sum.operation == "-")
                {

                }
                */



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
