Module ScheduledTransactions
    Public ArbTicker As String = ""
    Public SetUpArb As Boolean = False
    Public ArbUnderway As Boolean = False
    Public ArbAsk As Double = 0
    Public ArbNumberOfStocks As Double = 0
    Public ArbNumberOfOptions As Double = 0

    'Public Function IstheArbLegal(underlier)
    '    If IsInIP(underlier) = True Then
    '        If NumberInIP(underlier) < 0 Then
    '            Return 
    '        End If
    ''    End If


    'End Function

    'ExecuteAlgoTransaction moved to Smart Hedger and modified
    Public Sub DoScheduledTransactions()

        Globals.Dashboard.ExecuteOptionTransactionBtn.Enabled = False
        Globals.Dashboard.ExecuteStockTransactionBtn.Enabled = False

        For Each myRow As DataRow In myDataSet.Tables("StockMarketOneDayTable").Rows
            If myRow("DivDate").ToShortDateString = myRow("Date").AddDays(1).ToShortDateString Then
                ArbTicker = myRow("Ticker").Trim()
                ArbAsk = myRow("Ask")
                ArbNumberOfStocks = 10000 / ArbAsk
                CalcDelta(ArbTicker, currentDate)
                ArbNumberOfOptions = ArbNumberOfStocks * (1 / (Math.Abs(CalcDelta(ArbTicker, currentDate))))
                ' MessageBox.Show("Beep. Boop. Identified " + ArbTicker + " as a DivArb.", "Success?", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'ArbUnderway = True
            End If
        Next


        For Each myRow As DataRow In myDataSet.Tables("StockMarketOneDayTable").Rows
            If myRow("DivDate").ToShortDateString = myRow("Date").AddDays(1).ToShortDateString And myRow("Ticker").Trim() = ArbTicker Then
                ExecuteAlgoTransaction("SellShort", ArbNumberOfOptions, ArbTicker + "_COCTA")
                ExecuteAlgoTransaction("Buy", ArbNumberOfStocks, ArbTicker)

                ' MessageBox.Show("Beep. Boop. Bought Shares of " + ArbTicker, "Success?", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Next


        'For Each myRow As DataRow In myDataSet.Tables("StockMarketOneDayTable").Rows
        '    If myRow("DivDate").ToShortDateString = myRow("Date").ToShortDateString And myRow("Ticker").Trim() = ArbTicker And ArbUnits > 0 Then
        '        ExecuteAlgoTransaction("CashDiv", ArbNumberOfStocks, ArbTicker)
        '        ' MessageBox.Show("Beep. Boop. Cashed Dividend.", "Success?", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        'ArbYesterday = True
        '    End If
        'Next

        'For Each myRow As DataRow In myDataSet.Tables("StockMarketOneDayTable").Rows
        '    If ArbYesterday = True And myRow("DivDate").ToShortDateString <> myRow("Date").ToShortDateString And myRow("Ticker").Trim() = ArbTicker Then
        '        ExecuteAlgoTransaction("Sell", ArbNumber, ArbTicker)
        '        ExecuteAlgoTransaction("Buy", ArbHedgeVol, ArbTicker + "_COCTE")
        '        ' MessageBox.Show("Beep. Boop. Sold shares.", "Success?", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ArbYesterday = False
        '        ArbUnderway = False
        '    End If
        'Next




        Globals.Dashboard.ExecuteOptionTransactionBtn.Enabled = True
        Globals.Dashboard.ExecuteStockTransactionBtn.Enabled = True
    End Sub

    'Public Function FindArbTickers(currentDate)
    '    For Each myRow As DataRow In myDataSet.Tables("StockMarketOneDayTable").Rows
    '        If myRow("DivDate").ToShortDateString = myRow("Date").AddDays(1).ToShortDateString Then
    '            Return myRow("Ticker")
    '        Else
    '            Return Nothing
    '        End If
    '    Next
    '    Return Nothing
    'End Function

    'Public Sub TestArb()
    '    ArbTicker = FindArbTickers(currentDate)
    '    MessageBox.Show("Beep. Boop. Returned" + ArbTicker + ".", "Success?", MessageBoxButtons.OK, MessageBoxIcon.Error)
    'End Sub

    ''Public Sub MoneyMachine(targetDate As Date, currentDate As Date)
    ''    If currentDate = targetDate.AddDays(-1) Then

    ''    End If

    ''End Sub

End Module
