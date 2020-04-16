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
        Boolean infected, symptoms, recovered, dead;
        int days_since_infection = 0;
        int seed;

        public Person(int i, Boolean x, int s)
        {
            id = i;
            seed = s; //op
            infected = false;
            symptoms = false;
            recovered = false;
            dead = false;
            days_since_infection = 0;
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
            infected = true;
        }
        public Tuple<int,int,int,int,int> Advance()
        {
            Random rnd = new Random();
            if (!dead && infected)
            {
                days_since_infection += 1;
                if (!symptoms && days_since_infection >= 5)
                {
                    symptoms = true;
                }
                else if (days_since_infection >= 15)
                {
                    if (seed <= 9)
                    {
                        dead = true;
                        infected = false;
                        symptoms = false;
                    }
                    else
                    {
                        infected = false;
                        symptoms = false;
                        recovered = true;
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
