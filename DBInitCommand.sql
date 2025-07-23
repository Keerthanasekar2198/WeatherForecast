IF NOT EXISTS (SELECT name from sys.databases WHERE name='WeatherForecastDB')
BEGIN
	CREATE DATABASE WeatherForecastDB;
END
GO

USE [WeatherForecastDB]
GO

/****** Object: Table [dbo].[ForecastsData] Script Date: 23/07/2025 16:16:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ForecastsData] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [LocationName]   NVARCHAR (100) NULL,
    [Latitude]       FLOAT (53)     NULL,
    [Longitude]      FLOAT (53)     NULL,
    [ForecastDate]   DATE           NULL,
    [TemperatureMax] FLOAT (53)     NULL,
    [TemperatureMin] FLOAT (53)     NULL,
    [FetchedAt]      DATETIME       NULL
);


