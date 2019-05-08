namespace MyTaskedProgram.MultiTaskingExample
{
    partial class MultiTaskingExample
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
            this.button3 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TV1 = new System.Windows.Forms.TreeView();
            this.TV2 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(136, 21);
            this.button3.TabIndex = 2;
            this.button3.Text = "작업 개시";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TV1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TV2);
            this.splitContainer1.Size = new System.Drawing.Size(776, 392);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 5;
            // 
            // TV1
            // 
            this.TV1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV1.Location = new System.Drawing.Point(0, 0);
            this.TV1.Name = "TV1";
            this.TV1.Size = new System.Drawing.Size(400, 392);
            this.TV1.TabIndex = 4;
            // 
            // TV2
            // 
            this.TV2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV2.Location = new System.Drawing.Point(0, 0);
            this.TV2.Name = "TV2";
            this.TV2.Size = new System.Drawing.Size(372, 392);
            this.TV2.TabIndex = 5;
            // 
            // MultiTaskingExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button3);
            this.Name = "MultiTaskingExample";
            this.Text = "MultiTaskingExample";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView TV1;
        private System.Windows.Forms.TreeView TV2;
    }
}