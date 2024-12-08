using System.Data.Entity;

namespace DALProyectoCRUD
{
    public partial class DBPRUEBAS : DbContext
    {
        public DBPRUEBAS()
            : base("name=DBPRUEBAS")
        {
        }

        public virtual DbSet<CONTACTO> CONTACTO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CONTACTO>()
                .Property(e => e.Nombre)
                .IsUnicode(false);
        }
    }
}
