using System.Windows.Media;

namespace KeyboardTrainer
{
	internal class DigitKey : KeyboardButton
	{
		public DigitKey(string regularValue, string shiftValue, int row, int column, Color backgroundColor)
			: base(regularValue, shiftValue, row, column, 2, backgroundColor)
		{ }

		public override void RefreshText(bool shiftIsOn, bool capsIsOn)
		{
			if (shiftIsOn)
				TextElement.Text = ShiftValue;
			else
				TextElement.Text = Value;
		}
	}
}