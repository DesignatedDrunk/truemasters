Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.Threading

Public Class Form1
    Public Shared Property Regions As String() = {"euw", "br", "eune", "jp", "kr", "lan", "las", "na", "oce", "ru", "tr"}
    Public Shared Property MasteryRegions As String() = {"EUW1", "BR1", "EUN1", "JP1", "KR", "LA1", "LA2", "NA1", "OC1", "RU", "TR1"}

    Public ReadOnly Property ApiKey As String
        Get
            Return tbAPIKey.Text
        End Get
    End Property
    Public ReadOnly Property SaveDirectory As DirectoryInfo
        Get
            Return New DirectoryInfo(tbDir.Text)
        End Get
    End Property
    Public ReadOnly Property ApiUrl(ByVal _region As Int32, ByVal _version As String, ByVal _function As String, Optional ByVal _args As String = "", Optional ByVal isglobal As Boolean = False, Optional ByVal ismastery As Boolean = False) As String
        Get
            If isglobal Then
                Return "https://global.api.pvp.net/api/lol/static-data/" & Regions(_region) & "/" & _version & "/" & _function & "?api_key=" & ApiKey & _args
            ElseIf ismastery Then
                Return "https://" & Regions(_region) & ".api.pvp.net/championmastery/location/" & MasteryRegions(_region) & "/" & _function & "?api_key=" & ApiKey & _args
            Else
                Return "https://" & Regions(_region) & ".api.pvp.net/api/lol/" & Regions(_region) & "/" & _version & "/" & _function & "?api_key=" & ApiKey & _args
            End If
        End Get
    End Property

    Private Sub btnDownloadChampions_Click(sender As Object, e As EventArgs) Handles btnDownloadChampions.Click
        pbPRogress.Value = 0
        lblProgress.Text = "Starting ..."
        bwDownloadChampions.RunWorkerAsync()
    End Sub
    Private Sub bwDownloadChampions_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadChampions.DoWork
        Dim _error As Object = Nothing
        Dim _result As String = MakeRequest(ApiUrl(0, "v1.2", "champion"), _error) 'Download all ChampionIDs

        'Retrieve Static ChampionInfo
        If _error Is Nothing And Not String.IsNullOrEmpty(_result) Then
            Dim _fi As New FileInfo(Path.Combine(SaveDirectory.FullName, "data_champions.js"))
            If _fi.Exists Then _fi.Delete()

            Dim js As New JavaScriptSerializer()
            js.MaxJsonLength = Int32.MaxValue

            Dim championsDictionary As Dictionary(Of String, Object) = js.DeserializeObject(_result)
            Dim saveDictionary As New Dictionary(Of String, Object)()

            Dim lst As Object() = championsDictionary("champions")
            Dim i As Int32 = 0
            For Each d As Dictionary(Of String, Object) In lst
                Dim championID As String = d("id")
                _result = MakeRequest(ApiUrl(0, "v1.2", "champion/" & championID, "&champData=all", True), _error) 'Download Details Champion
                If _error Is Nothing And Not String.IsNullOrEmpty(_result) Then
                    saveDictionary.Add(championID, js.DeserializeObject(_result))
                ElseIf Not _error Is Nothing Then
                    bwDownloadChampions.ReportProgress(100, New String() {"Error Downloading Champion [" & CType(_error, WebException).ToString() & "] ..."})
                End If

                bwDownloadChampions.ReportProgress(i / lst.Length * 100, (i + 1) & " / " & lst.Length & " champions")
                i += 1
            Next
            File.WriteAllText(_fi.FullName, "var data_champions = " & js.Serialize(saveDictionary))
        ElseIf Not _error Is Nothing Then
            bwDownloadChampions.ReportProgress(100, New String() {"Error Downloading Champions [" & CType(_error, WebException).ToString() & "] ..."})
        End If
    End Sub

    Private Sub btnDownloadPlayers_Click(sender As Object, e As EventArgs) Handles btnDownloadPlayers.Click
        pbPRogress.Value = 0
        lblProgress.Text = "Starting ..."
        bwDownloadPlayers.RunWorkerAsync()
    End Sub
    Private Sub bwDownloadPlayers_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadPlayers.DoWork
        Dim _error As Object = Nothing
        Dim _result As String = Nothing
        Dim result As New Dictionary(Of String, Object)()

        Dim js As New JavaScriptSerializer()
        js.MaxJsonLength = Int32.MaxValue

        'Get Players From these tiers
        Dim _ranks As String() = {"master", "challenger"}
        Dim cnter As Int32 = 0
        For Each _rank As String In _ranks
            Dim rankDictionary As New List(Of Object)()

            'Get From All Regions
            For i As Int32 = 0 To Regions.Length - 1
                _result = MakeRequest(ApiUrl(i, "v2.5", "league/" & _rank, "&type=RANKED_SOLO_5x5"), _error) 'Download Players
                bwDownloadPlayers.ReportProgress((i + cnter) / (2 * Regions.Length) * 100, (i + cnter + 1) & " / " & (2 * Regions.Length) & " tiers / regions")

                If _error Is Nothing And Not String.IsNullOrEmpty(_result) Then
                    Dim playersDictionary As Dictionary(Of String, Object) = js.DeserializeObject(_result)
                    Dim lst As Object() = playersDictionary("entries")
                    Dim lsti As Int32 = 0
                    For Each player As Dictionary(Of String, Object) In lst
                        bwDownloadPlayers.ReportProgress((i + cnter) / (2 * Regions.Length) * 100, (i + cnter + 1) & " / " & (2 * Regions.Length) & " tiers / regions (" & (lsti + 1) & "/" & lst.Length & ")")
                        player.Add("region", Regions(i))

                        Dim playerID As String = player("playerOrTeamId")

                        _result = Nothing
                        _error = Nothing
                        While Not _error Is Nothing Or String.IsNullOrEmpty(_result)
                            _result = MakeRequest(ApiUrl(i, "", "player/" & playerID & "/topchampions", "", False, True), _error) 'Download Player MasteryData

                            If _error Is Nothing And Not String.IsNullOrEmpty(_result) Then
                                Dim masteryArray As Object() = js.DeserializeObject(_result)
                                player.Add("masterydata", masteryArray)
                            ElseIf Not _error Is Nothing Then
                                bwDownloadPlayers.ReportProgress((i + cnter) / (2 * Regions.Length) * 100, New String() {"Error Downloading Mastery [" & CType(_error, WebException).ToString() & "] ..."})
                                bwDownloadPlayers.ReportProgress((i + cnter) / (2 * Regions.Length) * 100, New String() {"Error Downloading Mastery [" & CType(_error, WebException).ToString() & "] ..."})
                                bwDownloadPlayers.ReportProgress((i + cnter) / (2 * Regions.Length) * 100, New String() {"Error Downloading Mastery [" & CType(_error, WebException).ToString() & "] ..."})
                            End If

                            If Not _error Is Nothing Or String.IsNullOrEmpty(_result) Then Thread.Sleep(15000)
                        End While

                        rankDictionary.Add(player)
                        lsti += 1
                    Next
                ElseIf Not _error Is Nothing Then
                    bwDownloadPlayers.ReportProgress(100, New String() {"Error Downloading LEague [" & CType(_error, WebException).ToString() & "] ..."})
                End If
            Next

            result.Add(_rank, rankDictionary.ToArray())
            cnter += Regions.Length
        Next

        Dim _fi As New FileInfo(Path.Combine(SaveDirectory.FullName, "data_players.js"))
        If _fi.Exists Then _fi.Delete()
        File.WriteAllText(_fi.FullName, "var data_players = " & js.Serialize(result))
    End Sub


    Private Sub btnAnalyzePlayerData_Click(sender As Object, e As EventArgs) Handles btnAnalyzePlayerData.Click
        pbPRogress.Value = 0
        lblProgress.Text = "Starting ..."
        bwAnalyzePlayers.RunWorkerAsync()
    End Sub
    Private Sub bwAnalyzePlayers_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwAnalyzePlayers.DoWork
        Dim js As New JavaScriptSerializer()
        js.MaxJsonLength = Int32.MaxValue

        Dim fiChampions As New FileInfo(Path.Combine(SaveDirectory.FullName, "data_champions.js"))
        Dim objChampions As Dictionary(Of String, Object) = js.DeserializeObject(File.ReadAllText(fiChampions.FullName).Substring(21)) 'Remove Javascript prefix

        Dim fiPlayers As New FileInfo(Path.Combine(SaveDirectory.FullName, "data_players.js"))
        Dim objPlayers As Dictionary(Of String, Object) = js.DeserializeObject(File.ReadAllText(fiPlayers.FullName).Substring(19)) 'Remove Javascript prefix

        'Setup Empty
        Dim result As New Dictionary(Of String, Object)()
        result.Add("global", New Dictionary(Of String, Object)()) 'Global stats
        For Each _region As String In Regions
            result.Add(_region, New Dictionary(Of String, Object)())
        Next

        For Each kvp As KeyValuePair(Of String, Object) In result
            kvp.Value.Add("global", New Dictionary(Of String, Object)()) 'All Tiers Stats
            For Each kvp2 As KeyValuePair(Of String, Object) In objPlayers 'Loop Tiers
                kvp.Value.Add(kvp2.Key, New Dictionary(Of String, Object)())
            Next
            For Each kvp2 As KeyValuePair(Of String, Object) In kvp.Value
                For Each kvp3 As KeyValuePair(Of String, Object) In objChampions 'Loop Champions
                    Dim d As New Dictionary(Of String, Object)()
                    d.Add("amount_lvl1", 0) 'To store all #1 playercounts
                    d.Add("amount_lvl2", 0) 'To store all #2 playercounts
                    d.Add("amount_lvl3", 0) 'To store all #3 playercounts
                    d.Add("amount_lvl", 0) 'To store all playercounts (#1 = 3pnt, #2 = 2pnt, #3 = 1pnt)
                    d.Add("amount_points", 0) 'To store total masterypoints
                    d.Add("champions", New Dictionary(Of String, Object)()) 'To store all connected champions
                    For Each kvp4 As KeyValuePair(Of String, Object) In objChampions 'Loop Champions
                        d("champions").Add(kvp4.Key, 0)
                    Next

                    kvp2.Value.Add(kvp3.Key, d)
                Next
            Next
        Next

        'Fill data
        For Each kvpTier As KeyValuePair(Of String, Object) In objPlayers
            Dim _tier As String = kvpTier.Key
            For Each _player As Dictionary(Of String, Object) In kvpTier.Value
                Dim _region As String = _player("region")
                If _player.ContainsKey("masterydata") Then
                    Dim _masterydata As Object() = _player("masterydata")
                    Dim _saveRegions As String() = {"global", _region}
                    Dim _saveTiers As String() = {"global", _tier}

                    For _spot As Int32 = 0 To _masterydata.Length - 1
                        Dim _championdata As Dictionary(Of String, Object) = _masterydata(_spot)
                        
                        For Each sRegion As String In _saveRegions
                            For Each sTier As String In _saveTiers
                                result(sRegion)(sTier)(_championdata("championId"))("amount_lvl" & (_spot + 1)) += 1
                                result(sRegion)(sTier)(_championdata("championId"))("amount_lvl") += (3 - _spot) ' (#0 = 3pnt, #1 = 2pnt, #2 = 1pnt)
                                result(sRegion)(sTier)(_championdata("championId"))("amount_points") += _championdata("championPoints")

                                'Save Connected Champions
                                For _secundaryspot As Int32 = 0 To _masterydata.Length - 1
                                    If Not _spot = _secundaryspot Then
                                        result(sRegion)(sTier)(_championdata("championId"))("champions")(_masterydata(_secundaryspot)("championId")) += 1
                                    End If
                                Next
                            Next
                        Next
                    Next
                End If
            Next
        Next

        Dim _fi As New FileInfo(Path.Combine(SaveDirectory.FullName, "data_regions.js"))
        If _fi.Exists Then _fi.Delete()
        File.WriteAllText(_fi.FullName, "var data_regions = " & js.Serialize(result))
    End Sub


    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bwDownloadChampions.ProgressChanged, bwDownloadPlayers.ProgressChanged, bwAnalyzePlayers.ProgressChanged
        pbPRogress.Value = e.ProgressPercentage

        If TypeOf e.UserState Is String Then
            lblProgress.Text = CType(e.UserState, String)
        ElseIf TypeOf e.UserState Is String() Then
            tbLog.Text &= String.Join(vbCrLf, CType(e.UserState, String()))
        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDownloadChampions.RunWorkerCompleted, bwDownloadPlayers.RunWorkerCompleted, bwAnalyzePlayers.RunWorkerCompleted
        pbPRogress.Value = 100
        lblProgress.Text = "... Completed"
    End Sub

    Public Function MakeRequest(ByVal url As String, ByRef _error As Object) As String
        _error = Nothing

        'Create Request
        Dim request As WebRequest = WebRequest.Create(url)
        'request.ContentType = "application/x-www-form-urlencoded"
        request.Method = "GET"

        'Write Body
        Dim requestStream As Stream = Nothing

        'Get Response
        Dim response As WebResponse = Nothing
        Dim responseReader As StreamReader = Nothing
        Try
            response = request.GetResponse()
            requestStream = response.GetResponseStream()
            responseReader = New StreamReader(requestStream)
            Dim responseString As String = responseReader.ReadToEnd()

            Return responseString
        Catch ex As WebException
            _error = ex
        Finally
            'Close Readers / Writers
            If Not responseReader Is Nothing Then responseReader.Close()
            If Not requestStream Is Nothing Then requestStream.Close()
            If Not response Is Nothing Then response.Close()
        End Try

        'Handle Result
        Return Nothing
    End Function
End Class
