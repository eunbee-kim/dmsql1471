CREATE TABLE [dbo].[Points]
(
	[Id] INT Identity(1,1) NOT NULL PRIMARY KEY,
	[UserId] INT NOT NULL,
	[Username] NVarChar(25)NULL,
	[TotalPoint] INT Default(0)

)
Go
CREATE TABLE [dbo].[PointLogs]
(
	[Id] INT Identity(1,1) NOT NULL PRIMARY KEY,
	[UserId] INT NOT NULL,
	[Username] NVarChar(25)NULL,
	[NowPoint] INT Default(1),
	[Created]  DatetimeOffset(7) Default(GetDate())

)
Go
