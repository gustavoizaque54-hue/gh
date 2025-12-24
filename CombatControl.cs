using System.Drawing;
using System.Windows.Forms;

namespace Gx_cheat
{
    public class CombatControl : UserControl
    {
        public CombatControl()
        {
            BackColor = Color.FromArgb(35, 35, 35);

#pragma warning disable IDE0090 // Usar 'new(...)'
            Label lbl = new Label
            {
                Text = "Combat",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 30)
            };
#pragma warning restore IDE0090 // Usar 'new(...)'

            Controls.Add(lbl);
        }
    }
}