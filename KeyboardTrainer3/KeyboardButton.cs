using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KeyboardTrainer
{
	internal class KeyboardButton
	{
		public KeyboardButton(string regularValue, string shiftValue, int row, int column, int columnSpan, Color backgroundColor)
		{
			Value = regularValue;
			ShiftValue = shiftValue;

			Border border = new Border
			{
				Margin = new Thickness(2.0),
				BorderBrush = new SolidColorBrush(Colors.Black),
				BorderThickness = new Thickness(1.5),
				Background = new SolidColorBrush(backgroundColor),
				CornerRadius = new CornerRadius(7.0)
			};

			TextBlock textBlock = new TextBlock
			{
				Text = regularValue,
				FontSize = 24.0,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				Margin = new Thickness(0, -3, 0, 0)
			};

			border.Child = textBlock;
			Grid.SetRow(border, row);
			Grid.SetColumn(border, column);
			Grid.SetColumnSpan(border, columnSpan);

			TextElement = textBlock;
			GridElement = border;
		}

		public UIElement GridElement { get; private set; }

		public string ShiftValue { get; private set; }

		public TextBlock TextElement { get; private set; }

		public string Value { get; private set; }

		public virtual void RefreshText(bool shiftIsOn, bool capsIsOn)
		{ }
	}
}