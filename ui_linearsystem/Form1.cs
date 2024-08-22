using System;
using System.Data.Common;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Windows.Forms;

//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
//                             PROTOTYPES::
/*SubmitButton_Click::بكل بساطه نا بكول الفانكشن اللي هتكريت التكست بوكسز كئني بعرف تو دايمنشن اراي بالظبط
 * InitializeMatrixUI()::هي اللي بتكريت التكست بوكس وبتعمل البروبيرتي بتاعته وبتظهر هي وبتون الحل كمان
 * TextBox_KeyDown::دي عشان لما اضفط انتر الكرسر يروح علي التكست بوكس اللي بعد كدا عشان ياخد القيمه الجديدة
 *  solve_Click::هنا انا هاخد الماتركس اللي فالتكست بوكس اخذنها ف اراي وبعدين اباصيها للدسبلاي 
 *  MatrixToString::
 *  DisplaySolutionSteps::
 *xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
 *                   SOLVING STEPS
 * first_step()::
 * step_two_1()::دي اصلا بترجعلي اول صف اصلا اول عنصر فيه لا يساوي الصفر
 *  StepTwo_2()::   عندي 3 احتمالات لو اول رقم ف الرو بتاعي صفر وتحتيه واحد هبدله او صفر ومش تحته واحد مدا هبدله باول رقم لا يساوي الصفر او لو عندي الرقم عادي برقم وتحته واحد فبرضو هبدله بس كدا
 *  HasNextElementOne():: بتساعني عشان اعرف الارقام اللي بعد الصف اللي انا فيه في تحته رقم يساوي الةاحد اصلا ولا لا
 *  third_step()::بنضرب في المعكوس بس
 *  fourth_step()::هنا بقي بخلي العناصر اللي فوقي واللي تحتي بالصفار
 * fifth_step()::بلوب
 *  infinity_check()::
 *  NoSolution_check()::
 
 */

namespace ui_linearsystem
{
    public partial class Form1 : Form
    {
        private static int rows;
        private static  int columns;
        public static  double[,] matrix;
        private TextBox[,] matrixTextBoxes;
        static int targeted_col;
        static int first_non_zero;
        static int current_row = 0;
        static bool inf_no = false;
        static int tar_row;
        static public StringBuilder solutionSteps = new StringBuilder();

        public Form1()
        {
            InitializeComponent();
  
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {


            if (int.TryParse(textBox1.Text, out rows) && int.TryParse(textBox2.Text, out columns))
            {
                InitializeMatrixUI();
                matrix = new double[rows, columns];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (double.TryParse(matrixTextBoxes[i, j].Text, out double value))
                        {
                            matrix[i, j] = value;
                        }
                    }
                }
            }
        }

  
        
