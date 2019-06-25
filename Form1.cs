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
        double num, result, MemoryStore;

        private bool radiansChecked; //Checks if radians is checked. True if yes. 

        List<String> equation = new List<String>();

        public Form1()
        {
            InitializeComponent();
        }

        private void input_Click(object sender, EventArgs e)
        {

            Button ButtonThatWasPushed = (Button)sender;
            string ButtonText = ButtonThatWasPushed.Text;

            if (ButtonText.Contains('x'))
            {
                ButtonText = ButtonText.Replace('x', ' ');
            }

            if (textBox1.Text == "0")
                textBox1.Text = ButtonText;
            else
                textBox1.Text += ButtonText;

            equation.Add(ButtonText);
        }

        private void btnPie_Click(object sender, EventArgs e)
        {
            double pi = 3.14159;

            if (textBox1.Text == "0")
                textBox1.Text = pi.ToString();
            else
                textBox1.Text += pi.ToString();

            equation.Add(pi.ToString());
        }


        private void btn_equals_Click(object sender, EventArgs e)
        {
            List<EquationObject> subEquations = new List<EquationObject>();
            double num1, num2;

            //As we are recording a string list, each digit will have its own element within our data structure.
            //So 66 + 2 will be "6", "6", "+", "2"
            //We need to comb through this. If there are two double parse-able strings, we need to combine into one element and remove the excess element while not changing the order.
            //The reason this isn't with out for loop below is because of the run times of having a while loop within a for loop within a for loop (Big O notation. x^3 run time speeds)            
            for (int i = 0; i < equation.Count; i++)
            {
                //Check if this element and next element are both doubles.
                while (Double.TryParse(equation[i], out num1) && Double.TryParse(equation[i + 1], out num2))
                {
                    equation[i] = equation[i] + equation[i + 1];
                    equation.RemoveAt(i + 1);
                }
            }

            //First sort them into a list of equations.
            //Then apply bidmass to the sub equations.
            //Then solve in the new order.
            for (int i = 0; i < equation.Count; i++)
            {
                if (equation.Count > 2)
                {
                    EquationObject eo = new EquationObject(double.Parse(equation[i]), equation[i + 1], double.Parse(equation[i + 2]));
                    subEquations.Add(eo);
                    i++;
                    //At the start of hte loop, the second number in our first equation object will be the first number in our second.
                }
                else
                {

                }

            }

            result = subEquations.FirstOrDefault().SolveBidmass(subEquations);

            textBox1.Text = result.ToString();

            equation.Clear();



            /*
                if (i == 0 && equation.Count > 2)
                {
                    EquationObject eo = new EquationObject(double.Parse(equation[i]), equation[i + 1], double.Parse(equation[i + 2]));

                    subEquations.Add(eo);
                    i = i + 2;
                }
                else
                {
                    //Checking the order. 
                    if (Double.TryParse(equation[i], out num))
                    {
                        EquationObject eo = new EquationObject(equation[i + 1], double.Parse(equation[i]));
                        subEquations.Add(eo);
                        i++;
                    }
                    else
                    {
                        EquationObject eo = new EquationObject(equation[i], double.Parse(equation[i + 1]));
                        subEquations.Add(eo);
                        i++;
                    }
                }
                
            //Add 1 to i again, so we add again in the loop and start at n2 of the previous equation.



            //Needs to solve for each one.
            //result = (double)subEquations.FirstOrDefault().SolveLeftToRight(subEquations);


            textBox1.Text = result.ToString();

            equation.Clear();
            */
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            num = 0;
            equation.Clear();

            textBox1.Text = "0";
        }

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
            //Todo: Add error handling here.
            while (num != 1)
            {
                result = result * num;
                num = num - 1;
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
