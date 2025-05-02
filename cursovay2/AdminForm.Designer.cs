namespace cursovaya
{
    partial class AdminForm
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.listView2 = new System.Windows.Forms.ListView();
            this.listView3 = new System.Windows.Forms.ListView();
            this.listView4 = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.FiltrButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.EditTableButton = new System.Windows.Forms.Button();
            this.AddNewTableButton = new System.Windows.Forms.Button();
            this.DeleteTableButton = new System.Windows.Forms.Button();
            this.NoticeButton = new System.Windows.Forms.Button();
            this.EmailButton = new System.Windows.Forms.Button();
            this.StatisticsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(24, 26);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(683, 425);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // listView2
            // 
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(723, 28);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(427, 310);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // listView3
            // 
            this.listView3.HideSelection = false;
            this.listView3.Location = new System.Drawing.Point(24, 473);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(326, 232);
            this.listView3.TabIndex = 2;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            // 
            // listView4
            // 
            this.listView4.HideSelection = false;
            this.listView4.Location = new System.Drawing.Point(375, 473);
            this.listView4.Name = "listView4";
            this.listView4.Size = new System.Drawing.Size(332, 232);
            this.listView4.TabIndex = 3;
            this.listView4.UseCompatibleStateImageBehavior = false;
            this.listView4.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Заказы:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(720, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Курьеры:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 454);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Клиенты:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(385, 454);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Рестораны";
            // 
            // FiltrButton
            // 
            this.FiltrButton.Location = new System.Drawing.Point(723, 411);
            this.FiltrButton.Name = "FiltrButton";
            this.FiltrButton.Size = new System.Drawing.Size(427, 40);
            this.FiltrButton.TabIndex = 8;
            this.FiltrButton.Text = "Отфильтровать";
            this.FiltrButton.UseVisualStyleBackColor = true;
            this.FiltrButton.Click += new System.EventHandler(this.FiltrButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(723, 370);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(206, 22);
            this.textBox1.TabIndex = 9;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(943, 370);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(207, 22);
            this.textBox2.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(720, 351);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Id курьера:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(967, 351);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Id ресторана:";
            // 
            // EditTableButton
            // 
            this.EditTableButton.Location = new System.Drawing.Point(723, 473);
            this.EditTableButton.Name = "EditTableButton";
            this.EditTableButton.Size = new System.Drawing.Size(427, 33);
            this.EditTableButton.TabIndex = 13;
            this.EditTableButton.Text = "Редактирование Существующих Таблиц";
            this.EditTableButton.UseVisualStyleBackColor = true;
            this.EditTableButton.Click += new System.EventHandler(this.EditTableButton_Click);
            // 
            // AddNewTableButton
            // 
            this.AddNewTableButton.Location = new System.Drawing.Point(723, 512);
            this.AddNewTableButton.Name = "AddNewTableButton";
            this.AddNewTableButton.Size = new System.Drawing.Size(427, 33);
            this.AddNewTableButton.TabIndex = 14;
            this.AddNewTableButton.Text = "Добавить таблицу";
            this.AddNewTableButton.UseVisualStyleBackColor = true;
            this.AddNewTableButton.Click += new System.EventHandler(this.AddNewTableButton_Click);
            // 
            // DeleteTableButton
            // 
            this.DeleteTableButton.Location = new System.Drawing.Point(723, 551);
            this.DeleteTableButton.Name = "DeleteTableButton";
            this.DeleteTableButton.Size = new System.Drawing.Size(427, 33);
            this.DeleteTableButton.TabIndex = 15;
            this.DeleteTableButton.Text = "Удалить таблицу";
            this.DeleteTableButton.UseVisualStyleBackColor = true;
            this.DeleteTableButton.Click += new System.EventHandler(this.DeleteTableButton_Click);
            // 
            // NoticeButton
            // 
            this.NoticeButton.Location = new System.Drawing.Point(723, 590);
            this.NoticeButton.Name = "NoticeButton";
            this.NoticeButton.Size = new System.Drawing.Size(427, 33);
            this.NoticeButton.TabIndex = 16;
            this.NoticeButton.Text = "Отправить уведомление курьерам";
            this.NoticeButton.UseVisualStyleBackColor = true;
            this.NoticeButton.Click += new System.EventHandler(this.NoticeButton_Click);
            // 
            // EmailButton
            // 
            this.EmailButton.Location = new System.Drawing.Point(723, 629);
            this.EmailButton.Name = "EmailButton";
            this.EmailButton.Size = new System.Drawing.Size(427, 33);
            this.EmailButton.TabIndex = 17;
            this.EmailButton.Text = "Отправить уведомление на почту!";
            this.EmailButton.UseVisualStyleBackColor = true;
            this.EmailButton.Click += new System.EventHandler(this.EmailButton_Click);
            // 
            // StatisticsButton
            // 
            this.StatisticsButton.Location = new System.Drawing.Point(723, 673);
            this.StatisticsButton.Name = "StatisticsButton";
            this.StatisticsButton.Size = new System.Drawing.Size(427, 32);
            this.StatisticsButton.TabIndex = 18;
            this.StatisticsButton.Text = "Статистика";
            this.StatisticsButton.UseVisualStyleBackColor = true;
            this.StatisticsButton.Click += new System.EventHandler(this.StatisticsButton_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 718);
            this.Controls.Add(this.StatisticsButton);
            this.Controls.Add(this.EmailButton);
            this.Controls.Add(this.NoticeButton);
            this.Controls.Add(this.DeleteTableButton);
            this.Controls.Add(this.AddNewTableButton);
            this.Controls.Add(this.EditTableButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.FiltrButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView4);
            this.Controls.Add(this.listView3);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listView1);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ListView listView4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button FiltrButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button EditTableButton;
        private System.Windows.Forms.Button AddNewTableButton;
        private System.Windows.Forms.Button DeleteTableButton;
        private System.Windows.Forms.Button NoticeButton;
        private System.Windows.Forms.Button EmailButton;
        private System.Windows.Forms.Button StatisticsButton;
    }
}