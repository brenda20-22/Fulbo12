using Fulbo12.Core.Futbol;
using Fulbo12.Core.Mvc.ViewModels;
using Fulbo12.Core.Persistencia;
using Fulbo12.Core.Persistencia.EFC;
using Fulbo12.Core.Persistencia.Excepciones;
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
    public async Task<IActionResult> Detalle(short id)
    {
        var futbolista = (await _unidad.RepoFutbolista.ObtenerAsync(filtro: f => f.Id == id, null, "Equipo")).FirstOrDefault();
        return View(futbolista);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }

    [HttpGet]
    public async Task<IActionResult> Alta(short idPersona)
    {
        var persona = (await _unidad.RepoPersona.ObtenerAsync(p => p.Id == idPersona, includes: "Pais")).FirstOrDefault();
        if (persona is null)
            return NotFound();

        var tipoFutbolistas = await _unidad.RepoTipoFutbolista.ObtenerAsync();
        var posiciones = await _unidad.RepoPosicion.ObtenerAsync();
        var equipos = await _unidad.RepoEquipo.ObtenerAsync(includes: "Liga");

        var vmFutbolista =
            new VMFutbolista(persona, posiciones, equipos, tipoFutbolistas);

        return View("Upsert", vmFutbolista);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(VMFutbolista vMFutbolista)
    {
        if (!ModelState.IsValid)
        {
            vMFutbolista.AsignarPosiciones(await _unidad.RepoPosicion.ObtenerAsync());
            return View("Upsert", vMFutbolista);
        }

        if (vMFutbolista.IdPersonaJuego == 0)
        {
            var futbolista = await vMFutbolista.CrearFutbolistaAsync(_unidad);
            await _unidad.RepoFutbolista.AltaAsync(futbolista);
        }
        else
        {
            var futbolistaRepo = await _unidad.RepoPersona.ObtenerPorIdAsync(vMFutbolista.IdPersonaJuego);
            if (futbolistaRepo is null)
                return NotFound();
            futbolistaRepo.Nombre = futbolistaRepo.NombreCompleto;
            _unidad.RepoPersona.Modificar(futbolistaRepo);
        }
        try
        {
            await _unidad.GuardarAsync();
        }
        catch (EntidadDuplicadaException)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(Listado));
    }
}