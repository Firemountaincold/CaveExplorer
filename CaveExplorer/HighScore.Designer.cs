namespace CaveExplorer
{
    partial class HighScore
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Columnscore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columnwin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columnstep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columnjob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columndatatime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeight = 30;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Columnscore,
            this.Columnwin,
            this.Columnstep,
            this.Columnjob,
            this.Columndatatime});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.Location = new System.Drawing.Point(13, 13);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 82;
            this.dataGridView.RowTemplate.Height = 39;
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView.Size = new System.Drawing.Size(775, 480);
            this.dataGridView.TabIndex = 0;
            // 
            // Columnscore
            // 
            this.Columnscore.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Columnscore.DataPropertyName = "score";
            this.Columnscore.HeaderText = "分数";
            this.Columnscore.MinimumWidth = 10;
            this.Columnscore.Name = "Columnscore";
            this.Columnscore.ReadOnly = true;
            this.Columnscore.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Columnwin
            // 
            this.Columnwin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Columnwin.DataPropertyName = "win";
            this.Columnwin.HeaderText = "通关";
            this.Columnwin.MinimumWidth = 10;
            this.Columnwin.Name = "Columnwin";
            this.Columnwin.ReadOnly = true;
            this.Columnwin.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Columnstep
            // 
            this.Columnstep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Columnstep.DataPropertyName = "step";
            this.Columnstep.HeaderText = "步数";
            this.Columnstep.MinimumWidth = 10;
            this.Columnstep.Name = "Columnstep";
            this.Columnstep.ReadOnly = true;
            this.Columnstep.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Columnjob
            // 
            this.Columnjob.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Columnjob.DataPropertyName = "job";
            this.Columnjob.HeaderText = "职业";
            this.Columnjob.MinimumWidth = 10;
            this.Columnjob.Name = "Columnjob";
            this.Columnjob.ReadOnly = true;
            // 
            // Columndatatime
            // 
            this.Columndatatime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Columndatatime.DataPropertyName = "datatime";
            this.Columndatatime.FillWeight = 200F;
            this.Columndatatime.HeaderText = "时间";
            this.Columndatatime.MinimumWidth = 10;
            this.Columndatatime.Name = "Columndatatime";
            this.Columndatatime.ReadOnly = true;
            this.Columndatatime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // buttonok
            // 
            this.buttonok.BackColor = System.Drawing.Color.Linen;
            this.buttonok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonok.Location = new System.Drawing.Point(284, 499);
            this.buttonok.Name = "buttonok";
            this.buttonok.Size = new System.Drawing.Size(192, 49);
            this.buttonok.TabIndex = 1;
            this.buttonok.Text = "确定";
            this.buttonok.UseVisualStyleBackColor = false;
            this.buttonok.Click += new System.EventHandler(this.buttonok_Click);
            // 
            // HighScore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Controls.Add(this.buttonok);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HighScore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "高分榜";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonok;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columnscore;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columnwin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columnstep;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columnjob;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columndatatime;
    }
}