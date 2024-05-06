using EzpeletaNetCore8.Data;
using Microsoft.AspNetCore.Mvc;
using EzpeletaNetCore8.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EzpeletaNetCore8.Controllers
{
    public class EjerciciosFisicosController : Controller
    {
        private ApplicationDbContext _context;

        public EjerciciosFisicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Crear una lista de SelectListItem que incluya el elemento adicional
            var selectListItems = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "[SELECCIONE...]" }
            };

            // Obtener todas las opciones del enum
            var enumValues = Enum.GetValues(typeof(EstadoEmocional)).Cast<EstadoEmocional>();

            // Convertir las opciones del enum en SelectListItem
            selectListItems.AddRange(enumValues.Select(e => new SelectListItem
            {
                Value = e.GetHashCode().ToString(),
                Text = e.ToString().ToUpper()
            }));

            // Pasar la lista de opciones al modelo de la vista
            ViewBag.EstadoEmocionalInicio = selectListItems.OrderBy(t => t.Text).ToList();
            ViewBag.EstadoEmocionalFin = selectListItems.OrderBy(t => t.Text).ToList();

            var tipoEjercicios = _context.TipoEjercicios.ToList();
            tipoEjercicios.Add(new TipoEjercicio { TipoEjercicioID = 0, Descripcion = "[SELECCIONE...]" });
            ViewBag.TipoEjercicioID = new SelectList(tipoEjercicios.OrderBy(c => c.Descripcion), "TipoEjercicioID", "Descripcion");

            return View();
        }

        public JsonResult ListadoEjerciciosFisicos(int? id)
        {
            var tipoejerciciosFisicos = _context.EjerciciosFisicos.ToList();

            if (id != null)
            {
                tipoejerciciosFisicos = tipoejerciciosFisicos.Where(t => t.EjercicioFisicoID == id).ToList();
            }

            return Json(tipoejerciciosFisicos);
        }

        //FUNCION PARA OBTENER LA DESCRIPCION DE UN TIPO DE EJERCICIO POR SU ID
        public JsonResult ObtenerDescripcionEjercicio(int id)
        {
            var tipoEjercicio = _context.TipoEjercicios.FirstOrDefault(t => t.TipoEjercicioID == id);

            if (tipoEjercicio == null)
            {
                return Json(new { error = "Tipo de ejercicio no encontrado" });
            }

            return Json(new { descripcion = tipoEjercicio.Descripcion });
        }

        public JsonResult GuardarEjercicio(int EjercicioFisicoID, int TipoEjercicioID, DateTime Inicio, DateTime Fin, EstadoEmocional EstadoEmocionalInicio, EstadoEmocional EstadoEmocionalFin, string Observaciones)
        {
            // Aquí puedes realizar las operaciones necesarias para guardar el ejercicio

            // Por ejemplo, puedes crear una nueva instancia de EjercicioFisico con los parámetros recibidos y guardarla en la base de datos
            var nuevoEjercicio = new EjercicioFisico
            {
                EjercicioFisicoID = EjercicioFisicoID,
                TipoEjercicioID = TipoEjercicioID,
                Inicio = Inicio,
                Fin = Fin,
                EstadoEmocionalInicio = EstadoEmocionalInicio,
                EstadoEmocionalFin = EstadoEmocionalFin,
                Observaciones = Observaciones
            };

            // Aquí puedes guardar el nuevo ejercicio en la base de datos utilizando Entity Framework o cualquier otro método que estés utilizando

            // Retorna un JsonResult indicando el éxito de la operación o cualquier otro resultado que desees devolver
            return Json(new { success = true, message = "Ejercicio guardado exitosamente" });
        }
    }
}
