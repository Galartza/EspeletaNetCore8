using EzpeletaNetCore8.Data;
using Microsoft.AspNetCore.Mvc;
using EzpeletaNetCore8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EzpeletaNetCore8.Controllers
{
    public class EjerciciosFisicosController : Controller
    {
        private readonly ApplicationDbContext _context;

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

            // Obtener todas las opciones del enum EstadoEmocional
            var enumValues = Enum.GetValues(typeof(EstadoEmocional)).Cast<EstadoEmocional>();

            // Convertir las opciones del enum en SelectListItem
            selectListItems.AddRange(enumValues.Select(e => new SelectListItem
            {
                Value = ((int)e).ToString(),
                Text = e.ToString().ToUpper()
            }));

            // Pasar la lista de opciones al modelo de la vista
            ViewBag.EstadoEmocionalInicio = selectListItems.OrderBy(t => t.Text).ToList();
            ViewBag.EstadoEmocionalFin = selectListItems.OrderBy(t => t.Text).ToList();

            var tipoEjercicios = _context.TipoEjercicios.ToList();
            tipoEjercicios.Insert(0, new TipoEjercicio { TipoEjercicioID = 0, Descripcion = "[SELECCIONE...]" });
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

        // FUNCION PARA OBTENER LA DESCRIPCION DE UN TIPO DE EJERCICIO POR SU ID
        public JsonResult ObtenerDescripcionEjercicio(int id)
        {
            var tipoEjercicio = _context.TipoEjercicios.FirstOrDefault(t => t.TipoEjercicioID == id);

            if (tipoEjercicio == null)
            {
                return Json(new { error = "Tipo de ejercicio no encontrado" });
            }

            return Json(new { descripcion = tipoEjercicio.Descripcion });
        }

        [HttpPost]
        public JsonResult GuardarEjercicio([FromBody] EjercicioFisico ejercicioFisico)
        {
            if (ModelState.IsValid)
            {
                // Guardar el ejercicioFisico en la base de datos
                _context.EjerciciosFisicos.Add(ejercicioFisico);
                _context.SaveChanges();

                return Json(new { success = true, message = "Ejercicio guardado exitosamente" });
            }
            else
            {
                // Si hay errores en el modelo, retornar los mensajes de error
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return Json(new { success = false, errors = errors });
            }
        }
    }
}
