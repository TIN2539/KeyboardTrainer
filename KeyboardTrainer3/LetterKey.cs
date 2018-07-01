using System.Windows.Media;

namespace KeyboardTrainer
{
	internal class LetterKey : KeyboardButton
	{
		public LetterKey(string value, int row, int column, Color backgroundColor)
			: base(value.ToLower(), value.ToUpper(), row, column, 2, backgroundColor)
		{ }

		public override void RefreshText(bool shiftIsOn, bool capsIsOn)
		{
			if (shiftIsOn ^ capsIsOn)
			{
				TextElement.Text = ShiftValue;
			}
			else
			{
				TextElement.Text = Value;
			}
		}
	}
}