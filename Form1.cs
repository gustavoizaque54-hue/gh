using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Gx_cheat
{
    public class MainForm : Form
    {
        private Panel sidebar;
        private Panel mainPanel;
        private Button btnCombat, btnVisuals, btnExploits, btnMisc, btnConfigs;
        private PictureBox logo;

#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
        public MainForm()
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Form
            Text = "Gx Cheat";
            Size = new Size(900, 550);
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.FromArgb(18, 18, 18);
            StartPosition = FormStartPosition.CenterScreen;

#pragma warning disable CS8622 // A nulidade de tipos de referência no tipo de parâmetro não corresponde ao delegado de destino (possivelmente devido a atributos de nulidade).
            MouseDown += DragForm;
#pragma warning restore CS8622 // A nulidade de tipos de referência no tipo de parâmetro não corresponde ao delegado de destino (possivelmente devido a atributos de nulidade).

            // Sidebar
            sidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 160,
                BackColor = Color.FromArgb(25, 25, 25)
            };
            Controls.Add(sidebar);

            // Main panel
            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 30, 30)
            };
            Controls.Add(mainPanel);

            // Logo (imagem na pasta "logo")
            string logoPath = Path.Combine(
                Application.StartupPath,
                "logo",
                "Gx_cheat.png"
            );

            logo = new PictureBox
            {
                Size = new Size(120, 60),
                Location = new Point(20, 15),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile(logoPath)
            };

            sidebar.Controls.Add(logo);

            // Buttons
            btnCombat = CreateButton("Combat", 100);
            btnVisuals = CreateButton("Visuals", 150);
            btnExploits = CreateButton("Exploits", 200);
            btnMisc = CreateButton("Misc", 250);
            btnConfigs = CreateButton("Configs", 300);

            sidebar.Controls.AddRange(new Control[]
            {
                btnCombat, btnVisuals, btnExploits, btnMisc, btnConfigs
            });

            // Close button
#pragma warning disable IDE0090 // Usar 'new(...)'
            Button closeBtn = new Button
            {
                Text = "X",
                ForeColor = Color.White,
                BackColor = Color.DarkRed,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(35, 30),
                Location = new Point(Width - 45, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
#pragma warning restore IDE0090 // Usar 'new(...)'
            closeBtn.FlatAppearance.BorderSize = 0;
            closeBtn.Click += (s, e) => Close();
            Controls.Add(closeBtn);
        }

#pragma warning disable CA1822 // Marcar membros como estáticos
        private Button CreateButton(string text, int top)
#pragma warning restore CA1822 // Marcar membros como estáticos
        {
#pragma warning disable IDE0090 // Usar 'new(...)'
            Button btn = new Button
            {
                Text = text,
                Size = new Size(140, 40),
                Location = new Point(10, top),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 40, 40)
            };
#pragma warning restore IDE0090 // Usar 'new(...)'

            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(70, 0, 120);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(40, 40, 40);

            return btn;
        }

        // Drag window
        private void DragForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
#pragma warning disable CA1806 // Não ignorar resultados de métodos
                SendMessage(Handle, 0x112, 0xf012, 0);
#pragma warning restore CA1806 // Não ignorar resultados de métodos
            }
        }

        [DllImport("user32.dll")]
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' em vez de 'DllImportAttribute' para gerar código de marshalling P/Invoke no tempo de compilação
        private static extern bool ReleaseCapture();
#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' em vez de 'DllImportAttribute' para gerar código de marshalling P/Invoke no tempo de compilação

        [DllImport("user32.dll")]
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' em vez de 'DllImportAttribute' para gerar código de marshalling P/Invoke no tempo de compilação
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' em vez de 'DllImportAttribute' para gerar código de marshalling P/Invoke no tempo de compilação
    }
}