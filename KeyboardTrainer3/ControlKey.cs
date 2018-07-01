using System.Windows.Media;

namespace KeyboardTrainer
{
	internal class ControlKey : KeyboardButton
	{
		public ControlKey(string value, int row, int column, int columnSpan)
			: base(value, value, row, column, columnSpan, Colors.LightGray)
		{
			TextElement.FontSize = 16.0;
		}
	}
}