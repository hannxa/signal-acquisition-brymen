namespace signal_acquisition_brymen
{
    partial class Form1
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
            result_label = new Label();
            trackBar1 = new TrackBar();
            label2 = new Label();
            v_button = new Button();
            Ω_button = new Button();
            I_button = new Button();
            ac_button = new Button();
            feedback_label = new Label();
            csv_button = new Button();
            button1 = new Button();
            motionCanvas1 = new LiveChartsCore.SkiaSharpView.WinForms.MotionCanvas();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // result_label
            // 
            result_label.AutoSize = true;
            result_label.Location = new Point(139, 208);
            result_label.Name = "result_label";
            result_label.Size = new Size(38, 15);
            result_label.TabIndex = 0;
            result_label.Text = "wynik";
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(482, 74);
            trackBar1.Margin = new Padding(3, 2, 3, 2);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(114, 45);
            trackBar1.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(482, 42);
            label2.Name = "label2";
            label2.Size = new Size(120, 15);
            label2.TabIndex = 4;
            label2.Text = "wybierz czas pomiaru";
            // 
            // v_button
            // 
            v_button.Location = new Point(56, 58);
            v_button.Margin = new Padding(3, 2, 3, 2);
            v_button.Name = "v_button";
            v_button.Size = new Size(82, 22);
            v_button.TabIndex = 5;
            v_button.Text = "V";
            v_button.TextAlign = ContentAlignment.MiddleRight;
            v_button.UseVisualStyleBackColor = true;
            v_button.Click += v_button_Click_1;
            // 
            // Ω_button
            // 
            Ω_button.Location = new Point(193, 94);
            Ω_button.Margin = new Padding(3, 2, 3, 2);
            Ω_button.Name = "Ω_button";
            Ω_button.Size = new Size(82, 22);
            Ω_button.TabIndex = 6;
            Ω_button.Text = "Ω";
            Ω_button.UseVisualStyleBackColor = true;
            Ω_button.Click += Ω_button_Click;
            // 
            // I_button
            // 
            I_button.Location = new Point(56, 94);
            I_button.Margin = new Padding(3, 2, 3, 2);
            I_button.Name = "I_button";
            I_button.Size = new Size(82, 22);
            I_button.TabIndex = 7;
            I_button.Text = "I";
            I_button.UseVisualStyleBackColor = true;
            I_button.Click += I_button_Click;
            // 
            // ac_button
            // 
            ac_button.Location = new Point(193, 57);
            ac_button.Margin = new Padding(3, 2, 3, 2);
            ac_button.Name = "ac_button";
            ac_button.Size = new Size(82, 22);
            ac_button.TabIndex = 9;
            ac_button.Text = "~V";
            ac_button.TextAlign = ContentAlignment.MiddleRight;
            ac_button.UseVisualStyleBackColor = true;
            ac_button.Click += ac_button_Click;
            // 
            // feedback_label
            // 
            feedback_label.AutoSize = true;
            feedback_label.Location = new Point(130, 163);
            feedback_label.Name = "feedback_label";
            feedback_label.Size = new Size(55, 15);
            feedback_label.TabIndex = 10;
            feedback_label.Text = "feedback";
            // 
            // csv_button
            // 
            csv_button.Location = new Point(443, 179);
            csv_button.Margin = new Padding(3, 2, 3, 2);
            csv_button.Name = "csv_button";
            csv_button.Size = new Size(199, 22);
            csv_button.TabIndex = 11;
            csv_button.Text = "pobierz do CSV";
            csv_button.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(443, 142);
            button1.Name = "button1";
            button1.Size = new Size(199, 23);
            button1.TabIndex = 12;
            button1.Text = "start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // motionCanvas1
            // 
            motionCanvas1.Location = new Point(56, 252);
            motionCanvas1.Name = "motionCanvas1";
            motionCanvas1.Size = new Size(440, 236);
            motionCanvas1.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PeachPuff;
            ClientSize = new Size(700, 500);
            Controls.Add(motionCanvas1);
            Controls.Add(button1);
            Controls.Add(csv_button);
            Controls.Add(feedback_label);
            Controls.Add(ac_button);
            Controls.Add(I_button);
            Controls.Add(Ω_button);
            Controls.Add(v_button);
            Controls.Add(label2);
            Controls.Add(trackBar1);
            Controls.Add(result_label);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label result_label;
        private TrackBar trackBar1;
        private Label label2;
        private Button v_button;
        private Button Ω_button;
        private Button I_button;
        private Button ac_button;
        private Label feedback_label;
        private Button csv_button;
        private Button button1;
        private LiveChartsCore.SkiaSharpView.WinForms.MotionCanvas motionCanvas1;
    }
}
