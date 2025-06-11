namespace signal_acquisition_brymen
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private Label result_label;
        private Label durationLabel;
        private Label feedback_label;
        private Button v_button;
        private Button Ω_button;
        private Button I_button;
        private Button ac_button;
        private Button csv_button;
        private Button capacitance_button;
        private Button startMeasurementButton;
        private TrackBar measurementDurationSlider;
        //private FormsPlot formsPlot1;

        private void InitializeComponent()
        {
            result_label = new Label();
            durationLabel = new Label();
            measurementDurationSlider = new TrackBar();
            v_button = new Button();
            Ω_button = new Button();
            I_button = new Button();
            ac_button = new Button();
            feedback_label = new Label();
            csv_button = new Button();
            capacitance_button = new Button();
            startMeasurementButton = new Button();
            //formsPlot1 = new ScottPlot.FormsPlot();

            ((System.ComponentModel.ISupportInitialize)measurementDurationSlider).BeginInit();
            SuspendLayout();

            // result_label
            result_label.AutoSize = true;
            result_label.Location = new Point(159, 278);
            result_label.Name = "result_label";
            result_label.Size = new Size(46, 20);
            result_label.Text = "wynik";

            // durationLabel
            durationLabel.AutoSize = true;
            durationLabel.Location = new Point(519, 76);
            durationLabel.Name = "durationLabel";
            durationLabel.Text = "Czas pomiaru: 10 s";

            // measurementDurationSlider
            measurementDurationSlider.Location = new Point(519, 125);
            measurementDurationSlider.Name = "measurementDurationSlider";
            measurementDurationSlider.Size = new Size(130, 56);
            measurementDurationSlider.Minimum = 1;
            measurementDurationSlider.Maximum = 60;
            measurementDurationSlider.Value = 10;
            measurementDurationSlider.ValueChanged += measurementDurationSlider_ValueChanged;

            // v_button
            v_button.Location = new Point(64, 78);
            v_button.Name = "v_button";
            v_button.Size = new Size(94, 29);
            v_button.Text = "V";
            v_button.UseVisualStyleBackColor = true;
            v_button.Click += v_button_Click_1;

            // Ω_button
            Ω_button.Location = new Point(221, 126);
            Ω_button.Name = "Ω_button";
            Ω_button.Size = new Size(94, 29);
            Ω_button.Text = "Ω";
            Ω_button.UseVisualStyleBackColor = true;
            Ω_button.Click += Ω_button_Click;

            // I_button
            I_button.Location = new Point(64, 126);
            I_button.Name = "I_button";
            I_button.Size = new Size(94, 29);
            I_button.Text = "I";
            I_button.UseVisualStyleBackColor = true;
            I_button.Click += I_button_Click;

            // ac_button
            ac_button.Location = new Point(221, 76);
            ac_button.Name = "ac_button";
            ac_button.Size = new Size(94, 29);
            ac_button.Text = "~V";
            ac_button.UseVisualStyleBackColor = true;
            ac_button.Click += ac_button_Click;

            // feedback_label
            feedback_label.AutoSize = true;
            feedback_label.Location = new Point(149, 217);
            feedback_label.Name = "feedback_label";
            feedback_label.Size = new Size(70, 20);
            feedback_label.Text = "feedback";

            // csv_button
            csv_button.Location = new Point(524, 366);
            csv_button.Name = "csv_button";
            csv_button.Size = new Size(160, 29);
            csv_button.Text = "pobierz do CSV";
            csv_button.UseVisualStyleBackColor = true;
            csv_button.Click += csv_button_Click;

            // capacitance_button
            capacitance_button.Location = new Point(363, 78);
            capacitance_button.Name = "capacitance_button";
            capacitance_button.Size = new Size(94, 29);
            capacitance_button.Text = "--||--";
            capacitance_button.UseVisualStyleBackColor = true;
            capacitance_button.Click += capacitance_button_Click;

            // startMeasurementButton
            startMeasurementButton.Location = new Point(350, 200);
            startMeasurementButton.Name = "startMeasurementButton";
            startMeasurementButton.Size = new Size(150, 29);
            startMeasurementButton.Text = "Start Measurement";
            startMeasurementButton.UseVisualStyleBackColor = true;
            startMeasurementButton.Click += startMeasurementButton_Click;

            // formsPlot1
            //formsPlot1.Location = new Point(50, 300);
            //formsPlot1.Size = new Size(600, 200);

            // Form1
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 520);
            Controls.Add(result_label);
            Controls.Add(durationLabel);
            Controls.Add(measurementDurationSlider);
            Controls.Add(v_button);
            Controls.Add(Ω_button);
            Controls.Add(I_button);
            Controls.Add(ac_button);
            Controls.Add(feedback_label);
            Controls.Add(csv_button);
            Controls.Add(capacitance_button);
            Controls.Add(startMeasurementButton);
            //Controls.Add(formsPlot1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)measurementDurationSlider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
