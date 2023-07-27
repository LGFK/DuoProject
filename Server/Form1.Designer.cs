namespace Server;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        listBox1 = new ListBox();
        button1 = new Button();
        listBox2 = new ListBox();
        SuspendLayout();
        // 
        // listBox1
        // 
        listBox1.FormattingEnabled = true;
        listBox1.ItemHeight = 15;
        listBox1.Location = new Point(12, 12);
        listBox1.Name = "listBox1";
        listBox1.Size = new Size(247, 199);
        listBox1.TabIndex = 0;
        // 
        // button1
        // 
        button1.Location = new Point(533, 12);
        button1.Name = "button1";
        button1.Size = new Size(75, 23);
        button1.TabIndex = 1;
        button1.Text = "StartServer";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // listBox2
        // 
        listBox2.FormattingEnabled = true;
        listBox2.ItemHeight = 15;
        listBox2.Location = new Point(280, 12);
        listBox2.Name = "listBox2";
        listBox2.Size = new Size(247, 199);
        listBox2.TabIndex = 2;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(615, 229);
        Controls.Add(listBox2);
        Controls.Add(button1);
        Controls.Add(listBox1);
        Name = "Form1";
        Text = "Server";
        ResumeLayout(false);
    }

    #endregion

    private ListBox listBox1;
    private Button button1;
    private ListBox listBox2;
}
