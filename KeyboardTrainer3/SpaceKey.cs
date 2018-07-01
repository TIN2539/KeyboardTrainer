using System.Windows.Media;

namespace KeyboardTrainer
{
	internal class SpaceKey : KeyboardButton
	{
		public SpaceKey(int row, int column, int columnSpan, Color backgroundColor)
			: base("Space", "Space", row, column, columnSpan, backgroundColor)
		{
			TextElement.FontSize = 16.0;
		}
	}
}