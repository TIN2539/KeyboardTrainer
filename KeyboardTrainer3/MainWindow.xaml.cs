using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace KeyboardTrainer
{
	public partial class MainWindow : Window
	{
		private const int printableChars = 47;
		private const int sourceTextLength = 67;
		private Dictionary<Key, KeyboardButton> allKeyboardButtons;
		private int correctlyTypedTextLength;
		private int fails;
		private Random rand;
		private DateTime startTime;

		public MainWindow()
		{
			InitializeComponent();
			rand = new Random(DateTime.Now.Millisecond);
			allKeyboardButtons = new Dictionary<Key, KeyboardButton>
			{
				[Key.Oem3] = new SpecialCharKey("`", "~", 0, 0, 2, Colors.Pink),
				[Key.D1] = new DigitKey("1", "!", 0, 2, Colors.Pink),
				[Key.D2] = new DigitKey("2", "@", 0, 4, Colors.Pink),
				[Key.D3] = new DigitKey("3", "#", 0, 6, Colors.Yellow),
				[Key.D4] = new DigitKey("4", "$", 0, 8, Colors.LawnGreen),
				[Key.D5] = new DigitKey("5", "%", 0, 10, Colors.DeepSkyBlue),
				[Key.D6] = new DigitKey("6", "^", 0, 12, Colors.DeepSkyBlue),
				[Key.D7] = new DigitKey("7", "&", 0, 14, Colors.MediumVioletRed),
				[Key.D8] = new DigitKey("8", "*", 0, 16, Colors.MediumVioletRed),
				[Key.D9] = new DigitKey("9", "(", 0, 18, Colors.Pink),
				[Key.D0] = new DigitKey("0", ")", 0, 20, Colors.Yellow),
				[Key.OemMinus] = new SpecialCharKey("-", "_", 0, 22, 2, Colors.LawnGreen),
				[Key.OemPlus] = new SpecialCharKey("=", "+", 0, 24, 2, Colors.LawnGreen),
				[Key.Back] = new ControlKey("Backspace", 0, 26, 4),
				[Key.Tab] = new ControlKey("Tab", 1, 0, 3),
				[Key.Q] = new LetterKey("Q", 1, 3, Colors.Pink),
				[Key.W] = new LetterKey("W", 1, 5, Colors.Yellow),
				[Key.E] = new LetterKey("E", 1, 7, Colors.LawnGreen),
				[Key.R] = new LetterKey("R", 1, 9, Colors.DeepSkyBlue),
				[Key.T] = new LetterKey("T", 1, 11, Colors.DeepSkyBlue),
				[Key.Y] = new LetterKey("Y", 1, 13, Colors.MediumVioletRed),
				[Key.U] = new LetterKey("U", 1, 15, Colors.MediumVioletRed),
				[Key.I] = new LetterKey("I", 1, 17, Colors.Pink),
				[Key.O] = new LetterKey("O", 1, 19, Colors.Yellow),
				[Key.P] = new LetterKey("p", 1, 21, Colors.LawnGreen),
				[Key.OemOpenBrackets] = new SpecialCharKey("[", "{", 1, 23, 2, Colors.LawnGreen),
				[Key.OemCloseBrackets] = new SpecialCharKey("]", "}", 1, 25, 2, Colors.LawnGreen),
				[Key.Oem5] = new SpecialCharKey("\\", "|", 1, 27, 3, Colors.LawnGreen),
				[Key.CapsLock] = new ControlKey("Caps Lock", 2, 0, 4),
				[Key.A] = new LetterKey("A", 2, 4, Colors.Pink),
				[Key.S] = new LetterKey("S", 2, 6, Colors.Yellow),
				[Key.D] = new LetterKey("D", 2, 8, Colors.LawnGreen),
				[Key.F] = new LetterKey("F", 2, 10, Colors.DeepSkyBlue),
				[Key.G] = new LetterKey("G", 2, 12, Colors.DeepSkyBlue),
				[Key.H] = new LetterKey("H", 2, 14, Colors.MediumVioletRed),
				[Key.J] = new LetterKey("J", 2, 16, Colors.MediumVioletRed),
				[Key.K] = new LetterKey("K", 2, 18, Colors.Pink),
				[Key.L] = new LetterKey("L", 2, 20, Colors.Yellow),
				[Key.OemSemicolon] = new SpecialCharKey(";", ":", 2, 22, 2, Colors.LawnGreen),
				[Key.OemQuotes] = new SpecialCharKey("'", "\"", 2, 24, 2, Colors.LawnGreen),
				[Key.Enter] = new ControlKey("Enter", 2, 26, 4),
				[Key.LeftShift] = new ControlKey("Shift", 3, 0, 5),
				[Key.Z] = new LetterKey("Z", 3, 5, Colors.Pink),
				[Key.X] = new LetterKey("X", 3, 7, Colors.Yellow),
				[Key.C] = new LetterKey("C", 3, 9, Colors.LawnGreen),
				[Key.V] = new LetterKey("V", 3, 11, Colors.DeepSkyBlue),
				[Key.B] = new LetterKey("B", 3, 13, Colors.DeepSkyBlue),
				[Key.N] = new LetterKey("N", 3, 15, Colors.MediumVioletRed),
				[Key.M] = new LetterKey("M", 3, 17, Colors.MediumVioletRed),
				[Key.OemComma] = new SpecialCharKey(",", "<", 3, 19, 2, Colors.Pink),
				[Key.OemPeriod] = new SpecialCharKey(".", ">", 3, 21, 2, Colors.Yellow),
				[Key.OemQuestion] = new SpecialCharKey("/", "?", 3, 23, 2, Colors.LawnGreen),
				[Key.RightShift] = new ControlKey("Shift", 3, 25, 5),
				[Key.LeftCtrl] = new ControlKey("Ctrl", 4, 0, 3),
				[Key.LWin] = new ControlKey("Win", 4, 3, 3),
				[Key.LeftAlt] = new ControlKey("Alt", 4, 6, 3),
				[Key.Space] = new SpaceKey(4, 9, 12, Colors.Orange),
				[Key.RightAlt] = new ControlKey("Alt", 4, 21, 3),
				[Key.RWin] = new ControlKey("Win", 4, 24, 3),
				[Key.RightCtrl] = new ControlKey("Ctrl", 4, 27, 3)
			};

			foreach (KeyboardButton keyboardButton in allKeyboardButtons.Values)
				keyboardGrid.Children.Add(keyboardButton.GridElement);
		}

		private void BtnStart_Click(object sender, RoutedEventArgs e)
		{
			btnStop.IsEnabled = true;
			btnStart.IsEnabled = false;
			cbCaseSensitive.IsEnabled = false;
			sliderDifficulty.IsEnabled = false;
			tbTypedText.Text = "";
			startTime = DateTime.Now;
			correctlyTypedTextLength = 0;
			fails = 0;
			tbFails.Text = "0";
			tbSpeed.Text = "0";

			int difficulty;
			try
			{
				difficulty = int.Parse(tbDifficulty.Text);

				if (difficulty > printableChars)
					difficulty = printableChars;
				else if (difficulty < 2)
					difficulty = 2;
			}
			catch (FormatException)
			{
				difficulty = 12;
				tbDifficulty.Text = "12";
			}
			tbDifficulty.Text = difficulty.ToString();
			tbGeneratedText.Text = GenerateText(difficulty);
			tbTypedText.Focus();
		}

		private void BtnStop_Click(object sender, RoutedEventArgs e)
		{
			Stop();
		}

		private string GenerateText(int difficulty)
		{
			List<KeyboardButton> charsForTextGeneration = allKeyboardButtons.Values.OfType<LetterKey>().ToList<KeyboardButton>();

			List<char> randomChars = new List<char>();
			int randomIndex;

			for (int i = 0; i < difficulty; ++i)
			{
				randomIndex = rand.Next(charsForTextGeneration.Count);
				randomChars.Add(charsForTextGeneration.ElementAt(randomIndex).Value[0]);
				if (cbCaseSensitive.IsChecked == true)
					randomChars.Add(charsForTextGeneration.ElementAt(randomIndex).ShiftValue[0]);
			}

			for (int i = 5; i <= difficulty; i += 5)
			{
				randomChars.Add(' ');
				if (cbCaseSensitive.IsChecked == true)
					randomChars.Add(' ');
			}

			string result = "";
			for (int i = 0; i < sourceTextLength; ++i)
			{
				result += randomChars[rand.Next(randomChars.Count)];
			}
			return result;
		}

		private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (!allKeyboardButtons.ContainsKey(e.Key))
				return;

			allKeyboardButtons[e.Key].GridElement.Effect = new DropShadowEffect();

			if (e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.CapsLock)
			{
				RefreshKeyboard();
				return;
			}

			if (btnStart.IsEnabled)
				return;

			if (allKeyboardButtons[e.Key] is ControlKey)
				return;

			e.Handled = true;


			if (e.Key == Key.Space)
			{
				tbTypedText.AppendText(" ");
			}
			else if (allKeyboardButtons[e.Key] is LetterKey || allKeyboardButtons[e.Key] is SpecialCharKey)
			{
				tbTypedText.AppendText(allKeyboardButtons[e.Key].TextElement.Text);
			}

			if (tbGeneratedText.Text[correctlyTypedTextLength] == tbTypedText.Text[correctlyTypedTextLength])
			{
				correctlyTypedTextLength++;
			}

			if (tbTypedText.Text.Length <= tbGeneratedText.Text.Length)
			{
				if (tbGeneratedText.Text[tbTypedText.Text.Length - 1] != tbTypedText.Text[tbTypedText.Text.Length - 1])
				{
					fails++;
					tbTypedText.Text = tbTypedText.Text.Substring(0, tbTypedText.Text.Length - 1);
				}
			}

			tbFails.Text = fails.ToString();
			tbSpeed.Text = Math.Round(correctlyTypedTextLength / (DateTime.Now - startTime).TotalMinutes).ToString();
			tbTypedText.Select(0, correctlyTypedTextLength);

			if (correctlyTypedTextLength == tbGeneratedText.Text.Length)
			{
				Stop();
				MessageBox.Show("Congrats! You have successfully typed the whole text", "Finished", MessageBoxButton.OK);
				RefreshKeyboard();
			}
		}

		private void MainWindow_PreviewKeyUp(object sender, KeyEventArgs e)
		{
			if (!allKeyboardButtons.ContainsKey(e.Key))
				return;

			allKeyboardButtons[e.Key].GridElement.Effect = null;

			if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
				RefreshKeyboard();

			e.Handled = true;
		}

		private void RefreshKeyboard()
		{
			bool shiftIsOn = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
			bool capsIsOn = Keyboard.IsKeyToggled(Key.CapsLock);
			foreach (KeyboardButton keyboardButton in allKeyboardButtons.Values)
			{
				keyboardButton.RefreshText(shiftIsOn, capsIsOn);
				keyboardButton.GridElement.Effect = null;
			}
		}

		private void SliderDifficulty_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			tbDifficulty.Text = Math.Round(sliderDifficulty.Value).ToString();
		}

		private void Stop()
		{
			btnStop.IsEnabled = false;
			btnStart.IsEnabled = true;
			cbCaseSensitive.IsEnabled = true;
			sliderDifficulty.IsEnabled = true;
		}
	}
}