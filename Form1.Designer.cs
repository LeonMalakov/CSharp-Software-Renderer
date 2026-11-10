namespace KG_Task4
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.hierarchyBox = new System.Windows.Forms.ListBox();
            this.editorBox = new System.Windows.Forms.FlowLayoutPanel();
            this.componentsBox = new System.Windows.Forms.ComboBox();
            this.btnAddComponent = new System.Windows.Forms.Button();
            this.btnSpawnObject = new System.Windows.Forms.Button();
            this.objectsBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(180, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(800, 600);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.SizeChanged += new System.EventHandler(this.pictureBox_SizeChanged);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // hierarchyBox
            // 
            this.hierarchyBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.hierarchyBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hierarchyBox.FormattingEnabled = true;
            this.hierarchyBox.ItemHeight = 20;
            this.hierarchyBox.Items.AddRange(new object[] {
            "Cube",
            "Icosaed",
            "Quad"});
            this.hierarchyBox.Location = new System.Drawing.Point(0, 0);
            this.hierarchyBox.Name = "hierarchyBox";
            this.hierarchyBox.ScrollAlwaysVisible = true;
            this.hierarchyBox.Size = new System.Drawing.Size(180, 604);
            this.hierarchyBox.TabIndex = 2;
            this.hierarchyBox.SelectedIndexChanged += new System.EventHandler(this.hierarchyBox_SelectedIndexChanged);
            // 
            // editorBox
            // 
            this.editorBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorBox.AutoScroll = true;
            this.editorBox.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.editorBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editorBox.Location = new System.Drawing.Point(980, 0);
            this.editorBox.Name = "editorBox";
            this.editorBox.Size = new System.Drawing.Size(204, 600);
            this.editorBox.TabIndex = 3;
            // 
            // componentsBox
            // 
            this.componentsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.componentsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.componentsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.componentsBox.FormattingEnabled = true;
            this.componentsBox.Location = new System.Drawing.Point(980, 628);
            this.componentsBox.Name = "componentsBox";
            this.componentsBox.Size = new System.Drawing.Size(204, 28);
            this.componentsBox.TabIndex = 4;
            // 
            // btnAddComponent
            // 
            this.btnAddComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddComponent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddComponent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddComponent.Location = new System.Drawing.Point(1020, 690);
            this.btnAddComponent.Name = "btnAddComponent";
            this.btnAddComponent.Size = new System.Drawing.Size(136, 28);
            this.btnAddComponent.TabIndex = 5;
            this.btnAddComponent.Text = "Add component";
            this.btnAddComponent.UseVisualStyleBackColor = true;
            this.btnAddComponent.Click += new System.EventHandler(this.btnAddComponent_Click);
            // 
            // btnSpawnObject
            // 
            this.btnSpawnObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSpawnObject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpawnObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSpawnObject.Location = new System.Drawing.Point(21, 690);
            this.btnSpawnObject.Name = "btnSpawnObject";
            this.btnSpawnObject.Size = new System.Drawing.Size(136, 28);
            this.btnSpawnObject.TabIndex = 7;
            this.btnSpawnObject.Text = "Spawn";
            this.btnSpawnObject.UseVisualStyleBackColor = true;
            this.btnSpawnObject.Click += new System.EventHandler(this.btnSpawnObject_Click);
            // 
            // objectsBox
            // 
            this.objectsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.objectsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objectsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.objectsBox.FormattingEnabled = true;
            this.objectsBox.Location = new System.Drawing.Point(0, 628);
            this.objectsBox.Name = "objectsBox";
            this.objectsBox.Size = new System.Drawing.Size(180, 28);
            this.objectsBox.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.btnSpawnObject);
            this.Controls.Add(this.objectsBox);
            this.Controls.Add(this.btnAddComponent);
            this.Controls.Add(this.componentsBox);
            this.Controls.Add(this.editorBox);
            this.Controls.Add(this.hierarchyBox);
            this.Controls.Add(this.pictureBox);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ListBox hierarchyBox;
        private System.Windows.Forms.FlowLayoutPanel editorBox;
        private System.Windows.Forms.ComboBox componentsBox;
        private System.Windows.Forms.Button btnAddComponent;
        private System.Windows.Forms.Button btnSpawnObject;
        private System.Windows.Forms.ComboBox objectsBox;
    }
}

