using System;
using System.Collections.Generic;
using static System.Console;

namespace TicTacToe
{

  public class Board
  {
    string[] board_parts;
    int[] locations;
    string[] items = { " ", " ", " ", " ", " ", " ", " ", " ", " " };
    string player = "o";
    string startPlayer;
    int filled = 0;
    int start = 4;
    int removed;
    int toColor;

    int[][] winPattern = new int[][]{new int[] {0, 4, 8},
                                        new int[] {0, 1, 2},
                                        new int[] {0, 3, 6},
                                        new int[] {3, 4, 5},
                                        new int[] {6, 7, 8},
                                        new int[] {1, 4, 7},
                                        new int[] {2, 5, 8},
                                        new int[] {2, 4, 6}};
    int winPoint;

    string GetWinner(string[] items)
    {
      int matched = -2;
      string ch = " ";
      for (int i = 0; i < 2; i++)
      {
        if (i == 0) ch = "x";
        else ch = "o";

        for (int x = 0; x < winPattern.Length; x++)
        {
          int y = 0;
          for (; y < winPattern[x].Length; y++)
          {
            if (items[winPattern[x][y]] == ch) matched++;
            else matched--;
          }
          if (matched == 1) { winPoint = x; break; }
          else matched = -2;
        }
        if (matched == 1) break;
        else matched = -2;

      }

      int noWinner = -items.Length;
      for (int i = 0; i < items.Length; i++) if (items[i] != " ") noWinner++;
      if (noWinner == 0) return "xo";

      if (matched < 1) ch = " ";
      return ch;
    }

    public Board()
    {
      this.board_parts = new string[]{
          "-------------\n",
          "|", " {0} ","|", " {0} ","|", " {0} ", "|\n",
          "-------------\n",
          "|", " {0} ","|", " {0} ","|", " {0} ", "|\n",
          "-------------\n",
          "|", " {0} ","|", " {0} ","|", " {0} ", "|\n",
          "-------------\n"
        };
      this.locations = new int[] { 2, 4, 6, 10, 12, 14, 18, 20, 22 };
      this.toColor = this.locations[start];
    }

    public static void DisplayTitle()
    {
      ForegroundColor = ConsoleColor.Green;
      WriteLine("===================================");
      WriteLine("===========  TicTacToe  ===========");
      WriteLine("===================================\n");
      ResetColor();
    }

    void MoveUp()
    {
      if (this.start < 0 || this.start - 3 < 0)
      {
        this.start = this.locations.Length;
      }
      this.toColor = this.locations[this.start - 3];
      this.start -= 3;
      removed = -3;
    }
    void MoveLeft()
    {
      if (this.start < 0 || this.start - 1 < 0)
      {
        this.start = this.locations.Length;
      }
      toColor = this.locations[this.start - 1];
      this.start--;
      removed = -1;
    }
    void MoveRight()
    {
      if (this.start >= this.locations.Length || this.start + 1 >= this.locations.Length)
      {
        this.start -= this.locations.Length;
      }
      this.toColor = this.locations[this.start + 1];
      this.start++;
      removed = 1;
    }
    void MoveDown()
    {
      if (this.start >= this.locations.Length || this.start + 3 >= this.locations.Length)
      {
        this.start -= this.locations.Length;
      }
      this.toColor = this.locations[this.start + 3];
      this.start += 3;
      removed = 3;
    }
    void ChangePlayer()
    {
      if (player == "o") player = "x";
      else player = "o";
    }
    void OnEnter()
    {
      if (this.board_parts[this.locations[this.start]] == " o " ||
          this.board_parts[this.locations[this.start]] == " x ") return;
      int loc = this.start;
      this.items[loc] = player;
      this.board_parts[this.locations[this.start]] = " " + player + " ";
      ChangePlayer();
      filled += 1;
    }
    public int Play()
    {
      Clear();
      DisplayTitle();
      Write("Player 1 as  [x/o] : ");
      ConsoleKeyInfo playAs = ReadKey(false);
      while (playAs.Key != ConsoleKey.X && playAs.Key != ConsoleKey.O)
      {
        playAs = ReadKey(false);
      }
      if (playAs.KeyChar == 'x' || playAs.KeyChar == 'X') player = "x";
      else if (playAs.KeyChar == 'o' || playAs.KeyChar == 'O') player = "o";
      startPlayer = player;

      DisplayBoard();
      MoveUp();
      MoveDown();
      while (filled < 9)
      {
        Clear();
        DisplayBoard();
        var key = ReadKey();
        switch (key.Key)
        {
          case ConsoleKey.UpArrow:
            MoveUp();
            break;
          case ConsoleKey.DownArrow:
            MoveDown();
            break;
          case ConsoleKey.RightArrow:
            MoveRight();
            break;
          case ConsoleKey.LeftArrow:
            MoveLeft();
            break;
          case ConsoleKey.Enter:
            OnEnter();
            break;
          default:
            break;
        }

        if (GetWinner(this.items) != " " && GetWinner(this.items) != "xo")
        {
          Clear();
          DisplayBoard(false);
          DisplayWinner();
          WriteLine(String.Format("The winner is {0}", GetPlayer(GetWinner(this.items))));
          Write("Restart game? [y/n] :");
          var restart = ReadKey(false);
          WriteLine("");
          while (restart.Key != ConsoleKey.Y && restart.Key != ConsoleKey.N) restart = ReadKey(false);
          if (restart.KeyChar == 'y' || restart.KeyChar == 'Y') return Restart();
          else if (restart.KeyChar == 'n' || restart.KeyChar == 'N') return 0;;
          break;
        }
        else if (GetWinner(items) == "xo")
        {
          Clear();
          DisplayBoard(false);
          Write("There is no winner. Restart game? [y/n] :");
          var restart = ReadKey(false);
          WriteLine("");
          while (restart.Key != ConsoleKey.Y && restart.Key != ConsoleKey.N) restart = ReadKey(true);
          if (restart.KeyChar == 'y' || restart.KeyChar == 'Y') return Restart();
          else if (restart.KeyChar == 'n' || restart.KeyChar == 'N') return 0;;
          filled = 9;
        }
      }
      return 0;
    }

