using System.Configuration;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SqlServer;
using NHibernate.Linq;

namespace Peach.Data.Sql
{
    class SqlSchemaMigrator : ISchemaMigrator
    {
        private readonly string _connectionString;

        public SqlSchemaMigrator()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Hadouken"].ConnectionString;
        }

        public void Migrate()
        {
            var processor = GetProcessor();
            var context = new RunnerContext(new NullAnnouncer());

            var migrationRunner = new MigrationRunner(
                typeof (SqlSchemaMigrator).Assembly,
                context,
                processor);

            migrationRunner.MigrateUp();
        }

        private IMigrationProcessor GetProcessor()
        {
            var factory = new SqlServerProcessorFactory();
            var processor = factory.Create(_connectionString, new NullAnnouncer(), new ProcessorOptions());

            return processor;
        }
    }
}