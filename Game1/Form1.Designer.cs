namespace Game1
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreer = new System.Windows.Forms.Button();
            this.tbxIP = new System.Windows.Forms.TextBox();
            this.tbxPseudo = new System.Windows.Forms.TextBox();
            this.btnRejoindre = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.RichTextBox();
            this.game = new System.Windows.Forms.RichTextBox();
            this.btnBombe = new System.Windows.Forms.Button();
            this.btnGagner = new System.Windows.Forms.Button();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.dataPlayer = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnCreer
            // 
            this.btnCreer.Location = new System.Drawing.Point(12, 64);
            this.btnCreer.Name = "btnCreer";
            this.btnCreer.Size = new System.Drawing.Size(138, 23);
            this.btnCreer.TabIndex = 0;
            this.btnCreer.Text = "Créer";
            this.btnCreer.UseVisualStyleBackColor = true;
            this.btnCreer.Click += new System.EventHandler(this.btnCreer_Click);
            // 
            // tbxIP
            // 
            this.tbxIP.Location = new System.Drawing.Point(12, 12);
            this.tbxIP.Name = "tbxIP";
            this.tbxIP.Size = new System.Drawing.Size(260, 20);
            this.tbxIP.TabIndex = 1;
            // 
            // tbxPseudo
            // 
            this.tbxPseudo.Location = new System.Drawing.Point(12, 38);
            this.tbxPseudo.Name = "tbxPseudo";
            this.tbxPseudo.Size = new System.Drawing.Size(260, 20);
            this.tbxPseudo.TabIndex = 2;
            // 
            // btnRejoindre
            // 
            this.btnRejoindre.Location = new System.Drawing.Point(156, 64);
            this.btnRejoindre.Name = "btnRejoindre";
            this.btnRejoindre.Size = new System.Drawing.Size(116, 23);
            this.btnRejoindre.TabIndex = 3;
            this.btnRejoindre.Text = "Rejoindre";
            this.btnRejoindre.UseVisualStyleBackColor = true;
            this.btnRejoindre.Click += new System.EventHandler(this.btnRejoindre_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 93);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(138, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Visible = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // result
            // 
            this.result.Location = new System.Drawing.Point(12, 119);
            this.result.Name = "result";
            this.result.ReadOnly = true;
            this.result.Size = new System.Drawing.Size(138, 130);
            this.result.TabIndex = 6;
            this.result.Text = "";
            // 
            // game
            // 
            this.game.Location = new System.Drawing.Point(156, 119);
            this.game.Name = "game";
            this.game.Size = new System.Drawing.Size(116, 130);
            this.game.TabIndex = 7;
            this.game.Text = "";
            // 
            // btnBombe
            // 
            this.btnBombe.Location = new System.Drawing.Point(197, 255);
            this.btnBombe.Name = "btnBombe";
            this.btnBombe.Size = new System.Drawing.Size(75, 23);
            this.btnBombe.TabIndex = 8;
            this.btnBombe.Text = "BOMBE";
            this.btnBombe.UseVisualStyleBackColor = true;
            this.btnBombe.Click += new System.EventHandler(this.btnBombe_Click);
            // 
            // btnGagner
            // 
            this.btnGagner.Location = new System.Drawing.Point(116, 255);
            this.btnGagner.Name = "btnGagner";
            this.btnGagner.Size = new System.Drawing.Size(75, 23);
            this.btnGagner.TabIndex = 9;
            this.btnGagner.Text = "GAGNER";
            this.btnGagner.UseVisualStyleBackColor = true;
            this.btnGagner.Click += new System.EventHandler(this.btnGagner_Click);
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(156, 93);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(116, 23);
            this.btnQuitter.TabIndex = 10;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Visible = false;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // dataPlayer
            // 
            this.dataPlayer.Location = new System.Drawing.Point(278, 12);
            this.dataPlayer.Name = "dataPlayer";
            this.dataPlayer.Size = new System.Drawing.Size(116, 237);
            this.dataPlayer.TabIndex = 11;
            this.dataPlayer.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 292);
            this.Controls.Add(this.dataPlayer);
            this.Controls.Add(this.btnQuitter);
            this.Controls.Add(this.btnGagner);
            this.Controls.Add(this.btnBombe);
            this.Controls.Add(this.game);
            this.Controls.Add(this.result);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnRejoindre);
            this.Controls.Add(this.tbxPseudo);
            this.Controls.Add(this.tbxIP);
            this.Controls.Add(this.btnCreer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreer;
        private System.Windows.Forms.TextBox tbxIP;
        private System.Windows.Forms.TextBox tbxPseudo;
        private System.Windows.Forms.Button btnRejoindre;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.RichTextBox result;
        private System.Windows.Forms.RichTextBox game;
        private System.Windows.Forms.Button btnBombe;
        private System.Windows.Forms.Button btnGagner;
        private System.Windows.Forms.Button btnQuitter;
        private System.Windows.Forms.RichTextBox dataPlayer;
    }
}

