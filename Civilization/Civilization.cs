using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Civilization
{
    class Civilization
    {
        private int population; // население
        private double intelligent; // ученые
        private double fanatics; // фанатики
        private int war; // вероятность войны

        static int countWar; // счетчик войн
        static int countScience; // счетчик работы ученых
        static int countGuru; // счетчик гуру
        static int forVolcano; // переменная для вулкана
        static bool Volc; //  было извержение или нет

        public int Population
        {
            get { return population; }
            set { population = value; }
        }
        public double Intelligent
        {
            get { return intelligent; }
            set { intelligent = value; }
        }
        public double Fanatics
        {
            get { return fanatics; }
            set { fanatics = value; }
        }
        public Civilization()
        {
            population = 1000000;
            intelligent = 70;
            fanatics = 30;
            war = 0;
        }
        static Civilization()
        {
            countWar = 0;
            countScience = 0;
            countGuru = 0;
            forVolcano = 9;
            Volc = false;
        }
        private void WarPosibility() // вероятность войны
        {
            int counting = 0;
            if (fanatics>70)
            {
                war += 10;
            }
            for (int i = 0; i < intelligent; i+=10)
            {
                counting++;
            }
            war -= counting;
        }
        private void War() /// война
        {
            int a = 0;
            Random rand = new Random();
            if(countWar%4 != 0)
            {
                a += 1;
            }
            a += countWar / 4;
            for (int i = 0; i < a; i++)
            {
                population +=(population * 5) / 100;
                countWar--;
                if(rand.Next(0, 100)<=10)
                {
                    countGuru--;
                }
            }
        }
        public void ScientistWork() // работают ученые
        {
            int a = 0;
            if(countScience%3!=0)
            {
                a += 1;
            }
            a += countScience / 3;
            for (int i = 0; i < a; i++)
            {
                intelligent += (intelligent * 3) / 100;
                countScience--;
            }
        }
        public void GuruWork() // работает гуру
        {
            for (int i = 0; i < countGuru; i++)
            {
                fanatics -= (fanatics * 5) / 100;
            }
        }
        private void IntelligenceLevel() // уровень образованности (уйдет гуру или нет)
        {
            if(intelligent<35)
            {
                int a = (int)intelligent / 10;
                Random rand = new Random();
                if(rand.Next(0, 100) <= a)
                {
                    countGuru--;
                }
            }
        }
        private double Earthquake() // землетрясение
        {
            Random rand = new Random();
            double res = rand.NextDouble() * (9.9 - 0.1) + 0.1;
            if(res>=0.0 && res<=4.0)
            {
                return res;
            }
            if(res>4.0 && res<=7.0)
            {
                Random popul = new Random();
                int num = popul.Next(100, 500000); // на сколько сократится населениие
                if(num<1000)
                {
                    population -= 100;
                }
                if(num>1000 && num<10000)
                {
                    population -= 1000;
                }
                if(num>10000 && num<100000)
                {
                    population -= 10000;
                }
                if(num>100000)
                {
                    population -= 100000;
                }
                Random chance1 = new Random();
                int ch1 = chance1.Next(0, 100); // шанс смерти ученого, гуру или окончания одной войны
                if(ch1>0 && ch1<=10)
                {
                    if(countScience<=3)
                    {
                        countScience = 0;
                    }
                    else
                    {
                        countScience -= 3;
                    }
                }
                if(ch1>10 && ch1<=20)
                {
                    if(countGuru!=0)
                    {
                        countGuru--;
                    }
                }
                if(ch1>20 && ch1<=30)
                {
                    if(countWar<=4)
                    {
                        countWar = 0;
                    }
                    else
                    {
                        countWar -= 4;
                    }
                }
            }
            if(res>=8.0 && res<=9.9)
            {
                Random peop = new Random();
                int n = peop.Next(0, 4);
                if(n == 1)
                {
                    population -= (population * 1) / 100;
                }
                if (n == 2)
                {
                    population -= (population * 2) / 100;
                }
                if (n == 3)
                {
                    population -= (population * 3) / 100;
                }
                Random chance2 = new Random();
                int ch2 = chance2.Next(0, 100); // шанс смерти ученого или гуру увеличивается
                if (ch2 > 0 && ch2 <= 20)
                {
                    if (countScience <= 3)
                    {
                        countScience = 0;
                    }
                    else
                    {
                        countScience -= 3;
                    }
                }
                if (ch2 > 20 && ch2 <= 40)
                {
                    if (countGuru != 0)
                    {
                        countGuru--;
                    }
                }
            }
            return res;
        }
        private int Volcano() // вулкан
        {
            Random volc = new Random();
            int v = volc.Next(1, 10);
            if(v<forVolcano)
            {
                if(Volc == false)
                {
                    forVolcano--;
                }
                return v;
            }
            if(v>=forVolcano)
            {
                Volc = true;
                population -= (population * (int)fanatics) / 100;
            }
            return v;
        }
        public void Year() // проживание года
        {
            string str;
            int countVolcano = 0;
            bool checkforFanatics = false;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            while (population < 12000000 && population>0)
            {
                Console.Clear();
                if(intelligent < fanatics)
                {
                    checkforFanatics = true;
                    break;
                }
                if(countWar == 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                }
                if ((uint)population > 3000000000)
                {
                    Console.WriteLine("Earthquake. Power: {0:F1}", Earthquake());
                    Thread.Sleep(1000);
                }
                if(countVolcano == 20)
                {
                    countVolcano = 0;
                    Console.WriteLine("Volcano. Power: {0}", Volcano());
                    Thread.Sleep(1000);
                }
                if (countScience > 0)
                {
                    ScientistWork();
                }
                Console.WriteLine("Add scientist?\nyes/no");
                str = Console.ReadLine();
                if (str == "yes")
                {
                    intelligent += (intelligent * 3) / 100;
                    countScience += 2;
                }
                if (countGuru > 0)
                {
                    GuruWork();
                }
                Console.WriteLine("Add guru?\nyes/no");
                str = Console.ReadLine();
                if (str == "yes")
                {
                    countGuru += 1;
                }
                if (countWar > 0)
                {
                    Console.WriteLine("War");
                    Thread.Sleep(1000);
                    War();
                }
                Random rand = new Random();
                WarPosibility();
                if (rand.Next(0, 100) <= war) // если начало войны
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("New war started");
                    Thread.Sleep(1000);
                    countWar += 4;
                    population -= (population * 10) / 100;
                }
                war = 0;
                if (countGuru > 0)
                {
                    IntelligenceLevel();
                }
                population += (population * 5) / 100;
                if (intelligent <= 100)
                {
                    intelligent -= (intelligent * 1) / 100;
                }
                if(fanatics<=97)
                {
                    fanatics += (fanatics * 3) / 100;
                }
                Console.WriteLine("Population: {0}\n", population);
                Console.WriteLine("Intelligence: {0:F2}%\n", intelligent);
                Console.WriteLine("Fanatism: {0:F2}%\n", fanatics);
                Thread.Sleep(1000);
                countVolcano++;
            }
            if(checkforFanatics == true)
            {
                Console.WriteLine("Fanatics grabbed the nation. You lose");
            }
            else
            {
                if (population == 0)
                {
                    Console.WriteLine("No people in population. You lose");
                }
                else
                {
                    Console.WriteLine("Population reached prosperity. You win!");
                }
            }
        }
    }
}
