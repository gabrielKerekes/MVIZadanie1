using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MVIZadanie1
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

        public ButtonGroupView(List<Button> buttons)
        {
            this.buttons = buttons;
            foreach (var button in buttons)
            {
                Children.Add(button);
            }

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

        public void RemoveButton(Button button)
        {
            buttons.Remove(button);
            Children.Remove(button);
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
            // todo: change color
            button.BackgroundColor = SelectedButtonColor;
        }

        private void DeactivateButton(Button button)
        {
            // todo: change color
            button.BackgroundColor = DefaultButtonColor;
        }
    }
}
