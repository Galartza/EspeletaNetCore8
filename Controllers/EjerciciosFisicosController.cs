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

        public int EjercicioFisicoID { get; private set; }

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

      public JsonResult ListadoEjerciciosFisicos(int? id, object TipoEjercicioID)
{
    List<VistaEjercicioFisico> ejercicioFisicosMostrar = new List<VistaEjercicioFisico>();

    var ejerciciosFisicos = _context.EjerciciosFisicos.ToList();

    if (id != null)
    {
        ejerciciosFisicos = ejerciciosFisicos.Where(t => t.EjercicioFisicoID == id).ToList();
    }

    var tipoEjerciciosFisicos = _context.TipoEjercicios.ToList();

    foreach (var ejercicioFisico in ejerciciosFisicos)
    {
        var tipoEjercicio = tipoEjerciciosFisicos.FirstOrDefault(t => t.TipoEjercicioID == ejercicioFisico.TipoEjercicioID);
        if (tipoEjercicio != null)
        {
            var ejercicioFisicoMostrar = new VistaEjercicioFisico
            {
                EjercicioFisicoID = ejercicioFisico.EjercicioFisicoID,
                TipoEjercicioID = ejercicioFisico.TipoEjercicioID,
                TipoEjercicioDescripcion = tipoEjercicio.Descripcion,
                FechaInicioString = ejercicioFisico.Inicio.ToString("dd/MM/yyyy HH:mm"),
                FechaFinString = ejercicioFisico.Fin.ToString("dd/MM/yyyy HH:mm"),
                EstadoEmocionalInicio = Enum.GetName(typeof(EstadoEmocional), ejercicioFisico.EstadoEmocionalInicio),
                EstadoEmocionalFin = Enum.GetName(typeof(EstadoEmocional), ejercicioFisico.EstadoEmocionalFin),
                Observaciones = ejercicioFisico.Observaciones
            };
            ejercicioFisicosMostrar.Add(ejercicioFisicoMostrar);
        }
    }

    return Json(ejercicioFisicosMostrar);
}

        public JsonResult TraerEjerciciosFisicos(int? ejercicioFisicoID)
        {
            var ejercicioFisico = _context.EjerciciosFisicos.ToList();
            if (ejercicioFisicoID != null)
            {
                ejercicioFisico = ejercicioFisico.Where(e => e.EjercicioFisicoID == ejercicioFisicoID).ToList();
            }

            return Json(ejercicioFisico.ToList());

        }

        // public JsonResult GuardarRegistro(int ejercicioFisicoID, int tipoEjercicioID, DateTime inicio, DateTime fin, EstadoEmocional estadoEmocionalInicio, EstadoEmocional estadoEmocionalFin, string observaciones)
        // {
        //     string resultado = "";

        //     if (ejercicioFisicoID != null)
        //     {
        //         if (ejercicioFisicoID == 0)
        //         {
        //             var EjercicioFisico = new EjercicioFisico
        //             {
        //                 EjercicioFisicoID = ejercicioFisicoID,
        //                 TipoEjercicioID = tipoEjercicioID,
        //                 Inicio = inicio,
        //                 Fin = fin,
        //                 EstadoEmocionalInicio = estadoEmocionalInicio,
        //                 EstadoEmocionalFin = estadoEmocionalFin,
        //                 Observaciones = observaciones
        //             };
        //             _context.EjerciciosFisicos.Add(EjercicioFisico);
        //             _context.SaveChanges();

        //             resultado = "Ejercicio fisico agregado correctamente";
        //         }
        //         else
        //         {
        //             // EDICION
        //             var ejercicioFisicoEditar = _context.EjerciciosFisicos.FirstOrDefault(e => EjercicioFisicoID == ejercicioFisicoID);
        //             if (ejercicioFisicoEditar != null)
        //             {
        //                 ejercicioFisicoEditar.TipoEjercicioID = tipoEjercicioID;
        //                 ejercicioFisicoEditar.Inicio = inicio;
        //                 ejercicioFisicoEditar.Fin = fin;
        //                 ejercicioFisicoEditar.EstadoEmocionalInicio = estadoEmocionalInicio;
        //                 ejercicioFisicoEditar.EstadoEmocionalFin = estadoEmocionalFin;
        //                 ejercicioFisicoEditar.Observaciones = observaciones;

        //                 _context.SaveChanges();
        //                 resultado = "Ejercicio fisico editado con exito";
        //             }
        //         }
        //     }
        //     else
        //     {
        //         resultado = "ejercicioFisicoID no puede ser null";
        //     }

        //     return Json(new { resultado });
        // }
    }
}