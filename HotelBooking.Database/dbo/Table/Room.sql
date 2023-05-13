CREATE TABLE [dbo].[Room]
(
  [RoomId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [StartReservation] DATETIME,
  [EndReservation] DATETIME,
);