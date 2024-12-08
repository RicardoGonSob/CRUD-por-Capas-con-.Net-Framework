namespace DALProyectoCRUD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CONTACTO")]
    public partial class CONTACTO
    {
        [Key]
        public int IdContacto { get; set; }

        [StringLength(40)]
        public string Nombre { get; set; }

        [StringLength(40)]
        public string Telefono { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaNacimiento { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}
