using Fulbo12.Core.Futbol;
using Fulbo12.Core.Mvc.ViewModels;
using Fulbo12.Core.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Fulbo12.Core.Mvc.Controllers;

public class FutbolistaController : Controller
{
    private readonly IUnidad _unidad;
    public FutbolistaController(IUnidad unidad) => _unidad = unidad;
    

    public async Task<IActionResult> Listado()
    {
        var futbolista = await _unidad.RepoFutbolista.ObtenerAsync();
        return View(futbolista);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
    [HttpGet]
    public ActionResult AgregarPosiciones()
    {
        List<SelectListItem> SelectPosiciones = new List<SelectListItem>();

        foreach(Posicion posicion in PosicionesFixture.posiciones) 
        {
            SelectListItem selectList = new SelectListItem()
            {
                Text = posicion.Abreviado,
                Value = posicion.Id.ToString(),
                Selected= posicion.IsSelected
            };
            SelectPosiciones.Add(selectList); 
        }
        FutbolistaVM futbolistaVM
    }

    [HttpGet]
    public async Task<IActionResult> Alta(short idPersona)
    {
        var persona = await _unidad.RepoPersona.ObtenerPorIdAsync(idPersona);
        if (persona is null)
            return NotFound();

        var tipoFutbolistas = await _unidad.RepoTipoFutbolista.ObtenerAsync();
        var posiciones = await _unidad.RepoPosicion.ObtenerAsync();
        var equipos = await _unidad.RepoEquipo.ObtenerAsync(includes: "Liga");

        var vmFutbolista =
            new VMFutbolista(persona, posiciones, equipos, tipoFutbolistas);

        return View("Upsert", vmFutbolista);
    }
}