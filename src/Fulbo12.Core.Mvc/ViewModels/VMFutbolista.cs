using System.ComponentModel.DataAnnotations;
using Fulbo12.Core.Futbol;
using Fulbo12.Core.Persistencia;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fulbo12.Core.Mvc.ViewModels;

public class VMFutbolista
{
    public PersonaJuego Persona { get; set; }
    public SelectList Posiciones { get; set; }
    public SelectList Equipos { get; set; }
    public SelectList Tipos { get; set; }

    [Range(50, 99, ErrorMessage = "La valoracion del jugador tiene que estar entre el rango definido")]

    public byte Valoracion { get; set; }


    public VMFutbolista(IEnumerable<Posicion> posiciones, IEnumerable<Equipo> equipos, IEnumerable<TipoFutbolista> tipos)
    {
        Posiciones = new SelectList(posiciones,
                                    dataTextField: nameof(Posicion.Abreviado),
                                    dataValueField: nameof(Posicion.Id));
        Equipos = new SelectList(equipos,
                                    dataTextField: nameof(Equipo.Nombre),
                                    dataValueField: nameof(Equipo.Id));
        Tipos = new SelectList(tipos,
                            dataTextField: nameof(TipoFutbolista.Nombre),
                            dataValueField: nameof(TipoFutbolista.Id));
    }
    public void AsignarPosiciones(IEnumerable<Posicion> posiciones)
    {
        Posiciones = new SelectList(posiciones,
                                            dataTextField: nameof(Posicion.Abreviado),
                                            dataValueField: nameof(Posicion.Id));
    }

    internal Futbolista CrearFutbolista(IUnidad unidad)
    {
        // IdPais se setea en base a una opción seleccionada del SelectList
        var futbolista = unidad.RepoPersona.ObtenerPorId(idPersona);
        return new PersonaJuego()
        {
            Altura = AlturaPersona,
            Nacimiento = NacimientoPersona,
            Nombre = NombrePersona,
            Pais = pais!,
            Peso = PesoPersona,
            Apellido = ApellidoPersona
        }
    }
}