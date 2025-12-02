//Name: Adil Azizov
// Date: 01.12.2025
// Link: https://github.com/AzAd705/Lab---5-Functionally-Random

using System;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /* Name: Adil Azizov
         * Date: November 2025
         * This program rolls one dice or calculates mark stats.
         * Link to your repo in GitHub: https://github.com/AzAd705/Lab---5-Functionally-Random
         * */

        //class-level random object
        Random rand = new Random();

        private void Form1_Load(object sender, EventArgs e)
        {
            //select one roll radiobutton
            // Look for the radio button - it might have a different name
            if (Controls.Find("radOneRoll", true).Length > 0)
            {
                ((RadioButton)Controls.Find("radOneRoll", true)[0]).Checked = true;
            }

            //add your name to end of form title
            this.Text += " - Adil Azizov";
        }

        // Make sure this event handler is connected to radOneRoll_CheckedChanged event
        private void radOneRoll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Based on the radiobutton selected, show or hide the appropriate groupbox 
                // and call the appropriate Clear method
                RadioButton rad = sender as RadioButton;
                if (rad != null && rad.Checked)
                {
                    // Show One Roll group, hide Mark Stats group
                    Control grpOneRoll = Controls.Find("grpOneRoll", true)[0];
                    Control grpMarkStats = Controls.Find("grpMarkStats", true)[0];

                    grpOneRoll.Visible = true;
                    grpMarkStats.Visible = false;
                    ClearStats(); // Clear the stats section when switching to One Roll
                }
                else if (rad != null && !rad.Checked)
                {
                    // Show Mark Stats group, hide One Roll group
                    Control grpOneRoll = Controls.Find("grpOneRoll", true)[0];
                    Control grpMarkStats = Controls.Find("grpMarkStats", true)[0];

                    grpOneRoll.Visible = false;
                    grpMarkStats.Visible = true;
                    ClearOneRoll(); // Clear the dice roll section when switching to Mark Stats
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in radio button change: {ex.Message}");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //call the function
            ClearOneRoll();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //call the function
            ClearStats();
        }

        private void btnRollDice_Click(object sender, EventArgs e)
        {
            try
            {
                int dice1, dice2;
                //call ftn RollDice, placing returned number into integers
                dice1 = RollDice();
                dice2 = RollDice();

                //place integers into labels
                Control lblDie1 = Controls.Find("lblDie1", true)[0];
                Control lblDie2 = Controls.Find("lblDie2", true)[0];
                lblDie1.Text = dice1.ToString();
                lblDie2.Text = dice2.ToString();

                // call ftn GetName sending total and returning name
                int total = dice1 + dice2;
                string rollName = GetName(total);

                //display name in label
                Control lblRollName = Controls.Find("lblRollName", true)[0];
                lblRollName.Text = rollName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error rolling dice: {ex.Message}");
            }
        }

        /* Name: ClearOneRoll
        *  Sent: nothing
        *  Return: nothing
        *  Clear the labels */
        private void ClearOneRoll()
        {
            try
            {
                Control lblDie1 = Controls.Find("lblDie1", true)[0];
                Control lblDie2 = Controls.Find("lblDie2", true)[0];
                Control lblRollName = Controls.Find("lblRollName", true)[0];

                lblDie1.Text = "";
                lblDie2.Text = "";
                lblRollName.Text = "";
            }
            catch { }
        }

        /* Name: ClearStats
        *  Sent: nothing
        *  Return: nothing
        *  Reset nud to minimum value, chkbox unselected, 
        *  clear labels and listbox */
        private void ClearStats()
        {
            try
            {
                // Reset numeric up/down
                Control nudMarks = Controls.Find("nudMarks", true)[0];
                ((NumericUpDown)nudMarks).Value = ((NumericUpDown)nudMarks).Minimum;

                // Uncheck seed checkbox
                Control chkSeed = Controls.Find("chkSeed", true)[0];
                ((CheckBox)chkSeed).Checked = false;

                // Clear labels
                Control lblAverage = Controls.Find("lblAverage", true)[0];
                Control lblPass = Controls.Find("lblPass", true)[0];
                Control lblFail = Controls.Find("lblFail", true)[0];
                lblAverage.Text = "";
                lblPass.Text = "";
                lblFail.Text = "";

                // Clear listbox
                Control lstMarks = Controls.Find("lstMarks", true)[0];
                ((ListBox)lstMarks).Items.Clear();
            }
            catch { }
        }

        /* Name: RollDice
        * Sent: nothing
        * Return: integer (1-6)
        * Simulates rolling one dice */
        private int RollDice()
        {
            return rand.Next(1, 7); // 1-6 inclusive
        }

        /* Name: GetName
        * Sent: 1 integer (total of dice1 and dice2) 
        * Return: string (name associated with total) 
        * Finds the name of dice roll based on total.
        * Use a switch statement with one return only
        * Names: 2 = Snake Eyes
        *        3 = Litle Joe
        *        5 = Fever
        *        7 = Most Common
        *        9 = Center Field
        *        11 = Yo-leven
        *        12 = Boxcars
        * Anything else = No special name*/
        private string GetName(int total)
        {
            switch (total)
            {
                case 2: return "Snake Eyes";
                case 3: return "Litle Joe";
                case 5: return "Fever";
                case 7: return "Most Common";
                case 9: return "Center Field";
                case 11: return "Yo-leven";
                case 12: return "Boxcars";
                default: return "No special name";
            }
        }

        private void btnSwapNumbers_Click(object sender, EventArgs e)
        {
            try
            {
                //call ftn DataPresent twice sending string returning boolean
                Control lblDie1 = Controls.Find("lblDie1", true)[0];
                Control lblDie2 = Controls.Find("lblDie2", true)[0];

                bool hasData1 = DataPresent(lblDie1.Text);
                bool hasData2 = DataPresent(lblDie2.Text);

                //if data present in both labels, call SwapData sending both strings
                if (hasData1 && hasData2)
                {
                    string data1 = lblDie1.Text;
                    string data2 = lblDie2.Text;

                    // Swap the data
                    SwapData(ref data1, ref data2);

                    //put data back into labels
                    lblDie1.Text = data1;
                    lblDie2.Text = data2;
                }
                else
                {
                    //if data not present in either label display error msg
                    MessageBox.Show("Both dice must have values to swap.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error swapping numbers: {ex.Message}");
            }
        }

        /* Name: DataPresent
        * Sent: string
        * Return: bool (true if data, false if not) 
        * See if string is empty or not*/
        private bool DataPresent(string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }

        /* Name: SwapData
        * Sent: 2 strings
        * Return: none 
        * Swaps the memory locations of two strings*/
        private void SwapData(ref string str1, ref string str2)
        {
            string temp = str1;
            str1 = str2;
            str2 = temp;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //declare variables and array
                Control nudMarks = Controls.Find("nudMarks", true)[0];
                int numMarks = (int)((NumericUpDown)nudMarks).Value;
                int[] marks = new int[numMarks];
                int pass = 0, fail = 0;

                //check if seed value
                Control chkSeed = Controls.Find("chkSeed", true)[0];
                if (((CheckBox)chkSeed).Checked)
                {
                    string seedInput = Microsoft.VisualBasic.Interaction.InputBox(
                        "Enter a seed value:", "Seed Value", "0");

                    if (int.TryParse(seedInput, out int seed))
                    {
                        rand = new Random(seed);
                    }
                    else
                    {
                        MessageBox.Show("Invalid seed value. Using default random.", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        rand = new Random();
                    }
                }

                //fill array using random number
                for (int i = 0; i < numMarks; i++)
                {
                    marks[i] = rand.Next(0, 101); // 0-100 inclusive
                }

                //call CalcStats sending and returning data
                double average = CalcStats(marks, ref pass, ref fail);

                //display data sent back in labels - average, pass and fail
                // Format average always showing 2 decimal places
                Control lblAverage = Controls.Find("lblAverage", true)[0];
                Control lblPass = Controls.Find("lblPass", true)[0];
                Control lblFail = Controls.Find("lblFail", true)[0];

                lblAverage.Text = average.ToString("F2");
                lblPass.Text = pass.ToString();
                lblFail.Text = fail.ToString();

                // Display marks in listbox
                Control lstMarks = Controls.Find("lstMarks", true)[0];
                ListBox listBox = (ListBox)lstMarks;
                listBox.Items.Clear();
                foreach (int mark in marks)
                {
                    listBox.Items.Add(mark.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating marks: {ex.Message}");
            }
        }

        private void grpMarkStats_Enter(object sender, EventArgs e)
        {
            // This event handler is not needed for functionality
        }

        private void chkSeed_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // If the checkbox is selected, ask the user if they want to use a seed value
                CheckBox chk = sender as CheckBox;
                if (chk != null && chk.Checked)
                {
                    DialogResult result = MessageBox.Show(
                        "Do you want to use a seed value?",
                        "Seed Value",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    // If they select No, unselect the checkbox
                    if (result == DialogResult.No)
                    {
                        chk.Checked = false;
                    }
                }
            }
            catch { }
        }

        /* Name: CalcStats
        * Sent: array and 2 integers
        * Return: average (double) 
        * Run a foreach loop through the array.
        * Passmark is 60%
        * Calculate average and count how many marks pass and fail
        * The pass and fail values must also get returned for display*/
        private double CalcStats(int[] marks, ref int pass, ref int fail)
        {
            int sum = 0;
            pass = 0;
            fail = 0;

            foreach (int mark in marks)
            {
                sum += mark;
                if (mark >= 60)
                    pass++;
                else
                    fail++;
            }

            return marks.Length > 0 ? (double)sum / marks.Length : 0;
        }

        private void chkSeed_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void radOneRoll_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}
