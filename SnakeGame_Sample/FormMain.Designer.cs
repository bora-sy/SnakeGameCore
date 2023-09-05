namespace SnakeGame_Sample
{
    partial class FormMain
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
            components = new System.ComponentModel.Container();
            pictureBoxMain = new PictureBox();
            buttonStart = new Button();
            timerMain = new System.Windows.Forms.Timer(components);
            labelPoints = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMain).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxMain
            // 
            pictureBoxMain.Location = new Point(12, 12);
            pictureBoxMain.Name = "pictureBoxMain";
            pictureBoxMain.Size = new Size(500, 500);
            pictureBoxMain.TabIndex = 0;
            pictureBoxMain.TabStop = false;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(12, 518);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(409, 40);
            buttonStart.TabIndex = 1;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // timerMain
            // 
            timerMain.Interval = 250;
            timerMain.Tick += timerMain_Tick;
            // 
            // labelPoints
            // 
            labelPoints.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            labelPoints.Location = new Point(427, 515);
            labelPoints.Name = "labelPoints";
            labelPoints.Size = new Size(85, 43);
            labelPoints.TabIndex = 2;
            labelPoints.Text = "0";
            labelPoints.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(526, 561);
            Controls.Add(labelPoints);
            Controls.Add(buttonStart);
            Controls.Add(pictureBoxMain);
            Name = "FormMain";
            Text = "FormMain";
            Load += FormMain_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxMain).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBoxMain;
        private Button buttonStart;
        private System.Windows.Forms.Timer timerMain;
        private Label labelPoints;
    }
}