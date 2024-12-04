using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace optimisation.Interceptor;

public class CommandInterceptor : DbCommandInterceptor
{
    private Stopwatch _stopwatch = new Stopwatch();

    public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
    {
        _stopwatch.Start();
        return base.ReaderExecuting(command, eventData, result);
    }

    public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        
        Console.WriteLine(result);
        _stopwatch.Stop();
        return base.ReaderExecuted(command, eventData, result);
    }
}