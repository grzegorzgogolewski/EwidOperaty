namespace EwidOperaty
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.groupBoxConnection = new System.Windows.Forms.GroupBox();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelPass = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.labelHost = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonOracleRead = new System.Windows.Forms.Button();
            this.buttonSaveBlobToDisk = new System.Windows.Forms.Button();
            this.checkedListBoxObreby = new System.Windows.Forms.CheckedListBox();
            this.contextMenuObreby = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuObreby_ZaznaczWszystko = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuObreby_OdzaznaczWszystko = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxPodzialAdministracyjny = new System.Windows.Forms.GroupBox();
            this.checkedListBoxGminy = new System.Windows.Forms.CheckedListBox();
            this.contextMenuGminy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuGminy_ZaznaczWszystko = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuGminy_OdznaczWszystko = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSaveData = new System.Windows.Forms.Button();
            this.checkBoxZakres = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.checkBoxBezObreb = new System.Windows.Forms.CheckBox();
            this.buttonSaveWktToDisk = new System.Windows.Forms.Button();
            this.groupBoxOperacje = new System.Windows.Forms.GroupBox();
            this.groupBoxConnection.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.contextMenuObreby.SuspendLayout();
            this.groupBoxPodzialAdministracyjny.SuspendLayout();
            this.contextMenuGminy.SuspendLayout();
            this.groupBoxOperacje.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxConnection
            // 
            this.groupBoxConnection.Controls.Add(this.buttonDisconnect);
            this.groupBoxConnection.Controls.Add(this.buttonConnect);
            this.groupBoxConnection.Controls.Add(this.labelPass);
            this.groupBoxConnection.Controls.Add(this.labelUser);
            this.groupBoxConnection.Controls.Add(this.textBoxDatabase);
            this.groupBoxConnection.Controls.Add(this.labelDatabase);
            this.groupBoxConnection.Controls.Add(this.textBoxHost);
            this.groupBoxConnection.Controls.Add(this.textBoxPass);
            this.groupBoxConnection.Controls.Add(this.labelHost);
            this.groupBoxConnection.Controls.Add(this.textBoxUser);
            this.groupBoxConnection.Location = new System.Drawing.Point(13, 13);
            this.groupBoxConnection.Name = "groupBoxConnection";
            this.groupBoxConnection.Size = new System.Drawing.Size(186, 158);
            this.groupBoxConnection.TabIndex = 0;
            this.groupBoxConnection.TabStop = false;
            this.groupBoxConnection.Text = "groupBoxConnection";
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDisconnect.Location = new System.Drawing.Point(101, 123);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonDisconnect.TabIndex = 9;
            this.buttonDisconnect.Text = "buttonDisconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.ButtonDisconnect_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConnect.Location = new System.Drawing.Point(9, 123);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 8;
            this.buttonConnect.Text = "buttonConnect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.Location = new System.Drawing.Point(6, 100);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(39, 13);
            this.labelPass.TabIndex = 5;
            this.labelPass.Text = "Hasło:";
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(6, 74);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(65, 13);
            this.labelUser.TabIndex = 6;
            this.labelUser.Text = "Użytkownik:";
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(76, 45);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(100, 20);
            this.textBoxDatabase.TabIndex = 1;
            this.textBoxDatabase.Text = "textBoxDatabase";
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(6, 48);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(34, 13);
            this.labelDatabase.TabIndex = 7;
            this.labelDatabase.Text = "Baza:";
            // 
            // textBoxHost
            // 
            this.textBoxHost.Location = new System.Drawing.Point(76, 19);
            this.textBoxHost.Name = "textBoxHost";
            this.textBoxHost.Size = new System.Drawing.Size(100, 20);
            this.textBoxHost.TabIndex = 0;
            this.textBoxHost.Text = "textBoxHost";
            // 
            // textBoxPass
            // 
            this.textBoxPass.Location = new System.Drawing.Point(76, 97);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.PasswordChar = '*';
            this.textBoxPass.Size = new System.Drawing.Size(100, 20);
            this.textBoxPass.TabIndex = 3;
            this.textBoxPass.Text = "textBoxPass";
            // 
            // labelHost
            // 
            this.labelHost.AutoSize = true;
            this.labelHost.Location = new System.Drawing.Point(6, 22);
            this.labelHost.Name = "labelHost";
            this.labelHost.Size = new System.Drawing.Size(32, 13);
            this.labelHost.TabIndex = 4;
            this.labelHost.Text = "Host:";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(76, 71);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(100, 20);
            this.textBoxUser.TabIndex = 2;
            this.textBoxUser.Text = "textBoxUser";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 369);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(673, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(112, 17);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            // 
            // buttonOracleRead
            // 
            this.buttonOracleRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOracleRead.Location = new System.Drawing.Point(6, 19);
            this.buttonOracleRead.Name = "buttonOracleRead";
            this.buttonOracleRead.Size = new System.Drawing.Size(150, 23);
            this.buttonOracleRead.TabIndex = 10;
            this.buttonOracleRead.Text = "buttonOracleRead";
            this.buttonOracleRead.UseVisualStyleBackColor = true;
            this.buttonOracleRead.Click += new System.EventHandler(this.ButtonOracleRead_Click);
            // 
            // buttonSaveBlobToDisk
            // 
            this.buttonSaveBlobToDisk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveBlobToDisk.Location = new System.Drawing.Point(234, 19);
            this.buttonSaveBlobToDisk.Name = "buttonSaveBlobToDisk";
            this.buttonSaveBlobToDisk.Size = new System.Drawing.Size(150, 23);
            this.buttonSaveBlobToDisk.TabIndex = 10;
            this.buttonSaveBlobToDisk.Text = "buttonSaveBlobToDisk";
            this.buttonSaveBlobToDisk.UseVisualStyleBackColor = true;
            this.buttonSaveBlobToDisk.Click += new System.EventHandler(this.ButtonSaveBlobToDisk_Click);
            // 
            // checkedListBoxObreby
            // 
            this.checkedListBoxObreby.CheckOnClick = true;
            this.checkedListBoxObreby.ContextMenuStrip = this.contextMenuObreby;
            this.checkedListBoxObreby.FormattingEnabled = true;
            this.checkedListBoxObreby.Location = new System.Drawing.Point(234, 19);
            this.checkedListBoxObreby.Name = "checkedListBoxObreby";
            this.checkedListBoxObreby.Size = new System.Drawing.Size(222, 214);
            this.checkedListBoxObreby.TabIndex = 11;
            // 
            // contextMenuObreby
            // 
            this.contextMenuObreby.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuObreby_ZaznaczWszystko,
            this.contextMenuObreby_OdzaznaczWszystko});
            this.contextMenuObreby.Name = "contextMenuObreby";
            this.contextMenuObreby.Size = new System.Drawing.Size(170, 48);
            // 
            // contextMenuObreby_ZaznaczWszystko
            // 
            this.contextMenuObreby_ZaznaczWszystko.Name = "contextMenuObreby_ZaznaczWszystko";
            this.contextMenuObreby_ZaznaczWszystko.Size = new System.Drawing.Size(169, 22);
            this.contextMenuObreby_ZaznaczWszystko.Text = "Zaznacz wszystko";
            this.contextMenuObreby_ZaznaczWszystko.Click += new System.EventHandler(this.ContextMenuObreby_ZaznaczWszystko_Click);
            // 
            // contextMenuObreby_OdzaznaczWszystko
            // 
            this.contextMenuObreby_OdzaznaczWszystko.Name = "contextMenuObreby_OdzaznaczWszystko";
            this.contextMenuObreby_OdzaznaczWszystko.Size = new System.Drawing.Size(169, 22);
            this.contextMenuObreby_OdzaznaczWszystko.Text = "Odznacz wszystko";
            this.contextMenuObreby_OdzaznaczWszystko.Click += new System.EventHandler(this.ContextMenuObreby_OdzaznaczWszystko_Click);
            // 
            // groupBoxPodzialAdministracyjny
            // 
            this.groupBoxPodzialAdministracyjny.Controls.Add(this.checkedListBoxGminy);
            this.groupBoxPodzialAdministracyjny.Controls.Add(this.checkedListBoxObreby);
            this.groupBoxPodzialAdministracyjny.Location = new System.Drawing.Point(206, 13);
            this.groupBoxPodzialAdministracyjny.Name = "groupBoxPodzialAdministracyjny";
            this.groupBoxPodzialAdministracyjny.Size = new System.Drawing.Size(462, 243);
            this.groupBoxPodzialAdministracyjny.TabIndex = 13;
            this.groupBoxPodzialAdministracyjny.TabStop = false;
            this.groupBoxPodzialAdministracyjny.Text = "Podział administracyjny";
            // 
            // checkedListBoxGminy
            // 
            this.checkedListBoxGminy.CheckOnClick = true;
            this.checkedListBoxGminy.ContextMenuStrip = this.contextMenuGminy;
            this.checkedListBoxGminy.FormattingEnabled = true;
            this.checkedListBoxGminy.Location = new System.Drawing.Point(6, 19);
            this.checkedListBoxGminy.Name = "checkedListBoxGminy";
            this.checkedListBoxGminy.Size = new System.Drawing.Size(222, 214);
            this.checkedListBoxGminy.TabIndex = 12;
            this.checkedListBoxGminy.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedListBoxGminy_ItemCheck);
            // 
            // contextMenuGminy
            // 
            this.contextMenuGminy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuGminy_ZaznaczWszystko,
            this.contextMenuGminy_OdznaczWszystko});
            this.contextMenuGminy.Name = "contextMenuStripObreby";
            this.contextMenuGminy.Size = new System.Drawing.Size(170, 48);
            // 
            // contextMenuGminy_ZaznaczWszystko
            // 
            this.contextMenuGminy_ZaznaczWszystko.Name = "contextMenuGminy_ZaznaczWszystko";
            this.contextMenuGminy_ZaznaczWszystko.Size = new System.Drawing.Size(169, 22);
            this.contextMenuGminy_ZaznaczWszystko.Text = "Zaznacz wszystko";
            this.contextMenuGminy_ZaznaczWszystko.Click += new System.EventHandler(this.ContextMenuGminy_ZaznaczWszystko_Click);
            // 
            // contextMenuGminy_OdznaczWszystko
            // 
            this.contextMenuGminy_OdznaczWszystko.Name = "contextMenuGminy_OdznaczWszystko";
            this.contextMenuGminy_OdznaczWszystko.Size = new System.Drawing.Size(169, 22);
            this.contextMenuGminy_OdznaczWszystko.Text = "Odznacz wszystko";
            this.contextMenuGminy_OdznaczWszystko.Click += new System.EventHandler(this.ContextMenuGminy_OdznaczWszystko_Click);
            // 
            // buttonSaveData
            // 
            this.buttonSaveData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveData.Location = new System.Drawing.Point(6, 48);
            this.buttonSaveData.Name = "buttonSaveData";
            this.buttonSaveData.Size = new System.Drawing.Size(150, 23);
            this.buttonSaveData.TabIndex = 14;
            this.buttonSaveData.Text = "buttonSaveData";
            this.buttonSaveData.UseVisualStyleBackColor = true;
            this.buttonSaveData.Click += new System.EventHandler(this.ButtonSaveData_Click);
            // 
            // checkBoxZakres
            // 
            this.checkBoxZakres.AutoSize = true;
            this.checkBoxZakres.Location = new System.Drawing.Point(22, 266);
            this.checkBoxZakres.Name = "checkBoxZakres";
            this.checkBoxZakres.Size = new System.Drawing.Size(107, 17);
            this.checkBoxZakres.TabIndex = 15;
            this.checkBoxZakres.Text = "checkBoxZakres";
            this.checkBoxZakres.UseVisualStyleBackColor = true;
            this.checkBoxZakres.CheckedChanged += new System.EventHandler(this.CheckBoxZakres_CheckedChanged);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 346);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(673, 23);
            this.progressBar.TabIndex = 16;
            // 
            // checkBoxBezObreb
            // 
            this.checkBoxBezObreb.AutoSize = true;
            this.checkBoxBezObreb.Location = new System.Drawing.Point(22, 243);
            this.checkBoxBezObreb.Name = "checkBoxBezObreb";
            this.checkBoxBezObreb.Size = new System.Drawing.Size(121, 17);
            this.checkBoxBezObreb.TabIndex = 17;
            this.checkBoxBezObreb.Text = "checkBoxBezObreb";
            this.checkBoxBezObreb.UseVisualStyleBackColor = true;
            this.checkBoxBezObreb.CheckedChanged += new System.EventHandler(this.CheckBoxBezObreb_CheckedChanged);
            // 
            // buttonSaveWktToDisk
            // 
            this.buttonSaveWktToDisk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveWktToDisk.Location = new System.Drawing.Point(234, 48);
            this.buttonSaveWktToDisk.Name = "buttonSaveWktToDisk";
            this.buttonSaveWktToDisk.Size = new System.Drawing.Size(150, 23);
            this.buttonSaveWktToDisk.TabIndex = 18;
            this.buttonSaveWktToDisk.Text = "buttonSaveWktToDisk";
            this.buttonSaveWktToDisk.UseVisualStyleBackColor = true;
            this.buttonSaveWktToDisk.Click += new System.EventHandler(this.ButtonSaveWktToDisk_Click);
            // 
            // groupBoxOperacje
            // 
            this.groupBoxOperacje.Controls.Add(this.buttonOracleRead);
            this.groupBoxOperacje.Controls.Add(this.buttonSaveWktToDisk);
            this.groupBoxOperacje.Controls.Add(this.buttonSaveData);
            this.groupBoxOperacje.Controls.Add(this.buttonSaveBlobToDisk);
            this.groupBoxOperacje.Location = new System.Drawing.Point(206, 263);
            this.groupBoxOperacje.Name = "groupBoxOperacje";
            this.groupBoxOperacje.Size = new System.Drawing.Size(462, 79);
            this.groupBoxOperacje.TabIndex = 19;
            this.groupBoxOperacje.TabStop = false;
            this.groupBoxOperacje.Text = "Operacje";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 391);
            this.Controls.Add(this.groupBoxOperacje);
            this.Controls.Add(this.checkBoxBezObreb);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.checkBoxZakres);
            this.Controls.Add(this.groupBoxPodzialAdministracyjny);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBoxConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBoxConnection.ResumeLayout(false);
            this.groupBoxConnection.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuObreby.ResumeLayout(false);
            this.groupBoxPodzialAdministracyjny.ResumeLayout(false);
            this.contextMenuGminy.ResumeLayout(false);
            this.groupBoxOperacje.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxConnection;
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxHost;
        private System.Windows.Forms.Label labelHost;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Button buttonOracleRead;
        private System.Windows.Forms.Button buttonSaveBlobToDisk;
        private System.Windows.Forms.CheckedListBox checkedListBoxObreby;
        private System.Windows.Forms.ContextMenuStrip contextMenuObreby;
        private System.Windows.Forms.ToolStripMenuItem contextMenuObreby_ZaznaczWszystko;
        private System.Windows.Forms.GroupBox groupBoxPodzialAdministracyjny;
        private System.Windows.Forms.CheckedListBox checkedListBoxGminy;
        private System.Windows.Forms.ContextMenuStrip contextMenuGminy;
        private System.Windows.Forms.ToolStripMenuItem contextMenuGminy_ZaznaczWszystko;
        private System.Windows.Forms.Button buttonSaveData;
        private System.Windows.Forms.ToolStripMenuItem contextMenuGminy_OdznaczWszystko;
        private System.Windows.Forms.ToolStripMenuItem contextMenuObreby_OdzaznaczWszystko;
        private System.Windows.Forms.CheckBox checkBoxZakres;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox checkBoxBezObreb;
        private System.Windows.Forms.Button buttonSaveWktToDisk;
        private System.Windows.Forms.GroupBox groupBoxOperacje;
    }
}

