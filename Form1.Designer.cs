namespace MyPlateViewer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonGetPlates = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonViewDoc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1_KeyDown);
            // 
            // buttonGetPlates
            // 
            this.buttonGetPlates.Location = new System.Drawing.Point(139, 11);
            this.buttonGetPlates.Name = "buttonGetPlates";
            this.buttonGetPlates.Size = new System.Drawing.Size(75, 23);
            this.buttonGetPlates.TabIndex = 1;
            this.buttonGetPlates.Text = "Get Plates";
            this.buttonGetPlates.UseVisualStyleBackColor = true;
            this.buttonGetPlates.Click += new System.EventHandler(this.buttonGetPlates_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(13, 40);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(281, 264);
            this.listBox1.TabIndex = 3;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // buttonViewDoc
            // 
            this.buttonViewDoc.Location = new System.Drawing.Point(220, 11);
            this.buttonViewDoc.Name = "buttonViewDoc";
            this.buttonViewDoc.Size = new System.Drawing.Size(75, 23);
            this.buttonViewDoc.TabIndex = 2;
            this.buttonViewDoc.Text = "View Plates";
            this.buttonViewDoc.UseVisualStyleBackColor = true;
            this.buttonViewDoc.Click += new System.EventHandler(this.buttonViewDoc_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 322);
            this.Controls.Add(this.buttonViewDoc);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonGetPlates);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(324, 360);
            this.Name = "Form1";
            this.Text = "MyPlateViewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonGetPlates;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonViewDoc;
    }
}

