namespace AssinarDFe
{
    partial class AssinadorXML
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
            this.tbMensagem = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbMensagem
            // 
            this.tbMensagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMensagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMensagem.Location = new System.Drawing.Point(0, 0);
            this.tbMensagem.Multiline = true;
            this.tbMensagem.Name = "tbMensagem";
            this.tbMensagem.ReadOnly = true;
            this.tbMensagem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMensagem.Size = new System.Drawing.Size(820, 432);
            this.tbMensagem.TabIndex = 1;
            // 
            // AssinadorXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 432);
            this.Controls.Add(this.tbMensagem);
            this.Name = "AssinadorXML";
            this.Text = "Assinandor de DFe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMensagem;
    }
}

