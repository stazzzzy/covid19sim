using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        Person[] people;
        int sample_size;
        int infected, symptoms, recovered, healthy, dead;
        int day = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //Button action
        {
            sample_size = Int32.Parse(textBox1.Text); //Get integer from text box
            simulate();
        }

        private void clicked_sample_size(object sender, EventArgs e) //Textbox enter action
        {
            if (textBox1.Text == "Sample Size")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Blue;
            }
        }

        private void exit_sample_size(object sender, EventArgs e) //Textbox leave action
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Sample Size";
                textBox1.ForeColor = Color.Gray;
            }
        }
        private void simulate()
        {
            people = new Person[sample_size];
            healthy = sample_size;
            for(int i=0;i<sample_size;i++)
            {
                people[i] = new Person(i,false,rnd.Next(1,100));
            }
            people[0].infect(); //Infect the first person
        }
        private void next_day()
        {
            infected = 0; symptoms = 0; recovered = 0; healthy = sample_size; dead = 0;
            for (int i=0;i<sample_size;i++)
            {
                var names = people[i].Advance(); //Take a total count of statuses in all people defined by sample size
                infected += names.Item1;
                symptoms += names.Item2;
                recovered += names.Item3;
                healthy += names.Item4;
                dead += names.Item5;
            }
            spread_infection();
            Console.WriteLine("Infected:" + infected + "\nDead:" + dead + "\nHealthy:" + healthy + "\n");
        }
        private void spread_infection()
        {
            for(int i = 0; i < infected * 2; i++) //method to determine infection rate (Every 1 infected can infect 2)
                                                  // [SUBJECT TO CHANGE!]
            {
                int index = rnd.Next(0, people.Length);
                if(!people[index].is_dead() && !people[index].is_recovered())
                {
                    people[index].infect();
                }
            }
        }

        private void next_day_clicked(object sender, EventArgs e) //Button action
        {
            next_day();
            chart.Series[0].Points.AddXY(day, healthy);
            chart.Series[1].Points.AddXY(day, infected);
            chart.Series[2].Points.AddXY(day, dead);
            day += 1;
        }
    }
}
