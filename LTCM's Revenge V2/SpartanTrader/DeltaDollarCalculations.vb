Module DeltaDollarCalculations
    Public Sub RunDeltaDollarCalcs()
        For Each myRow As DataRow In myDataSet.Tables("OptionMarketOneDayTable").Rows
            If myRow("DivDate").ToShortDateString = myRow("Date").AddDays(1).ToShortDateString Then

                CalcDelta("NKE_COCTA", currentDate)

            End If
        Next
    End Sub
End Module
