using System;
using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace ETicaretAPI.API.Configurations.SeriLogColumnWriters
{
    public class UserNameColumnWriter : ColumnWriterBase
    {
        //public UserNameColumnWriter(NpgsqlDbType dbType, int? columnLength = null) : base(dbType, columnLength)
        //{
        //}

        public UserNameColumnWriter() : base(NpgsqlDbType.Varchar)
        {
        }

        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
            var (username, value) = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name");
            return value?.ToString() ?? null;
        }
    }
}
