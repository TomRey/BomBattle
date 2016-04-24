namespace Game1
{
    partial class FormSession
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
            this.lblJoueur = new System.Windows.Forms.Label();
            this.tbxJoueur = new System.Windows.Forms.RichTextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnBackToConnection = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblJoueur
            // 
            this.lblJoueur.Font = new System.Drawing.Font("Open Sans Light", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJoueur.Location = new System.Drawing.Point(12, 10);
            this.lblJoueur.Name = "lblJoueur";
            this.lblJoueur.Size = new System.Drawing.Size(265, 34);
            this.lblJoueur.TabIndex = 7;
            this.lblJoueur.Text = "NOMBRE DE JOUEUR : 0 / 8";
            this.lblJoueur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbxJoueur
            // 
            this.tbxJoueur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxJoueur.Location = new System.Drawing.Point(12, 47);
            this.tbxJoueur.Name = "tbxJoueur";
            this.tbxJoueur.Size = new System.Drawing.Size(265, 177);
            this.tbxJoueur.TabIndex = 6;
            this.tbxJoueur.Text = "";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.LightBlue;
            this.btnStart.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Open Sans Light", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(151, 230);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(126, 41);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnBackToConnection
            // 
            this.btnBackToConnection.BackColor = System.Drawing.Color.Moccasin;
            this.btnBackToConnection.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBackToConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackToConnection.Font = new System.Drawing.Font("Open Sans Light", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackToConnection.Location = new System.Drawing.Point(12, 230);
            this.btnBackToConnection.Name = "btnBackToConnection";
            this.btnBackToConnection.Size = new System.Drawing.Size(133, 41);
            this.btnBackToConnection.TabIndex = 4;
            this.btnBackToConnection.Text = "RETOUR";
            this.btnBackToConnection.UseVisualStyleBackColor = false;
            this.btnBackToConnection.Click += new System.EventHandler(this.btnBackToConnection_Click);
            // 
            // FormSession
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(290, 289);
            this.ControlBox = false;
            this.Controls.Add(this.lblJoueur);
            this.Controls.Add(this.tbxJoueur);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnBackToConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSession";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSession";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblJoueur;
        private System.Windows.Forms.RichTextBox tbxJoueur;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnBackToConnection;
    }
}