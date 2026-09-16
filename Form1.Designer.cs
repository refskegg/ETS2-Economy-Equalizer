namespace ETS2_ModTool;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.PropertyGrid propertyGrid1;
    private System.Windows.Forms.Panel bottomPanel;
    private System.Windows.Forms.Label lblModName;
    private System.Windows.Forms.TextBox txtModName;
    private System.Windows.Forms.Label lblAuthor;
    private System.Windows.Forms.TextBox txtAuthor;
    private System.Windows.Forms.TableLayoutPanel buttonTableLayout;
    private System.Windows.Forms.ComboBox cmbPresets;
    private System.Windows.Forms.Button btnLoadPreset;
    private System.Windows.Forms.Button btnSavePreset;
    private System.Windows.Forms.Button btnImport;
    private System.Windows.Forms.Button btnExport;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
        this.bottomPanel = new System.Windows.Forms.Panel();
        this.lblModName = new System.Windows.Forms.Label();
        this.txtModName = new System.Windows.Forms.TextBox();
        this.lblAuthor = new System.Windows.Forms.Label();
        this.txtAuthor = new System.Windows.Forms.TextBox();
        this.buttonTableLayout = new System.Windows.Forms.TableLayoutPanel();
        this.cmbPresets = new System.Windows.Forms.ComboBox();
        this.btnLoadPreset = new System.Windows.Forms.Button();
        this.btnSavePreset = new System.Windows.Forms.Button();
        this.btnImport = new System.Windows.Forms.Button();
        this.btnExport = new System.Windows.Forms.Button();
        this.bottomPanel.SuspendLayout();
        this.buttonTableLayout.SuspendLayout();
        this.SuspendLayout();

        // 
        // bottomPanel
        // 
        this.bottomPanel.Controls.Add(this.buttonTableLayout);
        this.bottomPanel.Controls.Add(this.lblModName);
        this.bottomPanel.Controls.Add(this.txtModName);
        this.bottomPanel.Controls.Add(this.lblAuthor);
        this.bottomPanel.Controls.Add(this.txtAuthor);
        this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.bottomPanel.Height = 70;
        this.bottomPanel.Location = new System.Drawing.Point(0, 530);
        this.bottomPanel.Name = "bottomPanel";
        this.bottomPanel.TabIndex = 1;

        // 
        // lblModName
        // 
        this.lblModName.AutoSize = true;
        this.lblModName.Location = new System.Drawing.Point(12, 14);
        this.lblModName.Name = "lblModName";
        this.lblModName.Size = new System.Drawing.Size(71, 15);
        this.lblModName.Text = "Mod-Name:";

        // 
        // txtModName
        // 
        this.txtModName.Location = new System.Drawing.Point(90, 11);
        this.txtModName.Name = "txtModName";
        this.txtModName.Size = new System.Drawing.Size(150, 23);
        this.txtModName.TabIndex = 0;
        this.txtModName.Text = "Mein Custom Mod";

        // 
        // lblAuthor
        // 
        this.lblAuthor.AutoSize = true;
        this.lblAuthor.Location = new System.Drawing.Point(12, 42);
        this.lblAuthor.Name = "lblAuthor";
        this.lblAuthor.Size = new System.Drawing.Size(40, 15);
        this.lblAuthor.Text = "Autor:";

        // 
        // txtAuthor
        // 
        this.txtAuthor.Location = new System.Drawing.Point(90, 39);
        this.txtAuthor.Name = "txtAuthor";
        this.txtAuthor.Size = new System.Drawing.Size(150, 23);
        this.txtAuthor.TabIndex = 1;
        this.txtAuthor.Text = "Modder";

        // 
        // buttonTableLayout
        // 
        this.buttonTableLayout.ColumnCount = 5;
        this.buttonTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
        this.buttonTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
        this.buttonTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
        this.buttonTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
        this.buttonTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
        this.buttonTableLayout.Controls.Add(this.cmbPresets, 0, 0);
        this.buttonTableLayout.Controls.Add(this.btnLoadPreset, 1, 0);
        this.buttonTableLayout.Controls.Add(this.btnSavePreset, 2, 0);
        this.buttonTableLayout.Controls.Add(this.btnImport, 3, 0);
        this.buttonTableLayout.Controls.Add(this.btnExport, 4, 0);
        this.buttonTableLayout.Dock = System.Windows.Forms.DockStyle.Right;
        this.buttonTableLayout.Location = new System.Drawing.Point(250, 0);
        this.buttonTableLayout.Name = "buttonTableLayout";
        this.buttonTableLayout.RowCount = 1;
        this.buttonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.buttonTableLayout.Size = new System.Drawing.Size(650, 70);
        this.buttonTableLayout.Padding = new System.Windows.Forms.Padding(5, 12, 10, 10);
        this.buttonTableLayout.TabIndex = 2;

        // 
        // cmbPresets
        // 
        this.cmbPresets.Dock = System.Windows.Forms.DockStyle.Fill;
        this.cmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbPresets.FormattingEnabled = true;
        this.cmbPresets.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
        this.cmbPresets.Name = "cmbPresets";
        this.cmbPresets.TabIndex = 0;
        this.cmbPresets.Items.AddRange(new object[] {
            "Standard (Vanilla)",
            "Hardcore Realismus",
            "Schnelles Geld (Arcade)"
        });
        this.cmbPresets.SelectedIndex = 0;
        this.cmbPresets.SelectedIndexChanged += new System.EventHandler(this.cmbPresets_SelectedIndexChanged);

        // 
        // btnLoadPreset
        // 
        this.btnLoadPreset.Dock = System.Windows.Forms.DockStyle.Fill;
        this.btnLoadPreset.Margin = new System.Windows.Forms.Padding(3);
        this.btnLoadPreset.Name = "btnLoadPreset";
        this.btnLoadPreset.TabIndex = 1;
        this.btnLoadPreset.Text = "Preset laden";
        this.btnLoadPreset.UseVisualStyleBackColor = true;
        this.btnLoadPreset.Click += new System.EventHandler(this.btnLoadPreset_Click);

        // 
        // btnSavePreset
        // 
        this.btnSavePreset.Dock = System.Windows.Forms.DockStyle.Fill;
        this.btnSavePreset.Margin = new System.Windows.Forms.Padding(3);
        this.btnSavePreset.Name = "btnSavePreset";
        this.btnSavePreset.TabIndex = 2;
        this.btnSavePreset.Text = "Preset speichern";
        this.btnSavePreset.UseVisualStyleBackColor = true;
        this.btnSavePreset.Click += new System.EventHandler(this.btnSavePreset_Click);

        // 
        // btnImport
        // 
        this.btnImport.Dock = System.Windows.Forms.DockStyle.Fill;
        this.btnImport.Margin = new System.Windows.Forms.Padding(3);
        this.btnImport.Name = "btnImport";
        this.btnImport.TabIndex = 3;
        this.btnImport.Text = "Mod importieren";
        this.btnImport.UseVisualStyleBackColor = true;
        this.btnImport.Click += new System.EventHandler(this.btnImport_Click);

        // 
        // btnExport
        // 
        this.btnExport.Dock = System.Windows.Forms.DockStyle.Fill;
        this.btnExport.Margin = new System.Windows.Forms.Padding(3);
        this.btnExport.Name = "btnExport";
        this.btnExport.TabIndex = 4;
        this.btnExport.Text = "Mod erstellen";
        this.btnExport.UseVisualStyleBackColor = true;
        this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

        // 
        // propertyGrid1
        // 
        this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
        this.propertyGrid1.Name = "propertyGrid1";
        this.propertyGrid1.Size = new System.Drawing.Size(900, 530);
        this.propertyGrid1.TabIndex = 0;

        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(900, 600);
        this.Controls.Add(this.propertyGrid1);
        this.Controls.Add(this.bottomPanel);
        this.MinimumSize = new System.Drawing.Size(860, 480);
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "ETS2 Economy Equalizer";
        this.bottomPanel.ResumeLayout(false);
        this.bottomPanel.PerformLayout();
        this.buttonTableLayout.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion
}