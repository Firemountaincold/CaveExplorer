namespace CaveExplorer
{
    partial class GameForm
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
            this.panelplayer = new System.Windows.Forms.Panel();
            this.buttonUseItem = new System.Windows.Forms.Button();
            this.comboBoxitem = new System.Windows.Forms.ComboBox();
            this.labelbagstr = new System.Windows.Forms.Label();
            this.labelbag = new System.Windows.Forms.Label();
            this.charactorPhoto = new System.Windows.Forms.PictureBox();
            this.labelstatus = new System.Windows.Forms.Label();
            this.hpBar = new System.Windows.Forms.ProgressBar();
            this.panelcave = new System.Windows.Forms.Panel();
            this.buttonNext = new System.Windows.Forms.Button();
            this.infomation = new System.Windows.Forms.RichTextBox();
            this.panelplayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.charactorPhoto)).BeginInit();
            this.panelcave.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelplayer
            // 
            this.panelplayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelplayer.Controls.Add(this.buttonUseItem);
            this.panelplayer.Controls.Add(this.comboBoxitem);
            this.panelplayer.Controls.Add(this.labelbagstr);
            this.panelplayer.Controls.Add(this.labelbag);
            this.panelplayer.Controls.Add(this.charactorPhoto);
            this.panelplayer.Controls.Add(this.labelstatus);
            this.panelplayer.Controls.Add(this.hpBar);
            this.panelplayer.Location = new System.Drawing.Point(12, 12);
            this.panelplayer.Name = "panelplayer";
            this.panelplayer.Size = new System.Drawing.Size(308, 673);
            this.panelplayer.TabIndex = 0;
            // 
            // buttonUseItem
            // 
            this.buttonUseItem.Location = new System.Drawing.Point(209, 569);
            this.buttonUseItem.Name = "buttonUseItem";
            this.buttonUseItem.Size = new System.Drawing.Size(94, 46);
            this.buttonUseItem.TabIndex = 6;
            this.buttonUseItem.Text = "使用";
            this.buttonUseItem.UseVisualStyleBackColor = true;
            this.buttonUseItem.Click += new System.EventHandler(this.buttonUseItem_Click);
            // 
            // comboBoxitem
            // 
            this.comboBoxitem.FormattingEnabled = true;
            this.comboBoxitem.Location = new System.Drawing.Point(8, 572);
            this.comboBoxitem.Name = "comboBoxitem";
            this.comboBoxitem.Size = new System.Drawing.Size(195, 32);
            this.comboBoxitem.TabIndex = 5;
            // 
            // labelbagstr
            // 
            this.labelbagstr.AutoSize = true;
            this.labelbagstr.Location = new System.Drawing.Point(37, 266);
            this.labelbagstr.Name = "labelbagstr";
            this.labelbagstr.Size = new System.Drawing.Size(0, 24);
            this.labelbagstr.TabIndex = 4;
            // 
            // labelbag
            // 
            this.labelbag.AutoSize = true;
            this.labelbag.Location = new System.Drawing.Point(4, 234);
            this.labelbag.Name = "labelbag";
            this.labelbag.Size = new System.Drawing.Size(82, 24);
            this.labelbag.TabIndex = 3;
            this.labelbag.Text = "背包：";
            // 
            // charactorPhoto
            // 
            this.charactorPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.charactorPhoto.Image = global::CaveExplorer.Properties.Resources.fighter;
            this.charactorPhoto.Location = new System.Drawing.Point(0, 0);
            this.charactorPhoto.Name = "charactorPhoto";
            this.charactorPhoto.Size = new System.Drawing.Size(110, 188);
            this.charactorPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.charactorPhoto.TabIndex = 1;
            this.charactorPhoto.TabStop = false;
            // 
            // labelstatus
            // 
            this.labelstatus.AutoSize = true;
            this.labelstatus.Location = new System.Drawing.Point(113, 16);
            this.labelstatus.Name = "labelstatus";
            this.labelstatus.Size = new System.Drawing.Size(118, 24);
            this.labelstatus.TabIndex = 2;
            this.labelstatus.Text = "载入中...";
            // 
            // hpBar
            // 
            this.hpBar.Location = new System.Drawing.Point(0, 188);
            this.hpBar.Name = "hpBar";
            this.hpBar.Size = new System.Drawing.Size(308, 39);
            this.hpBar.TabIndex = 1;
            // 
            // panelcave
            // 
            this.panelcave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelcave.Controls.Add(this.buttonNext);
            this.panelcave.Location = new System.Drawing.Point(326, 13);
            this.panelcave.Name = "panelcave";
            this.panelcave.Size = new System.Drawing.Size(916, 405);
            this.panelcave.TabIndex = 2;
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(2, 354);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(99, 47);
            this.buttonNext.TabIndex = 0;
            this.buttonNext.Text = "前进";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // infomation
            // 
            this.infomation.Location = new System.Drawing.Point(327, 425);
            this.infomation.Name = "infomation";
            this.infomation.ReadOnly = true;
            this.infomation.Size = new System.Drawing.Size(915, 260);
            this.infomation.TabIndex = 3;
            this.infomation.Text = "";
            this.infomation.TextChanged += new System.EventHandler(this.infomation_TextChanged);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 697);
            this.Controls.Add(this.infomation);
            this.Controls.Add(this.panelcave);
            this.Controls.Add(this.panelplayer);
            this.MinimumSize = new System.Drawing.Size(1280, 768);
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "洞穴探险进行中！";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
            this.panelplayer.ResumeLayout(false);
            this.panelplayer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.charactorPhoto)).EndInit();
            this.panelcave.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelplayer;
        private System.Windows.Forms.Label labelstatus;
        private System.Windows.Forms.ProgressBar hpBar;
        private System.Windows.Forms.Label labelbag;
        private System.Windows.Forms.PictureBox charactorPhoto;
        private System.Windows.Forms.Panel panelcave;
        private System.Windows.Forms.RichTextBox infomation;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Label labelbagstr;
        private System.Windows.Forms.ComboBox comboBoxitem;
        private System.Windows.Forms.Button buttonUseItem;
    }
}