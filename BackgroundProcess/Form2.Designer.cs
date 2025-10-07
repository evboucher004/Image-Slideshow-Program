namespace BackgroundProcess
{
    partial class Form2
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
            this.backButton = new System.Windows.Forms.Button();
            this.forwardButton = new System.Windows.Forms.Button();
            this.imgCountBox = new System.Windows.Forms.TextBox();
            this.imgCountLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.folderCountLabel = new System.Windows.Forms.Label();
            this.folderCountBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(24, 56);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(64, 23);
            this.backButton.TabIndex = 2;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // forwardButton
            // 
            this.forwardButton.Location = new System.Drawing.Point(24, 85);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(64, 23);
            this.forwardButton.TabIndex = 3;
            this.forwardButton.Text = "Forward";
            this.forwardButton.UseVisualStyleBackColor = true;
            this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
            // 
            // imgCountBox
            // 
            this.imgCountBox.Location = new System.Drawing.Point(112, 58);
            this.imgCountBox.Name = "imgCountBox";
            this.imgCountBox.Size = new System.Drawing.Size(40, 20);
            this.imgCountBox.TabIndex = 4;
            this.imgCountBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.imgCountBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.imgCountBox_KeyUp);
            // 
            // imgCountLabel
            // 
            this.imgCountLabel.AutoSize = true;
            this.imgCountLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgCountLabel.Location = new System.Drawing.Point(112, 86);
            this.imgCountLabel.Name = "imgCountLabel";
            this.imgCountLabel.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.imgCountLabel.Size = new System.Drawing.Size(40, 21);
            this.imgCountLabel.TabIndex = 5;
            this.imgCountLabel.Text = "0 / 0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Image Controls";
            // 
            // folderCountLabel
            // 
            this.folderCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.folderCountLabel.AutoSize = true;
            this.folderCountLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.folderCountLabel.Location = new System.Drawing.Point(106, 136);
            this.folderCountLabel.Name = "folderCountLabel";
            this.folderCountLabel.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.folderCountLabel.Size = new System.Drawing.Size(40, 21);
            this.folderCountLabel.TabIndex = 8;
            this.folderCountLabel.Text = "0 / 0";
            // 
            // folderCountBox
            // 
            this.folderCountBox.Location = new System.Drawing.Point(38, 136);
            this.folderCountBox.Name = "folderCountBox";
            this.folderCountBox.Size = new System.Drawing.Size(40, 20);
            this.folderCountBox.TabIndex = 7;
            this.folderCountBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.folderCountBox.TextChanged += new System.EventHandler(this.folderCountBox_TextChanged);
            this.folderCountBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.folderCountBox_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Folder Controls";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(181, 199);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.folderCountLabel);
            this.Controls.Add(this.folderCountBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.imgCountLabel);
            this.Controls.Add(this.imgCountBox);
            this.Controls.Add(this.forwardButton);
            this.Controls.Add(this.backButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button forwardButton;
        public System.Windows.Forms.TextBox imgCountBox;
        private System.Windows.Forms.Label imgCountLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label folderCountLabel;
        public System.Windows.Forms.TextBox folderCountBox;
        private System.Windows.Forms.Label label4;
    }
}