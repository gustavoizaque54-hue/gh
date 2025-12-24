using System.Drawing;
using System.Windows.Forms;

namespace Gx_cheat
{
    public class MiscControl : UserControl
    {
        public MiscControl()
        {
            BackColor = Color.FromArgb(35, 35, 35);

            Controls.Add(new Label
            {
                Text = "Misc",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 30)
            });
        }
    }
}