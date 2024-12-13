namespace WinFormsApplication.Views
{
    partial class AddInvoiceView
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
            components = new System.ComponentModel.Container();
            textBoxCompany = new TextBox();
            labelCompanyName = new Label();
            labelVATNumber = new Label();
            textBoxNumber = new TextBox();
            labelNumber = new Label();
            textBoxStreetName = new TextBox();
            labelStreetName = new Label();
            labelException = new Label();
            comboBoxException = new ComboBox();
            dataGridViewInvoiceLines = new DataGridView();
            vATRateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            pricePerUnitDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            quantityDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            createInvoiceLineBindingSource = new BindingSource(components);
            buttonSafeInvoice = new Button();
            textBoxVATNumber = new TextBox();
            addInvoiceViewModelBindingSource = new BindingSource(components);
            customerViewModelBindingSource = new BindingSource(components);
            customerViewModelBindingSource1 = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dataGridViewInvoiceLines).BeginInit();
            ((System.ComponentModel.ISupportInitialize)createInvoiceLineBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)addInvoiceViewModelBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customerViewModelBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customerViewModelBindingSource1).BeginInit();
            SuspendLayout();
            // 
            // textBoxCompany
            // 
            textBoxCompany.Enabled = false;
            textBoxCompany.Location = new Point(134, 18);
            textBoxCompany.Name = "textBoxCompany";
            textBoxCompany.ReadOnly = true;
            textBoxCompany.Size = new Size(264, 27);
            textBoxCompany.TabIndex = 4;
            // 
            // labelCompanyName
            // 
            labelCompanyName.AutoSize = true;
            labelCompanyName.Location = new Point(12, 21);
            labelCompanyName.Name = "labelCompanyName";
            labelCompanyName.Size = new Size(116, 20);
            labelCompanyName.TabIndex = 3;
            labelCompanyName.Text = "Company name:";
            // 
            // labelVATNumber
            // 
            labelVATNumber.AutoSize = true;
            labelVATNumber.Location = new Point(418, 25);
            labelVATNumber.Name = "labelVATNumber";
            labelVATNumber.Size = new Size(92, 20);
            labelVATNumber.TabIndex = 5;
            labelVATNumber.Text = "VAT number:";
            // 
            // textBoxNumber
            // 
            textBoxNumber.Enabled = false;
            textBoxNumber.Location = new Point(516, 69);
            textBoxNumber.Name = "textBoxNumber";
            textBoxNumber.ReadOnly = true;
            textBoxNumber.Size = new Size(264, 27);
            textBoxNumber.TabIndex = 10;
            // 
            // labelNumber
            // 
            labelNumber.AutoSize = true;
            labelNumber.Location = new Point(444, 72);
            labelNumber.Name = "labelNumber";
            labelNumber.Size = new Size(66, 20);
            labelNumber.TabIndex = 9;
            labelNumber.Text = "Number:";
            // 
            // textBoxStreetName
            // 
            textBoxStreetName.Enabled = false;
            textBoxStreetName.Location = new Point(134, 69);
            textBoxStreetName.Name = "textBoxStreetName";
            textBoxStreetName.ReadOnly = true;
            textBoxStreetName.Size = new Size(264, 27);
            textBoxStreetName.TabIndex = 8;
            // 
            // labelStreetName
            // 
            labelStreetName.AutoSize = true;
            labelStreetName.Location = new Point(36, 72);
            labelStreetName.Name = "labelStreetName";
            labelStreetName.Size = new Size(92, 20);
            labelStreetName.TabIndex = 7;
            labelStreetName.Text = "Street name:";
            // 
            // labelException
            // 
            labelException.AutoSize = true;
            labelException.Location = new Point(36, 386);
            labelException.Name = "labelException";
            labelException.Size = new Size(77, 20);
            labelException.TabIndex = 11;
            labelException.Text = "Exception:";
            // 
            // comboBoxException
            // 
            comboBoxException.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxException.DropDownWidth = 540;
            comboBoxException.FormattingEnabled = true;
            comboBoxException.Location = new Point(119, 383);
            comboBoxException.Name = "comboBoxException";
            comboBoxException.Size = new Size(432, 28);
            comboBoxException.TabIndex = 12;
            // 
            // dataGridViewInvoiceLines
            // 
            dataGridViewInvoiceLines.AutoGenerateColumns = false;
            dataGridViewInvoiceLines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewInvoiceLines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewInvoiceLines.Columns.AddRange(new DataGridViewColumn[] { vATRateDataGridViewTextBoxColumn, pricePerUnitDataGridViewTextBoxColumn, quantityDataGridViewTextBoxColumn, descriptionDataGridViewTextBoxColumn });
            dataGridViewInvoiceLines.DataSource = createInvoiceLineBindingSource;
            dataGridViewInvoiceLines.Location = new Point(134, 150);
            dataGridViewInvoiceLines.Name = "dataGridViewInvoiceLines";
            dataGridViewInvoiceLines.RowHeadersWidth = 51;
            dataGridViewInvoiceLines.Size = new Size(646, 194);
            dataGridViewInvoiceLines.TabIndex = 13;
            dataGridViewInvoiceLines.DataError += dataGridViewInvoiceLines_DataError;
            // 
            // vATRateDataGridViewTextBoxColumn
            // 
            vATRateDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            vATRateDataGridViewTextBoxColumn.DataPropertyName = "VATRate";
            vATRateDataGridViewTextBoxColumn.HeaderText = "VATRate";
            vATRateDataGridViewTextBoxColumn.MinimumWidth = 6;
            vATRateDataGridViewTextBoxColumn.Name = "vATRateDataGridViewTextBoxColumn";
            vATRateDataGridViewTextBoxColumn.Width = 93;
            // 
            // pricePerUnitDataGridViewTextBoxColumn
            // 
            pricePerUnitDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            pricePerUnitDataGridViewTextBoxColumn.DataPropertyName = "PricePerUnit";
            pricePerUnitDataGridViewTextBoxColumn.HeaderText = "PricePerUnit";
            pricePerUnitDataGridViewTextBoxColumn.MinimumWidth = 6;
            pricePerUnitDataGridViewTextBoxColumn.Name = "pricePerUnitDataGridViewTextBoxColumn";
            pricePerUnitDataGridViewTextBoxColumn.Width = 117;
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            quantityDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
            quantityDataGridViewTextBoxColumn.HeaderText = "Quantity";
            quantityDataGridViewTextBoxColumn.MinimumWidth = 6;
            quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
            quantityDataGridViewTextBoxColumn.Width = 94;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            descriptionDataGridViewTextBoxColumn.MinimumWidth = 6;
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // createInvoiceLineBindingSource
            // 
            createInvoiceLineBindingSource.DataSource = typeof(Models.Input.CreateInvoiceLine);
            // 
            // buttonSafeInvoice
            // 
            buttonSafeInvoice.Enabled = false;
            buttonSafeInvoice.Location = new Point(637, 383);
            buttonSafeInvoice.Name = "buttonSafeInvoice";
            buttonSafeInvoice.Size = new Size(143, 28);
            buttonSafeInvoice.TabIndex = 14;
            buttonSafeInvoice.Text = "Safe Invoice";
            buttonSafeInvoice.UseVisualStyleBackColor = true;
            buttonSafeInvoice.Click += buttonSafeInvoice_Click;
            // 
            // textBoxVATNumber
            // 
            textBoxVATNumber.Location = new Point(516, 22);
            textBoxVATNumber.Name = "textBoxVATNumber";
            textBoxVATNumber.Size = new Size(264, 27);
            textBoxVATNumber.TabIndex = 15;
            textBoxVATNumber.TextChanged += textBoxVATNumber_TextChanged;
            // 
            // addInvoiceViewModelBindingSource
            // 
            addInvoiceViewModelBindingSource.DataSource = typeof(BlazorUI.ViewModels.AddInvoiceViewModel);
            // 
            // customerViewModelBindingSource
            // 
            customerViewModelBindingSource.DataSource = typeof(BlazorUI.ViewModels.CustomerController);
            // 
            // customerViewModelBindingSource1
            // 
            customerViewModelBindingSource1.DataSource = typeof(BlazorUI.ViewModels.CustomerController);
            // 
            // AddInvoiceView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBoxVATNumber);
            Controls.Add(buttonSafeInvoice);
            Controls.Add(dataGridViewInvoiceLines);
            Controls.Add(comboBoxException);
            Controls.Add(labelException);
            Controls.Add(textBoxNumber);
            Controls.Add(labelNumber);
            Controls.Add(textBoxStreetName);
            Controls.Add(labelStreetName);
            Controls.Add(labelVATNumber);
            Controls.Add(textBoxCompany);
            Controls.Add(labelCompanyName);
            Name = "AddInvoiceView";
            Text = "AddInvoiceView";
            Load += AddInvoiceView_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewInvoiceLines).EndInit();
            ((System.ComponentModel.ISupportInitialize)createInvoiceLineBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)addInvoiceViewModelBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)customerViewModelBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)customerViewModelBindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxCompany;
        private Label labelCompanyName;
        private Label labelVATNumber;
        private TextBox textBoxNumber;
        private Label labelNumber;
        private TextBox textBoxStreetName;
        private Label labelStreetName;
        private Label labelException;
        private ComboBox comboBoxException;
        private DataGridView dataGridViewInvoiceLines;
        private Button buttonSafeInvoice;
        private BindingSource createInvoiceLineBindingSource;
        private TextBox textBoxVATNumber;
        private BindingSource addInvoiceViewModelBindingSource;
        private BindingSource customerViewModelBindingSource;
        private BindingSource customerViewModelBindingSource1;
        private DataGridViewTextBoxColumn vATRateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn pricePerUnitDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
    }
}