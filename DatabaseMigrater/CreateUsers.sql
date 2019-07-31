﻿CREATE LOGIN [$(WebAppUser)] FROM WINDOWS WITH DEFAULT_DATABASE=[$(DatabaseName)], DEFAULT_LANGUAGE=[us_english]
GO

CREATE USER [$(WebAppUser)] FOR LOGIN [$(WebAppUser)] WITH DEFAULT_SCHEMA = GameDeals
GO

GRANT CONNECT TO [$(WebAppUser)]
GO

GRANT SELECT ON SCHEMA::GameDeals TO [$(WebAppUser)]
GO

CREATE LOGIN [$(ServiceUser)] FROM WINDOWS WITH DEFAULT_DATABASE=[apps], DEFAULT_LANGUAGE=[us_english]
GO

CREATE USER [$(ServiceUser)] FOR LOGIN [$(ServiceUser)] WITH DEFAULT_SCHEMA = GameDeals
GO

GRANT CONNECT TO [$(ServiceUser)]
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::GameDeals TO [$(ServiceUser)]
GO