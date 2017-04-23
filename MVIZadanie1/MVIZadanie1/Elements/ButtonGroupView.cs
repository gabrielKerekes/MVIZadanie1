using System.Collections.Generic;
using Xamarin.Forms;

namespace MVIZadanie1.Elements
{
    public class ButtonGroupView : StackLayout
    {
        private static readonly Color SelectedButtonColor = Color.FromRgb(180, 180, 180);
        private static readonly Color DefaultButtonColor = Color.FromRgb(240, 240, 240);

        private List<Button> buttons;
        private int selectedIndex = -1;

        public ButtonGroupView()
        {
            buttons = new List<Button>();
            Init();
        }

        private void Init()
        {
            Orientation = StackOrientation.Vertical;
        }

        public void AddButton(Button button)
        {
            buttons.Add(button);
            Children.Add(button);

            DeactivateButton(button);
        }

        public void SelectButton(Button button)
        {
            selectedIndex = buttons.FindIndex(b => b == button);

            for (var i = 0; i < buttons.Count; i++)
            {
                if (i == selectedIndex)
                    continue;

                DeactivateButton(buttons[i]);
            }

            ActivateButton(button);
        }

        public void UnselectButton(Button button)
        {
            DeactivateButton(button);
            selectedIndex = -1;
        }

        private void ActivateButton(Button button)
        {
            button.BackgroundColor = SelectedButtonColor;
        }

        private void DeactivateButton(Button button)
        {
            button.BackgroundColor = DefaultButtonColor;
        }
    }
}
