
using System;
using System.IO;
namespace TH002RegEx;
class Program
{
    static void Main(string[] args)
    {
        
         if (args.Length <= 0)// Проверяем, переданы ли в cmd аргументы : имена файлов с заданиями
        {
             Console.WriteLine("Аргументы не переданы, используйте: program.exe task001.txt task002.txt ... task00n.txt");
             return;
         }
        
        
        foreach (string taskFileName in args)// Обрабатываем каждый файл с заданием

        {
            // Релизация 
            try
            {
                // Читаем содержимое файла с заданием:
                string[] lines = File.ReadAllLines(taskFileName);// считываем строки из файла и записываем в массив[]

                using (StreamWriter writer = new StreamWriter(Path.GetFileNameWithoutExtension(taskFileName) + "solution.txt", true))
                // создаем файл  «Task00Nsolution.txt» ,а чтобы  убрать расширение txt в имени исходного файла используем GetFileNameWithoutExtension. 
                // открываем поток для записи с использованием класса StreamWriter.
                //true" в  StreamWriter указывает, что данные в файл добавляются, если он существует.
                {

                    foreach (string line in lines) //перебираем каждую строку в массиве lines
                    {
                        string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);//каждую строку разделяем на части и записываем в массив (parts), используя пробел в качестве разделителя
                                                                                                //StringSplitOptions.RemoveEmptyEntries используем для удаления  пустых записей из результирующего массива.

                        if (parts.Length == 4 && double.TryParse(parts[0], out double leftOperand) && double.TryParse(parts[2], out double rightOperand))
                        // условие проверяет, можно ли парсить строку на 4 части и отдельно первую и третью части

                        {
                            
                            switch (parts[1])// отрабатываем  вторую часть строки, где должен быть оператор
                            {
                                case "+":
                                    writer.WriteLine($"{line} {leftOperand + rightOperand}");//записываем в новый  файл: строку line и результат операции
                                    
                                    break;
                                case "-":
                                    writer.WriteLine($"{line} {leftOperand - rightOperand}");
                                   
                                    break;
                                case "*":
                                    writer.WriteLine($"{line} {leftOperand * rightOperand}");
                                  
                                    break;
                                case "/":
                                    if (rightOperand == 0)
                                    {
                                        writer.WriteLine( $"{line} ERROR: Деление  на ноль ");
                                        Console.WriteLine("ERROR: Деление  на ноль! Данные об ошибке записаны в новый  файл ");

                                    }
                                    else
                                    {
                                        writer.WriteLine($"{line} {leftOperand  / rightOperand}");
                                        

                                    }
                                    break;
                                default:
                                    writer.WriteLine($"{line} ERROR: Недопустимый оператор ");
                                    Console.WriteLine("ERROR: Недопустимый оператор! Данные об ошибке записаны в новый  файл");

                                    break;
                            }
                        }
                        else
                        {
                             writer.WriteLine($"{line} ERROR: Недопустимый формат ");
                             Console.WriteLine("ERROR: Недопустимый формат!Данные об ошибке записаны в новый файл");
                        }
                        
                    }
                    Console.WriteLine("Решения для {0} записаны в новый файл ", taskFileName);
                }



                Console.WriteLine("Задача выполнена! Нажмите клавишу ENTER.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();



        }

    }
}

