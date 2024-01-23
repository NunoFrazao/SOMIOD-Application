namespace DoorClient {
    partial class FormDoor {
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
            txtDisplayState = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtDisplayState
            // 
            txtDisplayState.Location = new Point(73, 81);
            txtDisplayState.Margin = new Padding(3, 4, 3, 4);
            txtDisplayState.Name = "txtDisplayState";
            txtDisplayState.Size = new Size(114, 27);
            txtDisplayState.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(73, 57);
            label1.Name = "label1";
            label1.Size = new Size(57, 20);
            label1.TabIndex = 1;
            label1.Text = "Estado:";
            // 
            // FormDoor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(267, 223);
            Controls.Add(label1);
            Controls.Add(txtDisplayState);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormDoor";
            Text = "FormDoor";
            FormClosing += FormLight_FormClosing;
            Load += FormDoor_Load;
            Shown += FormLight_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDisplayState;
        private Label label1;
    }
}
