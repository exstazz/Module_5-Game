using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace game_princes
{
    class Program
    {

        static void Main()
        {
            Program program = new Program();
            program.Start_game();
        }

        int[] spawn_bomb_x = new int[10];
        int[] spawn_bomb_y = new int[10];
        int x = 0;
        int y = 0;
        int hp = 10;
        int damage;

        public void Start_game()
        {
            x = 0;
            y = 0;
            hp = 10;
            damage = 0;
            Console.Clear();
            Console.SetCursorPosition(x, y);
            Console.Write('*');
            Program program = new Program();
            Console.SetWindowSize(50, 25);//Увеличил поле, оно всё равно квадратное, но поле 10 на 10 показалось мне маленькое            
            Console.CursorVisible = false;
            MessageBox.Show("Доберитесь до противоположного конца карты. Управление на стрелки");


            program.spawn_bomb();
            program.move();
        }

        public void move()
        {
            x = 0;
            y = 0;
            while (true)
            {
                do
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.UpArrow:
                            y--;
                            check_output_card();
                            draw_player();
                            Check_win_or_lose();

                            break;
                        case ConsoleKey.DownArrow:
                            y++;
                            check_output_card();
                            draw_player();
                            check_output_card();

                            break;
                        case ConsoleKey.RightArrow:
                            x++;
                            check_output_card();
                            draw_player();
                            Check_win_or_lose();

                            break;
                        case ConsoleKey.LeftArrow:
                            x--;
                            check_output_card();
                            draw_player();
                            Check_win_or_lose();

                            break;
                    }
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);

                    }
                    Thread.Sleep(10);
                } while (true);
            }
        }

        public void draw_player()
        {
            Console.Clear();
            Console.SetCursorPosition(x, y);
            Console.Write('*');
        }

        public void set_cursor()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Process[] proc = Process.GetProcessesByName("cmd");
            proc[0].Kill();
            //лучше тестить exe так как окна взаимно заменяются
        }

        public void spawn_bomb()
        {

            Random rnd = new Random();
            for (int i = 1; i < 10; i++)
            {
                int bomb_x = rnd.Next(10);
                int bomb_y = rnd.Next(10);
                spawn_bomb_x[i] = bomb_x;
                spawn_bomb_y[i] = bomb_y;
            }
            for (int i = 0; i < spawn_bomb_x.Length; i++)
            {
                if (0 == spawn_bomb_x[i] && 0 == spawn_bomb_y[i])
                {
                    spawn_bomb_x[i] += 2;
                    spawn_bomb_y[i] += 1;
                }
                if (44 == spawn_bomb_x[i] && 24 == spawn_bomb_y[i])
                {
                    spawn_bomb_x[i] += 2;
                    spawn_bomb_y[i] += 1;
                }
            }
        }
        public void check_output_card()
        {
            if (x < 0)
                x = 0;
            else if (x >= Console.BufferWidth)
                x = Console.BufferWidth - 1;

            if (y < 0)
                y = 0;
            else if (y >= Console.BufferHeight)
                y = Console.BufferHeight - 1;
            if (x > 49)
            {
                x--;
            }
            else if (y > 24)
            {
                y--;
            }
        }
        public void Check_win_or_lose()
        {
            if (x == 49 && y == 24)
            {
                Console.Write('*');
                MessageBox.Show("Вы выйграли. Поздравляю!");
                Form1 form1 = new Form1();
                form1.ShowDialog();
            }
            Random rnd = new Random();
            for (int i = 0; i < spawn_bomb_x.Length; i++)
            {
                damage = rnd.Next(1, 10);
                if (x == spawn_bomb_x[i] && y == spawn_bomb_y[i])
                {
                    hp = hp - damage;
                    if (hp <= 0)
                    {
                        Console.Write('*');
                        MessageBox.Show("Вы проиграли. В следующий раз у вас получится!");
                        Form form1 = new Form();
                        form1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Вы наткнулись на мину, вам нанесли " + damage + " урона, а осталось " + hp);
                        spawn_bomb_x[i] = -1;//убираем мину в другое место куда нельзя пробраться
                        spawn_bomb_y[i] = -1;//убираем мину в другое место куда нельзя пробраться
                    }
                }
            }
        }
    }
}