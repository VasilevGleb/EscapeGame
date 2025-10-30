using System;
using System.Collections.Generic;
using System.Threading;

namespace Esc0
{

    class Program
    {
        enum Item { Knife, LaboratoryPassword, Medkit, KeyCard }
        static List<Item> inventory = new List<Item>();

        static void TypeText(string text, ConsoleColor color, int speed = 30)
        {
            Console.ForegroundColor = color;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(speed);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        static string GetItemName(Item item)
        {
            switch (item)
            {
                case Item.Knife: return "Макетный нож";
                case Item.LaboratoryPassword: return "Код для лаборатории";
                case Item.Medkit: return "Аптечка";
                case Item.KeyCard: return "Ключ-карта";
                default: return "Неизвестный предмет";
            }
        }

        static string ActionInput()
        {
            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine();
            Console.ResetColor();
            return input?.Trim().ToLower() ?? string.Empty;
        }

        static void ShowInventory()
        {
            Console.Clear();
            TypeText("\n╔══════════════════════════════════════════╗", ConsoleColor.Gray);
            TypeText("║            И Н В Е Н Т А Р Ь           ║", ConsoleColor.Gray);
            TypeText("╚══════════════════════════════════════════╝", ConsoleColor.Gray);
            if (inventory.Count == 0)
            {
                TypeText("\n>>> Инвентарь пуст", ConsoleColor.Gray);
            }
            else
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    TypeText($"  [{i + 1}] {GetItemName(inventory[i])}", ConsoleColor.Gray);
                }
            }
            TypeText("\nНажми Enter чтобы продолжить...", ConsoleColor.Gray);
            Console.ReadLine();
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool haveKnife = false;
            bool doorUnlocked = false;
            bool knowCodeForLab = false;
            bool searchedRoom = false;
            bool haveKeyCard = false;

            TypeText("\n... ... ...", ConsoleColor.DarkYellow);
            TypeText("Голова болит...", ConsoleColor.DarkYellow);
            TypeText("\n>>> Остаться лежать (1)", ConsoleColor.DarkYellow);
            TypeText(">>> Открыть глаза (2)", ConsoleColor.DarkYellow);

            string choice1 = ActionInput();
            switch (choice1)
            {
                case "1":
                    TypeText("\nТы погружаешься во тьму снова...", ConsoleColor.DarkYellow);
                    TypeText("... ... ...", ConsoleColor.Red);
                    TypeText("Ты чувствуешь как что-то начинает есть твои ноги...", ConsoleColor.Red);
                    TypeText("\n💀 Т Ы  У М Е Р 💀", ConsoleColor.Red);
                    TypeText("\nНажми любую клавишу чтобы выйти...", ConsoleColor.DarkYellow);
                    Console.ReadKey(true);
                    return;
                case "2":
                    TypeText("\nЧерез боль в голове ты открываешь глаза...", ConsoleColor.DarkYellow);
                    TypeText("...сфокусировав взгляд — понимаешь что ты в...", ConsoleColor.DarkYellow);
                    TypeText("в подсобке уборщика....", ConsoleColor.DarkYellow);
                    TypeText("из окон - выходящих в коридор,", ConsoleColor.DarkYellow);
                    TypeText("падал красный свет — оповещающий о серьезной аварии: 'Протокол безопасности §1.2'.", ConsoleColor.DarkYellow);
                    TypeText("Если следовать протоколу, то ты должен найти ближайшего охранника для твоего дальнейшего сопровождения.", ConsoleColor.DarkYellow);
                    break;
                default:
                    TypeText("Неверный ввод! Попробуй еще раз", ConsoleColor.DarkYellow);
                    return;
            }

