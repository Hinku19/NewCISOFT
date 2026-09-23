using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CISOFTDataOperations.Objects.Menu
{
    public class DashboardCard : Panel
    {
        private Label lblIcon;
        private Label lblTitle;
        private Label lblDescription;
        private Label lblValue;

        private Color normalColor;
        private Color hoverColor;

        public DashboardCard()
        {
            InitializeCard();
            ConfigureEvents();
        }

        private void InitializeCard()
        {
            this.Width = 220;
            this.Height = 130;

            normalColor = Color.FromArgb(19, 64, 116);
            hoverColor = Color.FromArgb(19, 49, 92);

            this.BackColor = normalColor;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Cursor = Cursors.Hand;

            lblIcon = new Label();
            lblIcon.AutoSize = false;
            lblIcon.Width = 55;
            lblIcon.Height = 55;
            lblIcon.TextAlign = ContentAlignment.MiddleCenter;
            lblIcon.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblIcon.ForeColor = Color.White;
            lblIcon.Location = new Point(15, 15);
            lblIcon.BackColor = Color.Transparent;

            lblTitle = new Label();
            lblTitle.AutoSize = false;
            lblTitle.Width = 130;
            lblTitle.Height = 25;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(75, 18);
            lblTitle.BackColor = Color.Transparent;

            lblDescription = new Label();
            lblDescription.AutoSize = false;
            lblDescription.Width = 130;
            lblDescription.Height = 40;
            lblDescription.Font = new Font("Segoe UI", 8.5F);
            lblDescription.ForeColor = Color.FromArgb(238, 244, 237);
            lblDescription.Location = new Point(75, 45);
            lblDescription.BackColor = Color.Transparent;

            lblValue = new Label();
            lblValue.AutoSize = false;
            lblValue.Width = 55;
            lblValue.Height = 30;
            lblValue.TextAlign = ContentAlignment.MiddleCenter;
            lblValue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblValue.ForeColor = Color.White;
            lblValue.Location = new Point(15, 78);
            lblValue.BackColor = Color.Transparent;

            this.Controls.Add(lblIcon);
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblDescription);
            this.Controls.Add(lblValue);
        }

        private void ConfigureEvents()
        {
            this.MouseEnter += Card_MouseEnter;
            this.MouseLeave += Card_MouseLeave;

            lblIcon.MouseEnter += Card_MouseEnter;
            lblTitle.MouseEnter += Card_MouseEnter;
            lblDescription.MouseEnter += Card_MouseEnter;
            lblValue.MouseEnter += Card_MouseEnter;

            lblIcon.MouseLeave += Card_MouseLeave;
            lblTitle.MouseLeave += Card_MouseLeave;
            lblDescription.MouseLeave += Card_MouseLeave;
            lblValue.MouseLeave += Card_MouseLeave;

            lblIcon.Click += Card_Click;
            lblTitle.Click += Card_Click;
            lblDescription.Click += Card_Click;
            lblValue.Click += Card_Click;
        }

        private void Card_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = hoverColor;
        }

        private void Card_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = normalColor;
        }

        private void Card_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CardIcon
        {
            get { return lblIcon.Text; }
            set { lblIcon.Text = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CardTitle
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CardDescription
        {
            get { return lblDescription.Text; }
            set { lblDescription.Text = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CardValue
        {
            get { return lblValue.Text; }
            set { lblValue.Text = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color CardColor
        {
            get { return normalColor; }
            set
            {
                normalColor = value;
                this.BackColor = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color CardHoverColor
        {
            get { return hoverColor; }
            set { hoverColor = value; }
        }
    }
}