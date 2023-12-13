namespace EXAMEN.Models
{
    public class MecanicoModelos
    {
        //aqui ponemos las clases, estas son de la base de datos y tienen que estar en orden tal cual como en la database
        public int idMecanico { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Domicilio { get; set; }
        public string Titulo { get; set; }
        public string Especialidad { get; set; }
        public int SueldoBase { get; set; }
        public int GrantTitulo { get; set; }
        public int SueldoTotal { get; set; }
    }
}
