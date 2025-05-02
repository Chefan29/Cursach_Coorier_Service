namespace cursovaya
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
            this.label1 = new System.Windows.Forms.Label();
            this.ClientChooseButton = new System.Windows.Forms.Button();
            this.AdminChooseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Авторизуйтесь:";
            // 
            // ClientChooseButton
            // 
            this.ClientChooseButton.Location = new System.Drawing.Point(30, 56);
            this.ClientChooseButton.Name = "ClientChooseButton";
            this.ClientChooseButton.Size = new System.Drawing.Size(136, 34);
            this.ClientChooseButton.TabIndex = 1;
            this.ClientChooseButton.Text = "Курьер";
            this.ClientChooseButton.UseVisualStyleBackColor = true;
            this.ClientChooseButton.Click += new System.EventHandler(this.ClientChooseButton_Click);
            // 
            // AdminChooseButton
            // 
            this.AdminChooseButton.Location = new System.Drawing.Point(172, 56);
            this.AdminChooseButton.Name = "AdminChooseButton";
            this.AdminChooseButton.Size = new System.Drawing.Size(136, 34);
            this.AdminChooseButton.TabIndex = 2;
            this.AdminChooseButton.Text = "Админ";
            this.AdminChooseButton.UseVisualStyleBackColor = true;
            this.AdminChooseButton.Click += new System.EventHandler(this.AdminChooseButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 111);
            this.Controls.Add(this.AdminChooseButton);
            this.Controls.Add(this.ClientChooseButton);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "АИС \"КурьерPro\"";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ClientChooseButton;
        private System.Windows.Forms.Button AdminChooseButton;
    }
}

