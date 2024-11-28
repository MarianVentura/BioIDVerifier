namespace BioIDVerifier.Models;

public class Usuario
{
    public int UsuarioId { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string NumeroCedula { get; set; }
    public string FotoCedula { get; set; } // Ruta a la foto de la cédula
    public byte[] Huella { get; set; } // Huella en formato binario
}
