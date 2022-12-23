using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _13ZadWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click_1(object sender, EventArgs e)
        {

            List<Persona> persons = new List<Persona>();
            StreamReader sr = new StreamReader("persons.txt", Encoding.UTF8);
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
                persona.write_inf(richTextBox1);
            }

            int n = int.Parse(numericUpDown1.Text);
            int m = int.Parse(numericUpDown2.Text);
            richTextBox1.Text += "Поиск персон страше " + n + " лет, но младше " + m + " лет";
            foreach (Persona p in persons)
            {
                if (p.Age >= n && p.Age <= m)
                    p.write_inf(richTextBox1);

                else
                {
                    richTextBox1.Text += "\nНет персон страше этого возраста\n";
                    break;
                }
            }
        }
    }
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

            public abstract void write_inf(RichTextBox richTextBox);//вывод инфы


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

            public override void write_inf(RichTextBox richTextBox)
            {
                richTextBox.Text += ($"\nИмя - {Name}, Возраст - {this.Age}, Факультет - {faculty}\n");
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
            public override void write_inf(RichTextBox richTextBox)
            {
                richTextBox.Text +=($"\nИмя - {Name}, Возраст - {this.Age}, Факультет - {faculty}, Курс - {kourse}\n");
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


                public override void write_inf(RichTextBox richTextBox)
                {
                richTextBox.Text +=($"\nИмя - {Name}, Возраст - {this.Age}, Факультет - {faculty}, Должность - {job}, ");
                    if (stage < 4 && stage > 1)
                    {
                    richTextBox.Text += ($"Стаж - {stage} года\n\n");
                    }
                    else if (stage == 1)
                    {
                    richTextBox.Text += ($"Стаж - {stage} год\n\n" );
                    }
                    else
                    {
                    richTextBox.Text += ($"Стаж - {stage} лет\n\n" );
                    }
                }
            }
        }

