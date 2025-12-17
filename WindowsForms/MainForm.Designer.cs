namespace NEA
{
    partial class MainForm
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
            uploadMapButton = new Button();
            mapDisplay = new PictureBox();
            resetButton = new Button();
            progressBar = new ProgressBar();
            setStartPointButton = new Button();
            setEndPointButton = new Button();
            logLabel = new Label();
            logTextBox = new TextBox();
            processImageButton = new Button();
            settingsButton = new Button();
            saveButton = new Button();
            calculateRouteButton = new Button();
            nextButton = new Button();
            setFloodFillSeedButton = new Button();
            quitButton = new Button();
            ((System.ComponentModel.ISupportInitialize)mapDisplay).BeginInit();
            SuspendLayout();
            // 
            // uploadMapButton
            // 
            uploadMapButton.Font = new Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point);
            uploadMapButton.Location = new Point(12, 40);
            uploadMapButton.Name = "uploadMapButton";
            uploadMapButton.Size = new Size(316, 63);
            uploadMapButton.TabIndex = 0;
            uploadMapButton.Text = "Upload Map";
            uploadMapButton.UseVisualStyleBackColor = true;
            uploadMapButton.Click += uploadButton_Click;
            // 
            // mapDisplay
            // 
            mapDisplay.BorderStyle = BorderStyle.FixedSingle;
            mapDisplay.Location = new Point(362, 40);
            mapDisplay.Name = "mapDisplay";
            mapDisplay.Size = new Size(1232, 910);
            mapDisplay.SizeMode = PictureBoxSizeMode.Zoom;
            mapDisplay.TabIndex = 1;
            mapDisplay.TabStop = false;
            mapDisplay.MouseClick += mapDisplay_MouseClick;
            // 
            // resetButton
            // 
            resetButton.Font = new Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point);
            resetButton.Location = new Point(20, 747);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(300, 66);
            resetButton.TabIndex = 3;
            resetButton.Text = "Reset";
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += resetButton_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(350, 956);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1244, 49);
            progressBar.TabIndex = 5;
            // 
            // setStartPointButton
            // 
            setStartPointButton.BackColor = Color.Lime;
            setStartPointButton.Font = new Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point);
            setStartPointButton.Location = new Point(12, 290);
            setStartPointButton.Name = "setStartPointButton";
            setStartPointButton.Size = new Size(308, 85);
            setStartPointButton.TabIndex = 6;
            setStartPointButton.Text = "Set Start Point";
            setStartPointButton.UseVisualStyleBackColor = false;
            setStartPointButton.Click += setStartPointButton_Click;
            // 
            // setEndPointButton
            // 
            setEndPointButton.BackColor = Color.Red;
            setEndPointButton.Font = new Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point);
            setEndPointButton.Location = new Point(20, 391);
            setEndPointButton.Name = "setEndPointButton";
            setEndPointButton.Size = new Size(300, 91);
            setEndPointButton.TabIndex = 7;
            setEndPointButton.Text = "Set End Point";
            setEndPointButton.UseVisualStyleBackColor = false;
            setEndPointButton.Click += setEndPointButton_Click;
            // 
            // logLabel
            // 
            logLabel.AutoSize = true;
            logLabel.Font = new Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point);
            logLabel.Location = new Point(1709, 0);
            logLabel.Name = "logLabel";
            logLabel.Size = new Size(88, 48);
            logLabel.TabIndex = 8;
            logLabel.Text = "LOG";
            // 
            // logTextBox
            // 
            logTextBox.Location = new Point(1600, 40);
            logTextBox.Multiline = true;
            logTextBox.Name = "logTextBox";
            logTextBox.ReadOnly = true;
            logTextBox.Size = new Size(308, 890);
            logTextBox.TabIndex = 9;
            // 
            // processImageButton
            // 
            processImageButton.Font = new Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point);
            processImageButton.Location = new Point(12, 127);
            processImageButton.Name = "processImageButton";
            processImageButton.Size = new Size(316, 62);
            processImageButton.TabIndex = 13;
            processImageButton.Text = "Process Image";
            processImageButton.UseVisualStyleBackColor = true;
            processImageButton.Click += processImageButton_Click;
            // 
            // settingsButton
            // 
            settingsButton.Font = new Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point);
            settingsButton.Location = new Point(20, 835);
            settingsButton.Name = "settingsButton";
            settingsButton.Size = new Size(300, 66);
            settingsButton.TabIndex = 14;
            settingsButton.Text = "Settings";
            settingsButton.UseVisualStyleBackColor = true;
            settingsButton.Click += settingsButton_Click;
            // 
            // saveButton
            // 
            saveButton.Font = new Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point);
            saveButton.Location = new Point(20, 668);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(300, 64);
            saveButton.TabIndex = 15;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // calculateRouteButton
            // 
            calculateRouteButton.BackColor = Color.Blue;
            calculateRouteButton.Font = new Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point);
            calculateRouteButton.ForeColor = Color.White;
            calculateRouteButton.Location = new Point(20, 506);
            calculateRouteButton.Name = "calculateRouteButton";
            calculateRouteButton.Size = new Size(300, 142);
            calculateRouteButton.TabIndex = 16;
            calculateRouteButton.Text = "Calculate Route";
            calculateRouteButton.UseVisualStyleBackColor = false;
            calculateRouteButton.Click += calculateRouteButton_Click;
            // 
            // nextButton
            // 
            nextButton.Font = new Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point);
            nextButton.Location = new Point(1600, 939);
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(308, 66);
            nextButton.TabIndex = 17;
            nextButton.Text = "Next";
            nextButton.UseVisualStyleBackColor = true;
            nextButton.Click += nextButton_Click;
            // 
            // setFloodFillSeedButton
            // 
            setFloodFillSeedButton.Font = new Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point);
            setFloodFillSeedButton.Location = new Point(12, 207);
            setFloodFillSeedButton.Name = "setFloodFillSeedButton";
            setFloodFillSeedButton.Size = new Size(332, 61);
            setFloodFillSeedButton.TabIndex = 18;
            setFloodFillSeedButton.Text = "Set Flood Fill Seed";
            setFloodFillSeedButton.UseVisualStyleBackColor = true;
            setFloodFillSeedButton.Click += setFloodFillSeedButton_Click;
            // 
            // quitButton
            // 
            quitButton.Font = new Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point);
            quitButton.Location = new Point(20, 924);
            quitButton.Name = "quitButton";
            quitButton.Size = new Size(300, 66);
            quitButton.TabIndex = 19;
            quitButton.Text = "Quit";
            quitButton.UseVisualStyleBackColor = true;
            quitButton.Click += quitButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1920, 1061);
            Controls.Add(quitButton);
            Controls.Add(setFloodFillSeedButton);
            Controls.Add(nextButton);
            Controls.Add(calculateRouteButton);
            Controls.Add(saveButton);
            Controls.Add(settingsButton);
            Controls.Add(processImageButton);
            Controls.Add(logTextBox);
            Controls.Add(logLabel);
            Controls.Add(setEndPointButton);
            Controls.Add(setStartPointButton);
            Controls.Add(progressBar);
            Controls.Add(resetButton);
            Controls.Add(mapDisplay);
            Controls.Add(uploadMapButton);
            Name = "MainForm";
            Text = "MainForm";
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)mapDisplay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button uploadMapButton;
        private PictureBox mapDisplay;
        private Button resetButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private Button setStartPointButton;
        private Button setEndPointButton;
        private Label logLabel;
        private TextBox logTextBox;
        private Button processImageButton;
        private Button settingsButton;
        private Button saveButton;
        private Button calculateRouteButton;
        private Button nextButton;
        private Button setFloodFillSeedButton;
        private Button quitButton;
    }
}