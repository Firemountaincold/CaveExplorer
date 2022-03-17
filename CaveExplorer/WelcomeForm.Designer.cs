namespace CaveExplorer
{
    partial class WelcomeForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxjob = new System.Windows.Forms.GroupBox();
            this.groupBoxfeature = new System.Windows.Forms.GroupBox();
            this.rbFighter = new System.Windows.Forms.RadioButton();
            this.rbEngineer = new System.Windows.Forms.RadioButton();
            this.rbBeliever = new System.Windows.Forms.RadioButton();
            this.labelcn = new System.Windows.Forms.Label();
            this.labelfeature = new System.Windows.Forms.Label();
            this.buttonroll = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonload = new System.Windows.Forms.Button();
            this.groupBoxjob.SuspendLayout();
            this.groupBoxfeature.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxjob
            // 
            this.groupBoxjob.Controls.Add(this.rbBeliever);
            this.groupBoxjob.Controls.Add(this.rbEngineer);
            this.groupBoxjob.Controls.Add(this.rbFighter);
            this.groupBoxjob.Location = new System.Drawing.Point(12, 12);
            this.groupBoxjob.Name = "groupBoxjob";
            this.groupBoxjob.Size = new System.Drawing.Size(573, 143);
            this.groupBoxjob.TabIndex = 0;
            this.groupBoxjob.TabStop = false;
            this.groupBoxjob.Text = "职业选择";
            // 
            // groupBoxfeature
            // 
            this.groupBoxfeature.Controls.Add(this.buttonload);
            this.groupBoxfeature.Controls.Add(this.buttonroll);
            this.groupBoxfeature.Controls.Add(this.labelfeature);
            this.groupBoxfeature.Controls.Add(this.labelcn);
            this.groupBoxfeature.Location = new System.Drawing.Point(12, 161);
            this.groupBoxfeature.Name = "groupBoxfeature";
            this.groupBoxfeature.Size = new System.Drawing.Size(573, 277);
            this.groupBoxfeature.TabIndex = 0;
            this.groupBoxfeature.TabStop = false;
            this.groupBoxfeature.Text = "属性调整";
            // 
            // rbFighter
            // 
            this.rbFighter.AutoSize = true;
            this.rbFighter.Checked = true;
            this.rbFighter.Location = new System.Drawing.Point(54, 57);
            this.rbFighter.Name = "rbFighter";
            this.rbFighter.Size = new System.Drawing.Size(89, 28);
            this.rbFighter.TabIndex = 1;
            this.rbFighter.TabStop = true;
            this.rbFighter.Text = "斗士";
            this.rbFighter.UseVisualStyleBackColor = true;
            // 
            // rbEngineer
            // 
            this.rbEngineer.AutoSize = true;
            this.rbEngineer.Location = new System.Drawing.Point(210, 57);
            this.rbEngineer.Name = "rbEngineer";
            this.rbEngineer.Size = new System.Drawing.Size(113, 28);
            this.rbEngineer.TabIndex = 2;
            this.rbEngineer.Text = "工程师";
            this.rbEngineer.UseVisualStyleBackColor = true;
            // 
            // rbBeliever
            // 
            this.rbBeliever.AutoSize = true;
            this.rbBeliever.Location = new System.Drawing.Point(394, 57);
            this.rbBeliever.Name = "rbBeliever";
            this.rbBeliever.Size = new System.Drawing.Size(89, 28);
            this.rbBeliever.TabIndex = 3;
            this.rbBeliever.Text = "信徒";
            this.rbBeliever.UseVisualStyleBackColor = true;
            // 
            // labelcn
            // 
            this.labelcn.AutoSize = true;
            this.labelcn.Location = new System.Drawing.Point(50, 57);
            this.labelcn.Name = "labelcn";
            this.labelcn.Size = new System.Drawing.Size(442, 24);
            this.labelcn.TabIndex = 0;
            this.labelcn.Text = "生命    攻击    敏捷    防御    幸运";
            // 
            // labelfeature
            // 
            this.labelfeature.AutoSize = true;
            this.labelfeature.Location = new System.Drawing.Point(54, 98);
            this.labelfeature.Name = "labelfeature";
            this.labelfeature.Size = new System.Drawing.Size(430, 24);
            this.labelfeature.TabIndex = 1;
            this.labelfeature.Text = " 10      10      10      10      10";
            // 
            // buttonroll
            // 
            this.buttonroll.Location = new System.Drawing.Point(54, 158);
            this.buttonroll.Name = "buttonroll";
            this.buttonroll.Size = new System.Drawing.Size(155, 75);
            this.buttonroll.TabIndex = 2;
            this.buttonroll.Text = "随机属性";
            this.buttonroll.UseVisualStyleBackColor = true;
            this.buttonroll.Click += new System.EventHandler(this.buttonroll_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(592, 13);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(196, 425);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "开始游戏";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonload
            // 
            this.buttonload.Location = new System.Drawing.Point(328, 158);
            this.buttonload.Name = "buttonload";
            this.buttonload.Size = new System.Drawing.Size(155, 75);
            this.buttonload.TabIndex = 3;
            this.buttonload.Text = "继承存档";
            this.buttonload.UseVisualStyleBackColor = true;
            this.buttonload.Click += new System.EventHandler(this.buttonload_Click);
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBoxfeature);
            this.Controls.Add(this.groupBoxjob);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WelcomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "洞穴探险";
            this.groupBoxjob.ResumeLayout(false);
            this.groupBoxjob.PerformLayout();
            this.groupBoxfeature.ResumeLayout(false);
            this.groupBoxfeature.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxjob;
        private System.Windows.Forms.RadioButton rbBeliever;
        private System.Windows.Forms.RadioButton rbEngineer;
        private System.Windows.Forms.RadioButton rbFighter;
        private System.Windows.Forms.GroupBox groupBoxfeature;
        private System.Windows.Forms.Button buttonroll;
        private System.Windows.Forms.Label labelfeature;
        private System.Windows.Forms.Label labelcn;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonload;
    }
}

