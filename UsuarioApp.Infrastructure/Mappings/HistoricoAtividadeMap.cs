using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Infra.Data.Mappings
{
    public class HistoricoAtividadeMap : IEntityTypeConfiguration<HistoricoAtividade>
    {
        public void Configure(EntityTypeBuilder<HistoricoAtividade> builder)
        {
            builder.ToTable("HISTORICOATIVIDADE");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id)
                .HasColumnName("ID");

            builder.Property(h => h.DataHora)
                .HasColumnName("DATAHORA")
                .IsRequired();

            builder.Property(h => h.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(h => h.UsuarioId)
                .HasColumnName("USUARIOID");

            builder.HasOne(h => h.Usuario) //HistoricoAtividade TEM 1 Usuário
                .WithMany(u => u.Historicos) //Usuário TEM MUITOS Históricos
                .HasForeignKey(h => h.UsuarioId); //Chave estrangeira
        }
    }
}


