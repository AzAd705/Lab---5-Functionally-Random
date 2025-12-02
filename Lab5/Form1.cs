// Name: Adil Azizov
// Date: 01.12.2025
// Github: https://github.com/AzAd705/Lab---5-Functionally-Random/tree/main/Lab5

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

        Random rand = new Random();

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Controls.Find("radOneRoll", true).Length > 0)
            {
                ((RadioButton)Controls.Find("radOneRoll", true)[0]).Checked = true;
            }
            this.Text += " - Adil Azizov";
        }

        private void radOneRoll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton rad = sender as RadioButton;
                if (rad != null && rad.Checked)
                {
                    Control grpOneRoll = Controls.Find("grpOneRoll", true)[0];
                    Control grpMarkStats = Controls.Find("grpMarkStats", true)[0];
                    grpOneRoll.Visible = true;
                    grpMarkStats.Visible = false;
                    ClearStats();
                }
                else if (rad != null && !rad.Checked)
                {
                    Control grpOneRoll = Controls.Find("grpOneRoll", true)[0];
                    Control grpMarkStats = Controls.Find("grpMarkStats", true)[0];
                    grpOneRoll.Visible = false;
                    grpMarkStats.Visible = true;
                    ClearOneRoll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in radio button change: {ex.Message}");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearOneRoll();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearStats();
        }

        private void btnRollDice_Click(object sender, EventArgs e)
        {
            try
            {
                int dice1 = RollDice();
                int dice2 = RollDice();
                Control lblDie1 = Controls.Find("lblDie1", true)[0];
                Control lblDie2 = Controls.Find("lblDie2", true)[0];
                lblDie1.Text = dice1.ToString();
                lblDie2.Text = dice2.ToString();

                int total = dice1 + dice2;
                string rollName = GetName(total);
                Control lblRollName = Controls.Find("lblRollName", true)[0];
                lblRollName.Text = rollName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error rolling dice: {ex.Message}");
            }
        }

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

        private void ClearStats()
        {
            try
            {
                Control nudMarks = Controls.Find("nudMarks", true)[0];
                ((NumericUpDown)nudMarks).Value = ((NumericUpDown)nudMarks).Minimum;

                Control chkSeed = Controls.Find("chkSeed", true)[0];
                ((CheckBox)chkSeed).Checked = false;

                Control lblAverage = Controls.Find("lblAverage", true)[0];
                Control lblPass = Controls.Find("lblPass", true)[0];
                Control lblFail = Controls.Find("lblFail", true)[0];
                lblAverage.Text = "";
                lblPass.Text = "";
                lblFail.Text = "";

                Control lstMarks = Controls.Find("lstMarks", true)[0];
                ((ListBox)lstMarks).Items.Clear();
            }
            catch { }
        }

        private int RollDice()
        {
            return rand.Next(1, 7);
        }

        private string GetName(int total)
        {
            switch (total)
            {
                case 2: return "Snake Eyes";
                case 3: return "Little Joe";
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
                Control lblDie1 = Controls.Find("lblDie1", true)[0];
                Control lblDie2 = Controls.Find("lblDie2", true)[0];
                bool hasData1 = DataPresent(lblDie1.Text);
                bool hasData2 = DataPresent(lblDie2.Text);

                if (hasData1 && hasData2)
                {
                    string data1 = lblDie1.Text;
                    string data2 = lblDie2.Text;
                    SwapData(ref data1, ref data2);
                    lblDie1.Text = data1;
                    lblDie2.Text = data2;
                }
                else
                {
                    MessageBox.Show("Both dice must have values to swap.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error swapping numbers: {ex.Message}");
            }
        }

        private bool DataPresent(string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }

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
                Control nudMarks = Controls.Find("nudMarks", true)[0];
                int numMarks = (int)((NumericUpDown)nudMarks).Value;
                int[] marks = new int[numMarks];
                int pass = 0, fail = 0;

                Control chkSeed = Controls.Find("chkSeed", true)[0];
                if (((CheckBox)chkSeed).Checked)
                {
                    rand = new Random(1000);
                }

                Control lstMarks = Controls.Find("lstMarks", true)[0];
                ((ListBox)lstMarks).Items.Clear();

                int i = 0;
                while (i < numMarks)
                {
                    marks[i] = rand.Next(40, 101);
                    i++;
                }

                double average = CalcStats(marks, ref pass, ref fail);
                Control lblAverage = Controls.Find("lblAverage", true)[0];
                Control lblPass = Controls.Find("lblPass", true)[0];
                Control lblFail = Controls.Find("lblFail", true)[0];
                lblAverage.Text = average.ToString("F2");
                lblPass.Text = pass.ToString();
                lblFail.Text = fail.ToString();

                foreach (int mark in marks)
                {
                    ((ListBox)lstMarks).Items.Add(mark.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating marks: {ex.Message}");
            }
        }

        private void chkSeed_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;
                if (chk != null && chk.Checked)
                {
                    DialogResult result = MessageBox.Show(
                        "Do you want to use a seed value?",
                        "Seed Value",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        chk.Checked = false;
                    }
                }
            }
            catch { }
        }

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

        private void chkSeed_CheckedChanged_1(object sender, EventArgs e) { }
        private void radOneRoll_CheckedChanged_1(object sender, EventArgs e) { }
    }
}
