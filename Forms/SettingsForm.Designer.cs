namespace GameTexturePackManager
{
    partial class SettingsForm
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
            this.LanguageComboBox = new System.Windows.Forms.ComboBox();
            this.languageLabel = new System.Windows.Forms.Label();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LanguageComboBox
            // 
            this.LanguageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageComboBox.FormattingEnabled = true;
            this.LanguageComboBox.Location = new System.Drawing.Point(12, 32);
            this.LanguageComboBox.Name = "LanguageComboBox";
            this.LanguageComboBox.Size = new System.Drawing.Size(358, 28);
            this.LanguageComboBox.TabIndex = 3;
            // 
            // languageLabel
            // 
            this.languageLabel.AutoSize = true;
            this.languageLabel.Location = new System.Drawing.Point(12, 9);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(74, 20);
            this.languageLabel.TabIndex = 2;
            this.languageLabel.Text = "Language";
            this.languageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ApplyButton
            // 
            this.ApplyButton.Image = global::GameTexturePackManager.Properties.Resources.B_ConfirmButton;
            this.ApplyButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ApplyButton.Location = new System.Drawing.Point(12, 146);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ApplyButton.Size = new System.Drawing.Size(358, 35);
            this.ApplyButton.TabIndex = 8;
            this.ApplyButton.Text = "Apply Settings";
            this.ApplyButton.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 193);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.LanguageComboBox);
            this.Controls.Add(this.languageLabel);
            this.MaximumSize = new System.Drawing.Size(400, 240);
            this.MinimumSize = new System.Drawing.Size(400, 240);
            this.Name = "SettingsForm";
            this.Text = "GTPM Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ComboBox LanguageComboBox;
        private Label languageLabel;
        public Button ApplyButton;
    }
}