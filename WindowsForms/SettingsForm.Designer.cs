namespace NEA
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
            applyButton = new Button();
            aStarRadioButton = new RadioButton();
            DijkstraRadioButton = new RadioButton();
            pathFindingAlgorithmLabel = new Label();
            SuspendLayout();
            // 
            // applyButton
            // 
            applyButton.Location = new Point(349, 437);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(131, 62);
            applyButton.TabIndex = 1;
            applyButton.Text = "Apply";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += applyButton_Click;
            // 
            // aStarRadioButton
            // 
            aStarRadioButton.AutoSize = true;
            aStarRadioButton.Location = new Point(25, 109);
            aStarRadioButton.Name = "aStarRadioButton";
            aStarRadioButton.Size = new Size(55, 19);
            aStarRadioButton.TabIndex = 2;
            aStarRadioButton.TabStop = true;
            aStarRadioButton.Text = "A star";
            aStarRadioButton.UseVisualStyleBackColor = true;
            aStarRadioButton.CheckedChanged += aStarRadioButton_CheckedChanged;
            // 
            // DijkstraRadioButton
            // 
            DijkstraRadioButton.AutoSize = true;
            DijkstraRadioButton.Location = new Point(25, 74);
            DijkstraRadioButton.Name = "DijkstraRadioButton";
            DijkstraRadioButton.Size = new Size(64, 19);
            DijkstraRadioButton.TabIndex = 3;
            DijkstraRadioButton.TabStop = true;
            DijkstraRadioButton.Text = "Dijkstra";
            DijkstraRadioButton.UseVisualStyleBackColor = true;
            DijkstraRadioButton.CheckedChanged += DijkstraRadioButton_CheckedChanged;
            // 
            // pathFindingAlgorithmLabel
            // 
            pathFindingAlgorithmLabel.AutoSize = true;
            pathFindingAlgorithmLabel.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            pathFindingAlgorithmLabel.Location = new Point(12, 25);
            pathFindingAlgorithmLabel.Name = "pathFindingAlgorithmLabel";
            pathFindingAlgorithmLabel.Size = new Size(260, 32);
            pathFindingAlgorithmLabel.TabIndex = 4;
            pathFindingAlgorithmLabel.Text = "Path Finding Algorithm";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(502, 511);
            Controls.Add(pathFindingAlgorithmLabel);
            Controls.Add(DijkstraRadioButton);
            Controls.Add(aStarRadioButton);
            Controls.Add(applyButton);
            Name = "SettingsForm";
            Text = "Settings";
            Load += SettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button applyButton;
        private RadioButton aStarRadioButton;
        private RadioButton DijkstraRadioButton;
        private Label pathFindingAlgorithmLabel;
    }
}