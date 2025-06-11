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
            capacitance_button = new Button();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            button1 = new Button();
            motionCanvas1 = new LiveChartsCore.SkiaSharpView.WinForms.MotionCanvas();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // result_label
            // 
            result_label.AutoSize = true;
            result_label.Location = new Point(159, 278);
            result_label.Name = "result_label";
            result_label.Size = new Size(46, 20);
            result_label.TabIndex = 0;
            result_label.Text = "wynik";
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(519, 125);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(130, 56);
            trackBar1.TabIndex = 3;
            // 
            // label2
            // 
            
            label2.AutoSize = true;
            label2.Location = new Point(519, 76);
            label2.Name = "label2";
            label2.Size = new Size(152, 20);
            label2.TabIndex = 4;
            label2.Text = "wybierz czas pomiaru";
            // 
            // v_button
            // 
            v_button.Location = new Point(64, 78);
            v_button.Name = "v_button";
            v_button.Size = new Size(94, 29);
            v_button.TabIndex = 5;
            v_button.Text = "V";
            v_button.TextAlign = ContentAlignment.MiddleRight;
            v_button.UseVisualStyleBackColor = true;
            v_button.Click += v_button_Click_1;
            // 
            // Ω_button
            // 
            Ω_button.Location = new Point(221, 126);
            Ω_button.Name = "Ω_button";
            Ω_button.Size = new Size(94, 29);
            Ω_button.TabIndex = 6;
            Ω_button.Text = "Ω";
            Ω_button.UseVisualStyleBackColor = true;
            Ω_button.Click += Ω_button_Click;
            // 
            // I_button
            // 
            I_button.Location = new Point(64, 126);
            I_button.Name = "I_button";
            I_button.Size = new Size(94, 29);
            I_button.TabIndex = 7;
            I_button.Text = "I";
            I_button.UseVisualStyleBackColor = true;
            I_button.Click += I_button_Click;
            // 
            // ac_button
            // 
            ac_button.Location = new Point(221, 76);
            ac_button.Name = "ac_button";
            ac_button.Size = new Size(94, 29);
            ac_button.TabIndex = 9;
            ac_button.Text = "~V";
            ac_button.TextAlign = ContentAlignment.MiddleRight;
            ac_button.UseVisualStyleBackColor = true;
            ac_button.Click += ac_button_Click;
            // 
            // feedback_label
            // 
            feedback_label.AutoSize = true;
            feedback_label.Location = new Point(149, 217);
            feedback_label.Name = "feedback_label";
            feedback_label.Size = new Size(70, 20);
            feedback_label.TabIndex = 10;
            feedback_label.Text = "feedback";
            // 
            // csv_button
            // 
            csv_button.Location = new Point(524, 366);
            csv_button.Name = "csv_button";
            csv_button.Size = new Size(160, 29);
            csv_button.TabIndex = 11;
            csv_button.Text = "pobierz do CSV";
            csv_button.UseVisualStyleBackColor = true;
            csv_button.Click += csv_button_Click;
            // 
            // capacitance_button
            // 
            capacitance_button.Location = new Point(363, 78);
            capacitance_button.Name = "capacitance_button";
            capacitance_button.Size = new Size(94, 29);
            capacitance_button.TabIndex = 12;
            capacitance_button.Text = "--||--";
            capacitance_button.TextAlign = ContentAlignment.MiddleRight;
            capacitance_button.UseVisualStyleBackColor = true;
            capacitance_button.Click += capacitance_button_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(capacitance_button);
            Controls.Add(csv_button);
            Controls.Add(feedback_label);
            Controls.Add(ac_button);
            Controls.Add(I_button);
            Controls.Add(Ω_button);
            Controls.Add(v_button);
            Controls.Add(label2);
            Controls.Add(trackBar1);
            Controls.Add(result_label);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private Button capacitance_button;
        private Button button1;
        private LiveChartsCore.SkiaSharpView.WinForms.MotionCanvas motionCanvas1;
    }
}
