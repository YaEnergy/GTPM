namespace GameTexturePackManager
{
    partial class AddGameForm
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
            this.AllowedFileExtensionsCheckList = new System.Windows.Forms.CheckedListBox();
            this.ContentFolderPathTextBox = new System.Windows.Forms.TextBox();
            this.fileExtensionsChecklistLabel = new System.Windows.Forms.Label();
            this.contentFolderPathLabel = new System.Windows.Forms.Label();
            this.gameNameLabel = new System.Windows.Forms.Label();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.AddGameButton = new System.Windows.Forms.Button();
            this.GameNameTextBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // AllowedFileExtensionsCheckList
            // 
            this.AllowedFileExtensionsCheckList.CheckOnClick = true;
            this.AllowedFileExtensionsCheckList.FormattingEnabled = true;
            this.AllowedFileExtensionsCheckList.Location = new System.Drawing.Point(12, 153);
            this.AllowedFileExtensionsCheckList.Name = "AllowedFileExtensionsCheckList";
            this.AllowedFileExtensionsCheckList.Size = new System.Drawing.Size(286, 114);
            this.AllowedFileExtensionsCheckList.TabIndex = 0;
            // 
            // ContentFolderPathTextBox
            // 
            this.ContentFolderPathTextBox.Location = new System.Drawing.Point(12, 98);
            this.ContentFolderPathTextBox.Name = "ContentFolderPathTextBox";
            this.ContentFolderPathTextBox.ReadOnly = true;
            this.ContentFolderPathTextBox.Size = new System.Drawing.Size(164, 27);
            this.ContentFolderPathTextBox.TabIndex = 1;
            // 
            // fileExtensionsChecklistLabel
            // 
            this.fileExtensionsChecklistLabel.AutoSize = true;
            this.fileExtensionsChecklistLabel.Location = new System.Drawing.Point(12, 130);
            this.fileExtensionsChecklistLabel.Name = "fileExtensionsChecklistLabel";
            this.fileExtensionsChecklistLabel.Size = new System.Drawing.Size(132, 20);
            this.fileExtensionsChecklistLabel.TabIndex = 2;
            this.fileExtensionsChecklistLabel.Text = "Allowed File Types";
            // 
            // contentFolderPathLabel
            // 
            this.contentFolderPathLabel.AutoSize = true;
            this.contentFolderPathLabel.Location = new System.Drawing.Point(12, 75);
            this.contentFolderPathLabel.Name = "contentFolderPathLabel";
            this.contentFolderPathLabel.Size = new System.Drawing.Size(139, 20);
            this.contentFolderPathLabel.TabIndex = 3;
            this.contentFolderPathLabel.Text = "Content Folder Path";
            // 
            // gameNameLabel
            // 
            this.gameNameLabel.AutoSize = true;
            this.gameNameLabel.Location = new System.Drawing.Point(12, 15);
            this.gameNameLabel.Name = "gameNameLabel";
            this.gameNameLabel.Size = new System.Drawing.Size(92, 20);
            this.gameNameLabel.TabIndex = 5;
            this.gameNameLabel.Text = "Game Name";
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Location = new System.Drawing.Point(182, 96);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(116, 29);
            this.SelectFolderButton.TabIndex = 6;
            this.SelectFolderButton.Text = "Select Folder";
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // AddGameButton
            // 
            this.AddGameButton.Location = new System.Drawing.Point(12, 274);
            this.AddGameButton.Name = "AddGameButton";
            this.AddGameButton.Size = new System.Drawing.Size(286, 29);
            this.AddGameButton.TabIndex = 7;
            this.AddGameButton.Text = "Add Custom Game";
            this.AddGameButton.UseVisualStyleBackColor = true;
            // 
            // GameNameTextBox
            // 
            this.GameNameTextBox.FormattingEnabled = true;
            this.GameNameTextBox.Location = new System.Drawing.Point(12, 37);
            this.GameNameTextBox.Name = "GameNameTextBox";
            this.GameNameTextBox.Size = new System.Drawing.Size(286, 28);
            this.GameNameTextBox.TabIndex = 8;
            // 
            // AddGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 315);
            this.Controls.Add(this.GameNameTextBox);
            this.Controls.Add(this.AddGameButton);
            this.Controls.Add(this.SelectFolderButton);
            this.Controls.Add(this.gameNameLabel);
            this.Controls.Add(this.contentFolderPathLabel);
            this.Controls.Add(this.fileExtensionsChecklistLabel);
            this.Controls.Add(this.ContentFolderPathTextBox);
            this.Controls.Add(this.AllowedFileExtensionsCheckList);
            this.MaximumSize = new System.Drawing.Size(328, 362);
            this.MinimumSize = new System.Drawing.Size(328, 362);
            this.Name = "AddGameForm";
            this.Text = "Add Custom Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public TextBox ContentFolderPathTextBox;
        private Label fileExtensionsChecklistLabel;
        private Label contentFolderPathLabel;
        public CheckedListBox AllowedFileExtensionsCheckList;
        private Label gameNameLabel;
        public Button SelectFolderButton;
        public Button AddGameButton;
        public ComboBox GameNameTextBox;
    }
}