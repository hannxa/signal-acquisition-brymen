namespace signal_acquisition_brymen
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private ScottPlot.WinForms.FormsPlot formsPlot1;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.result_label = new System.Windows.Forms.Label();
            this.durationLabel = new System.Windows.Forms.Label();
            this.measurementDurationSlider = new System.Windows.Forms.TrackBar();
            this.v_button = new System.Windows.Forms.Button();
            this.Ω_button = new System.Windows.Forms.Button();
            this.I_button = new System.Windows.Forms.Button();
            this.ac_button = new System.Windows.Forms.Button();
            this.feedback_label = new System.Windows.Forms.Label();
            this.csv_button = new System.Windows.Forms.Button();
            this.capacitance_button = new System.Windows.Forms.Button();
            this.startMeasurementButton = new System.Windows.Forms.Button();
            this.formsPlot1 = new ScottPlot.WinForms.FormsPlot();

            ((System.ComponentModel.ISupportInitialize)(this.measurementDurationSlider)).BeginInit();
            this.SuspendLayout();

            // result_label
            this.result_label.AutoSize = true;
            this.result_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.result_label.Location = new System.Drawing.Point(159, 278);
            this.result_label.Name = "result_label";
            this.result_label.Size = new System.Drawing.Size(56, 17);
            this.result_label.TabIndex = 0;
            this.result_label.Text = "Wynik";

            // durationLabel
            this.durationLabel.AutoSize = true;
            this.durationLabel.Location = new System.Drawing.Point(519, 76);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(125, 20);
            this.durationLabel.TabIndex = 1;
            this.durationLabel.Text = "Czas pomiaru: 10 s";

            // measurementDurationSlider
            this.measurementDurationSlider.Location = new System.Drawing.Point(519, 125);
            this.measurementDurationSlider.Minimum = 1;
            this.measurementDurationSlider.Maximum = 60;
            this.measurementDurationSlider.Value = 10;
            this.measurementDurationSlider.Name = "measurementDurationSlider";
            this.measurementDurationSlider.Size = new System.Drawing.Size(130, 56);
            this.measurementDurationSlider.TabIndex = 2;
            this.measurementDurationSlider.ValueChanged += new System.EventHandler(this.measurementDurationSlider_ValueChanged);

            // v_button
            this.v_button.BackColor = System.Drawing.Color.LightBlue;
            this.v_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.v_button.Location = new System.Drawing.Point(64, 78);
            this.v_button.Name = "v_button";
            this.v_button.Size = new System.Drawing.Size(94, 29);
            this.v_button.TabIndex = 3;
            this.v_button.Text = "V DC";
            this.v_button.UseVisualStyleBackColor = false;
            this.v_button.Click += new System.EventHandler(this.v_button_Click_1);

            // Ω_button
            this.Ω_button.BackColor = System.Drawing.Color.LightGreen;
            this.Ω_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.Ω_button.Location = new System.Drawing.Point(221, 126);
            this.Ω_button.Name = "Ω_button";
            this.Ω_button.Size = new System.Drawing.Size(94, 29);
            this.Ω_button.TabIndex = 4;
            this.Ω_button.Text = "Ω";
            this.Ω_button.UseVisualStyleBackColor = false;
            this.Ω_button.Click += new System.EventHandler(this.Ω_button_Click);

            // I_button
            this.I_button.BackColor = System.Drawing.Color.LightCoral;
            this.I_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.I_button.Location = new System.Drawing.Point(64, 126);
            this.I_button.Name = "I_button";
            this.I_button.Size = new System.Drawing.Size(94, 29);
            this.I_button.TabIndex = 5;
            this.I_button.Text = "I";
            this.I_button.UseVisualStyleBackColor = false;
            this.I_button.Click += new System.EventHandler(this.I_button_Click);

            // ac_button
            this.ac_button.BackColor = System.Drawing.Color.LightYellow;
            this.ac_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.ac_button.Location = new System.Drawing.Point(221, 78);
            this.ac_button.Name = "ac_button";
            this.ac_button.Size = new System.Drawing.Size(94, 29);
            this.ac_button.TabIndex = 6;
            this.ac_button.Text = "V AC";
            this.ac_button.UseVisualStyleBackColor = false;
            this.ac_button.Click += new System.EventHandler(this.ac_button_Click);

            // feedback_label
            this.feedback_label.AutoSize = true;
            this.feedback_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic);
            this.feedback_label.ForeColor = System.Drawing.Color.DarkBlue;
            this.feedback_label.Location = new System.Drawing.Point(149, 217);
            this.feedback_label.Name = "feedback_label";
            this.feedback_label.Size = new System.Drawing.Size(66, 15);
            this.feedback_label.TabIndex = 7;
            this.feedback_label.Text = "Feedback";

            // csv_button
            this.csv_button.BackColor = System.Drawing.Color.Orange;
            this.csv_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.csv_button.Location = new System.Drawing.Point(524, 366);
            this.csv_button.Name = "csv_button";
            this.csv_button.Size = new System.Drawing.Size(160, 29);
            this.csv_button.TabIndex = 8;
            this.csv_button.Text = "Pobierz do CSV";
            this.csv_button.UseVisualStyleBackColor = false;
            this.csv_button.Click += new System.EventHandler(this.csv_button_Click);

            // capacitance_button
            this.capacitance_button.BackColor = System.Drawing.Color.Plum;
            this.capacitance_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.capacitance_button.Location = new System.Drawing.Point(363, 78);
            this.capacitance_button.Name = "capacitance_button";
            this.capacitance_button.Size = new System.Drawing.Size(94, 29);
            this.capacitance_button.TabIndex = 9;
            this.capacitance_button.Text = "--||--";
            this.capacitance_button.UseVisualStyleBackColor = false;
            this.capacitance_button.Click += new System.EventHandler(this.capacitance_button_Click);

            // startMeasurementButton
            this.startMeasurementButton.BackColor = System.Drawing.Color.LightGreen;
            this.startMeasurementButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.startMeasurementButton.Location = new System.Drawing.Point(350, 200);
            this.startMeasurementButton.Name = "startMeasurementButton";
            this.startMeasurementButton.Size = new System.Drawing.Size(150, 35);
            this.startMeasurementButton.TabIndex = 10;
            this.startMeasurementButton.Text = "Start Measurement";
            this.startMeasurementButton.UseVisualStyleBackColor = false;
            this.startMeasurementButton.Click += new System.EventHandler(this.startMeasurementButton_Click);

            // formsPlot1
            this.formsPlot1.Location = new System.Drawing.Point(50, 320);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(700, 180);
            this.formsPlot1.TabIndex = 11;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.result_label);
            this.Controls.Add(this.durationLabel);
            this.Controls.Add(this.measurementDurationSlider);
            this.Controls.Add(this.v_button);
            this.Controls.Add(this.Ω_button);
            this.Controls.Add(this.I_button);
            this.Controls.Add(this.ac_button);
            this.Controls.Add(this.feedback_label);
            this.Controls.Add(this.csv_button);
            this.Controls.Add(this.capacitance_button);
            this.Controls.Add(this.startMeasurementButton);
            this.Controls.Add(this.formsPlot1);

            this.Name = "Form1";
            this.Text = "Signal Acquisition - Real-time Data Logger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);

            ((System.ComponentModel.ISupportInitialize)(this.measurementDurationSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label result_label;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.TrackBar measurementDurationSlider;
        private System.Windows.Forms.Button v_button;
        private System.Windows.Forms.Button Ω_button;
        private System.Windows.Forms.Button I_button;
        private System.Windows.Forms.Button ac_button;
        private System.Windows.Forms.Label feedback_label;
        private System.Windows.Forms.Button csv_button;
        private System.Windows.Forms.Button capacitance_button;
        private System.Windows.Forms.Button startMeasurementButton;
    }
}