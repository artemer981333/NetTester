namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.InputIP = new System.Windows.Forms.TextBox();
            this.FirstParameter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SecondParameter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Result = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Graphs = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ProtocolType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.MetricsChoose = new System.Windows.Forms.CheckedListBox();
            this.TestsNumber = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортВExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.руководствоПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Graphs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestsNumber)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputIP
            // 
            this.InputIP.Location = new System.Drawing.Point(16, 40);
            this.InputIP.Name = "InputIP";
            this.InputIP.Size = new System.Drawing.Size(224, 20);
            this.InputIP.TabIndex = 7;
            this.InputIP.Text = "127.0.0.1";
            // 
            // FirstParameter
            // 
            this.FirstParameter.Enabled = false;
            this.FirstParameter.Location = new System.Drawing.Point(16, 125);
            this.FirstParameter.Name = "FirstParameter";
            this.FirstParameter.Size = new System.Drawing.Size(142, 20);
            this.FirstParameter.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Нзвание параметра";
            // 
            // SecondParameter
            // 
            this.SecondParameter.Enabled = false;
            this.SecondParameter.Location = new System.Drawing.Point(162, 125);
            this.SecondParameter.Name = "SecondParameter";
            this.SecondParameter.Size = new System.Drawing.Size(148, 20);
            this.SecondParameter.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Нзвание параметра";
            // 
            // Result
            // 
            this.Result.Location = new System.Drawing.Point(12, 284);
            this.Result.Name = "Result";
            this.Result.ReadOnly = true;
            this.Result.Size = new System.Drawing.Size(235, 20);
            this.Result.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 268);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Результат";
            // 
            // Graphs
            // 
            this.Graphs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.Graphs.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Graphs.Legends.Add(legend1);
            this.Graphs.Location = new System.Drawing.Point(391, 24);
            this.Graphs.Name = "Graphs";
            series1.BorderColor = System.Drawing.Color.White;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Black;
            series1.Legend = "Legend1";
            series1.Name = "Values";
            this.Graphs.Series.Add(series1);
            this.Graphs.Size = new System.Drawing.Size(470, 285);
            this.Graphs.TabIndex = 9;
            this.Graphs.Text = "chart1";
            // 
            // ProtocolType
            // 
            this.ProtocolType.FormattingEnabled = true;
            this.ProtocolType.Items.AddRange(new object[] {
            "TCP",
            "UDP"});
            this.ProtocolType.Location = new System.Drawing.Point(15, 79);
            this.ProtocolType.Name = "ProtocolType";
            this.ProtocolType.Size = new System.Drawing.Size(225, 21);
            this.ProtocolType.TabIndex = 10;
            this.ProtocolType.Text = "TCP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Количество тестов";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(162, 245);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(85, 21);
            this.StartButton.TabIndex = 12;
            this.StartButton.Text = "Тестировать";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.TestClick);
            // 
            // MetricsChoose
            // 
            this.MetricsChoose.BackColor = System.Drawing.SystemColors.Control;
            this.MetricsChoose.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MetricsChoose.FormattingEnabled = true;
            this.MetricsChoose.Items.AddRange(new object[] {
            "Ping",
            "Speed",
            "Delay",
            "DC",
            "PL"});
            this.MetricsChoose.Location = new System.Drawing.Point(15, 151);
            this.MetricsChoose.Name = "MetricsChoose";
            this.MetricsChoose.Size = new System.Drawing.Size(120, 75);
            this.MetricsChoose.TabIndex = 13;
            this.MetricsChoose.SelectedIndexChanged += new System.EventHandler(this.IndexChanged);
            // 
            // TestsNumber
            // 
            this.TestsNumber.Location = new System.Drawing.Point(16, 245);
            this.TestsNumber.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.TestsNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TestsNumber.Name = "TestsNumber";
            this.TestsNumber.Size = new System.Drawing.Size(142, 20);
            this.TestsNumber.TabIndex = 14;
            this.TestsNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.видToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(869, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.экспортВExcelToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // экспортВExcelToolStripMenuItem
            // 
            this.экспортВExcelToolStripMenuItem.Name = "экспортВExcelToolStripMenuItem";
            this.экспортВExcelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.экспортВExcelToolStripMenuItem.Text = "Экспорт в Excel";
            this.экспортВExcelToolStripMenuItem.Click += new System.EventHandler(this.ExportStripClick);
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem,
            this.руководствоПользователяToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // руководствоПользователяToolStripMenuItem
            // 
            this.руководствоПользователяToolStripMenuItem.Name = "руководствоПользователяToolStripMenuItem";
            this.руководствоПользователяToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.руководствоПользователяToolStripMenuItem.Text = "Руководство пользователя";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Протокол";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 315);
            this.Controls.Add(this.TestsNumber);
            this.Controls.Add(this.MetricsChoose);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ProtocolType);
            this.Controls.Add(this.Graphs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SecondParameter);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.FirstParameter);
            this.Controls.Add(this.InputIP);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Network Metrics";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClose);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Graphs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestsNumber)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox InputIP;
        private System.Windows.Forms.TextBox FirstParameter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SecondParameter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Result;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataVisualization.Charting.Chart Graphs;
        private System.Windows.Forms.ComboBox ProtocolType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.CheckedListBox MetricsChoose;
        private System.Windows.Forms.NumericUpDown TestsNumber;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспортВExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem руководствоПользователяToolStripMenuItem;
    }
}

