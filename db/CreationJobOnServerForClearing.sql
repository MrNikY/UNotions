use [UNotionsDB];
go

begin
    declare @jobId uniqueidentifier;
    exec msdb.dbo.sp_add_job
        @job_name = 'DeleteFromRecycleBin',
        @enabled = 1,
        @description = 'Notesthat are in the basket for more than seven days will be deleted.',
        @job_id = @jobId output;
        
    EXEC msdb.dbo.sp_add_jobstep
        @job_id = @jobId,
        @step_name = 'DeleteFromRecycleBin',
        @subsystem = 'TSQL',
        @command = '
            delete from [Notes]
            where [Status] = 2 and [DeletionDate] < DateAdd(day, -7, GetDate());
        ';
        
    EXEC msdb.dbo.sp_add_schedule
        @schedule_name = 'CheckEveryHour',
        @freq_type = 4,
        @freq_interval = 1,
        @active_start_time = 000000,
		@active_end_time = 235959,
        @schedule_id = 1;
        
    EXEC msdb.dbo.sp_attach_schedule
        @job_id = @jobId,
        @schedule_id = 1;
        
    EXEC msdb.dbo.sp_add_jobserver
        @job_id = @jobId,
        @server_name = '(local)';
        
    PRINT 'A job has been created to delete notes from the Recycle Bin.';
end
go