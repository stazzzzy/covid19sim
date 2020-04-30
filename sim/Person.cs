using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Person
    {
        private readonly int id;
        private Boolean infected, symptoms, recovered, dead, quaran;
        int days_since_infection = 0;
        int seed;   //Random seed to determine recovery/death
        int seed_2;
        int seed_3;
        int r_factor;


        public Person(int i, Boolean x, int s, int s2, int s3)
        {
            id = i;
            seed = s;
            infected = false;
            symptoms = false;
            recovered = false;
            dead = false;
            days_since_infection = 0;
            r_factor = 10;
            seed_3 = s3;

            if(s2 > 85)
            {
                quaran = false;
            }
            else
            {
                quaran = true;
            }

        }
        public Boolean Is_infected()
        {
            return infected;
        }
        public Boolean is_dead()
        {
            return dead;
        }
        public Boolean is_recovered()
        {
            return recovered;
        }
        public void infect()
        {
            if (!quaran || (quaran && seed < 9))
            {
                infected = true;
            }
        }
        public void true_infect()
        {
            infected = true;
        }
        public Tuple<int,int,int,int,int> Advance()     //Tuple returned is status of 1 person after 1 day advance
        {
            Random rnd = new Random();
            if (!dead && infected)
            {
                days_since_infection += 1;
                if (!symptoms && days_since_infection >= 5)
                {
                    symptoms = true;
                }
                else if (days_since_infection >= 15)        //Statements below determine if a person recovers or dies
                {
                    if (seed <= 9)
                    {
                        dead = true;
                        infected = false;
                        symptoms = false;
                    }
                    else
                    {
                        if (seed_3 < r_factor) //Ex. a seed value of 95 will take 8 extra days to recover
                        {
                            infected = false;
                            symptoms = false;
                            recovered = true;
                        }
                        else
                        {
                            r_factor += 10;
                        }
                    }
                }
            }
            if (infected && symptoms)
            {
                return Tuple.Create(1,1,0,-1,0);
            }
            else if (dead)
            {
                return Tuple.Create(0,0,0,-1,1);
            }
            else if (recovered)
            {
                return Tuple.Create(0,0,1,0,0);
            }
            else if (infected)
            {
                return Tuple.Create(1,0,0,-1,0);
            }
            else
            {
                return Tuple.Create(0,0,0,0,0);
            }
        }
    }
}
