using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EKZAMEN
{
    public class WordS
    {
        public string Word { get; set; }
       
        public List<string> Translations { get; set; }

        public WordS()
        {
            Word = string.Empty;
            Translations = new List<string>();
        }

        public WordS(string word, string translation)
        {
            Word = word;
            Translations = new List<string> { translation };
        }

        public WordS(string word, List<string> translations)
        {
            Word = word;
            Translations = translations;
        }

        public static WordS AddWord()// Добавление слова
        {
            Console.WriteLine("Введите слово: ");
            string word = Console.ReadLine();

            List<string> translations = new List<string>();
            bool continueAddingTranslations = true;

            while (continueAddingTranslations)
            {
                Console.WriteLine("Введите перевод: ");
                string translation = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(translation))
                {
                    translations.Add(translation);
                    Console.WriteLine("Перевод добавлен!");
                }
                else
                {
                    Console.WriteLine("Перевод не может быть пустым!");
                }

                if (translations.Count > 0)
                {
                    Console.WriteLine("Добавить еще один перевод? (y/n)");
                    string answer = Console.ReadLine();
                    continueAddingTranslations = answer.ToLower() == "y";
                }
            }

            return new WordS { Word = word, Translations = translations };
        }

        public void EditWord()//РЕДАКТИРОВАНИЕ СЛОВА
        {
            Console.WriteLine($"Текущее слово: {Word}");
            Console.WriteLine("Введите новое слово (или нажмите Enter чтобы оставить без изменений): ");
            string newWord = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newWord))//Если строка не пустая то слово обновится
            {
                Word = newWord;
            }

            Console.WriteLine("\nТекущие переводы:");
            DisplayTranslations();

            Console.WriteLine("\nВыберите действие с переводами:");
            Console.WriteLine("1 - Добавить новый перевод");
            Console.WriteLine("2 - Редактировать существующий перевод");
            Console.WriteLine("3 - Удалить перевод");
            Console.WriteLine("4 - Оставить без изменений");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddTranslation();
                    break;
                case "2":
                    EditTranslation();
                    break;
                case "3":
                    DeleteTranslation();
                    break;
                case "4":
                    Console.WriteLine("Переводы остались без изменений.");
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }

            Console.WriteLine("Слово успешно обновлено!");
        }

        // МЕТОД ДОБАВЛЕНИЯ НОВОГО ПЕРЕВОДА
        public void AddTranslation()
        {
            Console.WriteLine("Введите новый перевод: ");
            string newTranslation = Console.ReadLine();// строка нового превода

            if (!string.IsNullOrWhiteSpace(newTranslation))//если строкаа не пустая
            {
                if (Translations.Any(t => t.Equals(newTranslation, StringComparison.OrdinalIgnoreCase)))//проверка на совпадения
                {
                    Console.WriteLine("Такой перевод уже существует!");
                }
                else
                {
                    Translations.Add(newTranslation);
                    Console.WriteLine("Новый перевод добавлен!");
                }
            }
            else
            {
                Console.WriteLine("Перевод не может быть пустым!");
            }
        }

        // МЕТОД РЕДАКТИРОВАНИЯ СУЩЕСТВУЮЩЕГО ПЕРЕВОДА
        public void EditTranslation()
        {
            if (Translations.Count == 0)
            {
                Console.WriteLine("Нет переводов для редактирования.");
                return;
            }

            Console.WriteLine("\nТекущие переводы:");
            for (int i = 0; i < Translations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Translations[i]}");
            }

            Console.Write("Введите номер перевода для редактирования: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Translations.Count)//строку сразу преобразуем в число и сравниваем пределы
            {
                Console.WriteLine($"Текущий перевод: {Translations[index - 1]}");
                Console.WriteLine("Введите новый перевод: ");
                string newTranslation = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(newTranslation))
                {
                    Translations[index - 1] = newTranslation;
                    Console.WriteLine("Перевод обновлен!");
                }
                else
                {
                    Console.WriteLine("Перевод не может быть пустым!");
                }
            }
            else
            {
                Console.WriteLine("Неверный номер перевода!");
            }
        }

        // МЕТОД УДАЛЕНИЯ ПЕРЕВОДА
        public void DeleteTranslation()
        {
            if (Translations.Count == 0)
            {
                Console.WriteLine("Нет переводов для удаления.");
                return;
            }

            if (Translations.Count == 1)
            {
                Console.WriteLine("Нельзя удалить единственный перевод слова!");
                return;
            }

            Console.WriteLine("\nТекущие переводы:");
            for (int i = 0; i < Translations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Translations[i]}");
            }

            Console.Write("Введите номер перевода для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Translations.Count)
            {
                Console.WriteLine($"Вы уверены, что хотите удалить перевод: '{Translations[index - 1]}'? (y/n)");
                string confirm = Console.ReadLine();

                if (confirm.ToLower() == "y")
                {
                    Translations.RemoveAt(index - 1);
                    Console.WriteLine("Перевод удален!");
                }
                else
                {
                    Console.WriteLine("Удаление отменено.");
                }
            }
            else
            {
                Console.WriteLine("Неверный номер перевода!");
            }
        }

        // МЕТОД ДЛЯ ОТОБРАЖЕНИЯ ВСЕХ ПЕРЕВОДОВ
        public void DisplayTranslations()
        {
            if (Translations.Count == 0)
            {
                Console.WriteLine("  Нет переводов");
                return;
            }

            for (int i = 0; i < Translations.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {Translations[i]}");
            }
        }

        // МЕТОД ДЛЯ ПОЛУЧЕНИЯ ПЕРЕВОДОВ В ВИДЕ СТРОКИ
        public string GetTranslationsString()// БУДЕТ ИСПОЛЬЗОВАТЬСЯ ДЛЯ ОТОБРАЖЕНИЯ ПЕРЕВОДОВ
        {
            return string.Join(", ", Translations);
        }


    }

    public class Dictionary
    {
        public string NameDictionary { get; set; }
        public List<WordS> Words { get; set; }

        public Dictionary()
        {
            NameDictionary = "";
            Words = new List<WordS>();
        }

        public static Dictionary AddDictionary()// Создаем новый словарь
        {
            Console.WriteLine("Введите название словаря: ");
            string namedictionary = Console.ReadLine();
            return new Dictionary
            {
                NameDictionary = namedictionary,
                Words = new List<WordS>()
            };
        }

        // МЕТОД ДЛЯ ДОБАВЛЕНИЯ НОВЫХ СЛОВ В СУЩЕСТВУЮЩИЙ СЛОВАРЬ
        public void AddWordsToDictionary()
        {
            Console.WriteLine($"\n=== ДОБАВЛЕНИЕ СЛОВ В СЛОВАРЬ: {NameDictionary} ===");

            bool continueAdding = true;// Флаг добавления слов
            int wordsAdded = 0;//Количество добавленных слов

            while (continueAdding)
            {
                Console.WriteLine("\nВведите новое слово:");
                string word = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(word))
                {
                    Console.WriteLine("Слово не может быть пустым!");
                    continue;
                }

                var existingWord = Words.FirstOrDefault(w => w.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
                if (existingWord != null)
                {
                    Console.WriteLine($"Слово '{word}' уже существует в словаре.");
                    Console.WriteLine("Текущие переводы:");
                    existingWord.DisplayTranslations();

                    Console.WriteLine("Хотите добавить дополнительные переводы к существующему слову? (y/n)");
                    string addMoreAnswer = Console.ReadLine();

                    if (addMoreAnswer.ToLower() == "y")
                    {
                        existingWord.AddTranslation();
                        Console.WriteLine("Дополнительный перевод добавлен к существующему слову!");
                    }
                    else
                    {
                        Console.WriteLine("Слово не добавлено.");
                    }
                }
                else
                {
                    List<string> translations = new List<string>();
                    bool continueAddingTranslations = true;

                    while (continueAddingTranslations)
                    {
                        Console.WriteLine("Введите перевод:");
                        string translation = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(translation))
                        {
                            Console.WriteLine("Перевод не может быть пустым!");
                            continue;
                        }

                        translations.Add(translation);
                        Console.WriteLine("Перевод добавлен!");

                        Console.WriteLine("Добавить еще один перевод? (y/n)");
                        string answe = Console.ReadLine();
                        continueAddingTranslations = answe.ToLower() == "y";
                    }

                    Words.Add(new WordS(word, translations));
                    wordsAdded++;
                    Console.WriteLine("Слово успешно добавлено!");
                }

                Console.WriteLine("\nДобавить еще одно слово? (y/n)");
                string answer = Console.ReadLine();
                continueAdding = answer.ToLower() == "y";
            }

            Console.WriteLine($"\nДобавлено новых слов: {wordsAdded}");
            Console.WriteLine($"Общее количество слов в словаре: {Words.Count}");
        }

        // МЕТОД РЕДАКТИРОВАНИЯ СЛОВА
        public void EditWordInDictionary()
        {
            if (Words.Count == 0)
            {
                Console.WriteLine("Словарь пуст. Нет слов для редактирования.");
                return;
            }

            Console.WriteLine($"\n=== РЕДАКТИРОВАНИЕ СЛОВА В СЛОВАРЕ: {NameDictionary} ===");

            Console.WriteLine("Текущие слова в словаре:");
            for (int i = 0; i < Words.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Words[i].Word} - {Words[i].GetTranslationsString()}");
            }

            Console.WriteLine("\nВыберите способ поиска слова для редактирования:");
            Console.WriteLine("1 - Выбрать по номеру из списка");
            Console.WriteLine("2 - Найти по названию");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();
            WordS wordToEdit = null;

            switch (choice)
            {
                case "1":
                    Console.Write("Введите номер слова: ");
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Words.Count)
                    {
                        wordToEdit = Words[index - 1];
                    }
                    else
                    {
                        Console.WriteLine("Неверный номер слова!");
                        return;
                    }
                    break;

                case "2":
                    Console.Write("Введите слово для поиска: ");
                    string searchTerm = Console.ReadLine();
                    wordToEdit = Words.FirstOrDefault(w => w.Word.Equals(searchTerm, StringComparison.OrdinalIgnoreCase));

                    if (wordToEdit == null)
                    {
                        Console.WriteLine($"Слово '{searchTerm}' не найдено в словаре.");
                        return;
                    }
                    break;

                default:
                    Console.WriteLine("Неверный выбор!");
                    return;
            }

            if (wordToEdit != null)
            {
                Console.WriteLine($"\nРедактирование слова: {wordToEdit.Word} - {wordToEdit.GetTranslationsString()}");
                wordToEdit.EditWord();
            }
        }

        // МЕТОД УДАЛЕНИЯ СЛОВА
        public void DeleteWordFromDictionary()
        {
            if (Words.Count == 0)
            {
                Console.WriteLine("Словарь пуст. Нет слов для удаления.");
                return;
            }

            Console.WriteLine($"\n=== УДАЛЕНИЕ СЛОВА ИЗ СЛОВАРЯ: {NameDictionary} ===");

            Console.WriteLine("Текущие слова в словаре:");
            for (int i = 0; i < Words.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Words[i].Word} - {Words[i].GetTranslationsString()}");
            }

            Console.WriteLine("\nВыберите способ поиска слова для удаления:");
            Console.WriteLine("1 - Выбрать по номеру из списка");
            Console.WriteLine("2 - Найти по названию");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();
            WordS wordToDelete = null;

            switch (choice)
            {
                case "1":
                    Console.Write("Введите номер слова для удаления: ");
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Words.Count)
                    {
                        wordToDelete = Words[index - 1];
                    }
                    else
                    {
                        Console.WriteLine("Неверный номер слова!");
                        return;
                    }
                    break;

                case "2":
                    Console.Write("Введите слово для удаления: ");
                    string searchTerm = Console.ReadLine();
                    wordToDelete = Words.FirstOrDefault(w => w.Word.Equals(searchTerm, StringComparison.OrdinalIgnoreCase));

                    if (wordToDelete == null)
                    {
                        Console.WriteLine($"Слово '{searchTerm}' не найдено в словаре.");
                        return;
                    }
                    break;

                default:
                    Console.WriteLine("Неверный выбор!");
                    return;
            }

            if (wordToDelete != null)
            {
                Console.WriteLine($"\nВы уверены, что хотите удалить слово: {wordToDelete.Word} - {wordToDelete.GetTranslationsString()}? (y/n)");
                string confirm = Console.ReadLine();

                if (confirm.ToLower() == "y")
                {
                    Words.Remove(wordToDelete);
                    Console.WriteLine("Слово успешно удалено!");
                }
                else
                {
                    Console.WriteLine("Удаление отменено.");
                }
            }
        }
        public void DisplayDictionary()
        {
            Console.WriteLine($"\nСловарь: {NameDictionary}");
            if (Words.Count == 0)
            {
                Console.WriteLine("Словарь пуст.");
                return;
            }

            Console.WriteLine("Слова:");
            for (int i = 0; i < Words.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Words[i].Word} - {Words[i].GetTranslationsString()}");
            }
            Console.WriteLine($"Всего слов: {Words.Count}");
        }
        // МЕТОД ПОИСКА СЛОВА
        public void SearchWord()
        {
            if (Words.Count == 0)
            {
                Console.WriteLine("Словарь пуст. Нет слов для поиска.");
                return;
            }

            Console.WriteLine("\n=== ПОИСК СЛОВА ===");
            Console.WriteLine("Введите слово или часть слова для поиска:");
            string searchTerm = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine("Поисковый запрос не может быть пустым.");
                return;
            }

            // Поиск по слову и переводу (без учета регистра)
            var foundWords = Words.Where(w => w.Word.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 
            || w.Translations.Any(t => t.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
            //Words.Where фильтрует коллекцию Words по условию, когда в w.Word ищется запрос searchTerm на любое совпадение при котором возвращается либо 0, либо >0
            //если такого совпадения не случается, то возвращается -1. Что означает что совпадений нет.
            if (foundWords.Count == 0)
            {
                Console.WriteLine($"Слова, содержащие '{searchTerm}', не найдены.");
            }
            else
            {
                Console.WriteLine($"\nНайдено {foundWords.Count} слов, содержащих '{searchTerm}':");
                Console.WriteLine(new string('-', 40));

                foreach (var word in foundWords)
                {
                    Console.WriteLine($"{word.Word} - {word.GetTranslationsString()}");
                }
                Console.WriteLine(new string('-', 40));
            }
        }
        //СОХРАНЕНИЕ В ФАЙЛ
        public void SaveDictionaryToFile()
        {
            string fileName = $"{NameDictionary}.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))//false перезаписать полностью, true записать в конец файла
                {
                    writer.WriteLine($"Словарь: {NameDictionary}");
                    writer.WriteLine($"Дата создания: {DateTime.Now}");
                    writer.WriteLine($"Дата последнего изменения: {DateTime.Now}");
                    writer.WriteLine("Слова:");
                    writer.WriteLine(new string('-', 40));

                    foreach (var word in Words)
                    {
                        writer.WriteLine($"{word.Word} - {word.GetTranslationsString()}");
                    }

                    writer.WriteLine(new string('-', 40));
                    writer.WriteLine($"Всего слов: {Words.Count}");
                }

                Console.WriteLine($"\nСловарь успешно сохранен в файл: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении файла: {ex.Message}");
            }
        }
        //ЗАГРУЗКА ИЗ ФАЙЛА
        public static Dictionary LoadDictionaryFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))//проверка на существование файла
                {
                    if (!filePath.EndsWith(".txt"))//если введенный файл не содержит .txt
                    {
                        filePath += ".txt";//автоматически добавляет расширение .txt
                    }

                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine($"Файл {filePath} не найден!");
                        return null;
                    }
                }

                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))//using - автоматически закрывает файл послечтения
                {
                    Dictionary dictionary = new Dictionary();// новый пустой объект словаря, который будем заполнять
                    string line;// переменная для хранения текущей строки
                    bool readingWords = false;// флаг покажывающий что считывание дошло до самих слов

                    while ((line = reader.ReadLine()) != null)//считываем каждую строку пока строка не станет null
                    {
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("---"))//пропускаем пустые строки и дефисы
                            continue;

                        if (line.StartsWith("Словарь:"))//при считывании слова (Словарь:), записывает значение строки в dictionary.NameDictionary
                        {
                            dictionary.NameDictionary = line.Substring(8).Trim();//line.Substring(8) - берет подстроку начиная с 9-го символа (после "Словарь:")
                            continue;
                        }

                        if (line.Trim() == "Слова:")//Trim - убирает пробелы в начале и конце
                        {
                            readingWords = true;//флаг начала считывания слов
                            continue;
                        }

                        if (line.StartsWith("Всего слов:"))//Прерывает цикл когда встречает строку с количеством слов
                        {
                            break;
                        }

                        if (readingWords && line.Contains("-"))//условие когда readingWords - true и строка содежит разделитель ("-")
                        {
                            string[] parts = line.Split(new[] { '-' }, 2);//Split() - разделяет строку на части по указанному разделителю
                                                                          //new[] { '-' } - разделитель: символ дефиса
                                                                          //2 - максимальное количество частей
                            if (parts.Length == 2)
                            {
                                string word = parts[0].Trim();//первая часть слова
                                string translation = parts[1].Trim();//вторая часть слова - его перевод
                                dictionary.Words.Add(new WordS(word, translation));//Создает новый объект WordS и добавляет в коллекцию Words
                            }
                        }
                    }

                    Console.WriteLine($"Словарь успешно загружен из файла: {filePath}");
                    return dictionary;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return null;
            }
        }
        //ОТОБРАЖЕНИЕ ДОСТУПНЫХ СЛОВАРЕЙ
        public static void ShowAvailableDictionaries()
        {
            try
            {
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt");
                //files - массив строк для хранения названий файлов
                //GetFiles - Ищет все файлы в указанной папке, соответствующие шаблону (.txt)
                //GetCurrentDirectory - возвращает путь к текущей директории
                if (files.Length == 0)
                {
                    Console.WriteLine("Нет доступных файлов словарей.");
                    return;
                }

                Console.WriteLine("\nДоступные словари:");
                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске файлов: {ex.Message}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=======================================    СЛОВАРЬ    =====================================================\n\n\n");

            bool exitProgram = false;
            Dictionary currentDictionary = null;// пуслой словарь,Ю в который мы либо загрузим из файла, либо создадим новый (заполним этот)

            while (!exitProgram)
            {
                Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
                Console.WriteLine("1 - Создать новый словарь");
                Console.WriteLine("2 - Загрузить словарь из файла");

                if (currentDictionary != null)
                {
                    Console.WriteLine($"3 - Работа с текущим словарем: {currentDictionary.NameDictionary}");
                    Console.WriteLine("4 - Сохранить текущий словарь");
                }

                Console.WriteLine("0 - Выход из программы");
                Console.Write("Ваш выбор: ");

                string mainChoice = Console.ReadLine();

                switch (mainChoice)
                {
                    case "1":
                        currentDictionary = CreateNewDictionary();
                        break;

                    case "2":
                        currentDictionary = LoadDictionary();
                        break;

                    case "3":
                        if (currentDictionary != null)
                        {
                            WorkWithDictionary(currentDictionary);
                        }
                        else
                        {
                            Console.WriteLine("Сначала создайте или загрузите словарь!");
                        }
                        break;

                    case "4":
                        if (currentDictionary != null)
                        {
                            currentDictionary.SaveDictionaryToFile();
                        }
                        else
                        {
                            Console.WriteLine("Нет текущего словаря для сохранения!");
                        }
                        break;

                    case "0":
                        exitProgram = true;
                        Console.WriteLine("До свидания!");
                        break;

                    default:
                        Console.WriteLine("Неверный выбор! Попробуйте снова.");
                        break;
                }
            }

            Console.WriteLine("\nНажмите Enter для выхода...");
            Console.ReadLine();
        }
        //СОЗДАНИЕ СЛОВАРЯ И ЗАПОЛНЕНИЕ СЛОВАМИ
        static Dictionary CreateNewDictionary()
        {
            Console.WriteLine("\n=== СОЗДАНИЕ НОВОГО СЛОВАРЯ ===");
            Dictionary dictionary = Dictionary.AddDictionary();

            bool continueAdding = true;
            while (continueAdding)//Новый словарь заполняется словами пока continueAdding = true
            {
                WordS newWord = WordS.AddWord();//создали и заполнили слово
                dictionary.Words.Add(newWord);//добавили слово в коллекцию (словарь)
                Console.WriteLine("Добавить еще слово? (y/n)");
                string answer = Console.ReadLine();
                continueAdding = answer.ToLower() == "y";
            }

            dictionary.DisplayDictionary();//выводим словарь на экран
            dictionary.SaveDictionaryToFile();//сохраняем словарь в файл

            return dictionary;
        }
        //Метод загрузки словаря
        static Dictionary LoadDictionary()
        {
            Console.WriteLine("\n=== ЗАГРУЗКА СЛОВАРЯ ===");
            Dictionary.ShowAvailableDictionaries();//отображаем все доступные словари

            Console.WriteLine("Введите название загружаемого словаря (с расширением .txt или без): ");
            string dictionaryName = Console.ReadLine();

            Dictionary loadedDictionary = Dictionary.LoadDictionaryFromFile(dictionaryName);
            if (loadedDictionary != null)
            {
                Console.WriteLine("\nЗагруженный словарь:");
                loadedDictionary.DisplayDictionary();//отображаем слова в загруженном словаре
                return loadedDictionary;
            }

            return null;
        }

        static void WorkWithDictionary(Dictionary dictionary)
        {
            bool workingWithDictionary = true;

            while (workingWithDictionary)
            {
                Console.WriteLine($"\n=== РАБОТА СО СЛОВАРЕМ: {dictionary.NameDictionary} ===");
                Console.WriteLine("1 - Показать все слова");
                Console.WriteLine("2 - Поиск слова");
                Console.WriteLine("3 - Добавить новые слова");
                Console.WriteLine("4 - Редактировать слово");
                Console.WriteLine("5 - Удалить слово");
                Console.WriteLine("6 - Сохранить словарь");
                Console.WriteLine("0 - Вернуться в главное меню");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        dictionary.DisplayDictionary();
                        break;

                    case "2":
                        dictionary.SearchWord();
                        break;

                    case "3":
                        dictionary.AddWordsToDictionary();
                        break;

                    case "4":
                        dictionary.EditWordInDictionary();
                        break;

                    case "5":
                        dictionary.DeleteWordFromDictionary();
                        break;

                    case "6":
                        dictionary.SaveDictionaryToFile();
                        break;

                    case "0":
                        workingWithDictionary = false;
                        Console.WriteLine("Возврат в главное меню...");
                        break;

                    default:
                        Console.WriteLine("Неверный выбор! Попробуйте снова.");
                        break;
                }
            }
        }
    }
}
