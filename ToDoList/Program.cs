using System;
using System.Reflection.Metadata;

namespace ToDoList
{
	class Program
	{
		const string ContinueText = ", wciśnij dowolny klawisz by kontynuować.";

		static void PrintInColor(string text, ConsoleColor color, bool newLine = true)
		{
			Console.ForegroundColor = color;
			Console.Write(text);
			if (newLine)
				Console.WriteLine();
			Console.ResetColor();
		}

		static void DisplayList(List<Task> list)
		{
			Console.Clear();
			if (list.Count == 0)
			{
				Console.WriteLine("Lista zadań jest pusta.");
			}
			else
			{
				Console.WriteLine("Lista zadań do zrobienia:");
				for (int i = 0; i < list.Count; i++)
				{
					string[] task = list[i].ShowTask();
					Console.Write((i + 1).ToString() + "| " + task[0]);
					if (!String.IsNullOrEmpty(task[1]))
					{
						PrintInColor(" [Gotowe]", ConsoleColor.Green, false);
					}
					Console.WriteLine();
				}
			}
			Console.WriteLine();
			Console.WriteLine("[1] Dodaj zadanie\n" +
				"[2] Oznacz jako wykonane\n" +
				"[3] Usuń zadanie\n" +
				"[4] Wyjdź");
		}

		static int ValidateOption(string input, int maxValue)
		{
			int output = 0;
			if (!int.TryParse(input, out output))
			{
				PrintInColor("Nieprawidłowy numer opcji" + ContinueText, ConsoleColor.Red);
				Console.ReadKey();
				return -1;
			}
			output = int.Parse(input);
			if (output < 1|| output > maxValue)
			{
				if(output == 0)
					return -1;
				PrintInColor("Opcja o tym numerze nie istnieje" + ContinueText, ConsoleColor.Red);
				Console.ReadKey();
				return -1;
			}
			return output;
		}

		static void NullTaskDescription()
		{
			PrintInColor("Opis zadania nie może być pusty" + ContinueText, ConsoleColor.Red);
			Console.ReadKey();
		}

		static void Main(string[] args)
		{
			List<Task> list = new List<Task>();
			do
			{
				DisplayList(list);
				string inputRaw = Console.ReadLine();
				int inputOption = ValidateOption(inputRaw, 4);
				if (inputOption == -1)
					continue;
				if(inputOption == 1)
				{
					Console.WriteLine("Wpisz opis nowego zadania:");
					inputRaw = Console.ReadLine();
					if (!String.IsNullOrWhiteSpace(inputRaw))
						list.Add(new Task(inputRaw));
					else
						NullTaskDescription();
				}
				else if(inputOption == 2)
				{
					Console.WriteLine("Które zadanie z listy oznaczyć jako gotowe? Wpisz 0 by anulować.");
					inputRaw = Console.ReadLine();
					inputOption = ValidateOption(inputRaw, list.Count);
					if (inputOption != -1)
						list[inputOption - 1].SetDone();
				}
				else if(inputOption == 3)
				{
					Console.WriteLine("Które zadanie z listy usunąć? Wpisz 0 by anulować.");
					inputRaw = Console.ReadLine();
					inputOption = ValidateOption(inputRaw, list.Count);
					if(inputOption != -1)
						list.RemoveAt(inputOption - 1);
				}
				else if (inputOption == 4)
					break;
			} while (true);
		}
	}
}