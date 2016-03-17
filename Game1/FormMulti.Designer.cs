namespace Game1
{
    partial class FormMulti
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
            this.tbxPseudo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxIp = new System.Windows.Forms.TextBox();
            this.btnCreer = new System.Windows.Forms.Button();
            this.btnRetour = new System.Windows.Forms.Button();
            this.rdbHote = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbClient = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxPseudo
            // 
            this.tbxPseudo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxPseudo.Location = new System.Drawing.Point(14, 48);
            this.tbxPseudo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbxPseudo.Name = "tbxPseudo";
            this.tbxPseudo.Size = new System.Drawing.Size(261, 26);
            this.tbxPseudo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Adresse IP de l\'hote";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Open Sans Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Pseudo";
            // 
            // tbxIp
            // 
            this.tbxIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxIp.Location = new System.Drawing.Point(14, 110);
            this.tbxIp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbxIp.Name = "tbxIp";
            this.tbxIp.Size = new System.Drawing.Size(261, 26);
            this.tbxIp.TabIndex = 3;
            // 
            // btnCreer
            // 
            this.btnCreer.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnCreer.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCreer.Font = new System.Drawing.Font("Open Sans Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreer.Location = new System.Drawing.Point(152, 233);
            this.btnCreer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCreer.Name = "btnCreer";
            this.btnCreer.Size = new System.Drawing.Size(124, 43);
            this.btnCreer.TabIndex = 4;
            this.btnCreer.Text = "Valider";
            this.btnCreer.UseVisualStyleBackColor = false;
            this.btnCreer.Click += new System.EventHandler(this.btnCreer_Click);
            // 
            // btnRetour
            // 
            this.btnRetour.BackColor = System.Drawing.Color.Wheat;
            this.btnRetour.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRetour.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRetour.Font = new System.Drawing.Font("Open Sans Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetour.Location = new System.Drawing.Point(14, 233);
            this.btnRetour.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRetour.Name = "btnRetour";
            this.btnRetour.Size = new System.Drawing.Size(131, 43);
            this.btnRetour.TabIndex = 6;
            this.btnRetour.Text = "Retour";
            this.btnRetour.UseVisualStyleBackColor = false;
            this.btnRetour.Click += new System.EventHandler(this.btnRetour_Click);
            // 
            // rdbHote
            // 
            this.rdbHote.AutoSize = true;
            this.rdbHote.Checked = true;
            this.rdbHote.Location = new System.Drawing.Point(23, 35);
            this.rdbHote.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbHote.Name = "rdbHote";
            this.rdbHote.Size = new System.Drawing.Size(100, 32);
            this.rdbHote.TabIndex = 7;
            this.rdbHote.TabStop = true;
            this.rdbHote.Text = "Heberger";
            this.rdbHote.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbClient);
            this.groupBox1.Controls.Add(this.rdbHote);
            this.groupBox1.Font = new System.Drawing.Font("Open Sans Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(14, 142);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(261, 83);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type de connexion";
            // 
            // rdbClient
            // 
            this.rdbClient.AutoSize = true;
            this.rdbClient.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdbClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbClient.Location = new System.Drawing.Point(131, 35);
            this.rdbClient.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbClient.Name = "rdbClient";
            this.rdbClient.Size = new System.Drawing.Size(99, 32);
            this.rdbClient.TabIndex = 8;
            this.rdbClient.Text = "Rejoindre";
            this.rdbClient.UseVisualStyleBackColor = true;
            // 
            // FormMulti
            // 
            this.AcceptButton = this.btnCreer;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnRetour;
            this.ClientSize = new System.Drawing.Size(292, 296);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRetour);
            this.Controls.Add(this.btnCreer);
            this.Controls.Add(this.tbxIp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxPseudo);
            this.Font = new System.Drawing.Font("Open Sans Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormMulti";
            this.Text = "FormMulti";
            this.Load += new System.EventHandler(this.FormMulti_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxPseudo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxIp;
        private System.Windows.Forms.Button btnCreer;
        private System.Windows.Forms.Button btnRetour;
        private System.Windows.Forms.RadioButton rdbHote;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbClient;
    }
}