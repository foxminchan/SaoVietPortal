USE master;
IF NOT EXISTS(SELECT name FROM sys.databases WHERE name = 'SaoVietPortal')
BEGIN
   CREATE DATABASE SaoVietPortal
   ON PRIMARY (
      NAME = SaoVietPortal_data,
      FILENAME = 'C:\SaoVietPortal\SaoVietPortal_data.mdf',
      SIZE = 100MB,
      MAXSIZE = UNLIMITED,
      FILEGROWTH = 10MB
   )
   LOG ON (
      NAME = SaoVietPortal_log,
      FILENAME = 'C:\SaoVietPortal\SaoVietPortal_log.ldf',
      SIZE = 50MB,
      MAXSIZE = 2GB,
      FILEGROWTH = 5MB
   );
   PRINT 'Database SaoVietPortal created successfully.'
END
ELSE
BEGIN
   PRINT 'Database SaoVietPortal already exists.'
END
