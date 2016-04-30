<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbAPIKey = New System.Windows.Forms.TextBox()
        Me.pbPRogress = New System.Windows.Forms.ProgressBar()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbDir = New System.Windows.Forms.TextBox()
        Me.tbLog = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnDownloadPlayers = New System.Windows.Forms.Button()
        Me.btnDownloadChampions = New System.Windows.Forms.Button()
        Me.bwDownloadChampions = New System.ComponentModel.BackgroundWorker()
        Me.bwDownloadPlayers = New System.ComponentModel.BackgroundWorker()
        Me.btnAnalyzePlayerData = New System.Windows.Forms.Button()
        Me.bwAnalyzePlayers = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "API Key"
        '
        'tbAPIKey
        '
        Me.tbAPIKey.Location = New System.Drawing.Point(58, 20)
        Me.tbAPIKey.Name = "tbAPIKey"
        Me.tbAPIKey.Size = New System.Drawing.Size(150, 20)
        Me.tbAPIKey.TabIndex = 1
        Me.tbAPIKey.Text = ""
        '
        'pbPRogress
        '
        Me.pbPRogress.Location = New System.Drawing.Point(10, 72)
        Me.pbPRogress.Name = "pbPRogress"
        Me.pbPRogress.Size = New System.Drawing.Size(198, 23)
        Me.pbPRogress.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblProgress)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.tbDir)
        Me.GroupBox1.Controls.Add(Me.tbLog)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.tbAPIKey)
        Me.GroupBox1.Controls.Add(Me.pbPRogress)
        Me.GroupBox1.Location = New System.Drawing.Point(718, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(218, 369)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Progress"
        '
        'lblProgress
        '
        Me.lblProgress.Location = New System.Drawing.Point(10, 98)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(198, 22)
        Me.lblProgress.TabIndex = 6
        Me.lblProgress.Text = "..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Save"
        '
        'tbDir
        '
        Me.tbDir.Location = New System.Drawing.Point(58, 46)
        Me.tbDir.Name = "tbDir"
        Me.tbDir.Size = New System.Drawing.Size(150, 20)
        Me.tbDir.TabIndex = 4
        Me.tbDir.Text = "D:\Installed x64\inetpub\wwwroot\RiotApi\RiotApi\Export"
        '
        'tbLog
        '
        Me.tbLog.Location = New System.Drawing.Point(10, 123)
        Me.tbLog.Multiline = True
        Me.tbLog.Name = "tbLog"
        Me.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbLog.Size = New System.Drawing.Size(198, 230)
        Me.tbLog.TabIndex = 3
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnAnalyzePlayerData)
        Me.GroupBox2.Controls.Add(Me.btnDownloadPlayers)
        Me.GroupBox2.Controls.Add(Me.btnDownloadChampions)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 299)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(700, 82)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "General"
        '
        'btnDownloadPlayers
        '
        Me.btnDownloadPlayers.Location = New System.Drawing.Point(7, 53)
        Me.btnDownloadPlayers.Name = "btnDownloadPlayers"
        Me.btnDownloadPlayers.Size = New System.Drawing.Size(190, 23)
        Me.btnDownloadPlayers.TabIndex = 1
        Me.btnDownloadPlayers.Text = "Download PlayerData"
        Me.btnDownloadPlayers.UseVisualStyleBackColor = True
        '
        'btnDownloadChampions
        '
        Me.btnDownloadChampions.Location = New System.Drawing.Point(7, 20)
        Me.btnDownloadChampions.Name = "btnDownloadChampions"
        Me.btnDownloadChampions.Size = New System.Drawing.Size(190, 23)
        Me.btnDownloadChampions.TabIndex = 0
        Me.btnDownloadChampions.Text = "Download ChampionData"
        Me.btnDownloadChampions.UseVisualStyleBackColor = True
        '
        'bwDownloadChampions
        '
        Me.bwDownloadChampions.WorkerReportsProgress = True
        '
        'bwDownloadPlayers
        '
        Me.bwDownloadPlayers.WorkerReportsProgress = True
        '
        'btnAnalyzePlayerData
        '
        Me.btnAnalyzePlayerData.Location = New System.Drawing.Point(203, 20)
        Me.btnAnalyzePlayerData.Name = "btnAnalyzePlayerData"
        Me.btnAnalyzePlayerData.Size = New System.Drawing.Size(190, 23)
        Me.btnAnalyzePlayerData.TabIndex = 2
        Me.btnAnalyzePlayerData.Text = "Analyze PlayerData"
        Me.btnAnalyzePlayerData.UseVisualStyleBackColor = True
        '
        'bwAnalyzePlayers
        '
        Me.bwAnalyzePlayers.WorkerReportsProgress = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(948, 393)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbAPIKey As System.Windows.Forms.TextBox
    Friend WithEvents pbPRogress As System.Windows.Forms.ProgressBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents tbLog As System.Windows.Forms.TextBox
    Friend WithEvents btnDownloadChampions As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbDir As System.Windows.Forms.TextBox
    Friend WithEvents bwDownloadChampions As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents btnDownloadPlayers As System.Windows.Forms.Button
    Friend WithEvents bwDownloadPlayers As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnAnalyzePlayerData As System.Windows.Forms.Button
    Friend WithEvents bwAnalyzePlayers As System.ComponentModel.BackgroundWorker

End Class
