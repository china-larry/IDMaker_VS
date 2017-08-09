namespace IDMaker
{
    partial class FrmTempCalib
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
            this.cmb_Temp = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.tb_TCFcPlus = new System.Windows.Forms.TextBox();
            this.tb_DensFcPlus = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lv_Point = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.panel_Prerequiste = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.cm_Prerequisite2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cm_Prerequisite1 = new System.Windows.Forms.ComboBox();
            this.label57 = new System.Windows.Forms.Label();
            this.cm_SubCount = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_Subsection1 = new System.Windows.Forms.TextBox();
            this.cm_Log = new System.Windows.Forms.ComboBox();
            this.btn_ClearTemp = new System.Windows.Forms.Button();
            this.btn_Fit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_ItemNo = new System.Windows.Forms.Label();
            this.tb_Equation2 = new System.Windows.Forms.TextBox();
            this.tb_Equation1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_ReadEch = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_OpenEch = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.panel_Prerequiste.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_Temp
            // 
            this.cmb_Temp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Temp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Temp.FormattingEnabled = true;
            this.cmb_Temp.Items.AddRange(new object[] {
            "曲线1——20℃",
            "曲线2——25℃",
            "曲线3——27℃",
            "曲线4——29℃",
            "曲线5——31℃",
            "曲线6——35℃"});
            this.cmb_Temp.Location = new System.Drawing.Point(359, -4);
            this.cmb_Temp.Name = "cmb_Temp";
            this.cmb_Temp.Size = new System.Drawing.Size(112, 22);
            this.cmb_Temp.TabIndex = 62;
            this.cmb_Temp.Visible = false;
            this.cmb_Temp.SelectedIndexChanged += new System.EventHandler(this.cmb_Temp_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox10);
            this.panel1.Controls.Add(this.lv_Point);
            this.panel1.Controls.Add(this.groupBox7);
            this.panel1.Controls.Add(this.btn_ClearTemp);
            this.panel1.Controls.Add(this.btn_Fit);
            this.panel1.Location = new System.Drawing.Point(32, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(675, 346);
            this.panel1.TabIndex = 63;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.tb_TCFcPlus);
            this.groupBox10.Controls.Add(this.tb_DensFcPlus);
            this.groupBox10.Controls.Add(this.label11);
            this.groupBox10.Controls.Add(this.label10);
            this.groupBox10.Location = new System.Drawing.Point(24, 165);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(388, 150);
            this.groupBox10.TabIndex = 65;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "定标数据点";
            // 
            // tb_TCFcPlus
            // 
            this.tb_TCFcPlus.AcceptsReturn = true;
            this.tb_TCFcPlus.BackColor = System.Drawing.SystemColors.Window;
            this.tb_TCFcPlus.Location = new System.Drawing.Point(58, 87);
            this.tb_TCFcPlus.Multiline = true;
            this.tb_TCFcPlus.Name = "tb_TCFcPlus";
            this.tb_TCFcPlus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_TCFcPlus.Size = new System.Drawing.Size(316, 50);
            this.tb_TCFcPlus.TabIndex = 22;
            this.tb_TCFcPlus.TextChanged += new System.EventHandler(this.tb_TCFcPlus_TextChanged);
            // 
            // tb_DensFcPlus
            // 
            this.tb_DensFcPlus.BackColor = System.Drawing.SystemColors.Window;
            this.tb_DensFcPlus.Location = new System.Drawing.Point(58, 25);
            this.tb_DensFcPlus.Multiline = true;
            this.tb_DensFcPlus.Name = "tb_DensFcPlus";
            this.tb_DensFcPlus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_DensFcPlus.Size = new System.Drawing.Size(316, 51);
            this.tb_DensFcPlus.TabIndex = 21;
            this.tb_DensFcPlus.TextChanged += new System.EventHandler(this.tb_DensFcPlus_TextChanged);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(20, 104);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 21);
            this.label11.TabIndex = 20;
            this.label11.Text = "TC:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(6, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "浓度:";
            // 
            // lv_Point
            // 
            this.lv_Point.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv_Point.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lv_Point.FullRowSelect = true;
            this.lv_Point.GridLines = true;
            this.lv_Point.Location = new System.Drawing.Point(429, 42);
            this.lv_Point.Name = "lv_Point";
            this.lv_Point.Size = new System.Drawing.Size(224, 269);
            this.lv_Point.TabIndex = 64;
            this.lv_Point.UseCompatibleStateImageBehavior = false;
            this.lv_Point.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "浓度";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "TC";
            this.columnHeader3.Width = 80;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.panel_Prerequiste);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.cm_Prerequisite1);
            this.groupBox7.Controls.Add(this.label57);
            this.groupBox7.Controls.Add(this.cm_SubCount);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.tb_Subsection1);
            this.groupBox7.Controls.Add(this.cm_Log);
            this.groupBox7.Location = new System.Drawing.Point(24, 14);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(388, 143);
            this.groupBox7.TabIndex = 63;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "分段设置";
            // 
            // panel_Prerequiste
            // 
            this.panel_Prerequiste.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Prerequiste.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Prerequiste.Controls.Add(this.label29);
            this.panel_Prerequiste.Controls.Add(this.cm_Prerequisite2);
            this.panel_Prerequiste.Location = new System.Drawing.Point(39, 83);
            this.panel_Prerequiste.Name = "panel_Prerequiste";
            this.panel_Prerequiste.Size = new System.Drawing.Size(302, 46);
            this.panel_Prerequiste.TabIndex = 116;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.Location = new System.Drawing.Point(81, 15);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(107, 12);
            this.label29.TabIndex = 79;
            this.label29.Text = "第二分段限制条件:";
            // 
            // cm_Prerequisite2
            // 
            this.cm_Prerequisite2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cm_Prerequisite2.FormattingEnabled = true;
            this.cm_Prerequisite2.Items.AddRange(new object[] {
            "自动选择最佳",
            "1次方",
            "2次方",
            "3次方",
            "4次方",
            "5次方",
            "6次方"});
            this.cm_Prerequisite2.Location = new System.Drawing.Point(190, 9);
            this.cm_Prerequisite2.Name = "cm_Prerequisite2";
            this.cm_Prerequisite2.Size = new System.Drawing.Size(99, 20);
            this.cm_Prerequisite2.TabIndex = 80;
            this.cm_Prerequisite2.SelectedIndexChanged += new System.EventHandler(this.cm_Prerequisite2_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(33, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 14);
            this.label7.TabIndex = 14;
            this.label7.Text = "取对数:";
            // 
            // cm_Prerequisite1
            // 
            this.cm_Prerequisite1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cm_Prerequisite1.FormattingEnabled = true;
            this.cm_Prerequisite1.Items.AddRange(new object[] {
            "自动选择最佳",
            "1次方",
            "2次方",
            "3次方",
            "4次方",
            "5次方",
            "6次方"});
            this.cm_Prerequisite1.Location = new System.Drawing.Point(231, 54);
            this.cm_Prerequisite1.Name = "cm_Prerequisite1";
            this.cm_Prerequisite1.Size = new System.Drawing.Size(99, 20);
            this.cm_Prerequisite1.TabIndex = 113;
            this.cm_Prerequisite1.SelectedIndexChanged += new System.EventHandler(this.cm_Prerequisite1_SelectedIndexChanged);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label57.Location = new System.Drawing.Point(169, 58);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(59, 12);
            this.label57.TabIndex = 112;
            this.label57.Text = "限制条件:";
            // 
            // cm_SubCount
            // 
            this.cm_SubCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cm_SubCount.FormattingEnabled = true;
            this.cm_SubCount.Items.AddRange(new object[] {
            "不分段",
            "2"});
            this.cm_SubCount.Location = new System.Drawing.Point(230, 24);
            this.cm_SubCount.Name = "cm_SubCount";
            this.cm_SubCount.Size = new System.Drawing.Size(68, 20);
            this.cm_SubCount.TabIndex = 18;
            this.cm_SubCount.SelectedIndexChanged += new System.EventHandler(this.cm_SubCount_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(168, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 14);
            this.label8.TabIndex = 15;
            this.label8.Text = "分段数:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(37, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "第1段分段点:";
            // 
            // tb_Subsection1
            // 
            this.tb_Subsection1.Enabled = false;
            this.tb_Subsection1.Location = new System.Drawing.Point(123, 56);
            this.tb_Subsection1.Name = "tb_Subsection1";
            this.tb_Subsection1.Size = new System.Drawing.Size(33, 21);
            this.tb_Subsection1.TabIndex = 21;
            this.tb_Subsection1.Text = "0";
            this.tb_Subsection1.TextChanged += new System.EventHandler(this.tb_Subsection1_TextChanged);
            // 
            // cm_Log
            // 
            this.cm_Log.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cm_Log.FormattingEnabled = true;
            this.cm_Log.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cm_Log.Location = new System.Drawing.Point(95, 23);
            this.cm_Log.Name = "cm_Log";
            this.cm_Log.Size = new System.Drawing.Size(52, 20);
            this.cm_Log.TabIndex = 17;
            this.cm_Log.SelectedIndexChanged += new System.EventHandler(this.cm_Log_SelectedIndexChanged);
            // 
            // btn_ClearTemp
            // 
            this.btn_ClearTemp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ClearTemp.Location = new System.Drawing.Point(516, 13);
            this.btn_ClearTemp.Name = "btn_ClearTemp";
            this.btn_ClearTemp.Size = new System.Drawing.Size(69, 23);
            this.btn_ClearTemp.TabIndex = 62;
            this.btn_ClearTemp.Text = "清除";
            this.btn_ClearTemp.UseVisualStyleBackColor = true;
            this.btn_ClearTemp.Click += new System.EventHandler(this.btn_ClearTemp_Click);
            // 
            // btn_Fit
            // 
            this.btn_Fit.Location = new System.Drawing.Point(33, 318);
            this.btn_Fit.Name = "btn_Fit";
            this.btn_Fit.Size = new System.Drawing.Size(63, 25);
            this.btn_Fit.TabIndex = 65;
            this.btn_Fit.Text = "拟合(慎)";
            this.btn_Fit.UseVisualStyleBackColor = true;
            this.btn_Fit.Click += new System.EventHandler(this.btn_Fit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(273, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 64;
            this.label1.Text = "曲线选择:";
            this.label1.Visible = false;
            // 
            // lb_ItemNo
            // 
            this.lb_ItemNo.AutoSize = true;
            this.lb_ItemNo.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ItemNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lb_ItemNo.Location = new System.Drawing.Point(37, 26);
            this.lb_ItemNo.Name = "lb_ItemNo";
            this.lb_ItemNo.Size = new System.Drawing.Size(152, 19);
            this.lb_ItemNo.TabIndex = 66;
            this.lb_ItemNo.Text = "当前为第1个项目";
            // 
            // tb_Equation2
            // 
            this.tb_Equation2.BackColor = System.Drawing.SystemColors.Window;
            this.tb_Equation2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_Equation2.Location = new System.Drawing.Point(32, 509);
            this.tb_Equation2.Multiline = true;
            this.tb_Equation2.Name = "tb_Equation2";
            this.tb_Equation2.Size = new System.Drawing.Size(700, 42);
            this.tb_Equation2.TabIndex = 76;
            this.tb_Equation2.TextChanged += new System.EventHandler(this.tb_Equation_TextChanged);
            // 
            // tb_Equation1
            // 
            this.tb_Equation1.BackColor = System.Drawing.SystemColors.Window;
            this.tb_Equation1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_Equation1.Location = new System.Drawing.Point(32, 434);
            this.tb_Equation1.Multiline = true;
            this.tb_Equation1.Name = "tb_Equation1";
            this.tb_Equation1.Size = new System.Drawing.Size(700, 40);
            this.tb_Equation1.TabIndex = 75;
            this.tb_Equation1.TextChanged += new System.EventHandler(this.tb_Equation_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(39, 490);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 74;
            this.label5.Text = "方程2:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(39, 415);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 73;
            this.label3.Text = "方程1:";
            // 
            // btn_ReadEch
            // 
            this.btn_ReadEch.Location = new System.Drawing.Point(426, 26);
            this.btn_ReadEch.Name = "btn_ReadEch";
            this.btn_ReadEch.Size = new System.Drawing.Size(112, 30);
            this.btn_ReadEch.TabIndex = 78;
            this.btn_ReadEch.Text = "读取拟合软件数据";
            this.btn_ReadEch.UseVisualStyleBackColor = true;
            this.btn_ReadEch.Click += new System.EventHandler(this.btn_ReadEch_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(618, 24);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(89, 35);
            this.btn_Save.TabIndex = 79;
            this.btn_Save.Text = "保存当前";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_OpenEch
            // 
            this.btn_OpenEch.Location = new System.Drawing.Point(231, 25);
            this.btn_OpenEch.Name = "btn_OpenEch";
            this.btn_OpenEch.Size = new System.Drawing.Size(87, 30);
            this.btn_OpenEch.TabIndex = 80;
            this.btn_OpenEch.Text = "打开拟合软件";
            this.btn_OpenEch.UseVisualStyleBackColor = true;
            this.btn_OpenEch.Click += new System.EventHandler(this.btn_OpenEch_Click);
            // 
            // FrmTempCalib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 605);
            this.Controls.Add(this.btn_OpenEch);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_ReadEch);
            this.Controls.Add(this.tb_Equation2);
            this.Controls.Add(this.tb_Equation1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lb_ItemNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmb_Temp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FrmTempCalib";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "曲线定标";
            this.Load += new System.EventHandler(this.FrmTempCalib_Load);
            this.Activated += new System.EventHandler(this.FrmTempCalib_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTempCalib_FormClosing);
            this.panel1.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.panel_Prerequiste.ResumeLayout(false);
            this.panel_Prerequiste.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_Temp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox tb_TCFcPlus;
        private System.Windows.Forms.TextBox tb_DensFcPlus;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView lv_Point;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_Subsection1;
        private System.Windows.Forms.ComboBox cm_Prerequisite1;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.ComboBox cm_SubCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cm_Log;
        private System.Windows.Forms.Button btn_ClearTemp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Fit;
        private System.Windows.Forms.Panel panel_Prerequiste;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cm_Prerequisite2;
        public System.Windows.Forms.Label lb_ItemNo;
        private System.Windows.Forms.TextBox tb_Equation2;
        private System.Windows.Forms.TextBox tb_Equation1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_OpenEch;
        public System.Windows.Forms.Button btn_ReadEch;
    }
}