        private void InitializeMatrixUI()
        {
            matrixTextBoxes = new TextBox[rows, columns];
            const int textBoxWidth = 40;
            const int textBoxHeight = 20;
            const int spacing = 5;
            int centerX = (Width - columns * (textBoxWidth + spacing)) / 2;
            int centerY = (Height - rows * (textBoxHeight + spacing)) / 2;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Width = textBoxWidth;
                    textBox.Height = textBoxHeight;
                    textBox.KeyDown += TextBox_KeyDown; // Handle keypress
                    textBox.Location = new System.Drawing.Point(centerX + j * (textBoxWidth + spacing), centerY + i * (textBoxHeight + spacing));

                    matrixTextBoxes[i, j] = textBox;
                    Controls.Add(textBox);



                }
                Button solve = new Button();
                solve.Text = "Submit Matrix";
                solve.Location = new System.Drawing.Point(centerX, centerY + rows * (textBoxHeight + spacing) + 10);
                solve.Click += solve_Click;
                Controls.Add(solve);



            }

        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox currentTextBox = (TextBox)sender;

                // Move to the next TextBox in the same row
                int currentRow = Array.IndexOf(matrixTextBoxes.Cast<TextBox>().ToArray(), currentTextBox) / columns;
                int nextIndexInRow = (Array.IndexOf(matrixTextBoxes.Cast<TextBox>().ToArray(), currentTextBox) + 1) % columns;
                TextBox nextTextBoxInRow = matrixTextBoxes[currentRow, nextIndexInRow];

                // If it's the last TextBox in the row, move to the first TextBox in the next row
                if (nextIndexInRow == 0)
                {
                    int nextRow = (currentRow + 1) % rows;
                    nextTextBoxInRow = matrixTextBoxes[nextRow, 0];
                }

                // Set focus to the next TextBox
                nextTextBoxInRow.Focus();
            }
        }

        private void solve_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = double.Parse(matrixTextBoxes[i, j].Text);
               
                }
               
            }
            DisplaySolutionSteps(matrix);
            SolutionForm solutionForm = new SolutionForm(solutionSteps);
            solutionForm.ShowDialog();
            // Call the DisplaySolutionSteps method with the entered matrix

        }

        private void DisplaySolutionSteps(double[,] matrix2)
        {
              matrix2 = matrix;
            fifth_step();
        }



        //انا محتاجه اطبع الماتركس بعد اي تعديل عشان كدا هحولها لاسترنج تاني
        static string MatrixToString(double[,] matrix2)
        {
            matrix2 = matrix;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    sb.Append(matrix[i, j] + "\t");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxMATRIX_SOLUTIONxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        // تعين اول عمود غير صفري علي يسار المصفوفه
        static void first_step()
        {

            for (int i = 0; i < columns; i++)
            {
                for (int j = current_row; j < rows; j++)
                {
                    if (matrix[j, i] != 0)
                    {
                        targeted_col = i;
                        return; // Break out of the outer loop once targeted_col is found
                    }
                }
            }
        }
        // جعل اول عنصر في العمود قيمته لا تساوي الصفر
        // two methods in the first step

        // get targeted row 
        //دا الصف اللي اول قيمه فيه لا تساوي الصفر او لو اصلا القيمه بتاعه الرو بتاعي مش بصفر من اساسه
        static void step_two_1()
        {
            for (int j = current_row; j < rows;)
            {
                if (matrix[j, targeted_col] != 0)
                {
                    first_non_zero = j;
                    break;
                }
                else
                {
                    j++;
                }
            }

        }


        // تبديل الصف مكان الصف
        public static void StepTwo_2()
        {
            double[] temp = new double[columns];
            double[] temp2 = new double[columns];
            if (matrix[current_row, targeted_col] != 1)
            {
                if (matrix[current_row, targeted_col] == 0 && HasNextElementOne())
                {
                    for (int i1 = 0; i1 < columns; i1++)
                    {
                        temp[i1] = matrix[tar_row, i1]; // Store the values of the first_non_zero row
                        temp2[i1] = matrix[current_row, i1];
                    }

                    for (int i = 0; i < columns; i++)
                    {
                        matrix[current_row, i] = temp[i]; // Copy values of the current_row to the first_non_zero row
                        matrix[tar_row, i] = temp2[i]; // Copy values of the first_non_zero row to the current_row
                    }
                    Console.WriteLine("\nR{0} --> R{1}\t\tR{1} --> R{0}", current_row, tar_row);
                }
                else if (matrix[current_row, targeted_col] != 0 && HasNextElementOne())
                {
                    for (int i1 = 0; i1 < columns; i1++)
                    {
                        temp[i1] = matrix[tar_row, i1]; // Store the values of the first_non_zero row
                        temp2[i1] = matrix[current_row, i1];
                    }

                    for (int i = 0; i < columns; i++)
                    {
                        matrix[current_row, i] = temp[i]; // Copy values of the current_row to the first_non_zero row
                        matrix[tar_row, i] = temp2[i]; // Copy values of the first_non_zero row to the current_row
                    }
                    Console.WriteLine("\nR{0} --> R{1}\t\tR{1} --> R{0}", current_row, tar_row);

                }
                else if (matrix[current_row, targeted_col] == 0 && !HasNextElementOne())
                {
                    for (int i = current_row + 1; i < rows; i++)
                    {
                        if (matrix[i, targeted_col] != 0)
                        {
                            tar_row = i;
                            for (int i1 = 0; i1 < columns; i1++)
                            {
                                temp[i1] = matrix[tar_row, i1]; // Store the values of the first_non_zero row
                                temp2[i1] = matrix[current_row, i1];
                            }

                            for (int j = 0; j < columns; j++)
                            {
                                matrix[current_row, j] = temp[j]; // Copy values of the current_row to the first_non_zero row
                                matrix[tar_row, j] = temp2[j]; // Copy values of the first_non_zero row to the current_row
                            }
                            Console.WriteLine("\nR{0} --> R{1}\t\tR{1} --> R{0}", current_row, tar_row);
                            break;
                        }
                    }


                }
            }
        }



        private static bool HasNextElementOne()
        {
            for (int one_check = current_row + 1; one_check < rows; one_check++)
            {
                if (matrix[one_check, targeted_col] == 1)
                {
                    tar_row = one_check;
                    return true; // return true if any next element is 1

                }
            }
            return false; // return false if no next element is 1
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        static void third_step()
        {

            double inverse = 0;
            double num = matrix[current_row, targeted_col];
            if (matrix[current_row, targeted_col] != 1)
            {
                inverse = 1 / matrix[current_row, targeted_col];
                for (int i = 0; i < columns; i++)
                {

                    matrix[current_row, i] = matrix[current_row, i] * (inverse);

                }
               
                string stepMessage = $"((1/{num}) * R{current_row}) -->R{current_row}";
                Console.WriteLine(stepMessage);
                solutionSteps.AppendLine(stepMessage);

            }

        }

        static void fourth_step()
        {
            double[] oneRow = new double[columns];

            // Copy the row with the leading 1 to the 'oneRow' array
            for (int i = 0; i < columns; i++)
            {
                oneRow[i] = matrix[current_row, i];
            }

            for (int i = 0; i < rows; i++)
            {
                // Skip the current row and rows where the targeted column is already 0
                if (i == current_row || matrix[i, targeted_col] == 0)
                {
                    continue;
                }

                double factor = -matrix[i, targeted_col]; // Calculate the factor for row operation

                for (int j = 0; j < columns; j++)
                {
                    // Multiply the row with the factor and add to the current row
                    matrix[i, j] = factor * oneRow[j] + matrix[i, j];
                }

                string stepMessage = $"({factor}) * R{current_row} + R{i} -->R{i}";
                Console.WriteLine(stepMessage);
                solutionSteps.AppendLine(stepMessage);
            }
        }


         public void fifth_step()
        {

            solutionSteps.AppendLine("Initial Matrix:");
            solutionSteps.AppendLine(MatrixToString(matrix));

            while (current_row < rows)
            {
                first_step();
                step_two_1();
                StepTwo_2();
                solutionSteps.AppendLine($"(After step_two_2):");
                solutionSteps.AppendLine(MatrixToString(matrix));

                third_step();
                solutionSteps.AppendLine($"(After third_step):");
                solutionSteps.AppendLine(MatrixToString(matrix));

                fourth_step();
                solutionSteps.AppendLine($"(After fourth_step):");
                solutionSteps.AppendLine(MatrixToString(matrix));

                current_row++;

                infinity_check();
                NoSolution_check();


                if (inf_no == true)
                {
                    return;
                }
               
            }

          

                for (int i = 0; i < rows; i++)
                {

                    string stepMessage = $"(X({i + 1}) = {matrix[i, columns - 1]})";
                    Console.WriteLine(stepMessage);
                    solutionSteps.AppendLine(stepMessage);


                }
            

        }


        static void infinity_check()
        {
            bool zero_row = true;
            if (current_row == rows - 1)
            {
                for (int i = 0; i < columns; i++)
                {
                    if (matrix[current_row, i] != 0)
                    {
                        zero_row = false;
                        break;

                    }
                }
                if (zero_row == true)
                {
                    inf_no = true;
                    string stepMessage = $"the system has many solutions";
                    Console.WriteLine(stepMessage);
                    solutionSteps.AppendLine(stepMessage);
                }
            }
           
        }

        static void NoSolution_check()
        {
            bool zero_num = true;

            if (current_row == rows - 1)
            {
                for (int i = 0; i < columns - 1; i++) // Iterate through columns except the last one
                {
                    if (matrix[current_row, i] != 0)
                    {
                        zero_num = false;
                        break;
                    }
                }

                if (zero_num && matrix[current_row, columns - 1] != 0)
                {
                    inf_no = true;
                    string stepMessage = $"the system has no solutions";
                    Console.WriteLine(stepMessage);
                    solutionSteps.AppendLine(stepMessage);
                }
            }
        }


    }

}









