using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace _13zad
{
    abstract class Persona//основной абстрактный класс
    //  class Persona
    {
        string name;
        DateTime birthDay;

        public DateTime BirthDay //свойство
        {
            get { return birthDay; }
            set { birthDay = value; }
        }

        public string Name//свойство
        {
            get { return name; }
            set { name = value; }
        }

        public Persona(string name, DateTime birthDay)//конструктор
        {
            this.name = name;
            this.birthDay = birthDay;
        }

        public abstract void write_inf();//вывод инфы


        public int Age { get { return DateTime.Now.Year - birthDay.Year; } }


    }

    class Entrant : Persona//производный класс "Абитуриент"
    {
        string faculty;

        public string Faculty
        {
            get { return faculty; }
            set { faculty = value; }
        }

        public Entrant(string name, DateTime birthDay, string faculty)
            : base(name, birthDay)
        {
            this.faculty = faculty;
        }

        public override void write_inf()
        {
            Console.WriteLine("Имя - {0}, Возраст - {1}, Факультет - {2}", Name, this.Age, faculty);
        }
    }

    class Student : Persona//производный класс "Студент"
    {
        string faculty;
        int kourse;

        public string Faculty
        {
            get { return faculty; }
            set { faculty = value; }
        }

        public int Kourse
        {
            get { return kourse; }
            set { if (kourse > 0 && kourse < 7) kourse = value; }
        }

        public Student(string name, DateTime birthDay, string faculty, int kourse)
            : base(name, birthDay)
        {
            this.faculty = faculty;
            this.kourse = kourse;
        }

        public override void write_inf()
        {
            Console.WriteLine("Имя - {0}, Возраст - {1}, Факультет - {2}, Курс - {3}", Name, this.Age, faculty, kourse);
        }
    }

    class Teacher : Persona//производный класс "Преподаватель"
    {
        string faculty;
        string job;
        int stage;
        public string Faculty
        {
            get { return faculty; }
            set { faculty = value; }
        }
        public string Job
        {
            get { return job; }
            set { job = value; }
        }
        public int Stage
        {
            get { return stage; }
            set { stage = value; }
        }
        public Teacher(string name, DateTime birthDay, string faculty, string job, int stage) : base(name, birthDay) 
        {
            this.faculty = faculty;
            this.job = job;
            this.stage = stage;
        }

        public override void write_inf()
        {
            Console.Write("Имя - {0}, Возраст - {1}, Факультет - {2}, Должность - {3}, ", Name, this.Age, faculty,
                job);
            if (stage < 4 && stage > 1)
            {
                Console.WriteLine("Стаж - {0} года", stage);
            }
            else if (stage == 1)
            {
                Console.WriteLine("Стаж - {0} год", stage);
            }
            else
            {
                Console.WriteLine("Стаж - {0} лет", stage);
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            int n;
            int m;
            List<Persona> persons = new List<Persona>();
            StreamReader sr = new StreamReader("persons.txt", Encoding.Default);
            while (!sr.EndOfStream)
            {
                string[] s = sr.ReadLine().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (s.Length == 3)
                {
                    persons.Add(new Entrant(s[0], Convert.ToDateTime(s[1]), s[2]));
                }
                if (s.Length == 4)
                {
                    persons.Add(new Student(s[0], Convert.ToDateTime(s[1]), s[2], Convert.ToInt32(s[3])));
                }
                if (s.Length == 5)
                {
                    persons.Add(new Teacher(s[0], Convert.ToDateTime(s[1]), s[2], s[3], Convert.ToInt32(s[4])));
                }
            }
            sr.Close();

            foreach (Persona persona in persons)
            {
                persona.write_inf();
                Console.WriteLine();
            }

            Console.WriteLine("поиск персон по возрасту ");
            Console.WriteLine("Введите Диапозон");
            while (!int.TryParse(Console.ReadLine(), out n)||n<=0)
                Console.WriteLine("Введите возраст еще раз");
            while (!int.TryParse(Console.ReadLine(), out m) || m <= 0)
                Console.WriteLine("Введите возраст еще раз");
            foreach (Persona p in persons)
            {
                if (p.Age >= n && p.Age <= m)
                    p.write_inf();
            }

            Console.ReadLine();

        }

    }
}
