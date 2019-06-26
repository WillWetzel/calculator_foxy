using System;
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
        double num, num2, result, MemoryStore;

        private bool radiansChecked; //Checks if radians is checked. True if yes. 

        List<String> equation = new List<String>();
        string operation;
        string[] operators = new string[5] { "^", "*", "/", "+", "-" };

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Scans button text (sender) for input value and adds to string list and to screen.
        /// Unless its a power button. This is hard coded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void input_Click(object sender, EventArgs e)
        {

            Button ButtonThatWasPushed = (Button)sender;
            string ButtonText = ButtonThatWasPushed.Text;

            switch (ButtonThatWasPushed.Name)
            {
                case "btnPowerTwo":
                    equation.Add("^");
                    equation.Add("2");

                    if (textBox1.Text == "0")
                        textBox1.Text = "^2";
                    else
                        textBox1.Text += "^2";

                    break;
                case "btnPowerThree":
                    equation.Add("^");
                    equation.Add("3");

                    if (textBox1.Text == "0")
                        textBox1.Text = "^3";
                    else
                        textBox1.Text += "^3";
                    break;
                case "btnPowerY":
                    equation.Add("^");

                    if (textBox1.Text == "0")
                        textBox1.Text = "^";
                    else
                        textBox1.Text += "^";
                    break;
                default:
                    equation.Add(ButtonText);

                    if (textBox1.Text == "0")
                        textBox1.Text = ButtonText;
                    else
                        textBox1.Text += ButtonText;
                    break;
            }

        }

        /// <summary>
        /// For Pi button. Input of 3.14159
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPie_Click(object sender, EventArgs e)
        {
            double pi = 3.14159;

            if (textBox1.Text == "0")
                textBox1.Text = pi.ToString();
            else
                textBox1.Text += pi.ToString();

            equation.Add(pi.ToString());
        }


        /// <summary>
        /// Sorts string list into whole numbers and pass onto Bidmass before showing result to screen and clearing equation stored.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_equals_Click(object sender, EventArgs e)
        {
            double num1, num2;
            int i = 0;

            //As we are recording a string list, each digit will have its own element within our data structure.
            //So 66 + 2 will be "6", "6", "+", "2"
            //We need to comb through this. If there are two double parse-able strings, we need to combine into one element and remove the excess element while not changing the order.
            //The reason this isn't with out for loop below is because of the run times of having a while loop within a for loop within a for loop (Big O notation. x^3 run time speeds)            
            //Check if this element and next element are both doubles.
            do
            {
                while (Double.TryParse(equation[i], out num1) && Double.TryParse(equation[i + 1], out num2))
                {
                    equation[i] = equation[i] + equation[i + 1];
                    equation.RemoveAt(i + 1);
                    if ((i + 1) == equation.Count)
                    {
                        break;
                    }
                }

                i++;
                if ((i + 1) == equation.Count)
                {
                    break;
                }

            } while (i != equation.Count);


            //First sort them into a list of equations.
            //Then apply bidmass to the sub equations.
            //Then solve in the new order.
            result = SolveBidmas(equation);
            textBox1.Text = result.ToString();
            num = 0;
            num2 = 0;
            operation = "";
            result = 0;
            equation.Clear();
        }

        /// <summary>
        /// Solve equation following BIDMAS.
        /// B = Brackets (not added)
        /// I = Indices (Powers)
        /// D = Division
        /// M = Multiplication
        /// A = Addition
        /// S = Subtraction
        /// </summary>
        /// <param name="eo">string List of eqation elements</param>
        /// <returns></returns>
        public double SolveBidmas(List<string> eo)
        {
            double result = 0;
            int i = 0;
            List<string> NumOfOps = new List<string>();
            bool operatorCheck = false;

            //While thereare multiplications...
            //Find index and perform equation at index.
            //Then replace numbers in ajacent equations with answer.
            //Continue.
            //When done with mutliplications, go to division... etc
            do
            {
                operatorCheck = false;

                int index = eo.IndexOf(operators[i]);

                if (index == -1)
                {
                    i++;
                    continue;
                }

                num = Double.Parse(eo.ElementAt(index - 1));
                operation = eo.ElementAt(index);
                num2 = Double.Parse(eo.ElementAt(index + 1));

                result = performOperation();

                eo.RemoveRange(index - 1, 3);
                eo.Insert(index - 1, result.ToString());

                operatorCheck = eo.Contains(operators[i]);

                if (operatorCheck == false)
                {
                    i++;
                }


            } while (i < operators.Length);

            //Return result as final cell in equation string list
            return Double.Parse(eo[0]);
        }

        /// <summary>
        /// Performs operation of sum
        /// </summary>
        /// <returns></returns>
        protected double performOperation()
        {
            switch (operation)
            {
                case "+":
                    return this.num + this.num2;
                case "-":
                    return this.num - this.num2;
                case "*":
                    return this.num * this.num2;
                case "/":
                    return this.num / this.num2;
                case "%":
                    return this.num % this.num2;
                case "^":
                    //Math lib only takes doubles and only RETURNS DOUBLES. Got to convert it back to a decimal for our return type.
                    return Convert.ToDouble(Math.Pow(Convert.ToDouble(this.num), Convert.ToDouble(this.num2)));

            }
            return 0;
        }

        /// <summary>
        /// Clears screen only.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, EventArgs e)
        {
            num = 0;
            num2 = 0;
            operation = "";
            result = 0;
            equation.Clear();

            textBox1.Text = "0";
        }

        /// <summary>
        /// Clears all values in calculator, including memory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            num = 0;
            num2 = 0;
            operation = "";
            result = 0;
            equation.Clear();
            MemoryStore = 0;

            textBox1.Text = "0";
        }

        /// <summary>
        /// Checks for radians or degrees check boxes and performs sin() from Math library.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_sin_Click(object sender, EventArgs e)
        {
            //The Math library works this out with Radians and only takes doubles.
            num = double.Parse(textBox1.Text);             

            if (radioButtonDegrees.Checked)
            {
                //If degees checked...
                //Perform sum and convert the radian to degrees and show answer.
                result = Math.Sin((Math.PI / 180) * Convert.ToDouble(num));
                textBox1.Text = result.ToString();
            }
            else if (radioButtonRadians.Checked)
            {
                //Already in radians...
                result = Math.Sin(Convert.ToDouble(num));
                textBox1.Text = result.ToString();
            }

        }

        /// <summary>
        /// Checks for radians or degrees check boxes and performs cos() from Math library. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cos_Click(object sender, EventArgs e)
        {
            //The Math library works this out with Radians and only takes doubles.
            num = double.Parse(textBox1.Text);

            if (radioButtonDegrees.Checked)
            {
                //If degees checked...
                //Perform sum and convert the radian to degrees and show answer.
                result = Math.Cos((Math.PI / 180) * Convert.ToDouble(num));
                textBox1.Text = result.ToString();
            }
            else if (radioButtonRadians.Checked)
            {
                //Already in radians...
                result = Math.Cos(Convert.ToDouble(num));
                textBox1.Text = result.ToString();
            }
        }

        /// <summary>
        /// Checks for radians or degrees check boxes and performs tan() from Math library.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_tan_Click(object sender, EventArgs e)
        {
            //The Math library works this out with Radians and only takes doubles.
            num = double.Parse(textBox1.Text);

            if (radioButtonDegrees.Checked)
            {
                //If degees checked...
                //Perform sum and convert the radian to degrees and show answer.
                result = Math.Tan((Math.PI / 180) * Convert.ToDouble(num));
                textBox1.Text = result.ToString();
            }
            else if (radioButtonRadians.Checked)
            {
                //Already in radians...
                result = Math.Tan(Convert.ToDouble(num));
                textBox1.Text = result.ToString();
            }
        }

        /// <summary>
        /// Sum for radians to degrees
        /// </summary>
        /// <param name="radian"></param>
        /// <returns></returns>
        private double radians_to_degrees(double radian)
        {
            return radian * (180 / Math.PI);
        }

        private void radioButtonDegrees_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRadians.Checked)
            {
                radiansChecked = true;
            }
            else if (radioButtonDegrees.Checked)
            {
                radiansChecked = false;
            }
        }

        private void btnFactorial_Click(object sender, EventArgs e)
        {
            num = double.Parse(textBox1.Text);
            result = 1;

            //Factorial: 3! = 3 x 2 x 1 = 6 Or 5! = 5 x 4 x 3 x 2 x 1 = 120
            //While loop. Minus one off our number each repition.

            //Problem with decimals... Can't factorial 4 digit numbers.
            //TODO: Add error handling here.
            try
            {
                while (num != 1)
                {
                    result = result * num;
                    num = num - 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                textBox1.Text = "Result too large";
            }

            textBox1.Text = result.ToString();
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            num = double.Parse(textBox1.Text);
            double result = Math.Sqrt((double)num);

            textBox1.Text = result.ToString();
        }

        private void btnCubeRoot_Click(object sender, EventArgs e)
        {
            double num1;

            num1 = double.Parse(textBox1.Text);
            result = Math.Ceiling(Math.Pow(num1, (double)1 / 3));

            textBox1.Text = result.ToString();
        }

        private void btnPowerTwo_Click(object sender, EventArgs e)
        {
            Button ButtonThatWasPushed = (Button)sender;
            string ButtonText = ButtonThatWasPushed.Text;

            ButtonText = ButtonText.Replace('x', ' ');

            if (textBox1.Text == "0")
                textBox1.Text = ButtonText;
            else
                textBox1.Text += ButtonText;

            equation.Add("^");
            equation.Add("2");
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            num = double.Parse(textBox1.Text);
            result = Math.Log10((double)num);

            textBox1.Text = result.ToString();
        }

        /// <summary>
        /// Method for handling memory functions. Scans input from button text and performs appriopriate action with MemoryStore string variable.
        /// MC = Memory Clear
        /// MR = Memory Recall
        /// MS = Adds number in display to memory
        /// M+ = Memory add
        /// M- = Memory subtract
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMemory_Click(object sender, EventArgs e)
        {
            Button ButtonThatWasPushed = (Button)sender;
            string ButtonText = ButtonThatWasPushed.Text;

            if (ButtonText == "MC")
            {
                //Memory Clear
                MemoryStore = 0;
                return;
            }

            if (ButtonText == "MR")
            {
                //Memory Recall
                textBox1.Text = MemoryStore.ToString();
                return;
            }

            if (ButtonText == "MS")
            {
                //Adds number in display to memory
                MemoryStore = double.Parse(textBox1.Text);
                return;
            }

            if (ButtonText == "M+")
            {
                //Memory add 
                MemoryStore += result;
                return;
            }

            if (ButtonText == "M-")
            {
                //Memory subtract
                MemoryStore -= result;
                textBox1.Text = MemoryStore.ToString();
                return;
            }
        }
    }

}
