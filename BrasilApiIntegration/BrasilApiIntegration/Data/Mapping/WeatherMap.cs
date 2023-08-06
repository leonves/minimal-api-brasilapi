using BrasilApiIntegration.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BrasilApiIntegration.Data.Mapping
{
    public class WeatherMap : IEntityTypeConfiguration<Weather>
    {
        public void Configure(EntityTypeBuilder<Weather> builder)
        {
            builder.ToTable("Weathers"); // Nome da tabela no banco de dados

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CodigoIcao)
                .HasColumnName("CodigoIcao")
                .IsRequired(false);

            builder.Property(e => e.AtualizadoEm)
                .HasColumnName("AtualizadoEm")
                .IsRequired();

            builder.Property(e => e.PressaoAtmosferica)
                .HasColumnName("PressaoAtmosferica")
                .IsRequired(false);

            builder.Property(e => e.Visibilidade)
                .HasColumnName("Visibilidade")
                .IsRequired(false);

            builder.Property(e => e.Vento)
                .HasColumnName("Vento")
                .IsRequired();

            builder.Property(e => e.DirecaoVento)
                .HasColumnName("DirecaoVento")
                .IsRequired();

            builder.Property(e => e.Umidade)
                .HasColumnName("Umidade")
                .IsRequired();

            builder.Property(e => e.Condicao)
                .HasColumnName("Condicao")
                .IsRequired(false);

            builder.Property(e => e.CondicaoDescricao)
                .HasColumnName("CondicaoDescricao")
                .IsRequired(false);

            builder.Property(e => e.Temp)
                .HasColumnName("Temp")
                .IsRequired();
        }
    }
}
