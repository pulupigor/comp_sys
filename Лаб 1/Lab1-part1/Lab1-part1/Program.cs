using System;
using System.Text;
using System.IO;
using System.IO.Compression;


namespace Lab1_part1
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Default;
            string s="";
            bool isCorrect;
            int caseSwitch;
            Console.WriteLine("Enter wishful text:");
            do
            {
                caseSwitch = int.Parse(Console.ReadLine());
                switch (caseSwitch)
                {
                    case 1:
                        isCorrect = true;
                        s = File.ReadAllText(@"C:\Users\Ігор\Desktop\Наука\Комп'ютерні системи\Лаб 1\Камінний хрест(Encoded).txt", Encoding.Default);
                        break;
                    case 2:
                        isCorrect = true;
                        s = File.ReadAllText(@"C:\Users\Ігор\Desktop\Наука\Комп'ютерні системи\Лаб 1\Шевченко(Encoded).txt", Encoding.Default);
                        break;
                    case 3:
                        s = File.ReadAllText(@"C:\Users\Ігор\Desktop\Наука\Комп'ютерні системи\Лаб 1\Специфікація(Encoded).txt", Encoding.Default);
                        isCorrect = true; 
                        break;
                    default:
                        isCorrect = false;
                        break;
                }
            }
            while (!isCorrect);
                    
            int[] c = new int[char.MaxValue];
            foreach (char t in s)
            {
                c[t]++;//визначення к-сті кожного символу
            }

            double frequency,entropy=0,information;
            int charLengthCounter=0;
            for (int i = 0; i < char.MaxValue; i++)
            {
                if (c[i] > 0)
                {
                    charLengthCounter++;
                    frequency = (double)c[i] / s.Length;            
                    entropy += frequency * Math.Log(1/frequency,2);                 
                    Console.WriteLine("Символ: {0}  Ймовірність появи символу: {1}", (char)i, frequency);
                }
            }
                                 
            
            information = entropy * charLengthCounter/8.0;
            Console.WriteLine("Середня ентропія :{0} біт         Кількість інформації : {1} байт", entropy,information);
            switch (caseSwitch)
            {
                case 1:
                    FileInfo file = new FileInfo(@"C:\Users\Ігор\Desktop\Наука\Комп'ютерні системи\Лаб 1\Камінний хрест(Encoded).txt");
                    Console.WriteLine("Розмір файлу: {0} байт", file.Length);
                    break;
                case 2:
                    file = new FileInfo(@"C:\Users\Ігор\Desktop\Наука\Комп'ютерні системи\Лаб 1\Шевченко(Encoded).txt");
                    Console.WriteLine("Розмір файлу: {0} байт", file.Length);
                    break;
                case 3:
                    file = new FileInfo(@"C:\Users\Ігор\Desktop\Наука\Комп'ютерні системи\Лаб 1\Специфікація(Encoded).txt");
                    Console.WriteLine("Розмір файлу: {0} байт", file.Length);
                    break;
                default:
                    break;
            }

            Console.ReadKey();
        }
    }
}
 