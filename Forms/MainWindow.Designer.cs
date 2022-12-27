namespace GameTexturePackManager
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectedGameLabel = new System.Windows.Forms.Label();
            this.selectedGameComboBox = new System.Windows.Forms.ComboBox();
            this.availableTPLabel = new System.Windows.Forms.Label();
            this.AvailableTexturePacksList = new System.Windows.Forms.ListBox();
            this.selectedTexturePacksLabel = new System.Windows.Forms.Label();
            this.SelectedTexturePacksList = new System.Windows.Forms.ListBox();
            this.ApplyTexturePacksButton = new System.Windows.Forms.Button();
            this.OpenTexturePacksFolderButton = new System.Windows.Forms.Button();
            this.ConfigureGameButton = new System.Windows.Forms.Button();
            this.RefreshTexturePacksButton = new System.Windows.Forms.Button();
            this.ResetDefaultTexturePackButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectedGameLabel
            // 
            this.selectedGameLabel.AutoSize = true;
            this.selectedGameLabel.Location = new System.Drawing.Point(12, 22);
            this.selectedGameLabel.Name = "selectedGameLabel";
            this.selectedGameLabel.Size = new System.Drawing.Size(109, 20);
            this.selectedGameLabel.TabIndex = 0;
            this.selectedGameLabel.Text = "Selected Game";
            this.selectedGameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // selectedGameComboBox
            // 
            this.selectedGameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectedGameComboBox.FormattingEnabled = true;
            this.selectedGameComboBox.Location = new System.Drawing.Point(127, 19);
            this.selectedGameComboBox.Name = "selectedGameComboBox";
            this.selectedGameComboBox.Size = new System.Drawing.Size(242, 28);
            this.selectedGameComboBox.TabIndex = 1;
            // 
            // availableTPLabel
            // 
            this.availableTPLabel.AutoSize = true;
            this.availableTPLabel.Location = new System.Drawing.Point(11, 62);
            this.availableTPLabel.Name = "availableTPLabel";
            this.availableTPLabel.Size = new System.Drawing.Size(162, 20);
            this.availableTPLabel.TabIndex = 2;
            this.availableTPLabel.Text = "Available Texture Packs";
            this.availableTPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AvailableTexturePacksList
            // 
            this.AvailableTexturePacksList.FormattingEnabled = true;
            this.AvailableTexturePacksList.ItemHeight = 20;
            this.AvailableTexturePacksList.Location = new System.Drawing.Point(11, 85);
            this.AvailableTexturePacksList.Name = "AvailableTexturePacksList";
            this.AvailableTexturePacksList.Size = new System.Drawing.Size(266, 124);
            this.AvailableTexturePacksList.TabIndex = 3;
            // 
            // selectedTexturePacksLabel
            // 
            this.selectedTexturePacksLabel.AutoSize = true;
            this.selectedTexturePacksLabel.Location = new System.Drawing.Point(303, 62);
            this.selectedTexturePacksLabel.Name = "selectedTexturePacksLabel";
            this.selectedTexturePacksLabel.Size = new System.Drawing.Size(157, 20);
            this.selectedTexturePacksLabel.TabIndex = 4;
            this.selectedTexturePacksLabel.Text = "Selected Texture Packs";
            this.selectedTexturePacksLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SelectedTexturePacksList
            // 
            this.SelectedTexturePacksList.AllowDrop = true;
            this.SelectedTexturePacksList.FormattingEnabled = true;
            this.SelectedTexturePacksList.ItemHeight = 20;
            this.SelectedTexturePacksList.Location = new System.Drawing.Point(303, 85);
            this.SelectedTexturePacksList.Name = "SelectedTexturePacksList";
            this.SelectedTexturePacksList.Size = new System.Drawing.Size(266, 124);
            this.SelectedTexturePacksList.TabIndex = 5;
            // 
            // ApplyTexturePacksButton
            // 
            this.ApplyTexturePacksButton.Image = global::GameTexturePackManager.Properties.Resources.B_ConfirmButton;
            this.ApplyTexturePacksButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ApplyTexturePacksButton.Location = new System.Drawing.Point(11, 215);
            this.ApplyTexturePacksButton.Name = "ApplyTexturePacksButton";
            this.ApplyTexturePacksButton.Size = new System.Drawing.Size(266, 35);
            this.ApplyTexturePacksButton.TabIndex = 6;
            this.ApplyTexturePacksButton.Text = "Apply Texture Packs";
            this.ApplyTexturePacksButton.UseVisualStyleBackColor = true;
            // 
            // OpenTexturePacksFolderButton
            // 
            this.OpenTexturePacksFolderButton.Location = new System.Drawing.Point(303, 215);
            this.OpenTexturePacksFolderButton.Name = "OpenTexturePacksFolderButton";
            this.OpenTexturePacksFolderButton.Size = new System.Drawing.Size(266, 35);
            this.OpenTexturePacksFolderButton.TabIndex = 7;
            this.OpenTexturePacksFolderButton.Text = "Open Texture Packs Folder";
            this.OpenTexturePacksFolderButton.UseVisualStyleBackColor = true;
            // 
            // ConfigureGameButton
            // 
            this.ConfigureGameButton.Location = new System.Drawing.Point(375, 15);
            this.ConfigureGameButton.Name = "ConfigureGameButton";
            this.ConfigureGameButton.Size = new System.Drawing.Size(194, 35);
            this.ConfigureGameButton.TabIndex = 8;
            this.ConfigureGameButton.Text = "Configure Game";
            this.ConfigureGameButton.UseVisualStyleBackColor = true;
            // 
            // RefreshTexturePacksButton
            // 
            this.RefreshTexturePacksButton.Image = global::GameTexturePackManager.Properties.Resources.B_RefreshIcon;
            this.RefreshTexturePacksButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RefreshTexturePacksButton.Location = new System.Drawing.Point(11, 256);
            this.RefreshTexturePacksButton.Name = "RefreshTexturePacksButton";
            this.RefreshTexturePacksButton.Size = new System.Drawing.Size(266, 35);
            this.RefreshTexturePacksButton.TabIndex = 9;
            this.RefreshTexturePacksButton.Text = "Refresh Texture Packs";
            this.RefreshTexturePacksButton.UseVisualStyleBackColor = true;
            // 
            // ResetDefaultTexturePackButton
            // 
            this.ResetDefaultTexturePackButton.ForeColor = System.Drawing.Color.Crimson;
            this.ResetDefaultTexturePackButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ResetDefaultTexturePackButton.Location = new System.Drawing.Point(304, 256);
            this.ResetDefaultTexturePackButton.Name = "ResetDefaultTexturePackButton";
            this.ResetDefaultTexturePackButton.Size = new System.Drawing.Size(266, 35);
            this.ResetDefaultTexturePackButton.TabIndex = 10;
            this.ResetDefaultTexturePackButton.Text = "RESET DEFAULT TEXTURE PACK";
            this.ResetDefaultTexturePackButton.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 303);
            this.Controls.Add(this.ResetDefaultTexturePackButton);
            this.Controls.Add(this.RefreshTexturePacksButton);
            this.Controls.Add(this.ConfigureGameButton);
            this.Controls.Add(this.OpenTexturePacksFolderButton);
            this.Controls.Add(this.ApplyTexturePacksButton);
            this.Controls.Add(this.SelectedTexturePacksList);
            this.Controls.Add(this.selectedTexturePacksLabel);
            this.Controls.Add(this.AvailableTexturePacksList);
            this.Controls.Add(this.availableTPLabel);
            this.Controls.Add(this.selectedGameComboBox);
            this.Controls.Add(this.selectedGameLabel);
            this.MaximumSize = new System.Drawing.Size(600, 350);
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "MainWindow";
            this.Text = "Game Texture Pack Manager (v{Version})";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label selectedGameLabel;
        public ComboBox selectedGameComboBox;
        private Label availableTPLabel;
        public ListBox AvailableTexturePacksList;
        private Label selectedTexturePacksLabel;
        public ListBox SelectedTexturePacksList;
        public Button ApplyTexturePacksButton;
        public Button OpenTexturePacksFolderButton;
        public Button ConfigureGameButton;
        public Button RefreshTexturePacksButton;
        public Button ResetDefaultTexturePackButton;
    }
}