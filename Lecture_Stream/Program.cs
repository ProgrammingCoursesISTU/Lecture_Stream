using System;
using System.IO; //Для ввода/вывода
using System.Text; //Для кодировок

namespace Lecture_Stream
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine(Console.InputEncoding); //Стандартная кодировка - UTF8
            System.Console.WriteLine(Console.OutputEncoding);

            Console.InputEncoding = Encoding.UTF8; //Кодировка ввода в консоль
            Console.OutputEncoding = Encoding.UTF8; //Кодировка вывода в консоль

            string path = "SomeLogs.txt";

            using (var streamWriter = new StreamWriter(path, true, Encoding.Unicode)) //Запись (путь, добавлять в файл?, кодировка)
            {
                streamWriter.WriteLine("Hello!"); //Дописать в строку
                streamWriter.WriteLine("Привет!"); //Дописать в строку и перейти на новую
            }
        
            using (var streamReader = new StreamReader(path, Encoding.Unicode)) //Чтение (путь, кодировка) Стоит использовать одинаковые, так как кодировка при чтении записи и в консоли разная
            {
                var symbolCode = streamReader.Read(); //Считает код следующего символа
                Console.WriteLine(Convert.ToChar(symbolCode)); //Код в символ

                Console.WriteLine(streamReader.ReadLine()); //Считать строку

                var text = streamReader.ReadToEnd(); //Считать до конца файла

                System.Console.WriteLine(text);
                System.Console.WriteLine();

                streamReader.DiscardBufferedData(); //Сброс указателья на начало потока
                streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                while (streamReader.EndOfStream == false) //Чтение до конца файла построчно
                {
                    Console.WriteLine(streamReader.ReadLine());
                }               
            }        
        }

        //Сохранить в 2 файла названия городов и количество жителей в них, затем
        //загрузить оба и вывести 
        static void ShowCitiesPopulation()
        {
            var pathData = "Data.txt";
            var pathCity = "City.txt"; 

            using (var streamWriter = new StreamWriter(pathCity))
            {
                    streamWriter.WriteLine("City1");
                    streamWriter.WriteLine("City2");
                    streamWriter.WriteLine("City3");
                    streamWriter.WriteLine("City4");
            }

            var random = new Random();
            using (var streamWriter = new StreamWriter(pathData))
            {
                for (int i = 0; i < 4; i++)
                    streamWriter.WriteLine(random.Next(500000, 1000000));                        
            }

            using (var cityStreamReader = new StreamReader(pathCity))
            {
                using (var dataStreamReader = new StreamReader(pathData))
                {
                    while (cityStreamReader.EndOfStream == false && dataStreamReader.EndOfStream == false)
                    {
                        var city = cityStreamReader.ReadLine();
                        var population = dataStreamReader.ReadLine();
                        System.Console.WriteLine($"{city}, {population}");
                    }
               }
            }
        }
    }
}
