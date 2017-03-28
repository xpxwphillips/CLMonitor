namespace CLMonitor
{
    partial class ui
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
            this.lstURIs = new System.Windows.Forms.ListBox();
            this.linkURL = new System.Windows.Forms.LinkLabel();
            this.lblDetails = new System.Windows.Forms.Label();
            this.btnLeft = new System.Windows.Forms.Button();
            this.lblQueryDate = new System.Windows.Forms.Label();
            this.btnRight = new System.Windows.Forms.Button();
            this.txtNewURL = new System.Windows.Forms.TextBox();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnQueryAll = new System.Windows.Forms.Button();
            this.btnQueryOne = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtTag = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtNewTag = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstURIs
            // 
            this.lstURIs.FormattingEnabled = true;
            this.lstURIs.Location = new System.Drawing.Point(12, 106);
            this.lstURIs.Name = "lstURIs";
            this.lstURIs.Size = new System.Drawing.Size(333, 446);
            this.lstURIs.TabIndex = 0;
            this.lstURIs.SelectedIndexChanged += new System.EventHandler(this.lstURIs_SelectedIndexChanged);
            // 
            // linkURL
            // 
            this.linkURL.AutoSize = true;
            this.linkURL.Location = new System.Drawing.Point(351, 106);
            this.linkURL.Name = "linkURL";
            this.linkURL.Size = new System.Drawing.Size(55, 13);
            this.linkURL.TabIndex = 1;
            this.linkURL.TabStop = true;
            this.linkURL.Text = "linkLabel1";
            this.linkURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkURL_LinkClicked);
            // 
            // lblDetails
            // 
            this.lblDetails.AutoSize = true;
            this.lblDetails.Location = new System.Drawing.Point(351, 170);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(35, 13);
            this.lblDetails.TabIndex = 2;
            this.lblDetails.Text = "label1";
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(354, 123);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(52, 23);
            this.btnLeft.TabIndex = 3;
            this.btnLeft.Text = "<<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // lblQueryDate
            // 
            this.lblQueryDate.AutoSize = true;
            this.lblQueryDate.Location = new System.Drawing.Point(412, 128);
            this.lblQueryDate.Name = "lblQueryDate";
            this.lblQueryDate.Size = new System.Drawing.Size(105, 13);
            this.lblQueryDate.TabIndex = 4;
            this.lblQueryDate.Text = "03/02/2017 2:24PM";
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(671, 123);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(52, 23);
            this.btnRight.TabIndex = 5;
            this.btnRight.Tag = "0";
            this.btnRight.Text = ">>";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // txtNewURL
            // 
            this.txtNewURL.Location = new System.Drawing.Point(12, 22);
            this.txtNewURL.Name = "txtNewURL";
            this.txtNewURL.Size = new System.Drawing.Size(814, 20);
            this.txtNewURL.TabIndex = 0;
            this.txtNewURL.TextChanged += new System.EventHandler(this.txtNewURL_TextChanged);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(832, 20);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(108, 23);
            this.btnAddNew.TabIndex = 2;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnQueryAll
            // 
            this.btnQueryAll.Location = new System.Drawing.Point(13, 559);
            this.btnQueryAll.Name = "btnQueryAll";
            this.btnQueryAll.Size = new System.Drawing.Size(75, 23);
            this.btnQueryAll.TabIndex = 8;
            this.btnQueryAll.Text = "Query All";
            this.btnQueryAll.UseVisualStyleBackColor = true;
            this.btnQueryAll.Click += new System.EventHandler(this.btnQueryAll_Click);
            // 
            // btnQueryOne
            // 
            this.btnQueryOne.Location = new System.Drawing.Point(94, 559);
            this.btnQueryOne.Name = "btnQueryOne";
            this.btnQueryOne.Size = new System.Drawing.Size(98, 23);
            this.btnQueryOne.TabIndex = 9;
            this.btnQueryOne.Text = "Query Selected";
            this.btnQueryOne.UseVisualStyleBackColor = true;
            this.btnQueryOne.Click += new System.EventHandler(this.btnQueryOne_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(249, 559);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(96, 23);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Delete Selected";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtTag
            // 
            this.txtTag.Location = new System.Drawing.Point(383, 147);
            this.txtTag.Name = "txtTag";
            this.txtTag.Size = new System.Drawing.Size(372, 20);
            this.txtTag.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(351, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tag";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(761, 145);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save Tag";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNewTag
            // 
            this.txtNewTag.Location = new System.Drawing.Point(48, 48);
            this.txtNewTag.Name = "txtNewTag";
            this.txtNewTag.Size = new System.Drawing.Size(451, 20);
            this.txtNewTag.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Tag";
            // 
            // ui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 617);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNewTag);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTag);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnQueryOne);
            this.Controls.Add(this.btnQueryAll);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.txtNewURL);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.lblQueryDate);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.lblDetails);
            this.Controls.Add(this.linkURL);
            this.Controls.Add(this.lstURIs);
            this.Name = "ui";
            this.Text = "CL Monitor";
            this.Load += new System.EventHandler(this.ui_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstURIs;
        private System.Windows.Forms.LinkLabel linkURL;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Label lblQueryDate;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.TextBox txtNewURL;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnQueryAll;
        private System.Windows.Forms.Button btnQueryOne;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtTag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtNewTag;
        private System.Windows.Forms.Label label2;
    }
}