            // Room exploration loop
            while (true)
            {
                Console.WriteLine();
                TypeText(">>> Выйти из комнаты (1)", ConsoleColor.DarkYellow);
                TypeText(">>> Осмотреться (2)", ConsoleColor.DarkYellow);
                if (haveKnife)
                {
                    TypeText(">>> Инвентарь (3)", ConsoleColor.DarkYellow);
                }

                string choice2 = ActionInput();
                switch (choice2)
                {
                    case "3":
                        if (haveKnife)
                        {
                            ShowInventory();
                        }
                        else
                        {
                            TypeText("Неверный ввод. Попробуй еще раз.", ConsoleColor.DarkYellow);
                        }
                        break;

                    case "2":
                        if (!searchedRoom)
                        {
                            TypeText("\nОсмотрев комнату ты находишь макетный нож.", ConsoleColor.DarkYellow);
                            inventory.Add(Item.Knife);
                            haveKnife = true;
                            searchedRoom = true;
                            TypeText("✓ Макетный нож добавлен в инвентарь", ConsoleColor.Green);
                        }
                        else
                        {
                            TypeText("Ты уже нашел все интересное здесь", ConsoleColor.DarkYellow);
                        }
                        break;

                    case "1":
                        if (!haveKnife)
                        {
                            TypeText("\nТы не стал тратить драгоценное время на осмотр комнаты и вышел в коридор.", ConsoleColor.DarkYellow);
                        }
                        else
                        {
                            TypeText("\nОсмотревшись выходишь из каморки...", ConsoleColor.DarkYellow);
                        }
                        goto CorridorSection;
                    default:
                        TypeText("Неверный ввод. Попробуй еще раз.", ConsoleColor.DarkYellow);
                        break;
                }
            }

        CorridorSection:
            TypeText("\nНа стене в коридоре ты видишь табличку:", ConsoleColor.DarkYellow);
            TypeText("\n╔══════════════════════════════════════════╗", ConsoleColor.Green);
            TypeText("║                                          ║", ConsoleColor.Green);
            TypeText("║             [ З О Н А  07 ]              ║", ConsoleColor.Green);
            TypeText("║                                          ║", ConsoleColor.Green);
            TypeText("║        << Лаборатория | Склад >>         ║", ConsoleColor.Green);
            TypeText("╚══════════════════════════════════════════╝", ConsoleColor.Green);

            // Main corridor loop
            while (true)
            {
                Console.WriteLine();
                TypeText(">>> Идти к лаборатории (1)", ConsoleColor.DarkYellow);
                TypeText(">>> Идти к складу (2)", ConsoleColor.DarkYellow);
                TypeText(">>> Инвентарь (3)", ConsoleColor.DarkYellow);
                TypeText(">>> Выйти из игры (0)", ConsoleColor.DarkYellow);

                string choice3 = ActionInput();
                switch (choice3)
                {
                    case "2":
                        HandleWarehouse(ref knowCodeForLab, ref haveKeyCard);
                        break;
                    case "1":
                        HandleLaboratory(ref doorUnlocked, knowCodeForLab);
                        break;
                    case "3":
                        ShowInventory();
                        break;
                    case "0":
                        TypeText("\nПожалуй самый простой выход, но он же здесь не один.", ConsoleColor.DarkYellow);
                        TypeText("\nНажми любую клавишу чтобы выйти...", ConsoleColor.DarkYellow);
                        Console.ReadKey(true);
                        return;
                    default:
                        TypeText("\nНеверный ввод. Попробуй еще раз.", ConsoleColor.DarkYellow);
                        break;
                }
            }
        }

