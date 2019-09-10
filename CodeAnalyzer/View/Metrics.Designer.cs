namespace CodeAnalyzer.View
{
    partial class Metrics
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
            this.MetricsImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MetricsImage)).BeginInit();
            this.SuspendLayout();
            // 
            // MetricsImage
            // 
            this.MetricsImage.Location = new System.Drawing.Point(12, 12);
            this.MetricsImage.Name = "MetricsImage";
            this.MetricsImage.Size = new System.Drawing.Size(483, 499);
            this.MetricsImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MetricsImage.TabIndex = 0;
            this.MetricsImage.TabStop = false;
            // 
            // Metrics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 523);
            this.Controls.Add(this.MetricsImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Metrics";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metrics";
            this.Load += new System.EventHandler(this.Metrics_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MetricsImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox MetricsImage;
    }
}