namespace kucoin_buy_detector
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
            this.components = new System.ComponentModel.Container();
            this.log_richTextBox = new System.Windows.Forms.RichTextBox();
            this.kol_pos_label = new System.Windows.Forms.Label();
            this.timer_ping = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // log_richTextBox
            // 
            this.log_richTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.log_richTextBox.Location = new System.Drawing.Point(-1, 0);
            this.log_richTextBox.Name = "log_richTextBox";
            this.log_richTextBox.Size = new System.Drawing.Size(615, 175);
            this.log_richTextBox.TabIndex = 321;
            this.log_richTextBox.Text = "";
            this.log_richTextBox.WordWrap = false;
            // 
            // kol_pos_label
            // 
            this.kol_pos_label.AutoSize = true;
            this.kol_pos_label.Location = new System.Drawing.Point(0, 179);
            this.kol_pos_label.Name = "kol_pos_label";
            this.kol_pos_label.Size = new System.Drawing.Size(42, 13);
            this.kol_pos_label.TabIndex = 322;
            this.kol_pos_label.Text = "Parcels";
            // 
            // timer_ping
            // 
            this.timer_ping.Interval = 15000;
            this.timer_ping.Tick += new System.EventHandler(this.timer_ping_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 197);
            this.Controls.Add(this.log_richTextBox);
            this.Controls.Add(this.kol_pos_label);
            this.Name = "Form1";
            this.Text = "kucoin buy detector";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox log_richTextBox;
        private System.Windows.Forms.Label kol_pos_label;
        private System.Windows.Forms.Timer timer_ping;
    }
}

