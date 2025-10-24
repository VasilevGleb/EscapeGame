using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

class Program
{
    // Simple enum kept for future use
    enum Item { AidKit }

    // Shared static inventory so all static methods can access it
    static List<string> inventory = new List<string>();

    static void TypeText(string text, int speed = 30, ConsoleColor color = ConsoleColor.Yellow)
    {
        var prev = Console.ForegroundColor;
        Console.ForegroundColor = color;
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(speed);
        }
        Console.WriteLine();
        Console.ForegroundColor = prev;
    }

    static void TypeTextInventar(string text, int speed = 40)
        => TypeText(text, speed, ConsoleColor.White);

    static void TypeTextDeath(string text, int speed = 25)
        => TypeText(text, speed, ConsoleColor.Red);

    static string ActionInput()
    {
        string input = Console.ReadLine();
        return input?.Trim() ?? string.Empty;
    }

    static void ShowInventory()
    {
        TypeTextInventar("==Инвентарь==");
        if (inventory.Count == 0)
        {
            TypeText(">>> Инвентарь пуст");
            return;
        }

        foreach (var it in inventory)
        {
            Console.WriteLine($"============");
            TypeTextInventar($"= {it}");

        }
    }

    static void Main()
    {
        bool haveAidKit = false;
        bool doorUnlocked = false;
        bool knwowCodeForLab = false;

        TypeText("\n... ... ...");
        TypeText("Голова болит...");

        TypeText("\n>>>Остаться лежать (1)\n>>>Открыть глаза (2)");
        string choice1 = ActionInput();
        if (choice1 == "1")
        {
            TypeText("\nТы погружаешься во тьму снова...");
            TypeTextDeath("... ... ...\nТы чувствуешь как что-то начинает есть твои ноги...");
            TypeTextDeath("\nТы умер...");
            TypeText("Нажми любую клавишу чтобы выйти...");
            Console.ReadKey(true);
            return;
        }
        else if (choice1 != "2" && choice1 != "3")
        {
            TypeText("\nНеверный ввод. Игра завершается.");
            Console.ReadKey(true);
            return;
        }

        TypeText("\nЧерез боль в голове ты открываешь глаза...");
        TypeText(".....\nТы находишься в темной комнате");

        // Room loop
        while (true)
        {
            if (!haveAidKit)
                TypeText("\n>>>Выйти из комнаты(1)\n>>>Осмотреться(2)");
            else
            {
                TypeText("\n>>>Выйти из комнаты(1)\n>>>Осмотреться(2)\n>>>Инвентарь(3)");
            }
            string choice2 = ActionInput();
            if (choice2 == "2")
            {
                if (!haveAidKit)
                {
                    TypeText("\nТы находишь аптечку");
                    inventory.Add("Аптечка");
                    haveAidKit = true;
                }
                else
                {
                    TypeText("\nТы уже взял аптечку");
                }
            }
            else if (choice2 == "1")
            {
                TypeText("\nТы выходишь из комнаты и видишь коридор освещенный аварийными огнями...");
                break;
            }
            else if (choice2 == "3" && haveAidKit)
            {
                Console.Write("\n");
                ShowInventory();
            }
            else
            {
                TypeText("\nНеверный ввод. Попробуй еще раз.");
            }
        }

        TypeText("\nНа стене напротив ты видишь табличку:");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("\n╔══════════════════════════════════════════╗");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║             [ З О Н А [07] ]             ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║        <<<Лаборатория | | Склад>>>       ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.ResetColor();

        // Corridor loop
        while (true)
        {
            TypeText(">>>Идти к лаборатории(1)\n>>>Идти к складу(2)\n>>>Инвентарь(3)\n>>>Выйти из игры(0)");
            string choice3 = ActionInput();

            if (choice3 == "2")
            {
                TypeText("\nТы идешь к складу...");
                TypeText("\n(Склад пока пуст. Вернись позже.)");
                // return to corridor choices
            }
            else if (choice3 == "1")
            {
                TypeText("\nТы идешь в лабораторию...");

                // Laboratory door handling
                while (true)
                {
                    if (!doorUnlocked)
                    {
                        TypeText("Ты видишь запертую на кодовый замок дверь...");
                        if (!knwowCodeForLab)
                        {
                            TypeText(">>>Взломать пароль(1)\n>>>Вернуться назад(2)\n>>>Инвентарь(3)");
                        }
                        else
                        {
                            TypeText(">>>Ввести пароль(1)\n>>>Вернуться назад(2)\n>>>Инвентарь(3)");
                        }
                        string choice4 = ActionInput();

                        if (choice4 == "1")
                        {
                            TypeText("\nВведи код:");
                            string codeInput = ActionInput();

                            if (codeInput == "2208")
                            {
                                TypeText("\nДверь открывается и ты заходишь в лабораторию...");
                                doorUnlocked = true;
                                // laboratory content placeholder
                                TypeText("\nВ лаборатории полупусто, но на столе что-то есть.");
                                break;
                            }
                            else
                            {
                                TypeText("\nНеверный код.");
                            }
                        }
                        else if (choice4 == "2")
                        {
                            TypeText("\nТы возвращаешься в коридор...");
                            break;
                        }
                        else if (choice4 == "3")
                        {
                            ShowInventory();
                        }
                        else
                        {
                            TypeText("\nНеверный ввод. Попробуй еще раз.");
                        }
                    }
                    else
                    {
                        TypeText("\nТы заходишь в лабораторию...");
                        // If player has medkit, offer to use it
                        if (inventory.Contains("Аптечка"))
                        {
                            TypeText("\nТы можешь использовать аптечку, чтобы привести себя в порядок. Использовать аптечку? (y/n)");
                            string use = ActionInput();
                            if (use.Equals("y", StringComparison.OrdinalIgnoreCase))
                            {
                                TypeText("\nТы использовал аптечку. Ты чувствуешь себя лучше.");
                                inventory.Remove("Аптечка");
                                haveAidKit = false;
                            }
                            else
                            {
                                TypeText("\nТы оставляешь аптечку при себе.");
                            }
                        }

                        TypeText("\n(Дальше будет расширенный сюжет...)");
                        break;
                    }
                }
            }
            else if (choice3 == "3")
            {
                ShowInventory();
            }
            else if (choice3 == "0")
            {
                TypeText("\nПожалуй самый простой выход из игры");
                TypeTextDeath("Нажми любую клавишу чтобы выйти...");
                Console.ReadKey(true);
                break;
            }
            else
            {
                TypeText("\nНеверный ввод. Попробуй еще раз.");
            }
        }
    }
}