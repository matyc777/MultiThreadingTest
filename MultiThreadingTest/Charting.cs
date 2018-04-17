using System;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace MultiThreadingTest
{
    public partial class Charting : Form
    {
        int ConstComplexity;
        int ConstThreads;
        int Size;
        MultiThreadingTest test;

        public Charting()
        {
            InitializeComponent();
        }

        private void Charting_Load(object sender, EventArgs e)
        {
            textBox3.Text = "0";
            textBox2.Enabled = true;
            textBox3.Enabled = false;
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            chart1.Series[0].BorderWidth = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            Size = int.Parse(textBox1.Text);
            try
            {
                ConstComplexity = int.Parse(textBox2.Text);
                ConstThreads = int.Parse(textBox3.Text);
                if (ConstThreads > Size) throw new Exception();
                test = new MultiThreadingTest(Size, ConstComplexity, ConstThreads);
                if (radioButton1.Checked)
                {
                    chart1.Series[0].Points.AddXY(1, test.MediumTime(1));
                    chart1.Series[0].Points.AddXY(2, test.MediumTime(2));
                    chart1.Series[0].Points.AddXY(10, test.MediumTime(10));
                    chart1.Series[0].Points.AddXY(20, test.MediumTime(20));
                    chart1.Series[0].Points.AddXY(Size / 2, test.MediumTime(Size / 2));
                    chart1.Series[0].Points.AddXY(Size, test.MediumTime(Size));
                }
                else
                {
                    chart1.Series[0].Points.AddXY(1000, test.MediumTimeConstThreads(1000));
                    chart1.Series[0].Points.AddXY(2000, test.MediumTimeConstThreads(2000));
                    chart1.Series[0].Points.AddXY(3000, test.MediumTimeConstThreads(3000));
                    chart1.Series[0].Points.AddXY(4000, test.MediumTimeConstThreads(4000));
                    chart1.Series[0].Points.AddXY(5000, test.MediumTimeConstThreads(5000));
                }
            }
            catch
            {
                MessageBox.Show("Wrong input");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label4.Text = "x - Threads, y - Time";
                textBox3.Text = "0";
                textBox2.Text = "";
                textBox2.Enabled = true;
                textBox3.Enabled = false;
            }
            else
            {
                label4.Text = "x - Complexity, y - Time";
                textBox3.Text = "";
                textBox2.Text = "0";
                textBox2.Enabled = false;
                textBox3.Enabled = true;
            }
        }
    }
}
