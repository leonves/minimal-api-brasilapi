using BrasilApiIntegration.Data.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BrasilApiIntegration.Data.Extesions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ApplyGlobalStandards(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                /*
                    * Equivalente a: 
                    * modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                    * 
                    * O Contains('_') para NxN
                */
                if (!entityType.GetTableName().Contains('_'))
                    entityType.SetTableName(entityType.ClrType.Name);

                /*
                     * Equivalente a:
                     * modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                     * modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
                 */
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade).ToList()
                    .ForEach(fe => fe.DeleteBehavior = DeleteBehavior.Restrict);

                foreach (var property in entityType.GetProperties())
                {
                    switch (property.Name)
                    {
                        case nameof(Entity.Id):
                            property.IsKey();
                            break;
                        case nameof(Entity.ModifiedDate):
                            property.IsNullable = true;
                            break;
                        case nameof(Entity.CreatedDate):
                            property.IsNullable = false;
                            property.SetDefaultValueSql("GETDATE()");
                            break;
                    }

                    if (property.ClrType == typeof(string) && string.IsNullOrEmpty(property.GetColumnType()))
                        property.SetColumnType($"varchar({property.GetMaxLength() ?? 100})");

                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                        property.SetColumnType("datetime");
                }
            }

            return builder;
        }

        /// <summary>
        /// Utilize esse método para inserir informações no BD automaticamente através do comando 'Update-Database'
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            /*
                Nunca alterar os Guids para que não ocorra erros de REFERENCE constraint FKs.
                No momento do Update-Database, pois são os dados que já estão no BD.
            */

            return builder;
        }
    }
}