        static void HandleWarehouse(ref bool knowCodeForLab, ref bool haveKeyCard)
        {
            TypeText("\nТы подходишь к складу...", ConsoleColor.DarkYellow);
            TypeText("Возле склада развилка", ConsoleColor.DarkYellow);
            TypeText("На табличке написано:", ConsoleColor.DarkYellow);
            TypeText("\n╔══════════════════════════════════════════╗", ConsoleColor.Green);
            TypeText("║                                          ║", ConsoleColor.Green);
            TypeText("║             [ З О Н А  07 ]              ║", ConsoleColor.Green);
            TypeText("║                [ Склад ]                 ║", ConsoleColor.Green);
            TypeText("║                                          ║", ConsoleColor.Green);
            TypeText("║        << Серверная | Изолятор >>        ║", ConsoleColor.Green);
            TypeText("╚══════════════════════════════════════════╝", ConsoleColor.Green);

            while (true)
            {
                Console.WriteLine();
                TypeText(">>> Зайти на склад (1)", ConsoleColor.DarkYellow);
                TypeText(">>> Идти в серверную (2)", ConsoleColor.DarkYellow);
                TypeText(">>> Идти к изолятору (3)", ConsoleColor.DarkYellow);
                TypeText(">>> Инвентарь (4)", ConsoleColor.DarkYellow);
                TypeText(">>> Вернуться назад (0)", ConsoleColor.DarkYellow);

                string choice = ActionInput();
                switch (choice)
                {
                    case "1":
                        TypeText("\nТы заходишь на склад...", ConsoleColor.DarkYellow);
                        TypeText("На полках лежат старые коробки и инструменты.", ConsoleColor.DarkYellow);
                        if (!inventory.Contains(Item.Medkit))
                        {
                            TypeText("На столе ты замечаешь аптечку.", ConsoleColor.DarkYellow);
                            inventory.Add(Item.Medkit);
                            TypeText("✓ Аптечка добавлена в инвентарь", ConsoleColor.Green);
                        }
                        else
                        {
                            TypeText("Здесь больше ничего интересного нет.", ConsoleColor.DarkYellow);
                        }
                        break;
                    case "2":
                        TypeText("\nТы подходишь к серверной...", ConsoleColor.DarkYellow);
                        TypeText("Магнитная дверь не реагирует.", ConsoleColor.DarkYellow);
                        if (haveKeyCard)
                        {
                            TypeText("Ты прикладываешь ключ-карту.", ConsoleColor.DarkYellow);
                            TypeText("✓ Дверь открывается!", ConsoleColor.Green);
                            TypeText("\nТы заходишь в серверную...", ConsoleColor.DarkYellow);
                            TypeText("Комната заполнена серверными стойками.", ConsoleColor.DarkYellow);
                            if (!knowCodeForLab)
                            {
                                TypeText("На одном из мониторов ты видишь записку:", ConsoleColor.DarkYellow);
                                TypeText("\n'Код для лаборатории: 15247'", ConsoleColor.Green);
                                knowCodeForLab = true;
                                TypeText("✓ Ты запомнил код", ConsoleColor.Green);
                            }
                            else
                            {
                                TypeText("Ты уже был здесь.", ConsoleColor.DarkYellow);
                            }
                        }
                        else
                        {
                            TypeText("Видимо тебе нужна ключ-карта.", ConsoleColor.DarkYellow);
                        }
                        break;
                    case "3":
                        TypeText("\nНа удивление изолятор открыт...", ConsoleColor.DarkYellow);
                        TypeText("Ты осторожно заходишь внутрь.", ConsoleColor.DarkYellow);
                        TypeText("В углу камеры лежит охранник.", ConsoleColor.DarkYellow);
                        if (!haveKeyCard)
                        {
                            TypeText("На его поясе висит ключ-карта.", ConsoleColor.DarkYellow);
                            TypeText("Ты забираешь её.", ConsoleColor.DarkYellow);
                            inventory.Add(Item.KeyCard);
                            haveKeyCard = true;
                            TypeText("✓ Ключ-карта добавлена в инвентарь", ConsoleColor.Green);
                        }
                        else
                        {
                            TypeText("Больше здесь ничего нет.", ConsoleColor.DarkYellow);
                        }
                        break;
                    case "4":
                        ShowInventory();
                        break;
                    case "0":
                        TypeText("\nТы возвращаешься в коридор...", ConsoleColor.DarkYellow);
                        return;
                    default:
                        TypeText("Неверный ввод. Попробуй еще раз.", ConsoleColor.DarkYellow);
                        break;
                }
            }
        }

