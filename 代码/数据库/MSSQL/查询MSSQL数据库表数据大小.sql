declare @table table 
(name nvarchar(100) ,rows int ,reserved nvarchar(100) ,data nvarchar(100) ,index_size nvarchar(100) ,unused nvarchar(100) ) insert into @table
exec sp_MSforeachtable 'exec sp_spaceused ''?''' 
select name,rows,Cast(SUBSTRING(reserved,0,LEN(reserved)-2)as numeric(15,2))/1024 reserved_MB,Cast(SUBSTRING(data,0,len(data)-2)as numeric(15,2))/1024 data_MB,index_size,unused from @table
order by data_MB  desc