namespace MyTaskedProgram
{
    partial class PythonExecutor
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
            this.TBexe = new System.Windows.Forms.TextBox();
            this.TBpy = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.LBOutput = new System.Windows.Forms.ListBox();
            this.PBoutput = new System.Windows.Forms.ProgressBar();
            this.LOutput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TBexe
            // 
            this.TBexe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBexe.Location = new System.Drawing.Point(12, 28);
            this.TBexe.Name = "TBexe";
            this.TBexe.Size = new System.Drawing.Size(776, 21);
            this.TBexe.TabIndex = 0;
            this.TBexe.Text = "C:\\ProgramData\\Anaconda3\\envs\\tensorflow\\python.exe";
            // 
            // TBpy
            // 
            this.TBpy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBpy.Location = new System.Drawing.Point(12, 67);
            this.TBpy.Name = "TBpy";
            this.TBpy.Size = new System.Drawing.Size(776, 21);
            this.TBpy.TabIndex = 1;
            this.TBpy.Text = "C:\\Users\\MCA\\CatAndDog\\CAD.py";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "파이썬 스크립트 파일";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "파이썬 실행파일";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 95);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "실행!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(96, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "중지";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // LBOutput
            // 
            this.LBOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBOutput.BackColor = System.Drawing.Color.Black;
            this.LBOutput.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.LBOutput.FormattingEnabled = true;
            this.LBOutput.ItemHeight = 22;
            this.LBOutput.Location = new System.Drawing.Point(12, 125);
            this.LBOutput.Name = "LBOutput";
            this.LBOutput.Size = new System.Drawing.Size(776, 312);
            this.LBOutput.TabIndex = 7;
            // 
            // PBoutput
            // 
            this.PBoutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PBoutput.Location = new System.Drawing.Point(178, 96);
            this.PBoutput.Name = "PBoutput";
            this.PBoutput.Size = new System.Drawing.Size(430, 23);
            this.PBoutput.TabIndex = 8;
            // 
            // LOutput
            // 
            this.LOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LOutput.AutoSize = true;
            this.LOutput.Location = new System.Drawing.Point(614, 93);
            this.LOutput.Name = "LOutput";
            this.LOutput.Size = new System.Drawing.Size(21, 12);
            this.LOutput.TabIndex = 9;
            this.LOutput.Text = "    ";
            // 
            // PythonExecutor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LOutput);
            this.Controls.Add(this.PBoutput);
            this.Controls.Add(this.LBOutput);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TBpy);
            this.Controls.Add(this.TBexe);
            this.DoubleBuffered = true;
            this.Name = "PythonExecutor";
            this.Text = "PythonExecutor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBexe;
        private System.Windows.Forms.TextBox TBpy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox LBOutput;
        private System.Windows.Forms.ProgressBar PBoutput;
        private System.Windows.Forms.Label LOutput;
    }
}