        static void HandleLaboratory(ref bool doorUnlocked, bool knowCodeForLab)
        {
            TypeText("\nТы идешь к лаборатории...", ConsoleColor.DarkYellow);
            while (true)
            {
                if (!doorUnlocked)
                {
                    TypeText("Ты видишь дверь с кодовым замком...", ConsoleColor.DarkYellow);
                    Console.WriteLine();
                    if (knowCodeForLab)
                    {
                        TypeText(">>> Ввести код (1)", ConsoleColor.DarkYellow);
                    }
                    else
                    {
                        TypeText(">>> Взломать пароль (1)", ConsoleColor.DarkYellow);
                    }
                    TypeText(">>> Вернуться назад (2)", ConsoleColor.DarkYellow);
                    TypeText(">>> Инвентарь (3)", ConsoleColor.DarkYellow);

                    string choice = ActionInput();
                    switch (choice)
                    {
                        case "1":
                            if (knowCodeForLab)
                            {
                                TypeText("\nВведи 5-значный код:", ConsoleColor.DarkYellow);
                                string codeInput = ActionInput();
                                if (codeInput == "15247")
                                {
                                    TypeText("✓ Код принят!", ConsoleColor.Green);
                                    TypeText("Дверь открывается со щелчком...", ConsoleColor.DarkYellow);
                                    doorUnlocked = true;
                                    TypeText("\nТы заходишь в лабораторию...", ConsoleColor.DarkYellow);
                                    TypeText("В лаборатории полупусто, но на столе что-то есть.", ConsoleColor.DarkYellow);
                                }
                                else
                                {
                                    TypeText("✗ Неверный код. Замок издает предупреждающий сигнал.", ConsoleColor.Red);
                                }
                            }
                            else
                            {
                                TypeText("\nТы пытаешься взломать замок, но безуспешно.", ConsoleColor.DarkYellow);
                                TypeText("Нужно найти код или ключ.", ConsoleColor.DarkYellow);
                            }
                            break;
                        case "2":
                            TypeText("\nТы возвращаешься в коридор...", ConsoleColor.DarkYellow);
                            return;
                        case "3":
                            ShowInventory();
                            break;
                        default:
                            TypeText("Неверный ввод. Попробуй еще раз.", ConsoleColor.DarkYellow);
                            break;
                    }
                }
                else
                {
                    TypeText("\nТы в лаборатории...", ConsoleColor.DarkYellow);
                    Console.WriteLine();
                    TypeText(">>> Осмотреть лабораторию (1)", ConsoleColor.DarkYellow);
                    if (inventory.Contains(Item.Medkit))
                    {
                        TypeText(">>> Использовать аптечку (2)", ConsoleColor.DarkYellow);
                    }
                    TypeText(">>> Инвентарь (3)", ConsoleColor.DarkYellow);
                    TypeText(">>> Вернуться назад (0)", ConsoleColor.DarkYellow);

                    string choice = ActionInput();
                    switch (choice)
                    {
                        case "1":
                            TypeText("\nТы осматриваешь лабораторию...", ConsoleColor.DarkYellow);
                            TypeText("На стенах висят странные диаграммы.", ConsoleColor.DarkYellow);
                            TypeText("В углу стоит выключенное оборудование.", ConsoleColor.DarkYellow);
                            TypeText("\n(Дальше будет продолжение сюжета...)", ConsoleColor.DarkYellow);
                            break;
                        case "2":
                            if (inventory.Contains(Item.Medkit))
                            {
                                TypeText("\nТы открываешь аптечку...", ConsoleColor.DarkYellow);
                                TypeText("Ты обрабатываешь раны и чувствуешь себя лучше.", ConsoleColor.DarkYellow);
                                inventory.Remove(Item.Medkit);
                                TypeText("✓ Аптечка использована", ConsoleColor.Green);
                            }
                            else
                            {
                                TypeText("У тебя нет аптечки.", ConsoleColor.DarkYellow);
                            }
                            break;
                        case "3":
                            ShowInventory();
                            break;
                        case "0":
                            TypeText("\nТы выходишь из лаборатории...", ConsoleColor.DarkYellow);
                            return;
                        default:
                            TypeText("Неверный ввод. Попробуй еще раз.", ConsoleColor.DarkYellow);
                            break;
                    }
                }
            }
        }
    }

}
