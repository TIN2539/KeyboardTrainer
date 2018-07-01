using System.Windows.Media;

namespace KeyboardTrainer
{
	internal class SpecialCharKey : KeyboardButton
	{
		public SpecialCharKey(string regularValue, string shiftValue, int row, int column, int columnSpan, Color backgroundColor)
			: base(regularValue, shiftValue, row, column, columnSpan, backgroundColor)
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