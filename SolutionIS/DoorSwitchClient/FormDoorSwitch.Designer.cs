namespace DoorSwitchClient {
    partial class FormDoorSwitch {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnOn = new Button();
            btnOff = new Button();
            SuspendLayout();
            // 
            // btnOn
            // 
            btnOn.Location = new Point(121, 76);
            btnOn.Margin = new Padding(3, 4, 3, 4);
            btnOn.Name = "btnOn";
            btnOn.Size = new Size(144, 88);
            btnOn.TabIndex = 0;
            btnOn.Text = "OPEN";
            btnOn.UseVisualStyleBackColor = true;
            btnOn.Click += btnOn_Click;
            // 
            // btnOff
            // 
            btnOff.Location = new Point(121, 197);
            btnOff.Margin = new Padding(3, 4, 3, 4);
            btnOff.Name = "btnOff";
            btnOff.Size = new Size(144, 88);
            btnOff.TabIndex = 1;
            btnOff.Text = "CLOSE";
            btnOff.UseVisualStyleBackColor = true;
            btnOff.Click += btnOff_Click;
            // 
            // FormDoorSwitch
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(397, 371);
            Controls.Add(btnOff);
            Controls.Add(btnOn);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormDoorSwitch";
            Text = "DoorSwitch";
            Load += FormDoorSwitch_Load;
            Shown += FormLightSwitch_Shown;
            ResumeLayout(false);
        }

        #endregion

        private Button btnOn;
        private Button btnOff;
    }
}