    int Restart()
    {

      board_parts = new string[]{
          "-------------\n",
          "|", " {0} ","|", " {0} ","|", " {0} ", "|\n",
          "-------------\n",
          "|", " {0} ","|", " {0} ","|", " {0} ", "|\n",
          "-------------\n",
          "|", " {0} ","|", " {0} ","|", " {0} ", "|\n",
          "-------------\n"
      };
      locations = new int[] { 2, 4, 6, 10, 12, 14, 18, 20, 22 };
      items = new string[] { " ", " ", " ", " ", " ", " ", " ", " ", " " };
      player = "o";
      startPlayer = string.Empty;
      filled = 0;
      start = 4;
      removed = new int();
      toColor = new int();

      winPattern = new int[][]{new int[] {0, 4, 8},
                                        new int[] {0, 1, 2},
                                        new int[] {0, 3, 6},
                                        new int[] {3, 4, 5},
                                        new int[] {6, 7, 8},
                                        new int[] {1, 4, 7},
                                        new int[] {2, 5, 8},
                                        new int[] {2, 4, 6}};
      winPoint = new int();
      return Play();
    }

    void DisplayWinner()
    {
      int[] highlight;
      if (GetWinner(this.items) != " ") highlight = this.winPattern[this.winPoint];
      else return;
      Clear();
      for (int x = 0; x < this.board_parts.Length; x++)
      {
        int color = -1;
        for (int y = 0; y < 3; y++)
          if (x == this.locations[highlight[y]]) color = this.locations[highlight[y]];
        string toWrite = board_parts[x];
        if (toWrite == " {0} ")
        {
          string item = TicItem(x);
          toWrite = String.Format(toWrite, item);

        }
        else if (toWrite == " o ")
        {
          ForegroundColor = ConsoleColor.Red;
        }
        else if (toWrite == " x ")
        {
          ForegroundColor = ConsoleColor.Cyan;
        }
        if (x != color && color == -1)
        {
          Write(toWrite);
        }
        else if (x != 0)
        {
          BackgroundColor = ConsoleColor.DarkCyan;
          ForegroundColor = ConsoleColor.Black;
          Write(toWrite);
          ResetColor();
          Write("");
        }
        ResetColor();
      }

    }

    string GetPlayer(string ch)
    {
      if (startPlayer == ch) return "Player 1";
      else return "Player 2";
    }
    string TicItem(int x)
    {
      if (x == start + removed) return this.items[this.start];
      else return " ";
    }
    void DisplayBoard(bool paint = true)
    {
      //Clear();
      for (int x = 0; x < this.board_parts.Length; x++)
      {
        string toWrite = board_parts[x];
        if (toWrite == " {0} ")
        {
          string item = TicItem(x);
          toWrite = String.Format(toWrite, item);

        }
        else if (toWrite == " o ")
        {
          ForegroundColor = ConsoleColor.Red;
        }
        else if (toWrite == " x ")
        {
          ForegroundColor = ConsoleColor.Cyan;
        }
        if (x != toColor)
        {
          Write(toWrite);
        }
        else if (x != 0)
        {
          if (paint) BackgroundColor = ConsoleColor.Black;
          Write(toWrite);
          ResetColor();
          Write("");
        }
        ResetColor();
      }
    }
  }
  class Program
  {
    static void Main()
    {
      Clear();
      Board b = new();
      b.Play();
    }
  }
}

/*
*/