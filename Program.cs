using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ListToBin;

namespace Calendar {
    public class Program {
        private static string prazdnikiFileName = "Prazdniki.bin";
        private static string napominanieFileName = "Napominanies.bin";
        private static ListToBin<Event> Prazdniki;
        private static ListToBin<Event> Napominanies;
        public static void Main(string[] args) {
            DateTime todayDate = DateTime.Now;
            string dayOfWeek = DayOfWeekRussianString(todayDate);
            Console.WriteLine($"Сегодня {todayDate}, {dayOfWeek}.");

            Prazdniki = new ListToBin<Event>(prazdnikiFileName);
            Napominanies = new ListToBin<Event>(napominanieFileName);
            Prazdniki.Open();
            Napominanies.Open();

            foreach (var prazdnik in Prazdniki.List.Where(@event => @event.Date.Date.Equals(todayDate.Date))) {
                Console.WriteLine($"Сегодня праздник: {prazdnik.Name}.");
            }
            foreach (var napominanie in Napominanies.List.Where(@event => @event.Date.Date.Equals(todayDate.Date))) {
                Console.WriteLine($"Напоминание сегодня: {napominanie.Name} в {napominanie.Date.TimeOfDay}.");
            }

            while (true) {
                int motionType1;
                while (true) {
                    Console.WriteLine("Чтобы выйти, нажмите 1. Чтобы работать с праздниками и напоминаниями, введите 2.");
                    bool parsed = int.TryParse(Console.ReadLine(), out motionType1);
                    if (!parsed) {
                        continue;
                    }

                    if (motionType1 == 1 || motionType1 == 2) {
                        break;
                    }
                }

                if (motionType1 == 1) {
                    break;
                }
                else
                if (motionType1 == 2) {
                    Event.Type type;
                    while (true) {
                        Console.WriteLine("Чтобы работать с празниками, введите 1.");
                        Console.WriteLine("Чтобы работать с напоминанием, введите 2.");
                        bool parsed = int.TryParse(Console.ReadLine(), out int result);
                        if (!parsed) {
                            continue;
                        }

                        if (result == 1) {
                            type = Event.Type.Prazdnik;
                            break;
                        }
                        else
                        if (result == 2) {
                            type = Event.Type.Napominanie;
                            break;
                        }
                        else {
                            continue;
                        }
                    }

                    if (type == Event.Type.Prazdnik) {
                        int motionType;
                        while (true) {
                            Console.WriteLine("Чтобы посмотреть список праздников, введите 1.");
                            Console.WriteLine("Чтобы создать праздник, введите 2.");
                            Console.WriteLine("Чтобы удалить праздник, введите 3.");
                            bool parsed = int.TryParse(Console.ReadLine(), out motionType);
                            if (!parsed) {
                                continue;
                            }

                            if (motionType >= 1 && motionType <= 3) {
                                break;
                            }
                        }

                        if (motionType == 1) {
                            if (!Prazdniki.List.Any()) {
                                Console.WriteLine("Список праздников пуст.");
                            }
                            else {
                                foreach (var prazdnik in Prazdniki.List) {
                                    Console.WriteLine(prazdnik.ToString());
                                }
                            }
                        }
                        else
                        if (motionType == 2) {
                            DateTime date;
                            while (true) {
                                Console.Write("Введите дату: ");
                                try {
                                    date = DateTime.Parse(Console.ReadLine());
                                    break;
                                }
                                catch (FormatException) {
                                    Console.WriteLine("Дата некорректна.");
                                }
                            }

                            string name;
                            while (true) {
                                Console.Write("Введите название праздника: ");
                                name = Console.ReadLine();
                                if (!name.Any()) {
                                    Console.WriteLine("Название не может быть пустым.");
                                    continue;
                                }

                                break;
                            }

                            Prazdniki.List.Add(new Event(date, name, Event.Type.Prazdnik));
                            Console.WriteLine("Успешно.");
                        }
                        else
                        if (motionType == 3) {
                            while (true) {
                                DateTime date;
                                while (true) {
                                    Console.Write("Введите дату праздника: ");

                                    if (!DateTime.TryParse(Console.ReadLine(), out date)) {
                                        Console.WriteLine("Некорректная дата.");
                                        continue;
                                    }

                                    break;
                                }

                                Console.Write("Введите название праздника: ");
                                string prazdnikName = Console.ReadLine();

                                Event prazdnikToRemove = Prazdniki.List.FirstOrDefault(prazdnik => prazdnik.Date.Equals(date.Date) && prazdnik.Name.Equals(prazdnikName));
                                if (prazdnikToRemove == null) {
                                    Console.WriteLine("Праздника на текущую дату не найдено.");
                                    continue;
                                }

                                Prazdniki.List.Remove(prazdnikToRemove);
                                Console.WriteLine("Праздник успешно удалён.");
                                break;
                            }

                        }
                        else {
                            throw new Exception();
                        }
                    }
                    else
                    if (type == Event.Type.Napominanie) {
                        int motionType;
                        while (true) {
                            Console.WriteLine("Чтобы посмотреть список напоминаний, введите 1.");
                            Console.WriteLine("Чтобы создать напоминание, введите 2.");
                            Console.WriteLine("Чтобы удалить напоминание, введите 3.");
                            bool parsed = int.TryParse(Console.ReadLine(), out motionType);
                            if (!parsed) {
                                continue;
                            }

                            if (motionType < 1 || motionType > 3) {
                                continue;
                            }

                            break;
                        }

                        if (motionType == 1) {
                            if (!Napominanies.List.Any()) {
                                Console.WriteLine("Список напоминаний пуст.");
                            }
                            else {
                                foreach (var napominanie in Napominanies.List) {
                                    Console.WriteLine(napominanie);
                                }
                            }
                        }
                        else
                        if (motionType == 2) {
                            DateTime dateTime;
                            while (true) {
                                Console.Write("Введите дату и время напоминания: ");
                                try {
                                    dateTime = DateTime.Parse(Console.ReadLine());
                                    break;
                                }
                                catch (Exception) {
                                    Console.WriteLine("Некорректная дата.");
                                }
                            }

                            string name;
                            while (true) {
                                Console.Write("Введите название напоминания: ");
                                name = Console.ReadLine();
                                if (name.Any()) {
                                    break;
                                }
                                else {
                                    Console.WriteLine("Название не может быть пустым.");
                                }
                            }

                            Napominanies.List.Add(new Event(dateTime, name, Event.Type.Napominanie));
                            Console.WriteLine("Напоминание успешно добавлено.");
                        }
                        else
                        if (motionType == 3) {
                            DateTime date;
                            while (true) {
                                Console.Write("Введите дату и время напоминания: ");
                                bool parsed = DateTime.TryParse(Console.ReadLine(), out date);
                                if (!parsed) {
                                    Console.WriteLine("Дата имела неверный формат.");
                                    continue;
                                }

                                break;
                            }

                            var napominanieToRemove = Napominanies.List.FirstOrDefault(napominanie => napominanie.Date.Equals(date));
                            if (napominanieToRemove == null) {
                                Console.WriteLine("Напоминания с такой датой не найдено.");
                            }

                            Napominanies.List.Remove(napominanieToRemove);
                            Console.WriteLine("Успешно.");
                        }
                        else {
                            throw new Exception();
                        }
                    }
                    else {
                        throw new Exception();
                    }
                }
                else {
                    throw new Exception();
                }
            }

            Prazdniki.CommitAndClose();
            Napominanies.CommitAndClose();
        }

        private static string DayOfWeekRussianString(DateTime date) {
            switch (date.DayOfWeek) {
                case DayOfWeek.Monday:
                return "Понедельник";
                case DayOfWeek.Tuesday:
                return "Вторник";
                case DayOfWeek.Wednesday:
                return "Среда";
                case DayOfWeek.Thursday:
                return "Четверг";
                case DayOfWeek.Friday:
                return "Пятница";
                case DayOfWeek.Saturday:
                return "Суббота";
                case DayOfWeek.Sunday:
                return "Воскресенье";
                default:
                throw new Exception();
            }
        }
    }
}
