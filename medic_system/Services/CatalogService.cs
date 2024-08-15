using medic_system.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace medic_system.Services
{
    public class CatalogService
    {
        private readonly medical_systemContext _context;

        public CatalogService(medical_systemContext context)
        {
            _context = context;
        }

        private async Task<List<Catalogo>> ObtenerCatalogoPorCategoriaAsync(string categoria)
        {
            return await _context.Catalogos.Where(c => c.CategoriaCatalogo == categoria).ToListAsync();
        }

        public Task<List<Catalogo>> ObtenerTiposDocumentosAsync()
        {
            return ObtenerCatalogoPorCategoriaAsync("TIPO DOCUMENTO");
        }

        public Task<List<Catalogo>> ObtenerTiposDeSangreAsync()
        {
            return ObtenerCatalogoPorCategoriaAsync("TIPO DE SANGRE");
        }

        public Task<List<Catalogo>> ObtenerTiposDeGeneroAsync()
        {
            return ObtenerCatalogoPorCategoriaAsync("GENERO");
        }

        public Task<List<Catalogo>> ObtenerTiposDeEstadoCivilAsync()
        {
            return ObtenerCatalogoPorCategoriaAsync("ESTADO CIVIL");
        }

        public Task<List<Catalogo>> ObtenerTiposDeFormacionPAsync()
        {
            return ObtenerCatalogoPorCategoriaAsync("FORMACION PROFESIONAL");
        }
        public Task<List<Catalogo>> ObtenerTiposDeFamiliarAAsync()
        {
            return ObtenerCatalogoPorCategoriaAsync("ANTECEDENTE FAMILIAR");
        }
        public Task<List<Catalogo>> ObtenerTiposDeAlergiasAsync()
        {
            return ObtenerCatalogoPorCategoriaAsync("ALERGIAS");
        }   
        public Task<List<Catalogo>> ObtenerTiposDeCirugiasAsync()
        {
            return ObtenerCatalogoPorCategoriaAsync("Cirugias");
        }



        public async Task<List<Localidad>> ObtenerLocalidadesActivasAsync()
        {
            return await _context.Localidads.Where(l => l.EstadoLocalidad == 1).ToListAsync();
        }
        public async Task<List<Pai>> ObtenerNacionalidadesActivasAsync()
        {
            return await _context.Pais.Where(l => l.EstadoPais == 1).ToListAsync();
        }
        public async Task<List<Laboratorio>> ObtenerLaboratorioActivasAsync()
        {
            return await _context.Laboratorios.Where(l => l.EstadoLaboratorios == 1).ToListAsync();
        }
        public async Task<List<Imagen>> ObtenerImagenActivasAsync()
        {
            return await _context.Imagens.Where(l => l.EstadoImagen == 1).ToListAsync();
        }
     
        public async Task<List<Medicamento>> ObtenerMedicamentosActivasAsync()
        {
            return await _context.Medicamentos.Where(l => l.EstadoMedicamento == 1).ToListAsync();
        }

        public async Task<List<Pai>> ObtenerTiposDeNacionalidadPAsync()
        {
            return await ObtenerNacionalidadesActivasAsync();
        }

        public async Task<List<Localidad>> ObtenerTiposDeProvinciaPAsync()
        {
            return await ObtenerLocalidadesActivasAsync();
        }

        public Task<List<Catalogo>> ObtenerTiposDeSeguroAsync()
        {
            return ObtenerCatalogoPorCategoriaAsync("SEGUROS DE SALUD");
        }

        public async Task<List<Usuario>> ObtenerMedicoAsync()
        {
            return await _context.Usuarios.Where(u => u.PerfilId == 2).ToListAsync();
        }

        public async Task<List<Diagnostico>> ObtenerDiagnosticoAsync()
        {
            return await _context.Diagnosticos.Where(u => u.EstadoDiagnostico == 1).ToListAsync();
        }
        public Task<List<Catalogo>> ObtenerParienteAsync()
        {
            return ObtenerCatalogoPorCategoriaAsync("PARENTESCO");
        }
    }
}
