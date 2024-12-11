namespace WinformsApplication.Views
{
    partial class CustomerView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelFirstName = new Label();
            textBoxFirstName = new TextBox();
            textBoxLastName = new TextBox();
            labelLastName = new Label();
            textBoxGender = new TextBox();
            labelGender = new Label();
            comboBoxCustomers = new ComboBox();
            buttonAddInvoice = new Button();
            SuspendLayout();
            // 
            // labelFirstName
            // 
            labelFirstName.AutoSize = true;
            labelFirstName.Location = new Point(12, 117);
            labelFirstName.Name = "labelFirstName";
            labelFirstName.Size = new Size(80, 20);
            labelFirstName.TabIndex = 1;
            labelFirstName.Text = "First name:";
            // 
            // textBoxFirstName
            // 
            textBoxFirstName.Enabled = false;
            textBoxFirstName.Location = new Point(122, 114);
            textBoxFirstName.Name = "textBoxFirstName";
            textBoxFirstName.ReadOnly = true;
            textBoxFirstName.Size = new Size(264, 27);
            textBoxFirstName.TabIndex = 2;
            // 
            // textBoxLastName
            // 
            textBoxLastName.Enabled = false;
            textBoxLastName.Location = new Point(122, 68);
            textBoxLastName.Name = "textBoxLastName";
            textBoxLastName.ReadOnly = true;
            textBoxLastName.Size = new Size(264, 27);
            textBoxLastName.TabIndex = 4;
            // 
            // labelLastName
            // 
            labelLastName.AutoSize = true;
            labelLastName.Location = new Point(12, 71);
            labelLastName.Name = "labelLastName";
            labelLastName.Size = new Size(95, 20);
            labelLastName.TabIndex = 3;
            labelLastName.Text = "Family name:";
            // 
            // textBoxGender
            // 
            textBoxGender.Enabled = false;
            textBoxGender.Location = new Point(122, 157);
            textBoxGender.Name = "textBoxGender";
            textBoxGender.ReadOnly = true;
            textBoxGender.Size = new Size(264, 27);
            textBoxGender.TabIndex = 6;
            // 
            // labelGender
            // 
            labelGender.AutoSize = true;
            labelGender.Location = new Point(12, 160);
            labelGender.Name = "labelGender";
            labelGender.Size = new Size(60, 20);
            labelGender.TabIndex = 5;
            labelGender.Text = "Gender:";
            // 
            // comboBoxCustomers
            // 
            comboBoxCustomers.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCustomers.FormattingEnabled = true;
            comboBoxCustomers.Location = new Point(12, 12);
            comboBoxCustomers.Name = "comboBoxCustomers";
            comboBoxCustomers.Size = new Size(374, 28);
            comboBoxCustomers.TabIndex = 7;
            comboBoxCustomers.DropDown += comboBoxCustomers_DropDown;
            comboBoxCustomers.SelectedValueChanged += comboBoxCustomers_SelectedValueChanged;
            // 
            // buttonAddInvoice
            // 
            buttonAddInvoice.Enabled = false;
            buttonAddInvoice.Location = new Point(558, 404);
            buttonAddInvoice.Name = "buttonAddInvoice";
            buttonAddInvoice.Size = new Size(230, 34);
            buttonAddInvoice.TabIndex = 8;
            buttonAddInvoice.Text = "Add invoice";
            buttonAddInvoice.UseVisualStyleBackColor = true;
            buttonAddInvoice.Click += buttonAddInvoice_Click;
            // 
            // CustomerView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonAddInvoice);
            Controls.Add(comboBoxCustomers);
            Controls.Add(textBoxGender);
            Controls.Add(labelGender);
            Controls.Add(textBoxLastName);
            Controls.Add(labelLastName);
            Controls.Add(textBoxFirstName);
            Controls.Add(labelFirstName);
            Name = "CustomerView";
            Text = "CustomerView";
            Load += CustomerView_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label labelFirstName;
        private TextBox textBoxFirstName;
        private TextBox textBoxLastName;
        private Label labelLastName;
        private TextBox textBoxGender;
        private Label labelGender;
        private ComboBox comboBoxCustomers;
        private Button buttonAddInvoice;
    }
}