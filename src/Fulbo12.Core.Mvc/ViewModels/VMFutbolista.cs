using System.ComponentModel.DataAnnotations;
using Fulbo12.Core.Futbol;
using Fulbo12.Core.Persistencia;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fulbo12.Core.Mvc.ViewModels;

public class VMFutbolista
{
    public PersonaJuego PersonaJugador { get; set; }
    public short IdPersonaJuego { get; set; }
    public SelectList PosicionesJugador { get; set; }
    public SelectList EquipoJugador { get; set; }
    public SelectList Tipojugador { get; set; }

    [Range(50, 99, ErrorMessage = "La valoracion del jugador tiene que estar entre el rango definido")]

    public byte Valoracion { get; set; }


    public VMFutbolista(PersonaJuego personaJuego, IEnumerable<Posicion> posiciones, IEnumerable<Equipo> equipos, IEnumerable<TipoFutbolista> tipos)
    {
        PersonaJugador = personaJuego;
        PosicionesJugador = new SelectList(posiciones,
                                    dataTextField: nameof(Posicion.Abreviado),
                                    dataValueField: nameof(Posicion.Id));
        EquipoJugador = new SelectList(equipos,
                                    dataTextField: nameof(Equipo.Nombre),
                                    dataValueField: nameof(Equipo.Id));
        Tipojugador = new SelectList(tipos,
                            dataTextField: nameof(TipoFutbolista.Nombre),
                            dataValueField: nameof(TipoFutbolista.Id));
    }
    public void AsignarPosiciones(IEnumerable<Posicion> posiciones)
    {
        PosicionesJugador = new SelectList(posiciones,
                                            dataTextField: nameof(Posicion.Abreviado),
                                            dataValueField: nameof(Posicion.Id));
    }

    internal async Task<Futbolista> CrearFutbolistaAsync(IUnidad unidad)
    {
        // IdPais se setea en base a una opción seleccionada del SelectList
        var futbolista = await unidad.RepoPersona.ObtenerPorIdAsync(PersonaJugador.Id);
        return new Futbolista()
        {
            Persona = PersonaJugador,
            Posiciones = PosicionesJugador!,
            Tipofutbolista = Tipojugador!,
            Equipo = EquipoJugador!,
        };
    }
}