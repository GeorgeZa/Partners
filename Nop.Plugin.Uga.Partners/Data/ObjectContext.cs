using Nop.Core;
using Nop.Data;
using Nop.Plugin.Uga.Partners.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Uga.Partners.Data
{
    public class ObjectContext : DbContext, IDbContext
    {

        public void Detach(object entity)
        {
        }
        /// <summary>
        /// Gets or sets a value indicating whether proxy creation setting is enabled (used in EF)
        /// </summary>
        public bool ProxyCreationEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether auto detect changes setting is enabled (used in EF)
        /// </summary>
        public bool AutoDetectChangesEnabled { get; set; }
        public ObjectContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new Partners.Data.PartnersRecordMap());

            base.OnModelCreating(modelBuilder);
        }
        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public string CreateDatabaseInstallationScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public void Install()
        {

            var dbScript = CreateDatabaseScript();
            Database.ExecuteSqlCommand(dbScript);
            SaveChanges();

        }

        public void Uninstall()
        {
            //drop the table
            var tableName = this.GetTableName<PartnersRecord>();
            //var tableName = "ShippingByWeight";
            this.DropPluginTable(tableName);


        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public System.Collections.Generic.IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
