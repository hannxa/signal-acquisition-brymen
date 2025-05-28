namespace Rigol_siema
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.saveButton = new System.Windows.Forms.Button();
            this.DC_V = new System.Windows.Forms.RadioButton();
            this.Capacitance = new System.Windows.Forms.RadioButton();
            this.AC_I = new System.Windows.Forms.RadioButton();
            this.AC_V = new System.Windows.Forms.RadioButton();
            this.DC_I = new System.Windows.Forms.RadioButton();
            this.Resistance = new System.Windows.Forms.RadioButton();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.Continuous_measuring = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.end_save_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea5.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea5);
            legend5.Enabled = false;
            legend5.Name = "Legend1";
            this.chart1.Legends.Add(legend5);
            this.chart1.Location = new System.Drawing.Point(41, 259);
            this.chart1.Name = "chart1";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series5.Legend = "Legend1";
            series5.LegendText = "s";
            series5.Name = "Dane";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            this.chart1.Series.Add(series5);
            this.chart1.Size = new System.Drawing.Size(898, 300);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(348, 126);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(117, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Zapis";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click_1);
            // 
            // DC_V
            // 
            this.DC_V.AutoSize = true;
            this.DC_V.Location = new System.Drawing.Point(72, 103);
            this.DC_V.Name = "DC_V";
            this.DC_V.Size = new System.Drawing.Size(50, 17);
            this.DC_V.TabIndex = 2;
            this.DC_V.TabStop = true;
            this.DC_V.Text = "DC V";
            this.DC_V.UseVisualStyleBackColor = true;
            this.DC_V.CheckedChanged += new System.EventHandler(this.DC_V_CheckedChanged_1);
            // 
            // Capacitance
            // 
            this.Capacitance.AutoSize = true;
            this.Capacitance.Location = new System.Drawing.Point(214, 190);
            this.Capacitance.Name = "Capacitance";
            this.Capacitance.Size = new System.Drawing.Size(32, 17);
            this.Capacitance.TabIndex = 3;
            this.Capacitance.TabStop = true;
            this.Capacitance.Text = "C";
            this.Capacitance.UseVisualStyleBackColor = true;
            this.Capacitance.CheckedChanged += new System.EventHandler(this.Capacitance_CheckedChanged);
            // 
            // AC_I
            // 
            this.AC_I.AutoSize = true;
            this.AC_I.Location = new System.Drawing.Point(214, 148);
            this.AC_I.Name = "AC_I";
            this.AC_I.Size = new System.Drawing.Size(45, 17);
            this.AC_I.TabIndex = 4;
            this.AC_I.TabStop = true;
            this.AC_I.Text = "AC I";
            this.AC_I.UseVisualStyleBackColor = true;
            this.AC_I.CheckedChanged += new System.EventHandler(this.AC_I_CheckedChanged);
            // 
            // AC_V
            // 
            this.AC_V.AutoSize = true;
            this.AC_V.Location = new System.Drawing.Point(214, 103);
            this.AC_V.Name = "AC_V";
            this.AC_V.Size = new System.Drawing.Size(49, 17);
            this.AC_V.TabIndex = 5;
            this.AC_V.TabStop = true;
            this.AC_V.Text = "AC V";
            this.AC_V.UseVisualStyleBackColor = true;
            this.AC_V.CheckedChanged += new System.EventHandler(this.AC_V_CheckedChanged);
            // 
            // DC_I
            // 
            this.DC_I.AutoSize = true;
            this.DC_I.Location = new System.Drawing.Point(72, 148);
            this.DC_I.Name = "DC_I";
            this.DC_I.Size = new System.Drawing.Size(46, 17);
            this.DC_I.TabIndex = 6;
            this.DC_I.TabStop = true;
            this.DC_I.Text = "DC I";
            this.DC_I.UseVisualStyleBackColor = true;
            this.DC_I.CheckedChanged += new System.EventHandler(this.DC_I_CheckedChanged_1);
            // 
            // Resistance
            // 
            this.Resistance.AutoSize = true;
            this.Resistance.Location = new System.Drawing.Point(72, 190);
            this.Resistance.Name = "Resistance";
            this.Resistance.Size = new System.Drawing.Size(33, 17);
            this.Resistance.TabIndex = 7;
            this.Resistance.TabStop = true;
            this.Resistance.Text = "R";
            this.Resistance.UseVisualStyleBackColor = true;
            this.Resistance.CheckedChanged += new System.EventHandler(this.Resistance_CheckedChanged_1);
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ResultLabel.Location = new System.Drawing.Point(353, 42);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(125, 20);
            this.ResultLabel.TabIndex = 8;
            this.ResultLabel.Text = "Wynik pomiaru";
            // 
            // Continuous_measuring
            // 
            this.Continuous_measuring.AutoSize = true;
            this.Continuous_measuring.Enabled = false;
            this.Continuous_measuring.Location = new System.Drawing.Point(567, 66);
            this.Continuous_measuring.Name = "Continuous_measuring";
            this.Continuous_measuring.Size = new System.Drawing.Size(90, 17);
            this.Continuous_measuring.TabIndex = 9;
            this.Continuous_measuring.Text = "Ciągły pomiar";
            this.Continuous_measuring.UseVisualStyleBackColor = true;
            this.Continuous_measuring.CheckedChanged += new System.EventHandler(this.Continuous_measuring_CheckedChanged_1);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(752, 42);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(157, 173);
            this.listBox1.TabIndex = 10;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(749, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Historia pomiarów:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "0.5s",
            "1s",
            "5s",
            "30s",
            "60s"});
            this.comboBox1.Location = new System.Drawing.Point(567, 118);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(142, 21);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.Text = "Częstotliwość pomiaru";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(567, 187);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(142, 20);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "nazwapliku";
            // 
            // end_save_button
            // 
            this.end_save_button.Location = new System.Drawing.Point(348, 97);
            this.end_save_button.Name = "end_save_button";
            this.end_save_button.Size = new System.Drawing.Size(117, 23);
            this.end_save_button.TabIndex = 14;
            this.end_save_button.Text = "Koniec zapisu";
            this.end_save_button.UseVisualStyleBackColor = true;
            this.end_save_button.Click += new System.EventHandler(this.end_save_button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(564, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Częstotliwość pomiaru:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(564, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Nazwa pliku do zapisu:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(107, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 18);
            this.label4.TabIndex = 17;
            this.label4.Text = "Aktualny pomiar:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 571);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.end_save_button);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Continuous_measuring);
            this.Controls.Add(this.ResultLabel);
            this.Controls.Add(this.Resistance);
            this.Controls.Add(this.DC_I);
            this.Controls.Add(this.AC_V);
            this.Controls.Add(this.AC_I);
            this.Controls.Add(this.Capacitance);
            this.Controls.Add(this.DC_V);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Rigol DM3051 METER";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.RadioButton DC_V;
        private System.Windows.Forms.RadioButton Capacitance;
        private System.Windows.Forms.RadioButton AC_I;
        private System.Windows.Forms.RadioButton AC_V;
        private System.Windows.Forms.RadioButton DC_I;
        private System.Windows.Forms.RadioButton Resistance;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.CheckBox Continuous_measuring;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button end_save_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

