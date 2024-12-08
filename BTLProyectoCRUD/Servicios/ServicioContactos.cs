using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BTLProyectoCrud.Enum;
using BTLProyectoCrud.Nucleo;
using DALProyectoCRUD;

namespace BTLProyectoCRUD.Servicios
{
    public class ServicioContactos
    {
        string TAG = "BTLProyectoCrud.Servicios.ServicioContactos";

        DALProyectoCRUD.DBPRUEBAS dbo = new DALProyectoCRUD.DBPRUEBAS();

        // Método auxiliar para validar campos
        private Respuesta ValidarCampo(string valor, string nombreCampo, int maxLength)
        {
            if (string.IsNullOrEmpty(valor))
                return new Respuesta($"El {nombreCampo} del contacto es requerido", TAG);

            if (!Regex.IsMatch(valor, @"^[A-Za-zÑñÁáÉéÍíÓóÚú\s]+$") || valor.Length > maxLength)
                return new Respuesta($"El {nombreCampo} no debe poseer caracteres especiales y debe tener un máximo de {maxLength} caracteres", TAG);

            return null; // No hay errores
        }

        // Listar todos los contactos
        public Respuesta<List<CONTACTO>> Listar()
        {
            try
            {
                var datos = (from c in dbo.CONTACTO
                             select c).ToList();
                return new Respuesta<List<CONTACTO>>(datos);
            }
            catch (Exception ex)
            {
                return new Respuesta<List<CONTACTO>>(ex, "Error", TAG, EstadoProceso.Fatal);
            }
        }

        // Buscar un contacto por ID
        public Respuesta<CONTACTO> Buscar(int id)
        {
            try
            {
                var datos = (from c in dbo.CONTACTO
                             where c.IdContacto == id
                             select c).FirstOrDefault();
                return new Respuesta<CONTACTO>(datos);
            }
            catch (Exception ex)
            {
                return new Respuesta<CONTACTO>(ex, "Error", TAG, EstadoProceso.Fatal);
            }
        }

        // Agregar un nuevo contacto
        public Respuesta Agregar(string nombre, string telefono, DateTime fnacimiento, DateTime fregistro)
        {
            try
            {
                // Validar el nombre
                var respuestaNombre = ValidarCampo(nombre, "Nombre", 40);
                if (respuestaNombre != null) return respuestaNombre;

                // Validar duplicidad de nombre
                var valRepetido = dbo.CONTACTO.Any(c => c.Nombre == nombre);
                if (valRepetido)
                    return new Respuesta("El nombre del contacto ya está registrado", TAG);

                // Crear y guardar el nuevo contacto
                CONTACTO contacto = new CONTACTO()
                {
                    Nombre = nombre,
                    Telefono = telefono,
                    FechaNacimiento = fnacimiento,
                    FechaRegistro = fregistro
                };

                dbo.CONTACTO.Add(contacto);
                dbo.SaveChanges();

                return new Respuesta();
            }
            catch (Exception ex)
            {
                return new Respuesta(ex, "Error", TAG, EstadoProceso.Fatal);
            }
        }

        // Modificar un contacto existente
        public Respuesta Modificar(int id, string nombre, string telefono, DateTime fnacimiento, DateTime fregistro)
        {
            try
            {
                // Validar el nombre
                var respuestaNombre = ValidarCampo(nombre, "Nombre", 40);
                if (respuestaNombre != null) return respuestaNombre;

                // Validar duplicidad de nombre (excluyendo el actual)
                var valRepetido = dbo.CONTACTO.Any(c => c.Nombre == nombre && c.IdContacto != id);
                if (valRepetido)
                    return new Respuesta("El nombre del contacto ya está registrado", TAG);

                // Buscar el contacto por ID
                var contacto = dbo.CONTACTO.SingleOrDefault(c => c.IdContacto == id);
                if (contacto == null)
                    return new Respuesta("El contacto no existe", TAG);

                // Actualizar valores
                contacto.Nombre = nombre;
                contacto.Telefono = telefono;
                contacto.FechaNacimiento = fnacimiento;
                contacto.FechaRegistro = fregistro;

                dbo.SaveChanges();
                return new Respuesta();
            }
            catch (Exception ex)
            {
                return new Respuesta(ex, "Error", TAG, EstadoProceso.Fatal);
            }
        }

        // Eliminar un contacto
        public Respuesta Eliminar(int id)
        {
            try
            {
                var contacto = dbo.CONTACTO.SingleOrDefault(c => c.IdContacto == id);
                if (contacto == null)
                    return new Respuesta("El contacto no existe", TAG);

                dbo.CONTACTO.Remove(contacto);
                dbo.SaveChanges();

                return new Respuesta();
            }
            catch (Exception ex)
            {
                return new Respuesta(ex, "Error", TAG, EstadoProceso.Fatal);
            }
        }
    }
}
