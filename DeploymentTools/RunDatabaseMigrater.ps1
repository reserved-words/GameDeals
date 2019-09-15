param($MigraterPath, $ConnectionString, $DatabaseName, $WebAppUser, $ServiceUser)

Start-Process -FilePath $MigraterPath -ArgumentList ("`"" + $ConnectionString + "`" `"" + $DatabaseName + "`" `"" + $WebAppUser + "`" `"" + $ServiceUser + "`"")