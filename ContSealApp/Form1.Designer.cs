﻿namespace ContSealApp
{
    partial class InputForm1
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
            this.inputBox = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.weightMultiplierValueBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.inputBox2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.totalContainersBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.WriteToExcel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(13, 58);
            this.inputBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputBox.Multiline = true;
            this.inputBox.Name = "inputBox";
            this.inputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.inputBox.Size = new System.Drawing.Size(215, 423);
            this.inputBox.TabIndex = 1;
            this.inputBox.Text = "CLHU3902470 22.4\r\nTCLU3172470 22.34\r\nFESU2114840 22.35\r\nFICU3552444 20.55";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(13, 533);
            this.StartButton.Margin = new System.Windows.Forms.Padding(4);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(232, 45);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Input1";
            // 
            // weightMultiplierValueBox
            // 
            this.weightMultiplierValueBox.Location = new System.Drawing.Point(236, 178);
            this.weightMultiplierValueBox.Name = "weightMultiplierValueBox";
            this.weightMultiplierValueBox.Size = new System.Drawing.Size(106, 29);
            this.weightMultiplierValueBox.TabIndex = 11;
            this.weightMultiplierValueBox.Text = "1000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(236, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Weight Multiplier";
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(586, 67);
            this.outputBox.Margin = new System.Windows.Forms.Padding(4);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputBox.Size = new System.Drawing.Size(301, 423);
            this.outputBox.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(632, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 21);
            this.label5.TabIndex = 14;
            this.label5.Text = "Cntr + Weight + Seal";
            // 
            // inputBox2
            // 
            this.inputBox2.Location = new System.Drawing.Point(350, 67);
            this.inputBox2.Margin = new System.Windows.Forms.Padding(4);
            this.inputBox2.Multiline = true;
            this.inputBox2.Name = "inputBox2";
            this.inputBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.inputBox2.Size = new System.Drawing.Size(228, 423);
            this.inputBox2.TabIndex = 18;
            this.inputBox2.Text = "CLHU3902470 S25656\r\nTCLU3172470 N54545\r\nFESU2094893 M45545\r\nFICU3552444 S66566f";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(350, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 21);
            this.label7.TabIndex = 19;
            this.label7.Text = "Input2";
            // 
            // totalContainersBox
            // 
            this.totalContainersBox.Location = new System.Drawing.Point(803, 533);
            this.totalContainersBox.Name = "totalContainersBox";
            this.totalContainersBox.Size = new System.Drawing.Size(84, 29);
            this.totalContainersBox.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(753, 536);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 21);
            this.label3.TabIndex = 21;
            this.label3.Text = "Total";
            // 
            // WriteToExcel
            // 
            this.WriteToExcel.Location = new System.Drawing.Point(350, 533);
            this.WriteToExcel.Name = "WriteToExcel";
            this.WriteToExcel.Size = new System.Drawing.Size(228, 45);
            this.WriteToExcel.TabIndex = 22;
            this.WriteToExcel.Text = "Write to File";
            this.WriteToExcel.UseVisualStyleBackColor = true;
            this.WriteToExcel.Click += new System.EventHandler(this.WriteToExcel_Click);
            // 
            // InputForm1
            // 
            this.AcceptButton = this.StartButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(924, 591);
            this.Controls.Add(this.WriteToExcel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.totalContainersBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.inputBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.weightMultiplierValueBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.inputBox);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InputForm1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ContTextApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox inputBox;
        private Button StartButton;
        private Label label1;
        private TextBox weightMultiplierValueBox;
        private Label label2;
        private TextBox outputBox;
        private Label label5;
        private TextBox inputBox2;
        private Label label7;
        private TextBox totalContainersBox;
        private Label label3;
        private Button WriteToExcel;
    }
}