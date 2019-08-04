param($MigraterPath, $ConnectionString, $DatabaseName, $WebAppUser, $ServiceUser, $DomainName)

Start-Process -FilePath $MigraterPath -ArgumentList ("`"" + $ConnectionString + "`" `"" + $DatabaseName + "`" `"" + $WebAppUser + "`" `"" + $ServiceUser + "`" `"" + $DomainName + "`"")